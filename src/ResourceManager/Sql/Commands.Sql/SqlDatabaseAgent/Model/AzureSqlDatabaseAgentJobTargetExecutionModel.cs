using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.Job.JobExecution;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobTargetExecutionModel : AzureSqlDatabaseAgentJobExecutionBaseModel
    {
        public int? StepId;

        public string StepName;

        public string TargetServerName;
        public string TargetDatabaseName;
        public string TargetId;

        public AzureSqlDatabaseAgentJobTargetExecutionModel(
                string resourceGroupName,
                string serverName,
                string agentName,
                string jobName,
                Management.Sql.Models.JobExecution targetExecution)
        {
            this.ResourceGroupName = resourceGroupName;
            this.ServerName = serverName;
            this.AgentName = agentName;
            this.JobName = jobName;
            this.CreateTime = targetExecution.CreateTime;
            this.CurrentAttempts = targetExecution.CurrentAttempts;
            this.CurrentAttemptStartTime = targetExecution.CurrentAttemptStartTime;
            this.EndTime = targetExecution.EndTime;
            this.ResourceId = targetExecution.Id;
            this.JobExecutionId = targetExecution.JobExecutionId;
            this.JobVersion = targetExecution.JobVersion;
            this.LastMessage = targetExecution.LastMessage;
            this.Lifecycle = targetExecution.Lifecycle;
            this.ProvisioningState = targetExecution.ProvisioningState;
            this.StartTime = targetExecution.StartTime;
            this.StepId = targetExecution.StepId;
            this.StepName = targetExecution.StepName;
            this.TargetServerName = targetExecution.Target.ServerName;
            this.TargetDatabaseName = targetExecution.Target.DatabaseName;
            this.TargetId = targetExecution.Name;
            this.Type = targetExecution.Type;
        }
    }
}