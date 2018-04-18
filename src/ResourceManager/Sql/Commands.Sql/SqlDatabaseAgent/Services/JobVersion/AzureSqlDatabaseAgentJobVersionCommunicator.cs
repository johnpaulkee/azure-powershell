using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services.JobVersion
{
    public class AzureSqlDatabaseAgentJobVersionCommunicator
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
        public AzureSqlDatabaseAgentJobVersionCommunicator(IAzureContext context)
        {
            Context = context;
            if (Context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
            }
        }

        public Management.Sql.Models.JobVersion GetJobVersion(string resourceGroupName, string serverName, string agentName, string jobName, int jobVersion)
        {
            var resp = GetCurrentSqlClient().JobVersions.Get(resourceGroupName, serverName, agentName, jobName, jobVersion);
            return resp;
        }

        public IPage<Management.Sql.Models.JobVersion> GetJobVersion(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = GetCurrentSqlClient().JobVersions.ListByJob(resourceGroupName, serverName, agentName, jobName);
            return resp;
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