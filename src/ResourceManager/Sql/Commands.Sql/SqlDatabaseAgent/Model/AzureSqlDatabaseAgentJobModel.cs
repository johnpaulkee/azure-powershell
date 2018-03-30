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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    /// <summary>
    /// Represents the core properties of an Azure Sql Database Agent model
    /// </summary>
    public class AzureSqlDatabaseAgentJobModel
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
        /// Gets or sets the job name.
        /// </summary>
        public string JobName { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Type { get; set; }

        public string Interval { get; set; }

        public string Enabled { get; set; }
    }
}