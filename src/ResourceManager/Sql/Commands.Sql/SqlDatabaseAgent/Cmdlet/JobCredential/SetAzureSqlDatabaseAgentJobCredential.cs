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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseAgentJobCredential", SupportsShouldProcess = true)]
    public class SetAzureSqlDatabaseAgentJobCredential : AzureSqlDatabaseAgentJobCredentialCmdletBase
    {
        /// <summary>
        /// Gets or sets the agent's number of workers
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Job Credential")]
        public string CredentialName { get; set; }

        /// <summary>
        /// Gets or sets the job credential user name
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Job Credential with Username and Password")]
        public PSCredential Credential { get; set; }

        /// <summary>
        /// Overriding to add warning message
        /// </summary>
        public override void ExecuteCmdlet()
        {
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
                return new List<AzureSqlDatabaseAgentJobCredentialModel>() {
                    ModelAdapter.GetJobCredential(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.CredentialName)
                };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The credential does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobCredentialNotExists, this.CredentialName, this.AgentName),
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
            List<AzureSqlDatabaseAgentJobCredentialModel> newCredential = new List<AzureSqlDatabaseAgentJobCredentialModel>
            {
                new AzureSqlDatabaseAgentJobCredentialModel
                {
                    ResourceGroupName = this.ResourceGroupName,
                    AgentServerName = this.AgentServerName,
                    AgentName = this.AgentName,
                    CredentialName = this.CredentialName,
                    UserName = this.Credential.UserName,
                    Password = this.Credential.Password
                }
            };

            return newCredential;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
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