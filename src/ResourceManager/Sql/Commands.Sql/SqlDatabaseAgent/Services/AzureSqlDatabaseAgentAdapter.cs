// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.Sql.Server.Services;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.WindowsAzure.Commands.Common;
using System;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// Adapter for Azure SQL Database Agent operations
    /// </summary>
    public class AzureSqlDatabaseAgentAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlDatabaseAgentCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        public AzureSqlDatabaseAgentAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentCommunicator(Context);
        }

        #region Agent

        /// <summary>
        /// PUT: Creates or updates an agent
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted Azure SQL Database Agent</returns>
        public AzureSqlDatabaseAgentModel UpsertSqlDatabaseAgent(AzureSqlDatabaseAgentModel model)
        {
            // Construct database id
            string databaseId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/databases/{3}",
                AzureSqlDatabaseAgentCommunicator.Subscription.Id,
                model.ResourceGroupName,
                model.ServerName,
                model.DatabaseName);

            var param = new JobAgent
            {
                Location = model.Location,
                Tags = model.Tags,
                DatabaseId = databaseId
            };

            var resp = Communicator.CreateOrUpdateAgent(model.ResourceGroupName, model.ServerName, model.AgentName, param);
            return CreateAgentModelFromResponse(model.ResourceGroupName, model.ServerName, resp);
        }

        /// <summary>
        /// PATCH: Updates an existing agent
        /// </summary>
        /// <param name="model">The existing agent entity</param>
        /// <returns>The updated agent entity</returns>
        public AzureSqlDatabaseAgentModel UpdateSqlDatabaseAgent(AzureSqlDatabaseAgentModel model)
        {
            var param = new JobAgentUpdate
            {
                Tags = model.Tags
            };

            var resp = Communicator.UpdateAgent(model.ResourceGroupName, model.ServerName, model.AgentName, param);
            return CreateAgentModelFromResponse(model.ResourceGroupName, model.ServerName, resp);
        }

        /// <summary>
        /// GET: Get an agent
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentModel GetSqlDatabaseAgent(string resourceGroupName, string serverName, string agentName)
        {
            var resp = Communicator.GetAgent(resourceGroupName, serverName, agentName);
            return CreateAgentModelFromResponse(resourceGroupName, serverName, resp);
        }

        /// <summary>
        /// GET: Get a list of existing agents belong to server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agents are in</param>
        /// <returns>The converted agent model(s)</returns>
        public List<AzureSqlDatabaseAgentModel> ListAgents(string resourceGroupName, string serverName)
        {
            var resp = Communicator.ListAgentsByServer(resourceGroupName, serverName);
            return resp.Select(agent => CreateAgentModelFromResponse(resourceGroupName, serverName, agent)).ToList();
        }

        /// <summary>
        /// DELETE: Deletes an agent
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveSqlDatabaseAgent(string resourceGroupName, string serverName, string agentName)
        {
            Communicator.RemoveAgent(resourceGroupName, serverName, agentName);
        }

        /// <summary>
        /// Convert a JobAgent to AzureSqlDatabaseAgentModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted agent model</returns>
        private static AzureSqlDatabaseAgentModel CreateAgentModelFromResponse(string resourceGroupName, string serverName, JobAgent resp)
        {
            // Parse database name from database id
            // This is not expected to ever fail, but in case we have a bug here it's better to provide a more detailed error message
            int lastSlashIndex = resp.DatabaseId.LastIndexOf('/');
            string databaseName = resp.DatabaseId.Substring(lastSlashIndex + 1);
            int? workerCount = resp.Sku.Capacity;

            AzureSqlDatabaseAgentModel agent = new AzureSqlDatabaseAgentModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = resp.Name,
                Location = resp.Location,
                DatabaseName = databaseName,
                WorkerCount = workerCount,
                ResourceId = resp.Id,
                Tags = TagsConversionHelper.CreateTagDictionary(TagsConversionHelper.CreateTagHashtable(resp.Tags), false),
                DatabaseId = resp.DatabaseId,
                Type = resp.Type
            };

            return agent;
        }

        /// <summary>
        /// Gets the Location of the server. Throws an exception if the server does not support Azure SQL Database Agents.
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The name of the server</param>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        /// <remarks>
        /// These 2 operations (get location, throw if not supported) are combined in order to minimize round trips.
        /// </remarks>
        public string GetServerLocationAndThrowIfAgentNotSupportedByServer(string resourceGroupName, string serverName)
        {
            AzureSqlServerCommunicator serverCommunicator = new AzureSqlServerCommunicator(Context);
            var server = serverCommunicator.Get(resourceGroupName, serverName);
            return server.Location;
        }

        #endregion

        #region Job Credential

        /// <summary>
        /// Upserts an Azure SQL Database Agent to a server
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted Azure SQL Database Agent</returns>
        public AzureSqlDatabaseAgentJobCredentialModel UpsertJobCredential(AzureSqlDatabaseAgentJobCredentialModel model)
        {
            var param = new JobCredential
            {
                Username = model.UserName,
                Password = model.Password != null ? ConversionUtilities.SecureStringToString(model.Password) : null
            };

            var resp = Communicator.CreateOrUpdateJobCredential(model.ResourceGroupName, model.ServerName, model.AgentName, model.CredentialName, param);

            return CreateAgentCredentialModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, resp);
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentJobCredentialModel GetJobCredential(string resourceGroupName, string serverName, string agentName, string credentialName)
        {
            var resp = Communicator.GetJobCredential(resourceGroupName, serverName, agentName, credentialName);
            return CreateAgentCredentialModelFromResponse(resourceGroupName, serverName, agentName, resp);
        }

        /// <summary>
        /// Gets a list of SQL Database Agents associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agents are in</param>
        /// <returns>The converted agent model(s)</returns>
        public List<AzureSqlDatabaseAgentJobCredentialModel> ListJobCredentials(string resourceGroupName, string serverName, string agentName)
        {
            var resp = Communicator.GetJobCredential(resourceGroupName, serverName, agentName);
            return resp.Select(credentialName => CreateAgentCredentialModelFromResponse(resourceGroupName, serverName, agentName, credentialName)).ToList();
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveJobCredential(string resourceGroupName, string serverName, string agentName, string credentialName)
        {
            Communicator.RemoveJobCredential(resourceGroupName, serverName, agentName, credentialName);
        }

        /// <summary>
        /// Convert a JobAgent to AzureSqlDatabaseAgentJobCredentialModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted agent model</returns>
        private static AzureSqlDatabaseAgentJobCredentialModel CreateAgentCredentialModelFromResponse(string resourceGroupName, string serverName, string agentName, JobCredential resp)
        {
            // Parse credential name from id
            // This is not expected to ever fail, but in case we have a bug here it's better to provide a more detailed error message
            int lastSlashIndex = resp.Id.LastIndexOf('/');
            string credentialName = resp.Id.Substring(lastSlashIndex + 1);

            AzureSqlDatabaseAgentJobCredentialModel credential = new AzureSqlDatabaseAgentJobCredentialModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                CredentialName = resp.Name,
                UserName = resp.Username,
                ResourceId = resp.Id,
                Type = resp.Type
            };

            return credential;
        }

        #endregion

        #region Target Group

        /// <summary>
        /// Upserts an Azure SQL Database Agent to a server
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted Azure SQL Database Agent</returns>
        public AzureSqlDatabaseAgentTargetGroupModel UpsertTargetGroup(AzureSqlDatabaseAgentTargetGroupModel model)
        {
            var param = new JobTargetGroup
            {
                Members = model.Targets.Select((target) => CreateTargetModel(target)).ToList()
            };

            var resp = Communicator.CreateOrUpdateTargetGroup(model.ResourceGroupName, model.ServerName, model.AgentName, model.TargetGroupName, param);
            return CreateTargetGroupModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, resp);
        }

        public JobTarget CreateTargetModel(AzureSqlDatabaseAgentTargetModel target)
        {
            return new JobTarget
            {
                MembershipType = target.MembershipType,
                DatabaseName = target.TargetDatabaseName,
                ServerName = target.TargetServerName,
                ElasticPoolName = target.TargetElasticPoolName,
                RefreshCredential = target.RefreshCredentialName,
                ShardMapName = target.TargetShardMapName,
                Type = target.TargetType
            };
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentTargetGroupModel GetTargetGroup(string resourceGroupName, string serverName, string agentName, string targetGroupName)
        {
            var resp = Communicator.GetTargetGroup(resourceGroupName, serverName, agentName, targetGroupName);
            return CreateTargetGroupModelFromResponse(resourceGroupName, serverName, agentName, resp);
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public List<AzureSqlDatabaseAgentTargetGroupModel> ListTargetGroups(string resourceGroupName, string serverName, string agentName)
        {
            var resp = Communicator.GetTargetGroup(resourceGroupName, serverName, agentName);
            return resp.Select(targetGroup => CreateTargetGroupModelFromResponse(resourceGroupName, serverName, agentName, targetGroup)).ToList();
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveTargetGroup(string resourceGroupName, string serverName, string agentName, string targetGroupName)
        {
            Communicator.RemoveTargetGroup(resourceGroupName, serverName, agentName, targetGroupName);
        }

        /// <summary>
        /// Convert a Management.Sql.Models.JobAgent to AzureSqlDatabaseAgentTargetGroupModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted agent model</returns>
        private AzureSqlDatabaseAgentTargetGroupModel CreateTargetGroupModelFromResponse(string resourceGroupName, string serverName, string agentName, Management.Sql.Models.JobTargetGroup resp)
        {
            AzureSqlDatabaseAgentTargetGroupModel targetGroup = new AzureSqlDatabaseAgentTargetGroupModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                TargetGroupName = resp.Name,
                Targets = resp.Members.Select((target) => CreateTargetModel(resourceGroupName, serverName, agentName, resp.Name, target)).ToList(),
                ResourceId = resp.Id,
                Type = resp.Type
            };

            return targetGroup;
        }

        protected AzureSqlDatabaseAgentTargetModel CreateTargetModel(
            string resourceGroupName,
            string serverName,
            string agentName,
            string targetGroupName,
            JobTarget target)
        {
            return new AzureSqlDatabaseAgentTargetModel
            {
                TargetGroupName = targetGroupName,
                MembershipType = target.MembershipType,
                TargetType = target.Type,
                RefreshCredentialName = target.RefreshCredential,
                TargetServerName = target.ServerName,
                TargetDatabaseName = target.DatabaseName,
                TargetElasticPoolName = target.ElasticPoolName,
                TargetShardMapName = target.ShardMapName,
            };
        }

        #endregion

        #region Job

        /// <summary>
        /// Creates or updates a new job
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="agentServerName">The agent server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <param name="model">The job parameters</param>
        /// <returns></returns>
        public AzureSqlDatabaseAgentJobModel UpsertJob(AzureSqlDatabaseAgentJobModel model)
        {
            var param = new Job
            {
                Description = model.Description,
                Schedule = new JobSchedule
                {
                    Enabled = model.Enabled,
                    EndTime = model.EndTime,
                    Interval = model.Interval,
                    StartTime = model.StartTime,
                    Type = model.ScheduleType
                }
            };

            var resp = Communicator.CreateOrUpdateJob(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, param);
            return CreateJobModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, resp);
        }

        /// <summary>
        /// Gets a job from agent
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The agent server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <returns>A job</returns>
        public AzureSqlDatabaseAgentJobModel GetJob(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = Communicator.GetJob(resourceGroupName, serverName, agentName, jobName);
            return CreateJobModelFromResponse(resourceGroupName, serverName, agentName, resp);
        }

        /// <summary>
        /// Gets a list of jobs owned by agent
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The agent server name</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>A list of jobs</returns>
        public List<AzureSqlDatabaseAgentJobModel> GetJob(string resourceGroupName, string serverName, string agentName)
        {
            var resp = Communicator.ListJobsByAgent(resourceGroupName, serverName, agentName);
            return resp.Select(job => CreateJobModelFromResponse(resourceGroupName, serverName, agentName, job)).ToList();
        }

        /// <summary>
        /// Deletes a job owned by agent
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="agentServerName">The agent server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        public void RemoveJob(string resourceGroupName, string agentServerName, string agentName, string jobName)
        {
            Communicator.RemoveJob(resourceGroupName, agentServerName, agentName, jobName);
        }

        public AzureSqlDatabaseAgentJobModel CreateJobModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            Job resp)
        {
            return new AzureSqlDatabaseAgentJobModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = resp.Name,
                Description = resp.Description,
                ResourceId = resp.Id,

                StartTime = resp.Schedule.StartTime,
                EndTime = resp.Schedule.EndTime,
                ScheduleType = resp.Schedule.Type,
                Enabled = resp.Schedule.Enabled,
                Interval = resp.Schedule.Interval,
                Type = resp.Type,
                Version = resp.Version
            };
        }

        #endregion

        #region Job Step

        /// <summary>
        /// Upserts a job step
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted job step</returns>
        public AzureSqlDatabaseAgentJobStepModel UpsertJobStep(AzureSqlDatabaseAgentJobStepModel model)
        {
            var param = new JobStep
            {
                // Min params
                TargetGroup = model.TargetGroupName,
                Credential = model.CredentialName,
                Action = new JobStepAction
                {
                    Value = model.CommandText
                },
                // Max params
                ExecutionOptions = model.ExecutionOptions,
                Output = model.Output,
                StepId = model.StepId,
            };

            var resp = Communicator.CreateOrUpdateJobStep(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.StepName, param);
            return CreateJobStepModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.StepName, resp);
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentJobStepVersionModel GetJobStepByVersion(string resourceGroupName, string serverName, string agentName, string jobName, int jobVersion, string stepName)
        {
            var resp = Communicator.GetJobStepByVersion(resourceGroupName, serverName, agentName, jobName, jobVersion, stepName);
            return CreateJobStepVersionModelFromResponse(resourceGroupName, serverName, agentName, jobName, jobVersion, stepName, resp);
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentJobStepModel GetJobStep(string resourceGroupName, string serverName, string agentName, string jobName, string stepName)
        {
            var resp = Communicator.GetJobStep(resourceGroupName, serverName, agentName, jobName, stepName);
            return CreateJobStepModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepName, resp);
        }

        /// <summary>
        /// Gets a list of steps associated to a job
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <returns>A list of steps that the job has</returns>
        public List<AzureSqlDatabaseAgentJobStepModel> ListJobSteps(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = Communicator.ListJobStepsByJob(resourceGroupName, serverName, agentName, jobName);
            return resp.Select((step) => CreateJobStepModelFromResponse(resourceGroupName, serverName, agentName, jobName, step.Name, step)).ToList();
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveJobStep(string resourceGroupName, string serverName, string agentName, string jobName, string stepName)
        {
            Communicator.RemoveJobStep(resourceGroupName, serverName, agentName, jobName, stepName);
        }

        /// <summary>
        /// Convert a Management.Sql.Models.JobAgent to AzureSqlDatabaseAgentJobStepModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted agent model</returns>
        private static AzureSqlDatabaseAgentJobStepModel CreateJobStepModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            string stepName,
            JobStep resp)
        {
            AzureSqlDatabaseAgentJobStepModel jobStep = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                StepName = stepName,
                TargetGroupName = new ResourceIdentifier(resp.TargetGroup).ResourceName,
                CredentialName = new ResourceIdentifier(resp.Credential).ResourceName,
                CommandText = resp.Action.Value,
                ExecutionOptions = resp.ExecutionOptions,
                Output = resp.Output != null ? new JobStepOutput
                {
                    ResourceGroupName = resp.Output.ResourceGroupName,
                    SubscriptionId = resp.Output.SubscriptionId,
                    Credential = new ResourceIdentifier(resp.Output.Credential).ResourceName,
                    DatabaseName = resp.Output.DatabaseName,
                    SchemaName = resp.Output.SchemaName,
                    ServerName = resp.Output.ServerName,
                    TableName = resp.Output.TableName,
                    Type = resp.Output.Type
                } : null,
                ResourceId = resp.Id,
                StepId = resp.StepId,
                Type = resp.Type
            };

            return jobStep;
        }

        private static AzureSqlDatabaseAgentJobStepVersionModel CreateJobStepVersionModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            int jobVersion,
            string stepName,
            JobStep resp)
        {
            AzureSqlDatabaseAgentJobStepVersionModel jobStep = new AzureSqlDatabaseAgentJobStepVersionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                Version = jobVersion,
                StepName = stepName,
                TargetGroupName = new ResourceIdentifier(resp.TargetGroup).ResourceName,
                CredentialName = new ResourceIdentifier(resp.Credential).ResourceName,
                CommandText = resp.Action.Value,
                ExecutionOptions = resp.ExecutionOptions,
                Output = resp.Output != null ? new JobStepOutput
                {
                    ResourceGroupName = resp.Output.ResourceGroupName,
                    SubscriptionId = resp.Output.SubscriptionId,
                    Credential = new ResourceIdentifier(resp.Output.Credential).ResourceName,
                    DatabaseName = resp.Output.DatabaseName,
                    SchemaName = resp.Output.SchemaName,
                    ServerName = resp.Output.ServerName,
                    TableName = resp.Output.TableName,
                    Type = resp.Output.Type
                } : null,
                ResourceId = resp.Id,
                StepId = resp.StepId,
                Type = resp.Type
            };

            return jobStep;
        }

        #endregion

        #region Job Version

        /// <summary>
        /// Gets a job version
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted job step</returns>
        public AzureSqlDatabaseAgentJobVersionModel GetJobVersion(string resourceGroupName, string serverName, string agentName, string jobName, int version)
        {
            var resp = Communicator.GetJobVersion(resourceGroupName, serverName, agentName, jobName, version);
            return CreateJobVersionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
        }


        /// <summary>
        /// Gets all job versions from a job
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted job step</returns>
        public List<AzureSqlDatabaseAgentJobVersionModel> GetJobVersion(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = Communicator.GetJobVersion(resourceGroupName, serverName, agentName, jobName);
            return resp.Select((version) => CreateJobVersionModelFromResponse(resourceGroupName, serverName, agentName, jobName, version)).ToList();
        }

        public AzureSqlDatabaseAgentJobVersionModel CreateJobVersionModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Management.Sql.Models.JobVersion resp)
        {
            return new AzureSqlDatabaseAgentJobVersionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                Version = int.Parse(resp.Name),
                ResourceId = resp.Id,
                Type = resp.Type
            };
        }

        #endregion

        #region Job Execution

        /// <summary>
        /// Creates a root job execution and polls until execution completes.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The created root job execution</returns>
        public AzureSqlDatabaseAgentJobExecutionModel CreateJobExecution(AzureSqlDatabaseAgentJobExecutionModel model)
        {
            var resp = Communicator.CreateJobExecution(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName);
            return CreateJobExecutionModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, resp);
        }

        /// <summary>
        /// Creates a root job execution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The created root job execution</returns>
        public AzureSqlDatabaseAgentJobExecutionModel BeginCreateJobExecution(AzureSqlDatabaseAgentJobExecutionModel model)
        {
            var resp = Communicator.BeginCreate(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName);
            return CreateJobExecutionModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, resp);
        }

        /// <summary>
        /// Gets a root job execution
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentJobExecutionModel GetJobExecution(string resourceGroupName, string serverName, string agentName, string jobName, Guid jobExecutionId)
        {
            var resp = Communicator.GetJobExecution(resourceGroupName, serverName, agentName, jobName, jobExecutionId);
            return CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
        }

        /// <summary>
        /// Cancels a job execution
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agents are in</param>
        /// <returns>The converted agent model(s)</returns>
        public void CancelJobExecution(AzureSqlDatabaseAgentJobExecutionModel model)
        {
            Communicator.CancelJobExecution(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.JobExecutionId.Value);
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public List<AzureSqlDatabaseAgentJobExecutionModel> ListByAgent(
            string resourceGroupName,
            string serverName,
            string agentName,
            DateTime? createTimeMin = null,
            DateTime? createTimeMax = null,
            DateTime? endTimeMin = null,
            DateTime? endTimeMax = null,
            bool? isActive = null,
            int? skip = null,
            int? top = null)
        {
            // TODO: scrape job name here
            var resp = Communicator.ListJobExecutionsByAgent(
                resourceGroupName,
                serverName,
                agentName,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip,
                top);

            return resp.Select((jobExecution) =>
                CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName,
                GetJobName(jobExecution.Id), jobExecution)).ToList();
        }

        public string GetJobName(string jobExecutionResourceId)
        {
            var resourceId = new AzureSqlDatabaseAgentResourceIdentifier(jobExecutionResourceId);
            return resourceId.JobName;
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public List<AzureSqlDatabaseAgentJobStepExecutionModel> ListByJob(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            DateTime? createTimeMin = null,
            DateTime? createTimeMax = null,
            DateTime? endTimeMin = null,
            DateTime? endTimeMax = null,
            bool? isActive = null,
            int? skip = null,
            int? top = null)
        {
            var resp = Communicator.ListJobExecutionsByJob(
                resourceGroupName, serverName, agentName,
                jobName,
                createTimeMin, createTimeMax,
                endTimeMin, endTimeMax,
                isActive,
                skip, 
                top);

            return resp.Select((stepExecution) => CreateJobStepExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepExecution)).ToList();
        }

        #endregion

        #region Step Execution

        /// <summary>
        /// Gets a job step execution
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <param name="jobExecutionId"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        public AzureSqlDatabaseAgentJobStepExecutionModel GetJobStepExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName)
        {
            var resp = Communicator.GetJobStepExecution(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName);
            return CreateJobStepExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
        }

        /// <summary>
        /// Gets a list of job step executions by step name
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <param name="jobExecutionId"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        public List<AzureSqlDatabaseAgentJobStepExecutionModel> ListJobExecutionSteps(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            DateTime? createTimeMin = null,
            DateTime? createTimeMax = null,
            DateTime? endTimeMin = null,
            DateTime? endTimeMax = null,
            bool? isActive = null,
            int? skip = null,
            int? top = null)
        {
            var resp = Communicator.ListJobExecutionSteps(
                resourceGroupName, serverName, agentName,
                jobName, jobExecutionId,
                createTimeMin, createTimeMax,
                endTimeMin, endTimeMax,
                isActive,
                skip, top);

            return resp.Select((stepExecution) => CreateJobStepExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepExecution)).ToList();
        }

        #endregion

        #region Target Execution

        /// <summary>
        /// Gets a job target execution
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <param name="jobExecutionId"></param>
        /// <param name="stepName"></param>
        /// <param name="targetId"></param>
        /// <returns></returns>
        public AzureSqlDatabaseAgentJobTargetExecutionModel GetJobTargetExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName,
            Guid targetId)
        {
            var resp = Communicator.GetJobTargetExecution(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName, targetId);
            return CreateJobTargetExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
        }

        /// <summary>
        /// Gets a list of job target executions by step name
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <param name="jobExecutionId"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        public List<AzureSqlDatabaseAgentJobTargetExecutionModel> ListJobTargetExecutionsByStep(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName,
            DateTime? createTimeMin = null,
            DateTime? createTimeMax = null,
            DateTime? endTimeMin = null,
            DateTime? endTimeMax = null,
            bool? isActive = null,
            int? skip = null,
            int? top = null)
        {
            var resp = Communicator.ListJobTargetExecutionsByStep(
                resourceGroupName, serverName, agentName,
                jobName, jobExecutionId, stepName,
                createTimeMin, createTimeMax,
                endTimeMin, endTimeMax,
                isActive,
                skip, top);

            return resp.Select((targetExecution) => CreateJobTargetExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, targetExecution)).ToList();
        }

        /// <summary>
        /// Gets a list of job target executions by target id
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <param name="jobExecutionId"></param>
        /// <param name="stepName"></param>
        /// <returns></returns>
        public List<AzureSqlDatabaseAgentJobTargetExecutionModel> ListJobTargetExecutions(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            DateTime? createTimeMin = null,
            DateTime? createTimeMax = null,
            DateTime? endTimeMin = null,
            DateTime? endTimeMax = null,
            bool? isActive = null,
            int? skip = null,
            int? top = null)
        {
            var resp = Communicator.ListJobTargetExecutions(
                resourceGroupName, serverName, agentName,
                jobName, jobExecutionId,
                createTimeMin, createTimeMax,
                endTimeMin, endTimeMax,
                isActive,
                skip, top);

            return resp.Select((targetExecution) => CreateJobTargetExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, targetExecution)).ToList();
        }

        /// <summary>
        /// Convert a JobAgent to AzureSqlDatabaseAgentJobExecutionModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the server is in</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="resp">The management client server response to convert</param>
        /// <returns>The converted agent model</returns>
        private static AzureSqlDatabaseAgentJobExecutionModel CreateJobExecutionModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            JobExecution resp)
        {
            AzureSqlDatabaseAgentJobExecutionModel jobExecution = new AzureSqlDatabaseAgentJobExecutionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                JobExecutionId = resp.JobExecutionId,
                CreateTime = resp.CreateTime,
                CurrentAttempts = resp.CurrentAttempts,
                CurrentAttemptStartTime = resp.CurrentAttemptStartTime,
                EndTime = resp.EndTime,
                JobVersion = resp.JobVersion,
                LastMessage = resp.LastMessage,
                Lifecycle = resp.Lifecycle,
                ProvisioningState = resp.ProvisioningState,
                ResourceId = resp.Id,
                StartTime = resp.StartTime,
                Type = resp.Type,
            };

            return jobExecution;
        }

        private static AzureSqlDatabaseAgentJobStepExecutionModel CreateJobStepExecutionModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            JobExecution resp)
        {
            AzureSqlDatabaseAgentJobStepExecutionModel jobStepExecution = new AzureSqlDatabaseAgentJobStepExecutionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                JobExecutionId = resp.JobExecutionId,
                CreateTime = resp.CreateTime,
                CurrentAttempts = resp.CurrentAttempts,
                CurrentAttemptStartTime = resp.CurrentAttemptStartTime,
                EndTime = resp.EndTime,
                JobVersion = resp.JobVersion,
                LastMessage = resp.LastMessage,
                Lifecycle = resp.Lifecycle,
                ProvisioningState = resp.ProvisioningState,
                ResourceId = resp.Id,
                StartTime = resp.StartTime,
                Type = resp.Type,
                StepId = resp.StepId,
                StepName = resp.StepName,
            };

            return jobStepExecution;
        }

        private static AzureSqlDatabaseAgentJobTargetExecutionModel CreateJobTargetExecutionModelFromResponse(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            JobExecution resp)
        {
            AzureSqlDatabaseAgentJobTargetExecutionModel jobTargetExecution = new AzureSqlDatabaseAgentJobTargetExecutionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                JobExecutionId = resp.JobExecutionId,
                CreateTime = resp.CreateTime,
                CurrentAttempts = resp.CurrentAttempts,
                CurrentAttemptStartTime = resp.CurrentAttemptStartTime,
                EndTime = resp.EndTime,
                JobVersion = resp.JobVersion,
                LastMessage = resp.LastMessage,
                Lifecycle = resp.Lifecycle,
                ProvisioningState = resp.ProvisioningState,
                ResourceId = resp.Id,
                StartTime = resp.StartTime,
                Type = resp.Type,
                StepId = resp.StepId,
                StepName = resp.StepName,
                TargetDatabaseName = resp.Target.DatabaseName,
                TargetServerName = resp.Target.ServerName 
            };

            return jobTargetExecution;
        }

        #endregion
    }
}