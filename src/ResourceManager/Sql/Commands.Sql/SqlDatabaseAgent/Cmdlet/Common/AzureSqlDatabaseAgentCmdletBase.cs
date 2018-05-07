// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.Database.Model;
using Microsoft.Azure.Commands.Sql.Server.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.Common
{
    /// <summary>
    /// The base class for all Azure Sql database cmdlets. 
    /// - IO represents the InputObject type for Sets and Removes
    /// - M represents the response Model type
    /// - A represents the Adapter
    /// </summary>
    public abstract class AzureSqlDatabaseAgentCmdletBase<IO, M, A> : AzureSqlCmdletBase<M, A>
    {
        protected const string DefaultParameterSet = "Agent Default Parameter Set";
        protected const string InputObjectParameterSet = "Input Object Parameter Set";
        protected const string ResourceIdParameterSet = "Resource Id Parameter Set";

        /// <summary>
        /// Azure SQL Database Agent Object Parameter Sets
        /// </summary>
        protected const string DatabaseObjectParameterSet      = "Database Object Parameter Set";
        protected const string ServerObjectParameterSet        = "Server Object Parameter Set";
        protected const string AgentObjectParameterSet         = "Agent Object Parameter Set";
        protected const string JobObjectParameterSet           = "Job Object Parameter Set";
        protected const string JobStepObjectParameterSet       = "Job Step Object Parameter Set";
        protected const string TargetGroupObjectParameterSet   = "Target Group Object Parameter Set";
        protected const string JobCredentialObjectParameterSet = "Job Credential Object Parameter Set";
        protected const string JobExecutionObjectParameterSet  = "Job Execution Object Parameter Set";

        protected const string DatabaseResourceIdParameterSet      = "Database Resource Id Parameter Set";
        protected const string ServerResourceIdParameterSet        = "Server Resource Id Parameter Set";
        protected const string AgentResourceIdParameterSet         = "Agent Resource Id Parameter Set";
        protected const string JobResourceIdParameterSet           = "Job Resource Id Parameter Set";
        protected const string JobStepResourceIdParameterSet       = "Job Step Resource Id Parameter Set";
        protected const string TargetGroupResourceIdParameterSet   = "Target Group Resource Id Parameter Set";
        protected const string JobCredentialResourceIdParameterSet = "Job Credential Resource Id Parameter Set";
        protected const string JobExecutionResourceIdParameterSet  = "Job Execution Resource Id Parameter Set";

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        public virtual string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        public virtual string AgentName { get; set; }

        /// <summary>
        /// Gets or sets job name
        /// </summary>
        public virtual string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job execution id
        /// </summary>
        public virtual string JobExecutionId { get; set; }

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        public virtual string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the shard map name
        /// </summary>
        public virtual string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the refresh credential name
        /// </summary>
        public virtual string RefreshCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the elastic pool name
        /// </summary>
        public virtual string ElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the database name
        /// </summary>
        public virtual string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the resource name - depends on the override in child classes
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets server object
        /// </summary>
        public virtual IO InputObject { get; set; }

        /// <summary>
        /// Gets or sets server object
        /// </summary>
        public virtual AzureSqlServerModel ServerObject { get; set; }

        /// <summary>
        /// Gets or sets database object
        /// </summary>
        public virtual AzureSqlDatabaseModel DatabaseObject { get; set; }

        /// <summary>
        /// Gets or sets agent object
        /// </summary>
        public virtual AzureSqlDatabaseAgentModel AgentObject { get; set; }

        /// <summary>
        /// Gets or sets agent object
        /// </summary>
        public virtual AzureSqlDatabaseAgentModel JobCredentialObject { get; set; }

        /// <summary>
        /// Gets or sets target group object
        /// </summary>
        public virtual AzureSqlDatabaseAgentTargetGroupModel TargetGroupObject { get; set; }

        /// <summary>
        /// Gets or sets job object
        /// </summary>
        public virtual AzureSqlDatabaseAgentJobModel JobObject { get; set; }

        /// <summary>
        /// Gets or sets job step object
        /// </summary>
        public virtual AzureSqlDatabaseAgentJobStepModel JobStepObject { get; set; }

        /// <summary>
        /// Gets or sets job execution object
        /// </summary>
        public virtual AzureSqlDatabaseAgentJobExecutionModel JobExecutionObject { get; set; }

        /// <summary>
        /// Gets or sets the resource id
        /// </summary>
        public virtual string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the server resource id
        /// </summary>
        public virtual string ServerResourceId { get; set; }

        /// <summary>
        /// Gets or sets the database resource id
        /// </summary>
        public virtual string DatabaseResourceId { get; set; }
        /// <summary>
        /// Gets or sets the agent resource id
        /// </summary>
        public virtual string AgentResourceId { get; set; }

        /// <summary>
        /// Gets or sets the target group resource id
        /// </summary>
        public virtual string TargetGroupResourceId { get; set; }

        /// <summary>
        /// Gets or sets the job execution resource id
        /// </summary>
        public virtual string JobExecutionResourceId { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        public virtual string JobResourceId { get; set; }

        /// <summary>
        /// Gets or sets the job step resource id
        /// </summary>
        public virtual string JobStepResourceId { get; set; }

        /// <summary>
        /// Cmdlet execution starts here
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case ServerObjectParameterSet:
                    InitializeServerProperties(this.ServerObject);
                    break;
                case DatabaseObjectParameterSet:
                    InitializeDatabaseProperties(this.DatabaseObject);
                    break;
                case AgentObjectParameterSet:
                    InitializeAgentProperties(this.AgentObject);
                    break;
                case TargetGroupObjectParameterSet:
                    InitializeTargetGroupProperties(this.TargetGroupObject);
                    break;
                case JobObjectParameterSet:
                    this.ResourceGroupName = JobObject.ResourceGroupName;
                    this.ServerName = JobObject.ServerName;
                    this.AgentName = JobObject.AgentName;
                    this.Name = JobObject.JobName;
                    break;
                case JobStepObjectParameterSet:
                    this.ResourceGroupName = JobStepObject.ResourceGroupName;
                    this.ServerName = JobStepObject.ServerName;
                    this.AgentName = JobStepObject.AgentName;
                    this.JobName = JobStepObject.JobName;
                    this.Name = JobStepObject.StepName;
                    break;
                case JobExecutionObjectParameterSet:
                    this.ResourceGroupName = JobExecutionObject.ResourceGroupName;
                    this.ServerName = JobExecutionObject.ServerName;
                    this.AgentName = JobExecutionObject.AgentName;
                    this.JobName = JobExecutionObject.JobName;
                    this.JobExecutionId = JobExecutionObject.JobExecutionId.Value.ToString();
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
        }

        #region Server Helpers

        public void InitializeServerProperties(AzureSqlServerModel serverObject)
        {
            if (serverObject != null)
            {
                this.ResourceGroupName = serverObject.ResourceGroupName;
                this.ServerName = serverObject.ServerName;
            }
        }

        #endregion

        #region Agent Helpers

        public void InitializeAgentProperties(AzureSqlDatabaseAgentModel agentObject)
        {
            if (agentObject != null)
            {
                this.ResourceGroupName = agentObject.ResourceGroupName;
                this.ServerName = agentObject.ServerName;
                this.Name = agentObject.AgentName;
            }
        }

        #endregion

        #region Job Helpers

        public void InitializeJobProperties(AzureSqlDatabaseAgentJobModel jobObject)
        {
            if (jobObject != null)
            {
                this.ResourceGroupName = jobObject.ResourceGroupName;
                this.ServerName = jobObject.ServerName;
                this.AgentName = jobObject.AgentName;
                this.Name = jobObject.JobName;
            }
        }

        #endregion

        #region Job Credential Helpers

        public void InitializeJobCredentialProperties(AzureSqlDatabaseAgentJobCredentialModel jobCredentialObject)
        {
            if (jobCredentialObject != null)
            {
                this.ResourceGroupName = jobCredentialObject.ResourceGroupName;
                this.ServerName = jobCredentialObject.ServerName;
                this.AgentName = jobCredentialObject.AgentName;
                this.Name = jobCredentialObject.CredentialName;
            }
        }

        #endregion

        #region Job Step Helpers

        public void InitializeJobStepProperties(AzureSqlDatabaseAgentJobStepModel jobStep)
        {
            if (jobStep != null)
            {
                this.ResourceGroupName = jobStep.ResourceGroupName;
                this.ServerName = jobStep.ServerName;
                this.AgentName = jobStep.AgentName;
                this.JobName = jobStep.JobName;
                this.Name = jobStep.StepName;
            }
        }

        #endregion

        #region Job Execution Helpers

        public void InitializeJobExecutionProperties(AzureSqlDatabaseAgentJobExecutionModel jobExecution)
        {
            if (jobExecution != null)
            {
                this.ResourceGroupName = jobExecution.ResourceGroupName;
                this.ServerName = jobExecution.ServerName;
                this.AgentName = jobExecution.AgentName;
                this.JobName = jobExecution.JobName;
                this.JobExecutionId = jobExecution.JobExecutionId.Value.ToString();
            }
        }

        #endregion

        #region Database Helpers

        public void InitializeDatabaseProperties(AzureSqlDatabaseModel dbObject)
        {
            if (dbObject != null)
            {
                this.ResourceGroupName = dbObject.ResourceGroupName;
                this.ServerName = dbObject.ServerName;
                this.DatabaseName = dbObject.DatabaseName;
            }
        }

        #endregion

        #region Target Group Helpers

        private const string targetGroupResourceIdTemplate = "/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/targetGroups/{4}";

        public void InitializeTargetGroupProperties(AzureSqlDatabaseAgentTargetGroupModel targetGroupObject)
        {
            if (targetGroupObject != null)
            {
                this.ResourceGroupName = targetGroupObject.ResourceGroupName;
                this.ServerName = targetGroupObject.ServerName;
                this.AgentName = targetGroupObject.AgentName;
                this.Name = targetGroupObject.TargetGroupName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetGroupName"></param>
        /// <returns></returns>
        protected string CreateTargetGroupId(
            string resourceGroupName,
            string serverName,
            string agentName,
            string targetGroupName)
        {
            if (targetGroupName == null)
            {
                return null;
            }

            return string.Format(targetGroupResourceIdTemplate,
                            DefaultContext.Subscription.Id,
                            resourceGroupName,
                            serverName,
                            agentName,
                            targetGroupName);
        }

        #endregion

        #region Credential Helpers

        protected string CreateCredentialId(
            string resourceGroupName,
            string serverName,
            string agentName,
            string credentialName)
        {
            if (credentialName == null)
            {
                return null;
            }

            return string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                            DefaultContext.Subscription.Id,
                            resourceGroupName,
                            serverName,
                            agentName,
                            credentialName);
        }

        #endregion

        #region 

        public string GetJobNameFromJobExecutionResourceId(string jobExecutionResourceId)
        {
            return "";
        }

        #endregion
    }
}