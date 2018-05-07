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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.Sql.Database.Model;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

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
        /// Gets or sets the resource group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
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
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = InputObjectParameterSet,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = ResourceIdParameterSet,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The job step name")]
        [Alias("StepName")]
        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            Position = 5,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseObject,
            Position = 5,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOutputDatabaseId,
            Position = 5,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = InputObjectParameterSet,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = ResourceIdParameterSet,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The target group name")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The target group name")]
        public override string TargetGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = DefaultOutputDatabaseObject,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = DefaultOutputDatabaseId,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = InputObjectParameterSet,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = ResourceIdParameterSet,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The credential name")]
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The credential name")]
        public string CredentialName { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = DefaultOutputDatabaseObject,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = DefaultOutputDatabaseId,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = InputObjectParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = ResourceIdParameterSet,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The command text")]
        [Parameter(
            Mandatory = true,
            Position = 4,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The command text")]
        public string CommandText { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 8,
           ParameterSetName = DefaultOutputDatabaseObject,
           HelpMessage = "The output database object")]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The output database object")]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The output database object")]
        public AzureSqlDatabaseModel OutputDatabaseObject { get; set; }

        [Parameter(
           Mandatory = true,
           Position = 8,
           ParameterSetName = DefaultOutputDatabaseId,
           HelpMessage = "The output database resource id")]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The output database resource id")]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The output database resource id")]
        public string OutputDatabaseResourceId { get; set; }

        /// <summary>
        /// Gets or sets the output credential name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 9,
            ParameterSetName = DefaultOutputDatabaseObject,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            Position = 9,
            ParameterSetName = DefaultOutputDatabaseId,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The output credential name")]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The output credential name")]
        public string OutputCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the output table name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 10,
            ParameterSetName = DefaultOutputDatabaseObject,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = true,
            Position = 10,
            ParameterSetName = DefaultOutputDatabaseId,
            HelpMessage = "The output table name ")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The output table name")]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The output table name")]
        public string OutputTableName { get; set; }

        /// <summary>
        /// Gets or sets the output schema name
        /// </summary>
        [Parameter(
            Mandatory = false,
            Position = 11,
            ParameterSetName = DefaultOutputDatabaseObject,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            Position = 11,
            ParameterSetName = DefaultOutputDatabaseId,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            Position = 8,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            Position = 8,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            Position = 8,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The output schema name")]
        [Parameter(
            Mandatory = false,
            Position = 8,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The output schema name")]
        public string OutputSchemaName { get; set; }

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

        /// <summary>
        /// Gets or sets the job object
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ParameterSetName = InputObjectParameterSet,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ParameterSetName = InputObjectOutputDatabaseObject,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ParameterSetName = InputObjectOutputDatabaseId,
            HelpMessage = "The job object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobModel JobObject { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdParameterSet,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseObject,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ResourceIdOutputDatabaseId,
            HelpMessage = "The job resource id")]
        public override string JobResourceId { get; set; }

        /// <summary>
        /// Check to see if the job step already exists in this job
        /// </summary>
        /// <returns>Null if the job step doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> GetEntity()
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
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobStepModel> model)
        {
            string targetGroupId = CreateTargetGroupId(this.ResourceGroupName, this.ServerName, this.AgentName, this.TargetGroupName);
            string credentialId = CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.CredentialName);

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
            };

            if (this.OutputDatabaseObject != null)
            {
                updatedModel.Output = new Management.Sql.Models.JobStepOutput
                {
                    // TODO parse database object resource id to get subscription id chosen
                    //SubscriptionId = this.OutputDatabaseObject != null ? Guid.Parse(this.OutputSubscriptionId) : (Guid?) null,
                    ResourceGroupName = this.OutputDatabaseObject != null ? this.OutputDatabaseObject.ResourceGroupName : null,
                    ServerName = this.OutputDatabaseObject != null ? this.OutputDatabaseObject.ServerName : null,
                    DatabaseName = this.OutputDatabaseObject != null ? this.OutputDatabaseObject.DatabaseName : null,
                    Credential = this.OutputCredentialName != null ?
                    CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.OutputCredentialName) :
                    null,
                    SchemaName = this.OutputSchemaName != null ? this.OutputSchemaName : null,
                    TableName = this.OutputTableName != null ? this.OutputTableName : null
                };
            }

            return new List<AzureSqlDatabaseAgentJobStepModel> { updatedModel };
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job step
        /// </summary>
        /// <param name="entity">The job step to create</param>
        /// <returns>The created job step</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobStepModel> entity)
        {
            return new List<AzureSqlDatabaseAgentJobStepModel>
            {
                ModelAdapter.UpsertJobStep(entity.First())
            };
        }
    }
}