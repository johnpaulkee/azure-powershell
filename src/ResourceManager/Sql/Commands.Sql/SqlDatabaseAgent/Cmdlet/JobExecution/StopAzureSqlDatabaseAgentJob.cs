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
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobExecution;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Stop-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsLifecycle.Stop, "AzureRmSqlDatabaseAgentJob",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel>))]
    public class StopAzureSqlDatabaseAgentJobExecution : AzureSqlDatabaseAgentJobExecutionCmdletBase
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
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job execution id to cancel
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The job execution id.")]
        [ValidateNotNullOrEmpty]
        public override string JobExecutionId { get; set; }

        /// <summary>
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobExecutionObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The Agent Control Database Object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobExecutionModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobExecutionResourceIdParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job execution resource id")]
        [ValidateNotNullOrEmpty]
        public override string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (ParameterSetName == InputObjectParameterSet)
            {
                InitializeJobExecutionProperties(this.InputObject);
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets the existing job execution to cancel
        /// </summary>
        /// <returns>The existing job execution</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> GetEntity()
        {
            try
            {
                var resp = ModelAdapter.GetJobExecution(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, Guid.Parse(this.JobExecutionId));
                return new List<AzureSqlDatabaseAgentJobExecutionModel> { resp };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The job execution does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobExecutionNotExists, this.JobExecutionId, this.JobName),
                        "JobExecution");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">The job execution</param>
        /// <returns>The job execution</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> model)
        {
            return model;
        }

        /// <summary>
        /// Sends the changes to the service -> Cancels the job execution belonging to job
        /// </summary>
        /// <param name="entity">The job execution to cancel</param>
        /// <returns>The job execution that was cancelled</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> entity)
        {
            ModelAdapter.CancelJobExecution(entity.First());
            return entity;
        }
    }
}