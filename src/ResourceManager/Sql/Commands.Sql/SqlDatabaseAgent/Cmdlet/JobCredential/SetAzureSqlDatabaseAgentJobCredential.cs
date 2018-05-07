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
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlDatabaseAgentJobCredential Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseAgentJobCredential", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobCredentialModel))]
    public class SetAzureSqlDatabaseAgentJobCredential : AzureSqlDatabaseAgentJobCredentialCmdletBase
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
        /// Gets or sets the server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets job credential name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet, 
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job credential name")]
        [Alias("CredentialName")]
        [ValidateNotNullOrEmpty]
        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the credential
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The credential")]
        [Parameter(
            ParameterSetName = JobCredentialObjectParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The credential")]
        [Parameter(
            ParameterSetName = JobCredentialResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The credential")]
        [ValidateNotNullOrEmpty]
        public PSCredential Credential { get; set; }

        /// <summary>
        /// Gets or sets the job credential input object model to update
        /// </summary>
        [Parameter(
            ParameterSetName = JobCredentialObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job credential object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobCredentialModel InputObject { get; set; }

        /// <summary>
		/// Gets or sets the job credential resource id
		/// </summary>
		[Parameter(
            ParameterSetName = JobCredentialResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job credential resource id")]
        [ValidateNotNullOrEmpty]
        public override string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (ParameterSetName == InputObjectParameterSet)
            {
                InitializeJobCredentialProperties(this.InputObject);
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Check to see if the credential already exists for the agent.
        /// </summary>
        /// <returns>Null if the credential doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> GetEntity()
        {
            try
            {
                return new List<AzureSqlDatabaseAgentJobCredentialModel>
                {
                    ModelAdapter.GetJobCredential(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name)
                };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The credential does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobCredentialNotExists, this.Name, this.AgentName),
                        "CredentialName");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">The existing job credential entity</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> model)
        {
            AzureSqlDatabaseAgentJobCredentialModel newCredential = new AzureSqlDatabaseAgentJobCredentialModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                CredentialName = this.Name,
                UserName = this.Credential.UserName,
                Password = this.Credential.Password
            };

            return new List<AzureSqlDatabaseAgentJobCredentialModel> { newCredential };
        }

        /// <summary>
        /// Sends the changes to the service -> Updates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> entity)
        {
            return new List<AzureSqlDatabaseAgentJobCredentialModel>
            {
                ModelAdapter.UpsertJobCredential(entity.First())
            };
        }
    }
}