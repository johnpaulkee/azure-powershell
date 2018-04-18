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
        public string ResourceGroupName { get; set; }
        public string ServerName{ get; set; }
        public string AgentName{ get; set; }
        public string JobName{ get; set; }
        public int? JobVersion{ get; set; }
        public Guid? JobExecutionId{ get; set; }
        public string Lifecycle{ get; set; }
        public string ProvisioningState{ get; set; }
        public DateTime? CreateTime{ get; set; }
        public int? CurrentAttempts{ get; set; }
        public string LastMessage{ get; set; }
        public string ResourceId{ get; set; }
        public string Type{ get; set; }
        public int? StepId{ get; set; }
        public DateTime? CurrentAttemptStartTime{ get; set; }
        public string StepName { get; set; }
        public JobExecutionTarget Target { get; set; }
    }
}