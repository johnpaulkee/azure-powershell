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

using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
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
    [OutputType(typeof(AzureSqlDatabaseAgentModel))]
    public class RemoveAzureSqlDatabaseAgent : AzureSqlDatabaseAgentCmdletBase
    {
        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name")]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Server Name")]
        [Alias("ServerName")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to use.
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentName")]
        public string Name { get; set; }

        /// <summary>
        /// Server Dns Alias object to remove
        /// </summary>
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent object to remove")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentModel InputObject { get; set; }

        /// <summary>
		/// Gets or sets the resource id of the SQL Database Agent
		/// </summary>
		[Parameter(ParameterSetName = ResourceIdParameterSet,
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
                !ShouldProcess(string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentDescription, this.Name, this.ServerName),
                               string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentWarning, this.Name, this.ServerName),
                               Properties.Resources.ShouldProcessCaption))
            {
                return;
            }

            switch (ParameterSetName)
            {
                case InputObjectParameterSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.Name = InputObject.AgentName;
                    break;
                case ResourceIdParameterSet:
                    var resourceInfo = new ResourceIdentifier(ResourceId);
                    this.ResourceGroupName = resourceInfo.ResourceGroupName;
                    this.ServerName = ResourceIdentifier.GetTypeFromResourceType(resourceInfo.ParentResource);
                    this.Name = resourceInfo.ResourceName;
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets the entity to delete
        /// </summary>
        /// <returns>The entity going to be deleted</returns>
        protected override AzureSqlDatabaseAgentModel GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("AgentName: {0}", Name);

                return ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.ServerName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The sql database agent does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentNotExists, this.Name, this.ServerName),
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
        protected override AzureSqlDatabaseAgentModel ApplyUserInputToModel(AzureSqlDatabaseAgentModel model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the job account.
        /// </summary>
        /// <param name="entity">The job account being deleted</param>
        /// <returns>The job account that was deleted</returns>
        protected override AzureSqlDatabaseAgentModel PersistChanges(AzureSqlDatabaseAgentModel entity)
        {
            ModelAdapter.RemoveSqlDatabaseAgent(this.ResourceGroupName, this.ServerName, this.Name);
            return entity;
        }
    }
}