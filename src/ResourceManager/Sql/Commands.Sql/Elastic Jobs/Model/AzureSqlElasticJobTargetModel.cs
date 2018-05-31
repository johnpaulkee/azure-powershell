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

using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Management.Sql.Models;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Sql.ElasticJobs.Model
{
    /// <summary>
    /// Represents the core properties of a target group
    /// </summary>
    public class AzureSqlElasticJobTargetModel : AzureSqlElasticJobsBaseModel
    {
        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        public string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the membership type
        /// </summary>
        public JobTargetGroupMembershipType? MembershipType { get; set; }

        /// <summary>
        /// Gets or sets the target type
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// Gets or sets the target server name
        /// </summary>
        public string TargetServerName { get; set; }

        /// <summary>
        /// Gets or sets the target database name
        /// </summary>
        public string TargetDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the target elastic pool name
        /// </summary>
        public string TargetElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the target shard map name
        /// </summary>
        public string TargetShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the refresh credential name
        /// </summary>
        public string RefreshCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the refresh credential id
        /// </summary>
        public string RefreshCredentialId { get; set; }

        public AzureSqlElasticJobTargetModel() { }

        /// <summary>
        /// Creates the AzureSqlElasticJobTargetModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="serverName">The server name</param>
        /// <param name="agentName">The agent name</param>
        /// <param name="targetGroupName">The target group name</param>
        /// <param name="target">The Job Target</param>
        public AzureSqlElasticJobTargetModel(
            string resourceGroupName,
            string serverName,
            string agentName,
            string targetGroupName,
            JobTarget target)
        {
            this.ResourceGroupName = resourceGroupName;
            this.ServerName = serverName;
            this.AgentName = agentName;
            this.TargetGroupName = TargetGroupName;
            this.TargetServerName = target.ServerName;
            this.TargetDatabaseName = target.DatabaseName;
            this.TargetElasticPoolName = target.ElasticPoolName;
            this.TargetShardMapName = target.ShardMapName;
            this.TargetType = target.Type;
            this.MembershipType = target.MembershipType;
            this.RefreshCredentialName = new ResourceIdentifier(target.RefreshCredential).ResourceName;
            this.RefreshCredentialId = target.RefreshCredential;
        }
    }
}