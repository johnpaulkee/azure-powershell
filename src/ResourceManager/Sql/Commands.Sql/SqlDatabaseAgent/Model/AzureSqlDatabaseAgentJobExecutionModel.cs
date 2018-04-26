using Microsoft.Azure.Management.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobExecutionModel
    {
        /// <summary>
        /// Agent details
        /// </summary>
        public string ResourceGroupName { get; set; }
        public string ServerName{ get; set; }
        public string AgentName{ get; set; }
        
        /// <summary>
        /// Job details
        /// </summary>
        public string JobName{ get; set; }
        public int? JobVersion{ get; set; }

        /// <summary>
        /// Job step details
        /// </summary>
        public string StepName { get; set; }
        public int? StepId { get; set; }

        /// <summary>
        /// Root job execution details
        /// </summary>
        public Guid? JobExecutionId { get; set; }

        public string Lifecycle { get; set; }
        public string ProvisioningState{ get; set; }

        /// <summary>
        /// Target information
        /// </summary>
        public string TargetType { get; set; }
        public string TargetServerName { get; set; }
        public string TargetDatabaseName { get; set; }

        /// <summary>
        /// Summary details
        /// </summary>
        public string LastMessage { get; set; }

        public DateTime? CreateTime { get; set; }
        public int? CurrentAttempts { get; set; }
        public string ResourceId { get; set; }
        public string Type { get; set; }
        public DateTime? CurrentAttemptStartTime { get; set; }
    }
}