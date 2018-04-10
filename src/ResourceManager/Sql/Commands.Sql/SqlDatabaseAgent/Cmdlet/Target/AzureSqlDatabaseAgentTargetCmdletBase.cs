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
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentTargetCmdletBase : AzureSqlCmdletBase<IEnumerable<JobTarget>, AzureSqlDatabaseAgentTargetGroupAdapter>
    {
        /// <summary>
        /// Parameter set name for default sets
        /// </summary>
        protected const string SqlDatabaseSet = "Sql Database Target Type";
        protected const string SqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Target Type";
        protected const string SqlShardMapSet = "Sql Shard Map Target Type";

        /// <summary>
        /// Parameter sets for input object
        /// </summary>
        protected const string InputObjectSqlDatabaseSet = "Sql Database Input Object Parameter Set";
        protected const string InputObjectSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Input Object Parameter Set";
        protected const string InputObjectSqlShardMapSet = "Sql Shard Map Input Object Parameter Set";

        /// <summary>
        /// Parameter sets for resource id
        /// </summary>
        protected const string ResourceIdSqlDatabaseSet = "Sql Database ResourceId Parameter Set";
        protected const string ResourceIdSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool ResourceId Parameter Set";
        protected const string ResourceIdSqlShardMapSet = "Sql Shard Map ResourceId Parameter Set";

        /// <summary>
        /// Server Dns Alias object to remove
        /// </summary>
        [Parameter(ParameterSetName = InputObjectSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = InputObjectSqlServerOrElasticPoolSet,
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
        [Parameter(ParameterSetName = ResourceIdSqlServerOrElasticPoolSet,
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

        /// <summary>
        /// Gets or sets the name of the server to use
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            ParameterSetName = SqlServerOrElasticPoolSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            ParameterSetName = SqlDatabaseSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            ParameterSetName = SqlShardMapSet,
            HelpMessage = "Resource Group Name")]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server to use
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            ParameterSetName = SqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            ParameterSetName = SqlDatabaseSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            ParameterSetName = SqlShardMapSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [ValidateNotNullOrEmpty]
        public string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent to create
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            ParameterSetName = SqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            ParameterSetName = SqlDatabaseSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            ParameterSetName = SqlShardMapSet,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the agent's number of workers
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            ParameterSetName = SqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            ParameterSetName = SqlDatabaseSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            ParameterSetName = SqlShardMapSet,
            HelpMessage = "SQL Database Agent Name.")]
        public string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the Target Server Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name",
            ParameterSetName = SqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name",
            ParameterSetName = SqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name",
            ParameterSetName = SqlShardMapSet)]
        [Parameter(ParameterSetName = InputObjectSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = InputObjectSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = InputObjectSqlShardMapSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ResourceIdSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ResourceIdSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ResourceIdSqlShardMapSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Target Elastic Pool Name
        /// </summary>
        [Parameter(Mandatory = false,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = SqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = InputObjectSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = false,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = ResourceIdSqlServerOrElasticPoolSet)]
        public string ElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the Shard Map Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = SqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = InputObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = ResourceIdSqlShardMapSet)]
        public string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the Target Database Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = SqlDatabaseSet)]
        [Parameter(
            Mandatory = true,
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Shard Map Database Target Name",
            ParameterSetName = SqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = InputObjectSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = ResourceIdSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = InputObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Database Target Name",
            ParameterSetName = ResourceIdSqlShardMapSet)]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the Refresh Credential Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlServerOrElasticPoolSet)]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = InputObjectSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = InputObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = ResourceIdSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = ResourceIdSqlShardMapSet)]
        public string RefreshCredentialName { get; set; }

        public virtual SwitchParameter Exclude { get; set; }

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
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectSqlDatabaseSet:
                case InputObjectSqlServerOrElasticPoolSet:
                case InputObjectSqlShardMapSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.AgentServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    this.TargetGroupName = InputObject.TargetGroupName;
                    break;
                case ResourceIdSqlDatabaseSet:
                case ResourceIdSqlServerOrElasticPoolSet:
                case ResourceIdSqlShardMapSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.AgentServerName = tokens[7];
                    this.AgentName = tokens[9];
                    this.TargetGroupName = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
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
            bool updatedMembershipType = false;
            bool targetExists = false;
            foreach (JobTarget t in existingTargets)
            {
                if (t.ServerName == target.ServerName &&  
                    t.DatabaseName == target.DatabaseName &&
                    t.ElasticPoolName == target.ElasticPoolName &&
                    t.ShardMapName == target.ShardMapName &&
                    t.Type == target.Type &&
                    t.RefreshCredential == target.RefreshCredential)
                {
                    targetExists = true;
                    if (t.MembershipType != target.MembershipType)
                    {
                        updatedMembershipType = true;
                        t.MembershipType = target.MembershipType;
                    }
                }
            }

            // If target didn't exist, add this new target
            if (!targetExists)
            {
                existingTargets.Add(target);
                return existingTargets.ToList();
            }

            // If target already existed but it's membership type was updated, update existing target
            if (updatedMembershipType)
            {
                return existingTargets.ToList();
            }

            // If target to be added already exists and wasn't updated, return null to indicate no changes were made.
            return null;
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