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

using System.Collections;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.Database.Model;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepModel))]
    public class NewAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
    {
        /// <summary>
        /// Gets or sets the job object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultParameterSet)]
        public string JobName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = DefaultParameterSet)]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Alias("StepName")]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = DefaultParameterSet)]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            Position = 2,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            Position = 2,
            HelpMessage = "The target group name")]
        public string TargetGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = DefaultParameterSet)]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            Position = 3,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            Position = 3,
            HelpMessage = "The credential name")]
        public string CredentialName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = DefaultParameterSet)]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            Position = 4,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            Position = 4,
            HelpMessage = "The command text")]
        public string CommandText { get; set; }

        [Parameter(Mandatory = false)]
        public Management.Sql.Models.JobStepOutput Output { get; set; }

        [Parameter(Mandatory = false)]
        public int? TimeoutSeconds { get; set; }

        [Parameter(Mandatory = false)]
        public int? RetryAttempts { get; set; }

        [Parameter(Mandatory = false)]
        public int? InitialRetryIntervalSeconds { get; set; }

        [Parameter(Mandatory = false)]
        public int? MaximumRetryIntervalSeconds { get; set; }

        [Parameter(Mandatory = false)]
        public double? RetryIntervalBackoffMultipler { get; set; }

        [Parameter(Mandatory = false)]
        public int? StepId { get; set; }

        /// <summary>
        /// Cmdlet execution starts here
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectParameterSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    this.JobName = InputObject.JobName;
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[9]; // TODO:
                    this.JobName = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Check to see if the agent already exists in this resource group.
        /// </summary>
        /// <returns>Null if the agent doesn't exist. Otherwise throws exception</returns>
        protected override AzureSqlDatabaseAgentJobStepModel GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("AgentName: {0}", Name);
                ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.JobName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no agent with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The agent already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureSqlDatabaseAgentExists, this.Name, this.JobName),
                "AgentName");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentJobStepModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobStepModel model)
        {
            string targetGroupId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/targetGroups/{4}",
                AzureSqlDatabaseAgentTargetGroupCommunicator.Subscription.Id,
                this.ResourceGroupName,
                this.ServerName,
                this.AgentName,
                this.TargetGroupName);

            string credentialId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                AzureSqlDatabaseAgentTargetGroupCommunicator.Subscription.Id,
                this.ResourceGroupName,
                this.ServerName,
                this.AgentName,
                this.CredentialName);

            AzureSqlDatabaseAgentJobStepModel updatedModel = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.JobName,
                StepName = this.Name,
                TargetGroup = targetGroupId,
                Credential = credentialId,
                Output = Output,
                ExecutionOptions = new Management.Sql.Models.JobStepExecutionOptions
                {
                    InitialRetryIntervalSeconds = this.InitialRetryIntervalSeconds,
                    MaximumRetryIntervalSeconds = this.MaximumRetryIntervalSeconds,
                    RetryAttempts = this.RetryAttempts,
                    RetryIntervalBackoffMultiplier = this.RetryIntervalBackoffMultipler,
                    TimeoutSeconds = this.TimeoutSeconds
                },
                Action = new Management.Sql.Models.JobStepAction
                {
                    Source = "Inline",
                    Type = "TSql",
                    Value = this.CommandText
                },
                StepId = StepId
            };

            return updatedModel;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the agent
        /// </summary>
        /// <param name="entity">The agent to create</param>
        /// <returns>The created agent</returns>
        protected override AzureSqlDatabaseAgentJobStepModel PersistChanges(AzureSqlDatabaseAgentJobStepModel entity)
        {
            return ModelAdapter.UpsertJobStep(entity);
        }
    }
}