using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services.JobVersion
{
    public class AzureSqlDatabaseAgentJobVersionAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlDatabaseAgentJobVersionCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        public AzureSqlDatabaseAgentJobVersionAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlDatabaseAgentJobVersionCommunicator(Context);
        }

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
        public IEnumerable<AzureSqlDatabaseAgentJobVersionModel> GetJobVersion(string resourceGroupName, string serverName, string agentName, string jobName)
        {
            var resp = Communicator.GetJobVersion(resourceGroupName, serverName, agentName, jobName);
            return resp.Select((version) => CreateJobVersionModelFromResponse(resourceGroupName, serverName, agentName, jobName, version));
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
    }
}
