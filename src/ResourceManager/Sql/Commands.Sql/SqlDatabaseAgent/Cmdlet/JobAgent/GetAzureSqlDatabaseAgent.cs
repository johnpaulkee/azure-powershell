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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.Server.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgent cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgent",
        SupportsShouldProcess = true, 
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentModel>))]
    public class GetAzureSqlDatabaseAgent : AzureSqlDatabaseAgentCmdletBase<AzureSqlServerModel>
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
        [ValidateNotNullOrEmpty]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Alias("AgentName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Agent Server Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The server input object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlServerModel ServerObject { get; set; }

        /// <summary>
        /// Gets or sets the Agent Server Resource Id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The server resource id")]
        public string ServerResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            InitializeInputObjectProperties(this.ServerObject);
            InitializeResourceIdProperties(this.ServerResourceId);
            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets one or more Azure SQL Database Agents from the service.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureSqlDatabaseAgentModel> GetEntity()
        {
            ICollection<AzureSqlDatabaseAgentModel> results = null;

            // Lets us return a list of agents
            if (this.Name == null)
            {
                results = ModelAdapter.ListAgents(this.ResourceGroupName, this.ServerName);
            }
            else
            {
                results = new List<AzureSqlDatabaseAgentModel>();
                results.Add(ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.ServerName, this.Name));
            }

            return results;
        }

        /// <summary>
        /// No user input to apply to model.
        /// </summary>
        /// <param name="model">The model to modify</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentModel> model)
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
        protected override IEnumerable<AzureSqlDatabaseAgentModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentModel> entity)
        {
            return entity;
        }
    }
}