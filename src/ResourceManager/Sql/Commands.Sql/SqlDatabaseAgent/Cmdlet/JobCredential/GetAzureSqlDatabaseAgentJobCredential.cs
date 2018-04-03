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

using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJobCredential", SupportsShouldProcess = true)]
    public class GetAzureSqlDatabaseAgentJobCredential : AzureSqlDatabaseAgentJobCredentialCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Credential Name.")]
        [ValidateNotNullOrEmpty]
        public string CredentialName { get; set; }

        /// <summary>
        /// Gets one or more credentials from the Azure SQL Database Agent
        /// </summary>
        /// <returns>Null if the credential doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobCredentialModel> GetEntity()
        {
            if (this.MyInvocation.BoundParameters.ContainsKey("CredentialName"))
            {
                return new List<AzureSqlDatabaseAgentJobCredentialModel>
                {
                    ModelAdapter.GetJobCredential(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.CredentialName)
                };
            }
            else
            {
                return ModelAdapter.GetJobCredential(this.ResourceGroupName, this.AgentServerName, this.AgentName);
            }
        }
    }
}