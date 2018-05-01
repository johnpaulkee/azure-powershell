using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.Job.JobExecution;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobStepExecutionModel : AzureSqlDatabaseAgentJobExecutionBaseModel
    {
        public AzureSqlDatabaseAgentJobStepExecutionModel(
                string resourceGroupName,
                string serverName,
                string agentName,
                string jobName,
                Management.Sql.Models.JobExecution stepExecution,
                SqlManagementClient client)
        {
            ResourceGroupName = resourceGroupName;
            ServerName = serverName;
            AgentName = agentName;
            JobName = jobName;
            JobVersion = stepExecution.JobVersion;
            JobExecutionId = stepExecution.JobExecutionId;
            LastMessage = stepExecution.LastMessage;
            Lifecycle = stepExecution.Lifecycle;
            StepName = stepExecution.Name;
            StepId = stepExecution.StepId;
            ProvisioningState = stepExecution.ProvisioningState;
            CreateTime = stepExecution.CreateTime;
            StartTime = stepExecution.StartTime;
            EndTime = stepExecution.EndTime;
            CurrentAttempts = stepExecution.CurrentAttempts;
            CurrentAttemptStartTime = stepExecution.CurrentAttemptStartTime;
            ResourceId = stepExecution.Id;
            Type = stepExecution.Type;
            _targetsFunc = (rg, s, a, j, je, step) => client.JobTargetExecutions.ListByStep(rg, s, a, j, je, step);
        }

        public string StepName;

        public int? StepId;

        private Func<string, string, string, string, Guid, string, IPage<Management.Sql.Models.JobExecution>> _targetsFunc { get; set; }

        private IList<AzureSqlDatabaseAgentJobTargetExecutionModel> _targets;

        public IList<AzureSqlDatabaseAgentJobTargetExecutionModel> Targets
        {
            get
            {
                if (_targets == null)
                {
                    IPage<Management.Sql.Models.JobExecution> targets = _targetsFunc(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.JobExecutionId.Value, this.StepName);
                    _targets = targets.Select((targetExecution) => CreateJobTargetExecutionModel(targetExecution)).ToList();
                    return _targets;
                }
                return _targets;
            }
        }
    }
}