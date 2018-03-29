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
using System.Linq;
using System.Management.Automation;
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true)]
    public class NewAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// Parameter set name for the SqlDatabase Target Type
        /// </summary>
        private const string SqlDatabaseSet = Management.Sql.Models.JobTargetType.SqlDatabase;

        /// <summary>
        /// Parameter set name for the SqlServer Target Type
        /// </summary>
        private const string SqlServerSet = Management.Sql.Models.JobTargetType.SqlServer;

        /// <summary>
        /// Parameter set name for the SqlElasticPool Target Type
        /// </summary>
        private const string SqlElasticPoolSet = Management.Sql.Models.JobTargetType.SqlElasticPool;

        /// <summary>
        /// Parameter set name for the SqlShardMap Target Type
        /// </summary>
        private const string SqlShardMapSet = Management.Sql.Models.JobTargetType.SqlShardMap;

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
            Position = 6,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlServerSet)]
        [Parameter(ParameterSetName = SqlElasticPoolSet)]
        [Parameter(ParameterSetName = SqlShardMapSet)]
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
        /// Gets or sets the flag indicating whether we want to ignore conflict errors if target is already in target group.
        /// </summary>
        [Parameter(Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Ignore conflict errors if target already exists in target group.")]
        public SwitchParameter IgnoreConflict { get; set; }

        /// <summary>
        /// Check to see if the target group member already exists in the target group.
        /// </summary>
        /// <returns>Null if the target doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> GetEntity()
        {
            Management.Sql.Models.JobTarget target = ModelAdapter.GetTarget(
                this.ResourceGroupName, 
                this.AgentServerName, 
                this.AgentName, 
                this.TargetGroupName, 
                CreateJobTargetModel());
            
            // This is something we don't want. We shouldn't be able to add a new target to group if it already exists.
            if (target != null)
            {
                // If IgnoreConflict flag is present, we can just quit since the target already exists.
                if (MyInvocation.BoundParameters.ContainsKey("IgnoreConflict"))
                {
                    Environment.Exit(0);
                }

                switch (ParameterSetName)
                {
                    case Management.Sql.Models.JobTargetType.SqlServer:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetServerExists, this.ServerName, this.TargetGroupName),
                            "Target");
                    case Management.Sql.Models.JobTargetType.SqlDatabase:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetDatabaseExists, this.DatabaseName, this.ServerName, this.TargetGroupName),
                            "Target");
                    case Management.Sql.Models.JobTargetType.SqlElasticPool:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetElasticPoolExists, this.ElasticPoolName, this.ServerName, this.TargetGroupName),
                            "Target");
                    case Management.Sql.Models.JobTargetType.SqlShardMap:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetShardMapExists, this.ShardMapName, this.ServerName, this.TargetGroupName),
                            "Target");
                    default:
                        // TODO: Check if some default exception should be return if this case ever appears.
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> ApplyUserInputToModel(IEnumerable<Management.Sql.Models.JobTarget> model)
        {
            // TODO: Need to a valid resource identifier.


            List<Management.Sql.Models.JobTarget> newTarget = new List<Management.Sql.Models.JobTarget>
            {
                CreateJobTargetModel()
            };

            return newTarget;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> PersistChanges(IEnumerable<Management.Sql.Models.JobTarget> entity)
        {
            return new List<Management.Sql.Models.JobTarget>
            {
                ModelAdapter.UpsertTarget(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName, entity.First())
            };
        }

        /// <summary>
        /// Helper to create a job target model from user input.
        /// </summary>
        /// <returns>Job target model</returns>
        private Management.Sql.Models.JobTarget CreateJobTargetModel()
        {
            string credentialId = string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                Services.AzureSqlDatabaseAgentTargetGroupCommunicator.Subscription.Id,
                this.ResourceGroupName,
                this.AgentServerName,
                this.AgentName,
                this.RefreshCredentialName);

            return new Management.Sql.Models.JobTarget
            {
                MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ?
                    Management.Sql.Models.JobTargetGroupMembershipType.Exclude :
                    Management.Sql.Models.JobTargetGroupMembershipType.Include,
                Type = ParameterSetName,
                ServerName = this.ServerName,
                DatabaseName = MyInvocation.BoundParameters.ContainsKey("DatabaseName") ? this.DatabaseName : null,
                ElasticPoolName = MyInvocation.BoundParameters.ContainsKey("ElasticPoolName") ? this.ElasticPoolName : null,
                ShardMapName = MyInvocation.BoundParameters.ContainsKey("ShardMapName") ? this.ShardMapName : null,
                RefreshCredential = MyInvocation.BoundParameters.ContainsKey("RefreshCredentialName") ? credentialId : null,
            };
        }
    }
}