using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.Job.JobExecution;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobExecutionModel : AzureSqlDatabaseAgentJobExecutionBaseModel
    {
        public AzureSqlDatabaseAgentJobExecutionModel() { }


        private IList<AzureSqlDatabaseAgentJobStepExecutionModel> _steps;

        public virtual IList<AzureSqlDatabaseAgentJobStepExecutionModel> Steps
        {
            get
            {
                if (_steps == null)
                {
                    IPage<JobExecution> steps = _stepsFunc(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.JobExecutionId.Value);
                    _steps = steps.Select((stepExecution) => CreateJobStepExecutionModel(stepExecution)).ToList();
                    return _steps;
                }
                return _steps;
            }
        }

        private Func<string, string, string, string, Guid, IPage<JobExecution>> _stepsFunc { get; set; }

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
        }
    }
}