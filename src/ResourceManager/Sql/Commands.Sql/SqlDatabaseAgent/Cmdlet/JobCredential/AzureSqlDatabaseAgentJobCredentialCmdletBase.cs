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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentJobCredentialCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlDatabaseAgentJobCredentialModel>, AzureSqlDatabaseAgentJobCredentialAdapter>
    {
        /// <summary>
        /// Gets or sets the name of the SQL Database Agent Server to use
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Server Name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentServerName")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        public string AgentName { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent adapter</returns>
        protected override AzureSqlDatabaseAgentJobCredentialAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobCredentialAdapter(DefaultContext);
        }
    }
}