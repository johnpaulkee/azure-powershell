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

using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Rest.Azure;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Remove-AzureRmSqlDatabaseAgent cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgent", SupportsShouldProcess = true)]
    [OutputType(typeof(Model.AzureSqlDatabaseAgentModel))]
    public class RemoveAzureSqlDatabaseAgent : AzureSqlDatabaseAgentCmdletBase
    {
        /// <summary>
        /// Parameter sets
        /// </summary>
        protected const string RemoveDefaultParameterSet = "Remove a SQL Database Agent using default parameters";
        protected const string RemoveByInputObjectParameterSet = "Remove a SQL Database Agent from Model.AzureSqlDatabaseAgentModel instance definition";
        protected const string RemoveByResourceIdParameterSet = "Remove a  SQL Database Agent from an Azure resource id";


        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        [Parameter(ParameterSetName = RemoveDefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name")]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        [Parameter(ParameterSetName = RemoveDefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Server Name")]
        [Alias("ServerName")]
        public override string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to use.
        /// </summary>
        [Parameter(ParameterSetName = RemoveDefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        public string AgentName { get; set; }

        /// <summary>
        /// Server Dns Alias object to remove
        /// </summary>
        [Parameter(ParameterSetName = RemoveByInputObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent object to remove")]
        [ValidateNotNullOrEmpty]
        public Model.AzureSqlDatabaseAgentModel InputObject { get; set; }

        /// <summary>
		/// Gets or sets the resource id of the SQL Database Agent
		/// </summary>
		[Parameter(ParameterSetName = RemoveByResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the SQL Database Agent object to remove")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Defines whether it is ok to skip the requesting of rule removal confirmation
        /// </summary>
        [Parameter(HelpMessage = "Skip confirmation message for performing the action")]
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (!Force.IsPresent && 
                !ShouldProcess(string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentDescription, this.AgentName, this.AgentServerName),
                               string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentWarning, this.AgentName, this.AgentServerName),
                               Properties.Resources.ShouldProcessCaption))
            {
                return;
            }

            if (string.Equals(ParameterSetName, RemoveByInputObjectParameterSet, System.StringComparison.OrdinalIgnoreCase))
            {
                this.ResourceGroupName = InputObject.ResourceGroupName;
                this.AgentServerName = InputObject.AgentServerName;
                this.AgentName = InputObject.AgentName;
            }
            else if (string.Equals(this.ParameterSetName, RemoveByResourceIdParameterSet, System.StringComparison.OrdinalIgnoreCase))
            {
                var resourceInfo = new ResourceIdentifier(ResourceId);
                this.ResourceGroupName = resourceInfo.ResourceGroupName;
                this.AgentServerName = ResourceIdentifier.GetTypeFromResourceType(resourceInfo.ParentResource);
                this.AgentName = resourceInfo.ResourceName;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets the entity to delete
        /// </summary>
        /// <returns>The entity going to be deleted</returns>
        protected override Model.AzureSqlDatabaseAgentModel GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("AgentName: {0}", AgentName);

                return ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.AgentServerName, this.AgentName);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The sql database agent does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentNotExists, this.AgentName, this.AgentServerName),
                        "AgentName");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Apply user input.  Here nothing to apply
        /// </summary>
        /// <param name="model">The result of GetEntity</param>
        /// <returns>The input model</returns>
        protected override Model.AzureSqlDatabaseAgentModel ApplyUserInputToModel(Model.AzureSqlDatabaseAgentModel model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the job account.
        /// </summary>
        /// <param name="entity">The job account being deleted</param>
        /// <returns>The job account that was deleted</returns>
        protected override Model.AzureSqlDatabaseAgentModel PersistChanges(Model.AzureSqlDatabaseAgentModel entity)
        {
            ModelAdapter.RemoveSqlDatabaseAgent(this.ResourceGroupName, this.AgentServerName, this.AgentName);
            return entity;
        }
    }
}