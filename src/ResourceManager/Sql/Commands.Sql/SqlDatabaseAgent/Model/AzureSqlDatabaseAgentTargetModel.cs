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
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentTargetModel
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
        /// Gets or sets the credential name
        /// </summary>
        public string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the target membership type
        /// </summary>
        public JobTargetGroupMembershipType? MembershipType { get; set; }

        /// <summary>
        /// Gets or sets the target type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the target server
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the target database
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the target pool
        /// </summary>
        public string ElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the target shard map
        /// </summary>
        public string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the target credential
        /// </summary>
        public string RefreshCredential { get; set; }
    }
}
