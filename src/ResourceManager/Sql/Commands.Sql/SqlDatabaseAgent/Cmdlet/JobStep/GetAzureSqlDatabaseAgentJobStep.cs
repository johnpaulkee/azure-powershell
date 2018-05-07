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

using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.Job
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepModel))]
    [OutputType(typeof(AzureSqlDatabaseAgentJobStepVersionModel))]
    [OutputType(typeof(List<AzureSqlDatabaseAgentJobStepModel>))]
    public class GetAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
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
            ParameterSetName = DefaultGetVersionParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
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
            ParameterSetName = DefaultGetVersionParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultGetVersionParameterSet,
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
            ParameterSetName = DefaultGetVersionParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            Position = 4,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultGetVersionParameterSet,
            Position = 4,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobObjectParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectGetVersionParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobResourceIdParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdGetVersionParameterSet,
            Position = 1,
            HelpMessage = "The job step name")]
        [Alias("StepName")]
        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the job version
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultGetVersionParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 5,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectGetVersionParameterSet,
            ValueFromPipeline = true,
            Position = 2,
            HelpMessage = "The job step name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdGetVersionParameterSet,
            Position = 2,
            HelpMessage = "The job step name")]
        public int? Version { get; set; }

        /// <summary>
        /// Gets or sets the job input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectGetVersionParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobModel JobObject { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdGetVersionParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public override string JobResourceId { get; set; }

        /// <summary>
        /// Cmdlet execution starts here
        /// </summary>
        public sealed override void ExecuteCmdlet()
        {


            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets a job step from the service.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> GetEntity()
        {
            if (this.Version.HasValue)
            {
                return new List<AzureSqlDatabaseAgentJobStepModel>
                {
                    ModelAdapter.GetJobStepByVersion(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Version.Value, this.Name)
                };
            }
            else if (this.Name == null)
            {
                // Returns a list of jobs if name is not provided
                return ModelAdapter.ListJobSteps(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName);
            }
            else
            {
                return new List<AzureSqlDatabaseAgentJobStepModel>
                {
                    ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name)
                };
            }
        }

        /// <summary>
        /// No user input to apply to model.
        /// </summary>
        /// <param name="model">The model to modify</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobStepModel> model)
        {
            return model;
        }

        /// <summary>
        /// No changes, thus nothing to persist.
        /// Note: even though we technically don't need to override this, we want to pass the entity forward so that we can take advantage of
        /// powershell's understanding of a list with one item defaulting to just the item itself.
        /// </summary>
        /// <param name="entity">The entity retrieved</param>
        /// <returns>The unchanged entity</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobStepModel> entity)
        {
            return entity;
        }
    }
}