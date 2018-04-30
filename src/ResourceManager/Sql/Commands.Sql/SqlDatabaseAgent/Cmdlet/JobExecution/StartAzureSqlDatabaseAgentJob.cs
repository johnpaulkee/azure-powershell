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
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The Agent Control Database Object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the Agent's Control Database Resource Id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The Agent Control Database Resource Id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter to indicate whether customer wants to poll completion of job
        /// or if not set, to return job execution id immediately upon creation.
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet, Mandatory = false)]
        [Parameter(ParameterSetName = InputObjectParameterSet, Mandatory = false)]
        [Parameter(ParameterSetName = ResourceIdParameterSet, Mandatory = false)]
        public SwitchParameter Wait { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

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
                    break;
                case ResourceIdParameterSet:
                    var resourceInfo = new ResourceIdentifier(ResourceId);
                    this.ResourceGroupName = resourceInfo.ResourceGroupName;
                    this.ServerName = ResourceIdentifier.GetTypeFromResourceType(resourceInfo.ParentResource);
                    this.AgentName = resourceInfo.ResourceName;
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
        protected override IEnumerable<AzureSqlDatabaseAgentJobExecutionModel> GetEntity()
        {
            return new List<AzureSqlDatabaseAgentJobExecutionModel> { };
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