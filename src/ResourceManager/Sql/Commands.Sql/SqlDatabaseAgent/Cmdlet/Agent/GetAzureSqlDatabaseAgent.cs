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
    [OutputType(typeof(AzureSqlDatabaseAgentModel))]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentModel>))]
    public class GetAzureSqlDatabaseAgent : AzureSqlDatabaseAgentCmdletBase
    {
        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Name")]
        [Alias("AgentName")]
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
                WriteObject(ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.ServerName), true);
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets one or more Azure SQL Database Agents from the service.
        /// </summary>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentModel GetEntity()
        {
            return ModelAdapter.GetSqlDatabaseAgent(this.ResourceGroupName, this.ServerName, this.Name);
        }
    }
}