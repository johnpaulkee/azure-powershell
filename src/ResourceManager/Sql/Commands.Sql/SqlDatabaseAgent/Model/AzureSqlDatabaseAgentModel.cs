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

using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    /// <summary>
    /// Represents the core properties of an Azure Sql Database Agent model
    /// </summary>
    public class AzureSqlDatabaseAgentModel
    {
        /// <summary>
        /// Gets or sets the name of the resource group the Sql Database Agent is in
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the azure sql database agent name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the location the sql database agent is in
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the database name for the sql database agent name.
        /// </summary>
        public string AgentDatabaseName { get; set; }

        /// <summary>
        /// The SQL Database Agent Resource Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the agent's number of workers.
        /// </summary>
        public int? WorkerCount { get; set; }

        /// <summary>
        /// Gets or sets the tags associated with the server.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; }
    }
}