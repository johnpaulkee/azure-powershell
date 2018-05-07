using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentResourceIdentifier : ResourceIdentifier
    {
        public string ServerName { get; set; }

        public string AgentName { get; set; }

        public string TargetGroupName { get; set; }

        public string JobName { get; set; }

        public string StepName { get; set; }

        public string JobExecutionId { get; set; }

        public AzureSqlDatabaseAgentResourceIdentifier(string resourceId)
        {
            // TODO: make this work for agent, target group, credential, job, job step, job execution, step execution, target execution
            string[] tokens = resourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            Subscription = tokens[1];
            ResourceGroupName = tokens[3];
            ServerName = tokens[7];
            AgentName = tokens[9];
            JobName = tokens[11];

            ResourceName = tokens[tokens.Length - 1];
        }
    }
}
