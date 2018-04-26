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

using System.Management.Automation;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = AddOutputInputObjectParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepModel))]
    public class SetAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
    {
        /// <summary>
        /// Gets or sets the job object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputInputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobStepModel InputObject { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        public override string ResourceGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public override string ServerName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The step name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The step name")]
        public string JobName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = AddOutputDefaultParameterSet)]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The step name")]
        [Alias("StepName")]
        public string Name { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The target group name")]
        public string TargetGroupName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The credential name")]
        public string CredentialName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The command text")]
        public string CommandText { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output subscription id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output subscription id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output subscription id")]
        public override string OutputSubscriptionId { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output resource group name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output resource group name")]
        public override string OutputResourceGroupName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output server name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output server name")]
        public override string OutputServerName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output database name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output database name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output database name")]
        public override string OutputDatabaseName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output schema name")]
        public override string OutputSchemaName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output table name")]
        public override string OutputTableName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output credential name")]
        public override string OutputCredentialName { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputInputObjectParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = RemoveOutputResourceIdParameterSet,
            HelpMessage = "The command text")]
        public SwitchParameter RemoveOutput { get; set; }

        /// <summary>
        /// Execution Options
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The timeout seconds")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The timeout seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The timeout seconds")]
        public int? TimeoutSeconds { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The retry attempts")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The retry attempts")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The retry attempts")]
        public int? RetryAttempts { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        public int? InitialRetryIntervalSeconds { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        public int? MaximumRetryIntervalSeconds { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        public double? RetryIntervalBackoffMultiplier { get; set; }

        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The step id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The step id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The step id")]
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
                    this.Name = InputObject.StepName;
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[9];
                    this.JobName = tokens[11];
                    this.Name = tokens[tokens.Length - 1];
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
                WriteDebugWithTimestamp("StepName: {0}", Name);
                return ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no agent with this name.
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobStepNotExists, this.Name, this.JobName),
                        "JobStep");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentJobStepModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobStepModel model)
        {
            AzureSqlDatabaseAgentJobStepModel updatedModel = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.JobName,
                StepName = this.Name,
                TargetGroup = this.TargetGroupName != null ? CreateTargetGroupId(this.TargetGroupName) : model.TargetGroup,
                Credential = this.CredentialName != null ? CreateCredentialId(this.CredentialName) : model.Credential,
                ExecutionOptions = new Management.Sql.Models.JobStepExecutionOptions
                {
                    InitialRetryIntervalSeconds = this.InitialRetryIntervalSeconds != null ? this.InitialRetryIntervalSeconds : model.ExecutionOptions.InitialRetryIntervalSeconds,
                    MaximumRetryIntervalSeconds = this.MaximumRetryIntervalSeconds != null ? this.MaximumRetryIntervalSeconds : model.ExecutionOptions.MaximumRetryIntervalSeconds,
                    RetryAttempts = this.RetryAttempts != null ? this.RetryAttempts : model.ExecutionOptions.RetryAttempts,
                    RetryIntervalBackoffMultiplier = this.RetryIntervalBackoffMultiplier != null ? this.RetryIntervalBackoffMultiplier : model.ExecutionOptions.RetryIntervalBackoffMultiplier,
                    TimeoutSeconds = this.TimeoutSeconds != null ? this.TimeoutSeconds : model.ExecutionOptions.TimeoutSeconds
                },
                Action = new Management.Sql.Models.JobStepAction
                {
                    Value = this.CommandText != null ? this.CommandText : model.Action.Value
                },
                StepId = this.StepId != null ? this.StepId : model.StepId
            };

            // If remove output is not present, then we will update output details if necessary, otherwise use existing output model.
            // If remove output is present, then model will exclude output details and update will clear it.
            if (!this.RemoveOutput.IsPresent)
            {
                Management.Sql.Models.JobStepOutput output;
                bool existingOutput = model.Output != null;

                if (existingOutput)
                {
                    output = CreateJobStepOutputModel(
                        outputSubscriptionId: model.Output.SubscriptionId,
                        outputResourceGroupName: model.Output.ResourceGroupName,
                        outputServerName: model.Output.ServerName,
                        outputDatabaseName: model.Output.DatabaseName,
                        outputSchemaName: model.Output.SchemaName,
                        outputCredential: model.Output.Credential,
                        outputTableName: model.Output.TableName);
                }
                else
                {
                    output = CreateJobStepOutputModel();
                }

                updatedModel.Output = output;
            }

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