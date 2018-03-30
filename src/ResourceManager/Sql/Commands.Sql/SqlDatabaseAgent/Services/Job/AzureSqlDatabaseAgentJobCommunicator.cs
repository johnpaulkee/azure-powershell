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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// This class is responsible for all the REST communication with the audit REST endpoints
    /// </summary>
    public class AzureSqlDatabaseAgentJobCommunicator
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
        public AzureSqlDatabaseAgentJobCommunicator(IAzureContext context)
        {
            Context = context;
            if (Context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
            }
        }

        /// <summary>
        /// Creates a Job
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="parameters">The agent's create parameters</param>
        /// <returns>The newly created agent</returns>
        public Management.Sql.Models.Job CreateOrUpdate(string resourceGroupName, string serverName, string agentName, string jobName, Management.Sql.Models.Job parameters)
        {
            return GetCurrentSqlClient().Jobs.CreateOrUpdate(resourceGroupName, serverName, agentName, jobName, parameters);
        }

        /// <summary>
        /// Gets the agent associated to resourceGroup
        /// </summary>
        /// <param name="resourceGroupName"></param>
        /// <param name="serverName"></param>
        /// <param name="agentName"></param>
        /// <returns>The agent belonging to specified server</returns>
        public Management.Sql.Models.Job Get(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            return GetCurrentSqlClient().Jobs.Get(resourceGroupName, serverName, agentName, jobName);
        }

        /// <summary>
        /// List jobs by agent.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <returns>Returns a list of jobs the agent has</returns>
        public IPage<Management.Sql.Models.Job> List(string resourceGroupName, string serverName, string agentName)
        {
            return GetCurrentSqlClient().Jobs.ListByAgent(resourceGroupName, serverName, agentName);
        }

        /// <summary>
        /// Deletes a job.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name.</param>
        /// <param name="serverName">The server name.</param>
        /// <param name="agentName">The agent name.</param>
        /// <param name="jobName">The job name.</param>
        public void Remove(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            GetCurrentSqlClient().Jobs.Delete(resourceGroupName, serverName, agentName, jobName);
        }

        /// <summary>
        /// Retrieve the SQL Management client for the currently selected subscription, adding the session and request
        /// id tracing headers for the current cmdlet invocation.
        /// </summary>
        /// <returns>The SQL Management client for the currently selected subscription.</returns>
        private SqlManagementClient GetCurrentSqlClient()
        {
            // Get the SQL management client for the current subscription
            // Note: client is not cached in static field because that causes ObjectDisposedException in functional tests.
            var sqlClient = AzureSession.Instance.ClientFactory.CreateArmClient<Management.Sql.SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            return sqlClient;
        }
    }
}