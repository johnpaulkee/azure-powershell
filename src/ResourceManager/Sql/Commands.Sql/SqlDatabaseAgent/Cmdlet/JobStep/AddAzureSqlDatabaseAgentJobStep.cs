﻿// ----------------------------------------------------------------------------------
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
    /// Defines the Add-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepModel))]
    public class AddAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
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

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The job step name")]
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

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The target group name")]
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

        /// <summary>
        /// The credential name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The credential name")]
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

        /// <summary>
        /// Gets or sets the command text
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The command text")]
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


        #region Output parameters

        /// <summary>
        /// Gets or sets the output subscription id
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputSubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the output resource group name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the output server name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputServerName { get; set; }

        /// <summary>
        /// Gets or sets the output database name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the output schema name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputSchemaName { get; set; }

        /// <summary>
        /// Gets or sets the output table name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputTableName { get; set; }

        /// <summary>
        /// Gets or sets the output credential name
        /// </summary>
        [Parameter(Mandatory = false)]
        public override string OutputCredentialName { get; set; }

        #endregion

        #region Execution option parameters

        /// <summary>
        /// Gets or sets the timeout seconds
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? TimeoutSeconds { get; set; }

        /// <summary>
        /// Gets or sets the retry attempts
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? RetryAttempts { get; set; }

        /// <summary>
        /// Gets or sets the initial retry interval seconds
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? InitialRetryIntervalSeconds { get; set; }

        /// <summary>
        /// Gets or sets the maximum retry interval seconds
        /// </summary>
        [Parameter(Mandatory = false)]
        public int? MaximumRetryIntervalSeconds { get; set; }

        /// <summary>
        /// Gets or sets the retry interval backoff multiplier
        /// </summary>
        [Parameter(Mandatory = false)]
        public double? RetryIntervalBackoffMultiplier { get; set; }

        #endregion

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
        /// Check to see if the job step already exists in this job
        /// </summary>
        /// <returns>Null if the job step doesn't exist. Otherwise throws exception</returns>
        protected override AzureSqlDatabaseAgentJobStepModel GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("StepName: {0}", Name);
                ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name);
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

            // The job step already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureSqlDatabaseAgentJobStepExists, this.Name, this.JobName),
                "JobStep");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the job step doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentJobStepModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobStepModel model)
        {
            string targetGroupId = CreateTargetGroupId(this.ResourceGroupName, this.ServerName, this.AgentName, this.TargetGroupName);
            string credentialId = CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.CredentialName);
            Management.Sql.Models.JobStepOutput output = CreateOrUpdateJobStepOutputModel();

            AzureSqlDatabaseAgentJobStepModel updatedModel = new AzureSqlDatabaseAgentJobStepModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.JobName,
                StepName = this.Name,
                TargetGroupName = targetGroupId,
                CredentialName = credentialId,
                ExecutionOptions = new Management.Sql.Models.JobStepExecutionOptions
                {
                    InitialRetryIntervalSeconds = this.InitialRetryIntervalSeconds,
                    MaximumRetryIntervalSeconds = this.MaximumRetryIntervalSeconds,
                    RetryAttempts = this.RetryAttempts,
                    RetryIntervalBackoffMultiplier = this.RetryIntervalBackoffMultiplier,
                    TimeoutSeconds = this.TimeoutSeconds
                },
                CommandText = this.CommandText,
                Output = output.ServerName != null ? output : null
            };

            return updatedModel;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job step
        /// </summary>
        /// <param name="entity">The job step to create</param>
        /// <returns>The created job step</returns>
        protected override AzureSqlDatabaseAgentJobStepModel PersistChanges(AzureSqlDatabaseAgentJobStepModel entity)
        {
            return ModelAdapter.UpsertJobStep(entity);
        }
    }
}