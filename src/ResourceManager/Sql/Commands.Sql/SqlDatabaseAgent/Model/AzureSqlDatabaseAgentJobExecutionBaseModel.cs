using Microsoft.Azure.Management.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.Job.JobExecution
{
    public class AzureSqlDatabaseAgentJobExecutionBaseModel : AzureSqlDatabaseAgentJobModelBase
    {
        /// <summary>
        /// Root job execution details
        /// </summary>
        protected SqlManagementClient Client { get; set; }

        public Guid? JobExecutionId { get; set; }

        public int? JobVersion { get; set; }

        public string Lifecycle { get; set; }

        public string ProvisioningState { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? CurrentAttempts { get; set; }

        public DateTime? CurrentAttemptStartTime { get; set; }

        public string LastMessage { get; set; }

        protected AzureSqlDatabaseAgentJobStepExecutionModel CreateJobStepExecutionModel(
            Management.Sql.Models.JobExecution stepExecution)
        {
            return new AzureSqlDatabaseAgentJobStepExecutionModel(
                this.ResourceGroupName,
                this.ServerName,
                this.AgentName,
                this.JobName,
                stepExecution,
                this.Client);
        }

        protected AzureSqlDatabaseAgentJobTargetExecutionModel CreateJobTargetExecutionModel(
            Management.Sql.Models.JobExecution targetExecution)
        {
            return new AzureSqlDatabaseAgentJobTargetExecutionModel(
                this.ResourceGroupName,
                this.ServerName,
                this.AgentName,
                this.JobName,
                targetExecution);
        }
    }
}
