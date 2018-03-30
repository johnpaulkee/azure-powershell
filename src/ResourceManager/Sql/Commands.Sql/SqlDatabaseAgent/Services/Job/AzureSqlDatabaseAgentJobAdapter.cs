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

        public AzureSqlDatabaseAgentJobAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentJobCommunicator(Context);
        }

        /// <summary>
        /// Upserts an Azure SQL Database Agent to a server
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The upserted Azure SQL Database Agent</returns>
        public Management.Sql.Models.Job UpsertJob(string resourceGroupName, string agentServerName, string agentName, string jobName, Management.Sql.Models.Job model)
        {
            var param = new Management.Sql.Models.Job
            {
                Description = "",
                Schedule = new Management.Sql.Models.JobSchedule
                {
                    Enabled = true,
                    EndTime = null,
                    StartTime = null,
                    Interval = null,
                    Type = null
                }
            };

            var resp = Communicator.CreateOrUpdate(resourceGroupName, agentServerName, agentName, jobName, param);

            return resp;
        }

        /// <summary>
        /// Gets a SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group</param>
        /// <param name="serverName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>The converted agent model</returns>
        public Management.Sql.Models.Job GetJob(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = Communicator.Get(resourceGroupName, serverName, agentName, jobName);
            return resp;
        }

        /// <summary>
        /// Gets a list of SQL Database Agents associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="agentServerName">The server the agents are in</param>
        /// <returns>The converted agent model(s)</returns>
        public Rest.Azure.IPage<Management.Sql.Models.Job> GetJob(string resourceGroupName, string agentServerName, string agentName)
        {
            var resp = Communicator.List(resourceGroupName, agentServerName, agentName);
            return resp;
        }

        /// <summary>
        /// Deletes an Azure SQL Database Agent associated to a server
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="agentServerName">The server the agent is in</param>
        /// <param name="agentName">The agent name</param>
        public void RemoveSqlDatabaseAgent(string resourceGroupName, string agentServerName, string agentName, string jobName)
        {
            Communicator.Remove(resourceGroupName, agentServerName, agentName, jobName);
        }
    }
}