using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.Job.JobExecution;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobExecutionModel : AzureSqlDatabaseAgentJobExecutionBaseModel
    {
        public AzureSqlDatabaseAgentJobExecutionModel() { }

        protected SqlManagementClient Client { get; private set; }

        public virtual IList<AzureSqlDatabaseAgentJobStepExecutionModel> Steps
        {
            get
            {
                IPage<JobExecution> steps = _stepsFunc(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.JobExecutionId.Value);
                return steps.Select((stepExecution) => CreateJobStepExecutionModel(stepExecution)).ToList();
            }
        }

        private Func<string, string, string, string, Guid, IPage<JobExecution>> _stepsFunc { get; set; }

        public IPage<JobExecution> Targets => _targetsFunc(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.JobExecutionId.Value);

        private Func<string, string, string, string, Guid, IPage<JobExecution>> _targetsFunc { get; set; }

        public AzureSqlDatabaseAgentJobExecutionModel(
            string resourceGroupName,
            string serverName,
            string agentName,
            string jobName,
            JobExecution resp,
            SqlManagementClient client)
        {
            this.ResourceGroupName = resourceGroupName;
            this.ServerName = serverName;
            this.AgentName = agentName;
            this.JobName = jobName;

            this.JobVersion = resp.JobVersion;
            this.JobExecutionId = resp.JobExecutionId;
            this.Lifecycle = resp.Lifecycle;
            this.ProvisioningState = resp.ProvisioningState;
            this.CreateTime = resp.CreateTime;
            this.StartTime = resp.StartTime;
            this.EndTime = resp.EndTime;
            this.CurrentAttempts = resp.CurrentAttempts;
            this.CurrentAttemptStartTime = resp.CurrentAttemptStartTime;
            this.LastMessage = resp.LastMessage;

            this.ResourceId = resp.Id;
            this.Type = resp.Type;

            this.Client = client;

            _stepsFunc = (rg, s, a, j, je) => client.JobStepExecutions.ListByJobExecution(rg, s, a, j, je);
            _targetsFunc = (rg, s, a, j, je) => client.JobTargetExecutions.ListByJobExecution(rg, s, a, j, je);
        }

        public AzureSqlDatabaseAgentJobStepExecutionModel CreateJobStepExecutionModel(
            JobExecution stepExecution)
        {
            return new AzureSqlDatabaseAgentJobStepExecutionModel(
                this.ResourceGroupName,
                this.ServerName,
                this.AgentName,
                this.JobName,
                stepExecution,
                this.Client);
        }
    }
}