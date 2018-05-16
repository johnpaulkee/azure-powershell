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
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
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
        /// <summary>
        /// The default parameter sets
        /// </summary>
        protected const string DefaultParameterSet = "Agent Default Parameter Set";
        protected const string InputObjectParameterSet = "Input Object Parameter Set";
        protected const string ResourceIdParameterSet = "Resource Id Parameter Set";

        public string SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        public virtual string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent server name (used in target)
        /// </summary>
        public virtual string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        public virtual string AgentName { get; set; }

        /// <summary>
        /// Gets or sets job name
        /// </summary>
        public virtual string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        public virtual string StepName { get; set; }

        /// <summary>
        /// Gets or sets the job execution id
        /// </summary>
        public virtual string JobExecutionId { get; set; }

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        public virtual string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the credential name
        /// </summary>
        public virtual string CredentialName { get; set; }

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
        /// Initializes the input from model object if necessary
        /// </summary>
        /// <param name="model"></param>
        public void InitializeInputObjectProperties(IO model)
        {
            if (model == null)
            {
                return;
            }

            var resourceGroupProperty = model.GetType().GetProperty("ResourceGroupName");
            this.ResourceGroupName = (resourceGroupProperty != null) ? resourceGroupProperty.GetValue(model).ToString() : this.ResourceGroupName;

            var serverProperty = model.GetType().GetProperty("ServerName");

            if (serverProperty != null)
            {
                string value = serverProperty.GetValue(model).ToString();
                this.AgentServerName = value;
                if (this.ServerName == null)
                {
                    this.ServerName = value;
                }
            }

            var databaseProperty = model.GetType().GetProperty("DatabaseName");
            this.DatabaseName = (databaseProperty != null) ? databaseProperty.GetValue(model).ToString() : this.DatabaseName;

            var agentProperty = model.GetType().GetProperty("AgentName");
            this.AgentName = (agentProperty != null) ? agentProperty.GetValue(model).ToString() : this.AgentName;

            var jobProperty = model.GetType().GetProperty("JobName");
            this.JobName = (jobProperty != null) ? jobProperty.GetValue(model).ToString() : this.JobName;

            var stepProperty = model.GetType().GetProperty("StepName");
            this.StepName = (stepProperty != null) ? stepProperty.GetValue(model).ToString() : this.StepName;

            var targetGroupProperty = model.GetType().GetProperty("TargetGroupName");
            this.TargetGroupName = (targetGroupProperty != null) ? targetGroupProperty.GetValue(model).ToString() : this.TargetGroupName;

            var jobCredentialProperty = model.GetType().GetProperty("CredentialName");
            this.CredentialName = (jobCredentialProperty != null) ? jobCredentialProperty.GetValue(model).ToString() : this.CredentialName;

            var jobExecutionProperty = model.GetType().GetProperty("JobExecutionId");
            this.JobExecutionId = (jobExecutionProperty != null) ? jobExecutionProperty.GetValue(model).ToString() : this.JobExecutionId;
        }

        /// <summary>
        /// Initializes the input from resource id if necessary
        /// </summary>
        /// <param name="resourceId"></param>
        public void InitializeResourceIdProperties(string resourceId)
        {
            if (resourceId == null)
            {
                return;
            }

            string[] tokens = resourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int length = tokens.Length;

            if (length >= 7)
            {
                this.SubscriptionId = tokens[1];
                this.ResourceGroupName = tokens[3];
                this.ServerName = tokens[7];
            }

            if (length >= 9)
            {
                if (tokens[8] == "databases")
                {
                    this.DatabaseName = tokens[9];
                }
                else if (tokens[8] == "jobAgents")
                {
                    this.AgentName = tokens[9];
                }
            }

            if (length >= 11)
            {
                if (tokens[10] == "jobs")
                {
                    this.JobName = tokens[11];
                }
                else if (tokens[10] == "credentials")
                {
                    this.CredentialName = tokens[11];
                }
                else if (tokens[10] == "targetGroups")
                {
                    this.TargetGroupName = tokens[11];
                }
            }

            if (length >= 13)
            {
                if (tokens[12] == "steps")
                {
                    this.StepName = tokens[13];
                }
                else if (tokens[12] == "executions")
                {
                    this.JobExecutionId = tokens[13];
                }
            }
        }

        #region Target Group Helpers

        private const string targetGroupResourceIdTemplate = "/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/targetGroups/{4}";

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