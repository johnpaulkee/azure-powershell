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
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// Adapter for Azure SQL Database Agent operations
    /// </summary>
    public class AzureSqlDatabaseAgentJobExecutionAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlDatabaseAgentJobExecutionCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        public AzureSqlDatabaseAgentJobExecutionAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentJobExecutionCommunicator(Context);
        }

        #region Root job execution

        /// <summary>
        /// Creates a root job execution
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The created root job execution</returns>
        public AzureSqlDatabaseAgentJobExecutionModel CreateJobExecution(AzureSqlDatabaseAgentJobExecutionModel model)
        {
            var resp = Communicator.Create(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName);
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
            var resp = Communicator.Get(resourceGroupName, serverName, agentName, jobName, jobExecutionId);
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
            Communicator.Cancel(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.JobExecutionId.Value);
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ListByAgent(
            string resourceGroupName, 
            string serverName, 
            string agentName,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip)
        {
            // TODO: scrape job name here
            var resp = Communicator.ListByAgent(
                resourceGroupName, 
                serverName, 
                agentName,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip);

            return resp.Select((jobExecution) => CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobExecution.Id, jobExecution));
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ListByJob(
            string resourceGroupName, 
            string serverName, 
            string agentName, 
            string jobName,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip)
        {
            var resp = Communicator.ListByJob(
                resourceGroupName, 
                serverName, 
                agentName, 
                jobName,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip);

            return resp.Select((jobExecution) => CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, jobExecution));
        }

        #endregion

        #region Step level execution
        
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
        public AzureSqlDatabaseAgentJobExecutionModel GetJobStepExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName)
        {
            var resp = Communicator.GetJobStepExecution(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName);
            return CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
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
        public IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ListJobExecutionSteps(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip,
            int? top)
        {
            var resp = Communicator.ListJobExecutionSteps(
                resourceGroupName, 
                serverName, 
                agentName, 
                jobName, 
                jobExecutionId,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip,
                top);

            return resp.Select((stepExecution) => CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepExecution));
        }

        #endregion


        #region Target level execution

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
        public AzureSqlDatabaseAgentJobExecutionModel GetJobTargetExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName,
            Guid targetId)
        {
            var resp = Communicator.GetJobTargetExecution(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName, targetId);
            return CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, resp);
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
        public IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ListJobTargetExecutionsByStep(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip,
            int? top)
        {
            var resp = Communicator.ListJobTargetExecutionsByStep(
                resourceGroupName, 
                serverName, 
                agentName, 
                jobName, 
                jobExecutionId, 
                stepName);
            return resp.Select((stepExecution) => CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepExecution));
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
        public IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ListJobTargetExecutionsByTarget(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip,
            int? top)
        {
            var resp = Communicator.ListJobTargetExecutionsByTarget(
                resourceGroupName, 
                serverName, 
                agentName, 
                jobName, 
                jobExecutionId,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip,
                top);

            return resp.Select((stepExecution) => CreateJobExecutionModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepExecution));
        }

        #endregion


        /// <summary>
        /// Convert a Management.Sql.Models.JobAgent to AzureSqlDatabaseAgentJobExecutionModel
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
            Management.Sql.Models.JobExecution resp)
        {
            AzureSqlDatabaseAgentJobExecutionModel jobExecution = new AzureSqlDatabaseAgentJobExecutionModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                CreateTime = resp.CreateTime,
                CurrentAttempts = resp.CurrentAttempts,
                CurrentAttemptStartTime = resp.CurrentAttemptStartTime,
                JobExecutionId = resp.JobExecutionId,
                JobName = jobName,
                JobVersion = resp.JobVersion,
                LastMessage = resp.LastMessage,
                Lifecycle = resp.Lifecycle,
                ProvisioningState = resp.ProvisioningState,
                StepId = resp.StepId,
                Type = resp.Type,
                ResourceId = resp.Id,
                StepName = resp.StepName,
                Target = resp.Target
            };

            return jobExecution;
        }
    }
}