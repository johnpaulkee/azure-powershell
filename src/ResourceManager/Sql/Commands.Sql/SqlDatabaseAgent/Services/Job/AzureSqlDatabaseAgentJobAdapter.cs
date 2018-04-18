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
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// Adapter for Azure SQL Database Agent operations
    /// </summary>
    public class AzureSqlDatabaseAgentJobAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlDatabaseAgentJobCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// The job adapter constructor
        /// </summary>
        /// <param name="context">The current powershell Azure Context</param>
        public AzureSqlDatabaseAgentJobAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentJobCommunicator(Context);
        }

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
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Interval = model.Interval,
                    Type = model.Type,
                    Enabled = model.Enabled,
                }
            };

            var resp = Communicator.CreateOrUpdate(model.ResourceGroupName, model.ServerName, model.AgentName, model.JobName, param);
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
            var resp = Communicator.Get(resourceGroupName, serverName, agentName, jobName);
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
            var resp = Communicator.List(resourceGroupName, serverName, agentName);
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
            Communicator.Remove(resourceGroupName, agentServerName, agentName, jobName);
        }

        public AzureSqlDatabaseAgentJobModel CreateJobModelFromResponse(string resourceGroupName, string serverName, string agentName, Job resp)
        {
            return new AzureSqlDatabaseAgentJobModel
            {
                ResourceGroupName = resourceGroupName,
                ServerName = serverName,
                AgentName = agentName,
                JobName = resp.Name
            };
        }
    }
}