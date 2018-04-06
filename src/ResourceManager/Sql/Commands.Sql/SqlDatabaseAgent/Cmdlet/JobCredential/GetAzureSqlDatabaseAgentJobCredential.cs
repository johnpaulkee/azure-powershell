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
    [OutputType(typeof(AzureSqlDatabaseAgentJobCredentialModel))]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentJobCredentialModel>))]
    public class GetAzureSqlDatabaseAgentJobCredential : AzureSqlDatabaseAgentJobCredentialCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Credential Name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Writes a list of agents if AgentName is not given, otherwise returns the agent asked for.
        /// </summary>
        public override void ExecuteCmdlet()
        {
            // Lets us return a list of agents
            if (this.Name == null)
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.GetJobCredential(this.ResourceGroupName, this.ServerName, this.Name), true);
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets one or more credentials from the Azure SQL Database Agent
        /// </summary>
        /// <returns>Null if the credential doesn't exist. Otherwise throws exception</returns>
        protected override AzureSqlDatabaseAgentJobCredentialModel GetEntity()
        {
            return ModelAdapter.GetJobCredential(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name);
        }
    }
}