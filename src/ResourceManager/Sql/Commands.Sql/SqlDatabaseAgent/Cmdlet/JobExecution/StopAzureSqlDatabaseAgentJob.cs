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
    [OutputType(typeof(List<AzureSqlDatabaseAgentJobExecutionModel>))]
    public class StopAzureSqlDatabaseAgentJobExecution : AzureSqlDatabaseAgentJobExecutionCmdletBase
    {
        /// <summary>
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The Agent Control Database Object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobExecutionModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job execution id to cancel
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The job execution id.")]
        [ValidateNotNullOrEmpty]
        public string JobExecutionId { get; set; }

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
                    this.JobExecutionId = InputObject.JobExecutionId.Value.ToString();
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[9];
                    this.JobName = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets the existing job execution to cancel
        /// </summary>
        /// <returns>The existing job execution</returns>
        protected override List<AzureSqlDatabaseAgentJobExecutionModel> GetEntity()
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
        protected override List<AzureSqlDatabaseAgentJobExecutionModel> ApplyUserInputToModel(List<AzureSqlDatabaseAgentJobExecutionModel> model)
        {
            return model;
        }

        /// <summary>
        /// Sends the changes to the service -> Cancels the job execution belonging to job
        /// </summary>
        /// <param name="entity">The job execution to cancel</param>
        /// <returns>The job execution that was cancelled</returns>
        protected override List<AzureSqlDatabaseAgentJobExecutionModel> PersistChanges(List<AzureSqlDatabaseAgentJobExecutionModel> entity)
        {
            ModelAdapter.CancelJobExecution(entity.First());
            return entity;
        }
    }
}