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
        /// Gets or sets the job step input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job step input object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobStepModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job step resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job step resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

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
        public string Name { get; set; }


        /// <summary>
        /// Entry point for the cmdlet
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
        /// Gets job to see if job step exists before removing
        /// </summary>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentJobStepModel GetEntity()
        {
            try
            {
                return ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name);
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
        protected override AzureSqlDatabaseAgentJobStepModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobStepModel model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the job step from job.
        /// </summary>
        /// <param name="entity">The job step to be deleted</param>
        /// <returns>The job step that was deleted</returns>
        protected override AzureSqlDatabaseAgentJobStepModel PersistChanges(AzureSqlDatabaseAgentJobStepModel entity)
        {
            ModelAdapter.RemoveJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name);
            return entity;
        }
    }
}