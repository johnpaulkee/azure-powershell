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
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentTargetGroup", SupportsShouldProcess = true), OutputType(typeof(AzureSqlDatabaseAgentTargetGroupModel))]
    public class GetAzureSqlDatabaseAgentTargetGroup : AzureSqlDatabaseAgentTargetGroupCmdletBase
    {
        /// <summary>
        /// Server Dns Alias object to remove
        /// </summary>
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Parent Object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentModel InputObject { get; set; }

        /// <summary>
		/// Gets or sets the resource id of the SQL Database Agent
		/// </summary>
		[Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the credential to remove")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the agent's number of workers
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultParameterSet,
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Job Credential")]
        [Parameter(ParameterSetName = InputObjectParameterSet,
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The SQL Database Agent Parent Object")]
        [Parameter(ParameterSetName = ResourceIdParameterSet,
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The resource id of the credential to remove")]
        [ValidateNotNullOrEmpty]
        [Alias("TargetGroupName")]
        public string Name { get; set; }

        /// <summary>
        /// Writes a list of agents if AgentName is not given, otherwise returns the agent asked for.
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

            // Lets us return a list of agents
            if (this.Name == null)
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.ServerName, this.AgentName), true);
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets one or more credentials from the Azure SQL Database Agent
        /// </summary>
        /// <returns>Null if the credential doesn't exist. Otherwise throws exception</returns>
        protected override AzureSqlDatabaseAgentTargetGroupModel GetEntity()
        {
            return ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name);
        }
    }
}