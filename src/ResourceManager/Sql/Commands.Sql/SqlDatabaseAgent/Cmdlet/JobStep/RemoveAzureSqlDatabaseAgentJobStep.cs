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
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.Job
{
    /// <summary>
    /// Defines the Remove-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class RemoveAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
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
        [Alias("StepName")]
        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the job step input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job step input object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobStepModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job step resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobStepResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job step resource id")]
        [ValidateNotNullOrEmpty]
        public override string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (ParameterSetName == InputObjectParameterSet)
            {
                InitializeJobStepProperties(this.InputObject);
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets job to see if job step exists before removing
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> GetEntity()
        {
            try
            {
                return new List<AzureSqlDatabaseAgentJobStepModel>
                {
                    ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name)
                };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The job doesn't exists
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobStepNotExists, this.Name, this.JobName),
                        "JobName");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Apply user input.
        /// </summary>
        /// <param name="model">The result of GetEntity</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobStepModel> model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the job step from job.
        /// </summary>
        /// <param name="entity">The job step to be deleted</param>
        /// <returns>The job step that was deleted</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobStepModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobStepModel> entity)
        {
            var existingEntity = entity.First();
            ModelAdapter.RemoveJobStep(existingEntity.ResourceGroupName, existingEntity.ServerName, existingEntity.AgentName, existingEntity.JobName, existingEntity.StepName);
            return entity;
        }
    }
}