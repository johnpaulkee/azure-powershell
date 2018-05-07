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
    /// Defines the Start-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "AzureRmSqlDatabaseAgentJob",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel>))]
    public class StartAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobExecutionCmdletBase
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
        /// Gets or sets the job model input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobModel JobObject { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public override string JobResourceId { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter to indicate whether customer wants to poll completion of job
        /// or if not set, to return job execution id immediately upon creation.
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet, Mandatory = false)]
        [Parameter(ParameterSetName = JobObjectParameterSet, Mandatory = false)]
        [Parameter(ParameterSetName = JobResourceIdParameterSet, Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> GetEntity()
        {
            try
            {
                ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName);
                return new List<AzureSqlDatabaseAgentJobExecutionModel> { };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The job does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobNotExists, this.JobName, this.AgentName),
                        "JobName");
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
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> model)
        {
            AzureSqlDatabaseAgentJobExecutionModel updatedModel = new AzureSqlDatabaseAgentJobExecutionModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.JobName
            };

            return new List<AzureSqlDatabaseAgentJobExecutionModel> { updatedModel };
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the agent
        /// </summary>
        /// <param name="entity">The agent to create</param>
        /// <returns>The created agent</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> entity)
        {
            AzureSqlDatabaseAgentJobExecutionModel resp;

            if (this.Wait.IsPresent)
            {
                resp = ModelAdapter.CreateJobExecution(entity.First());
            }
            else
            {
                resp = ModelAdapter.BeginCreateJobExecution(entity.First());
            }

            return new List<AzureSqlDatabaseAgentJobExecutionModel> { resp }; ;
        }
    }
}