﻿// ----------------------------------------------------------------------------------
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.Database.Model;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgent", SupportsShouldProcess = true)]
    [OutputType(typeof(AzureSqlDatabaseAgentModel))]
    public class NewAzureSqlDatabaseAgent : AzureSqlDatabaseAgentCmdletBase
    {
        /// <summary>
        /// The Azure SQL Database Input Object
        /// </summary>
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Object ID")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseModel InputObject { get; set; }

        /// <summary>
        /// The Azure SQL Database Resource Id
        /// </summary>
        [Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The SQL Database Resource ID")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the database to use
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "SQL Database Agent Database Name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentDatabaseName")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentName")]
        public string Name { get; set; }

        /// <summary>
        /// The tags to assocciate wit the Azure SQL Database Server
        /// </summary>
        [Parameter(Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            HelpMessage = "The tags to associate with the Azure SQL Database Agent",
            Position = 4)]
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = false,
            Position = 2,
            HelpMessage = "The tags to associate with the Azure SQL Database Agent")]
        [Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = false,
            Position = 2,
            HelpMessage = "The tags to associate with the Azure SQL Database Agent")]
        [ValidateNotNullOrEmpty]
        public Hashtable Tag { get; set; }

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
                    this.DatabaseName = InputObject.DatabaseName;
                    break;
                case ResourceIdParameterSet:
                    var resourceInfo = new ResourceIdentifier(ResourceId);
                    this.ResourceGroupName = resourceInfo.ResourceGroupName;
                    this.ServerName = ResourceIdentifier.GetTypeFromResourceType(resourceInfo.ParentResource);
                    this.DatabaseName = resourceInfo.ResourceName;
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
        protected override AzureSqlDatabaseAgentModel GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("AgentName: {0}", Name);
                ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.ServerName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no agent with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The agent already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureSqlDatabaseAgentExists, this.Name, this.ServerName),
                "AgentName");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentModel ApplyUserInputToModel(AzureSqlDatabaseAgentModel model)
        {
            string location = ModelAdapter.GetServerLocationAndThrowIfAgentNotSupportedByServer(this.ResourceGroupName, this.ServerName);

            AzureSqlDatabaseAgentModel newEntity = new AzureSqlDatabaseAgentModel
            {
                    Location = location,
                    ResourceGroupName = this.ResourceGroupName,
                    ServerName = this.ServerName,
                    AgentName = this.Name,
                    DatabaseName = this.DatabaseName,
                    Tags = TagsConversionHelper.CreateTagDictionary(Tag, validate: true)
            };

            return newEntity;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the agent
        /// </summary>
        /// <param name="entity">The agent to create</param>
        /// <returns>The created agent</returns>
        protected override AzureSqlDatabaseAgentModel PersistChanges(AzureSqlDatabaseAgentModel entity)
        {
            return ModelAdapter.UpsertSqlDatabaseAgent(entity);
        }
    }
}