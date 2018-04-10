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
        /// Parameter set name for default sets
        /// </summary>
        protected const string SqlDatabaseSet = "Sql Database Target Type";
        protected const string SqlServerSet = "Sql Server Target Type";
        protected const string SqlElasticPoolSet = "Sql Elastic Pool Target Type";
        protected const string SqlShardMapSet = "Sql Shard Map Target Type";

        /// <summary>
        /// Parameter sets for input object
        /// </summary>
        protected const string InputObjectSqlDatabaseSet = "Sql Database Input Object Parameter Set";
        protected const string InputObjectSqlServerSet = "Sql Server Input Object Parameter Set";
        protected const string InputObjectSqlElasticPoolSet = "Sql Elastic Pool Input Object Parameter Set";
        protected const string InputObjectSqlShardMapSet = "Sql Shard Map Input Object Parameter Set";

        /// <summary>
        /// Parameter sets for resource id
        /// </summary>
        protected const string ResourceIdSqlDatabaseSet = "Sql Database ResourceId Parameter Set";
        protected const string ResourceIdSqlServerSet = "Sql Server ResourceId Parameter Set";
        protected const string ResourceIdSqlElasticPoolSet = "Sql Elastic Pool ResourceId Parameter Set";
        protected const string ResourceIdSqlShardMapSet = "Sql Shard Map ResourceId Parameter Set";

        /// <summary>
        /// Unique case for removing parameter set for Server or Database
        /// </summary>
        protected const string SqlServerDatabaseShardMapSet = "Sql Server Or Database Target Type";
        protected const string InputObjectSqlServerDatabaseShardMapSet = "Input Object Sql Server Or Database Target Type";
        protected const string ResourceIdSqlServerDatabaseShardMapSet = "Resource Id Sql Server Or Database Target Type";

        /// <summary>
        /// Server Dns Alias object to remove
        /// </summary>
        [Parameter(ParameterSetName = InputObjectSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = InputObjectSqlServerSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = InputObjectSqlElasticPoolSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = InputObjectSqlShardMapSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentTargetGroupModel InputObject { get; set; }

        /// <summary>
		/// Gets or sets the resource id of the SQL Database Agent
		/// </summary>
		[Parameter(ParameterSetName = ResourceIdSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the target group")]
        [Parameter(ParameterSetName = ResourceIdSqlServerSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the target group")]
        [Parameter(ParameterSetName = ResourceIdSqlElasticPoolSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the target group")]
        [Parameter(ParameterSetName = ResourceIdSqlShardMapSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource id of the target group")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        public virtual string AgentServerName { get; set; }
        public virtual string AgentName { get; set; }
        public virtual string TargetGroupName { get; set; }
        public virtual string ServerName { get; set; }
        public virtual string ElasticPoolName { get; set; }
        public virtual string ShardMapName { get; set; }
        public virtual string DatabaseName { get; set; }
        public virtual string RefreshCredentialName { get; set; }

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
            return new JobTarget
            {
                MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ?
                    JobTargetGroupMembershipType.Exclude :
                    JobTargetGroupMembershipType.Include,
                Type = GetTargetType(),
                ServerName = this.ServerName,
                DatabaseName = MyInvocation.BoundParameters.ContainsKey("DatabaseName") ? this.DatabaseName : null,
                ElasticPoolName = MyInvocation.BoundParameters.ContainsKey("ElasticPoolName") ? this.ElasticPoolName : null,
                ShardMapName = MyInvocation.BoundParameters.ContainsKey("ShardMapName") ? this.ShardMapName : null,
                RefreshCredential = MyInvocation.BoundParameters.ContainsKey("RefreshCredentialName") ? GetJobCredentialId(this.RefreshCredentialName) : null,
            };
        }

        /// <summary>
        /// Helper for determining based on parameter set what target type this target should be.
        /// </summary>
        /// <returns>The target type</returns>
        public string GetTargetType()
        {
            if (this.ShardMapName != null)
            {
                return JobTargetType.SqlShardMap;
            }

            if (this.ElasticPoolName != null)
            {
                return JobTargetType.SqlElasticPool;
            }

            if (this.DatabaseName != null)
            {
                return JobTargetType.SqlDatabase;
            }

            return JobTargetType.SqlServer;
        }

        /// <summary>
        /// Returns the job credential id
        /// </summary>
        /// <param name="refreshCredentialName"></param>
        /// <returns>The job credential id</returns>
        protected string GetJobCredentialId(
            string refreshCredentialName)
        {
            if (refreshCredentialName == null)
            {
                return null;
            }

            string credentialId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                AzureSqlDatabaseAgentTargetGroupCommunicator.Subscription.Id,
                this.ResourceGroupName,
                this.AgentServerName,
                this.AgentName,
                refreshCredentialName);

            return credentialId;
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
            int initialCount = existingTargets.Count;

            // Merge Targets and Remove Duplicates Just In Case
            // https://stackoverflow.com/questions/16983618/how-to-remove-duplicates-from-collection-using-iequalitycomparer-linq-distinct
            var mergedTargets = existingTargets
                .Concat(new List<JobTarget> { target })
                .GroupBy(t => new { t.ServerName, t.DatabaseName, t.ElasticPoolName, t.ShardMapName, t.MembershipType, t.Type, t.RefreshCredential })
                .Select(t => t.First())
                .ToList();

            if (initialCount >= mergedTargets.Count)
            {
                return null;
            }

            return mergedTargets;
        }

        /// <summary>
        /// Check to see if the target group member already exists in the target group.
        /// </summary>
        /// <returns>Null if the target doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<JobTarget> GetEntity()
        {
            IList<JobTarget> existingTargets = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName).Members;

            return existingTargets;
        }
    }
}