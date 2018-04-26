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
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Management.Sql;
using Microsoft.Rest.Azure;
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// This class is responsible for all the REST communication with the audit REST endpoints
    /// </summary>
    public class AzureSqlDatabaseAgentJobExecutionCommunicator
    {
        /// <summary>
        /// Gets or sets the Azure subscription
        /// </summary>
        internal static IAzureSubscription Subscription { get; private set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Creates an Azure SQL Database Agent Communicator
        /// </summary>
        /// <param name="context"></param>
        public AzureSqlDatabaseAgentJobExecutionCommunicator(IAzureContext context)
        {
            Context = context;
            if (Context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
            }
        }

        /// <summary>
        /// Creates a job execution in job
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <returns>A new job execution</returns>
        public Management.Sql.Models.JobExecution Create(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName)
        {
            return GetCurrentSqlClient().JobExecutions.Create(resourceGroupName, serverName, agentName, jobName);
        }

        /// <summary>
        /// Gets the associated Job Credential associated to the Azure SQL Database Agent
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <param name="jobName"></param>
        /// <returns>The agent belonging to specified server</returns>
        public Management.Sql.Models.JobExecution Get(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId)
        {
            return GetCurrentSqlClient().JobExecutions.Get(resourceGroupName, serverName, agentName, jobName, jobExecutionId);
        }

        /// <summary>
        /// Lists the credentials associated to the Azure SQL Database Agent.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>A list of credentials belonging to specified agent</returns>
        public IPage<Management.Sql.Models.JobExecution> ListByAgent(
            string resourceGroupName,
            string serverName,
            string agentName,
            DateTime? createTimeMin,
            DateTime? createTimeMax,
            DateTime? endTimeMin,
            DateTime? endTimeMax,
            bool? isActive,
            int? skip,
            int? top)
        {
            // TODO: .NET SDK update should include top executions too
            return GetCurrentSqlClient().JobExecutions.ListByAgent(
                resourceGroupName: resourceGroupName,
                serverName: serverName,
                jobAgentName: agentName,
                createTimeMin: createTimeMin,
                createTimeMax: createTimeMax,
                endTimeMin: endTimeMin,
                endTimeMax: endTimeMax,
                isActive: isActive,
                skip: skip,
                top: top);
        }

        /// <summary>
        /// Lists the credentials associated to the Azure SQL Database Agent.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>A list of credentials belonging to specified agent</returns>
        public IPage<Management.Sql.Models.JobExecution> ListByJob(
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
            // TODO:
            // create time min, create time max, end time min, end time max, is active, skip, top
            return GetCurrentSqlClient().JobExecutions.ListByJob(
                resourceGroupName: resourceGroupName,
                serverName: serverName, 
                jobAgentName: agentName, 
                jobName: jobName,
                createTimeMin: createTimeMin,
                createTimeMax: createTimeMax,
                endTimeMin: endTimeMin,
                endTimeMax: endTimeMax,
                isActive: isActive,
                skip: skip);
        }

        /// <summary>
        /// Deletes the credential associated to the agent.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="credentialName">The credential name</param>
        public void Cancel(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId)
        {
            GetCurrentSqlClient().JobExecutions.Cancel(resourceGroupName, serverName, agentName, jobName, jobExecutionId);
        }


        #region Job Step Executions

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
        public Management.Sql.Models.JobExecution GetJobStepExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName)
        {
            return GetCurrentSqlClient().JobStepExecutions.Get(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName);
        }

        public IPage<Management.Sql.Models.JobExecution> ListJobExecutionSteps(
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
            return GetCurrentSqlClient().JobStepExecutions.ListByJobExecution(
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
        }

        #endregion


        #region Job Target Executions

        public Management.Sql.Models.JobExecution GetJobTargetExecution(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            Guid jobExecutionId,
            string stepName,
            Guid targetId)
        {
            return GetCurrentSqlClient().JobTargetExecutions.Get(resourceGroupName, serverName, agentName, jobName, jobExecutionId, stepName, targetId);
        }

        public IPage<Management.Sql.Models.JobExecution> ListJobTargetExecutions(
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
            return GetCurrentSqlClient().JobTargetExecutions.ListByJobExecution(
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
        }

        public IPage<Management.Sql.Models.JobExecution> ListJobTargetExecutionsByStep(
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
            return GetCurrentSqlClient().JobTargetExecutions.ListByStep(
                resourceGroupName, 
                serverName, 
                agentName, 
                jobName, 
                jobExecutionId, 
                stepName,
                createTimeMin,
                createTimeMax,
                endTimeMin,
                endTimeMax,
                isActive,
                skip,
                top);
        }

        #endregion


        /// <summary>
        /// Retrieve the SQL Management client for the currently selected subscription, adding the session and request
        /// id tracing headers for the current cmdlet invocation.
        /// </summary>
        /// <returns>The SQL Management client for the currently selected subscription.</returns>
        private SqlManagementClient GetCurrentSqlClient()
        {
            // Get the SQL management client for the current subscription
            // Note: client is not cached in static field because that causes ObjectDisposedException in functional tests.
            var sqlClient = AzureSession.Instance.ClientFactory.CreateArmClient<SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            return sqlClient;
        }
    }
}