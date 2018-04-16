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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// Adapter for Azure SQL Database Agent operations
    /// </summary>
    public class AzureSqlDatabaseAgentJobStepAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlDatabaseAgentJobStepCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        public AzureSqlDatabaseAgentJobStepAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentJobStepCommunicator(Context);
        }

        /// <summary>
        /// Upserts a job step
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted job step</returns>
        public AzureSqlDatabaseAgentJobStepModel UpsertJobStep(AzureSqlDatabaseAgentJobStepModel model)
        {
            var param = new Management.Sql.Models.JobStep
            {

            };

            var resp = Communicator.CreateOrUpdate(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.StepName, param);
            return CreateJobStepModelFromResponse(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, model.StepName, resp);
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public AzureSqlDatabaseAgentJobStepModel GetJobStepByVersion(string resourceGroupName, string serverName, string agentName, string jobName, int jobVersion, string stepName)
        {
            var resp = Communicator.GetByVersion(resourceGroupName, serverName, agentName, jobName, jobVersion, stepName);
            return CreateJobStepModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepName, resp);
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
            var resp = Communicator.Get(resourceGroupName, serverName, agentName, jobName, stepName);
            return CreateJobStepModelFromResponse(resourceGroupName, serverName, agentName, jobName, stepName, resp);
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveTargetGroup(string resourceGroupName, string serverName, string agentName, string jobName, string stepName)
        {
            Communicator.Remove(resourceGroupName, serverName, agentName, jobName, stepName);
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
            Management.Sql.Models.JobStep resp)
        {
            // TODO: Update this to include response details
            AzureSqlDatabaseAgentJobStepModel targetGroup = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = jobName,
                StepName = stepName
            };

            return targetGroup;
        }
    }
}