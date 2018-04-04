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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System.Collections.Generic;
using System.Management.Automation;
using System.Linq;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentTargetCmdletBase : AzureSqlCmdletBase<IEnumerable<JobTarget>, AzureSqlDatabaseAgentTargetGroupAdapter>
    {
        /// <summary>
        /// Parameter set name for the SqlDatabase Target Type
        /// </summary>
        private const string SqlDatabaseSet = JobTargetType.SqlDatabase;

        /// <summary>
        /// Parameter set name for the SqlServer Target Type
        /// </summary>
        private const string SqlServerSet = JobTargetType.SqlServer;

        /// <summary>
        /// Parameter set name for the SqlElasticPool Target Type
        /// </summary>
        private const string SqlElasticPoolSet = JobTargetType.SqlElasticPool;

        /// <summary>
        /// Parameter set name for the SqlShardMap Target Type
        /// </summary>
        private const string SqlShardMapSet = JobTargetType.SqlShardMap;

        /// <summary>
        /// Gets or sets the name of the server to use
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Server Name.")]
        [ValidateNotNullOrEmpty]
        public string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent name.")]
        [ValidateNotNullOrEmpty]
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the agent's number of workers
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Target Group Name")]
        public string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the Target Server Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name",
            ParameterSetName = SqlServerSet)]
        [Parameter(ParameterSetName = SqlDatabaseSet)]
        [Parameter(ParameterSetName = SqlElasticPoolSet)]
        [Parameter(ParameterSetName = SqlShardMapSet)]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Target Database Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = SqlDatabaseSet)]
        [Parameter(
            Position = 6,
            ParameterSetName = SqlShardMapSet)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the Target Elastic Pool Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = SqlElasticPoolSet)]
        public string ElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the Shard Map Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = SqlShardMapSet)]
        public string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the Refresh Credential Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlServerSet)]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlElasticPoolSet)]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlShardMapSet)]
        public string RefreshCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether we want to exclude this target.
        /// This really represents membership type.
        /// </summary>
        [Parameter(Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Exclude this target from the target group.")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Exclude { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent adapter</returns>
        protected override AzureSqlDatabaseAgentTargetGroupAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentTargetGroupAdapter(DefaultContext);
        }

        /// <summary>
        /// Helper to create a job target model from user input.
        /// </summary>
        /// <returns>Job target model</returns>
        protected JobTarget CreateJobTargetModel()
        {
            string credentialId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                AzureSqlDatabaseAgentTargetGroupCommunicator.Subscription.Id,
                this.ResourceGroupName,
                this.AgentServerName,
                this.AgentName,
                this.RefreshCredentialName);

            return new JobTarget
            {
                MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ?
                    JobTargetGroupMembershipType.Exclude :
                    JobTargetGroupMembershipType.Include,
                Type = ParameterSetName,
                ServerName = this.ServerName,
                DatabaseName = MyInvocation.BoundParameters.ContainsKey("DatabaseName") ? this.DatabaseName : null,
                ElasticPoolName = MyInvocation.BoundParameters.ContainsKey("ElasticPoolName") ? this.ElasticPoolName : null,
                ShardMapName = MyInvocation.BoundParameters.ContainsKey("ShardMapName") ? this.ShardMapName : null,
                RefreshCredential = MyInvocation.BoundParameters.ContainsKey("RefreshCredentialName") ? credentialId : null,
            };
        }

        /// <summary>
        /// This merges the target group members list with the new target that customer wants added.
        /// Throws PSArgumentException if the target for it's target type already exists.s
        /// </summary>
        /// <param name="existingTargets">The existing target group members</param>
        /// <param name="target">The target we want to add to the group</param>
        /// <returns>A merged list of targets if the target doesn't already exist in the group.</returns>
        protected List<JobTarget> MergeTargets(IList<JobTarget> existingTargets, JobTarget target)
        {
            // Merge Targets and Remove Duplicates Just In Case
            // https://stackoverflow.com/questions/16983618/how-to-remove-duplicates-from-collection-using-iequalitycomparer-linq-distinct
            var mergedTargets = existingTargets
                .Concat(new List<JobTarget> { target })
                .GroupBy(t => new { t.ServerName, t.DatabaseName, t.ElasticPoolName, t.ShardMapName, t.MembershipType, t.Type, t.RefreshCredential })
                .Select(t => t.First())
                .ToList();

            return mergedTargets;
        }

        /// <summary>
        /// Check to see if the target group member already exists in the target group.
        /// </summary>
        /// <returns>Null if the target doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<JobTarget> GetEntity()
        {
            try
            {
                IList<JobTarget> existingTargets = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName).Members;

                return existingTargets;
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The target group does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetGroupNotExists, this.TargetGroupName, this.AgentName),
                        "TargetGroupName");
                }
                throw;
            }
        }
    }
}