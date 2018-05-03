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
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = AddOutputDefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepModel))]
    public class SetAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
    {
        /// <summary>
        /// Gets or sets the resource group name
        /// </summary>
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
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
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

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
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
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The job name")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The step name")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The step name")]
        [Alias("StepName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
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

        /// <summary>
        /// Gets or sets the credential name
        /// </summary>
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

        /// <summary>
        /// Gets or sets the command text
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The command text")]
        public string CommandText { get; set; }

        /// <summary>
        /// Gets or sets the output subscription id
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output subscription id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output subscription id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output subscription id")]
        public override string OutputSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the output resource group name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output resource group name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output resource group name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output resource group name")]
        public override string OutputResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the output server name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output server name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output server name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output server name")]
        public override string OutputServerName { get; set; }

        /// <summary>
        /// Gets or sets the output database name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output database name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output database name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output database name")]
        public override string OutputDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the output schema name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output schema name")]
        public override string OutputSchemaName { get; set; }

        /// <summary>
        /// Gets or sets the output table name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output table name")]
        public override string OutputTableName { get; set; }

        /// <summary>
        /// Gets or sets the output credential name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The output credential name")]
        public override string OutputCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter to remove the job step output target
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputDefaultParameterSet,
            HelpMessage = "The flag to indicate to removing the job output from this job step")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputInputObjectParameterSet,
            HelpMessage = "The flag to indicate to removing the job output from this job step")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputResourceIdParameterSet,
            HelpMessage = "The flag to indicate to removing the job output from this job step")]
        public SwitchParameter RemoveOutput { get; set; }

        /// <summary>
        /// Gets or sets the timeout seconds
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The timeout seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The timeout seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The timeout seconds")]
        public int? TimeoutSeconds { get; set; }

        /// <summary>
        /// Gets or sets the retry attempts
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The retry attempts")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The retry attempts")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The retry attempts")]
        public int? RetryAttempts { get; set; }

        /// <summary>
        /// Gets or sets the initial retry interval seconds
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The initial retry interval seconds")]
        public int? InitialRetryIntervalSeconds { get; set; }

        /// <summary>
        /// Gets or sets the maximum retry interval seconds
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The maximum retry interval seconds")]
        public int? MaximumRetryIntervalSeconds { get; set; }

        /// <summary>
        /// Gets or sets the retry interval backoff multiplier
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The retry interval backoff multiplier")]
        public double? RetryIntervalBackoffMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the job step id
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputDefaultParameterSet,
            HelpMessage = "The job step id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputInputObjectParameterSet,
            HelpMessage = "The job step id")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AddOutputResourceIdParameterSet,
            HelpMessage = "The job step id")]
        public int? StepId { get; set; }

        /// <summary>
        /// Gets or sets the job step object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputInputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job step object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputInputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job step object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobStepModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job step resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AddOutputResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job step resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = RemoveOutputResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job step resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Cmdlet execution starts here
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case AddOutputInputObjectParameterSet:
                case RemoveOutputInputObjectParameterSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    this.JobName = InputObject.JobName;
                    this.Name = InputObject.StepName;
                    break;
                case AddOutputResourceIdParameterSet:
                case RemoveOutputResourceIdParameterSet:
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
        /// Check to see if the job step already exists in job.
        /// </summary>
        /// <returns>Null if the job step doesn't exist. Otherwise throws exception</returns>
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
        /// <param name="model">The existing job step model</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentJobStepModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobStepModel model)
        {
            Management.Sql.Models.JobStepOutput output =
                this.RemoveOutput.IsPresent ? null :
                    (model.Output == null) ?
                        CreateOrUpdateJobStepOutputModel() :                // create new output model
                        CreateOrUpdateJobStepOutputModel(model.Output);     // update existing output model

            AzureSqlDatabaseAgentJobStepModel updatedModel = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.JobName,
                StepName = this.Name,
                TargetGroupName = this.TargetGroupName != null ? 
                    CreateTargetGroupId(this.ResourceGroupName, this.ServerName, this.AgentName, this.TargetGroupName) : 
                    CreateTargetGroupId(this.ResourceGroupName, this.ServerName, this.AgentName, model.TargetGroupName),
                CredentialName = this.CredentialName != null ? 
                    CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.CredentialName) : 
                    CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, model.CredentialName),
                ExecutionOptions = new Management.Sql.Models.JobStepExecutionOptions
                {
                    InitialRetryIntervalSeconds = this.InitialRetryIntervalSeconds != null ? this.InitialRetryIntervalSeconds : model.ExecutionOptions.InitialRetryIntervalSeconds,
                    MaximumRetryIntervalSeconds = this.MaximumRetryIntervalSeconds != null ? this.MaximumRetryIntervalSeconds : model.ExecutionOptions.MaximumRetryIntervalSeconds,
                    RetryAttempts = this.RetryAttempts != null ? this.RetryAttempts : model.ExecutionOptions.RetryAttempts,
                    RetryIntervalBackoffMultiplier = this.RetryIntervalBackoffMultiplier != null ? this.RetryIntervalBackoffMultiplier : model.ExecutionOptions.RetryIntervalBackoffMultiplier,
                    TimeoutSeconds = this.TimeoutSeconds != null ? this.TimeoutSeconds : model.ExecutionOptions.TimeoutSeconds
                },
                CommandText = this.CommandText != null ? this.CommandText : model.CommandText,
                StepId = this.StepId != null ? this.StepId : model.StepId,
                Output = output != null && output.ServerName != null ? output : null
            };

            return updatedModel;
        }

        /// <summary>
        /// Sends the changes to the service -> Updates the job step
        /// </summary>
        /// <param name="entity">The job step to update</param>
        /// <returns>The updated job step</returns>
        protected override AzureSqlDatabaseAgentJobStepModel PersistChanges(AzureSqlDatabaseAgentJobStepModel entity)
        {
            return ModelAdapter.UpsertJobStep(entity);
        }
    }
}