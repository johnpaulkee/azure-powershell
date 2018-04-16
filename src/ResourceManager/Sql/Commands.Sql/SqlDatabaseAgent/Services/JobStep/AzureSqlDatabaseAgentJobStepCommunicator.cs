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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services
{
    /// <summary>
    /// This class is responsible for all the REST communication with the job step REST endpoints
    /// </summary>
    public class AzureSqlDatabaseAgentJobStepCommunicator
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
        /// Creates a job step communicator
        /// </summary>
        /// <param name="context"></param>
        public AzureSqlDatabaseAgentJobStepCommunicator(IAzureContext context)
        {
            Context = context;
            if (Context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
            }
        }

        /// <summary>
        /// Creates a job step
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <param name="stepName">The target groups name</param>
        /// <param name="parameters">The target group's create parameters</param>
        /// <returns>The created target group</returns>
        public Management.Sql.Models.JobStep CreateOrUpdate(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            string stepName,
            Management.Sql.Models.JobStep parameters)
        {
            return GetCurrentSqlClient().JobSteps.CreateOrUpdate(resourceGroupName, serverName, agentName, jobName, stepName, parameters);
        }

        /// <summary>
        /// Gets a job step
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <param name="stepName">The step name</param>
        /// <returns>The job step belonging to the job</returns>
        public Management.Sql.Models.JobStep Get(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            string stepName)
        {
            return GetCurrentSqlClient().JobSteps.Get(resourceGroupName, serverName, agentName, jobName, stepName);
        }

        /// <summary>
        /// Gets all job steps by job version
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <param name="jobVersion">The job version</param>
        /// <param name="stepName">The step name</param>
        /// <returns>A list of steps in a job for specific job version</returns>
        public Management.Sql.Models.JobStep GetByVersion(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            int jobVersion,
            string stepName)
        {
            return GetCurrentSqlClient().JobSteps.GetByVersion(resourceGroupName, serverName, agentName, jobName, jobVersion, stepName);
        }

        /// <summary>
        /// Deletes a job step belong to a job.
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="jobName">The job name</param>
        /// <param name="stepName">The step name</param>
        public void Remove(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            string stepName)
        {
            GetCurrentSqlClient().JobSteps.Delete(resourceGroupName, serverName, agentName, jobName, stepName);
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
            var sqlClient = AzureSession.Instance.ClientFactory.CreateArmClient<SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            return sqlClient;
        }
    }
}