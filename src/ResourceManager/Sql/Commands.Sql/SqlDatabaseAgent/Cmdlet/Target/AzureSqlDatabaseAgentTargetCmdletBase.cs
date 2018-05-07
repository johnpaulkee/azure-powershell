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
using Microsoft.Azure.Management.Sql.Models;
using System;
using System.Linq;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentTargetCmdletBase : 
        AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentTargetModel, IEnumerable<AzureSqlDatabaseAgentTargetModel>, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Parameter sets name for default target group db, server, elastic pool, and shard map
        /// </summary>
        protected const string DefaultSqlDatabaseSet = "Sql Database Target Type";
        protected const string DefaultSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Target Type";
        protected const string DefaultSqlShardMapSet = "Sql Shard Map Target Type";

        /// <summary>
        /// Parameter sets for target group object db, server, elastic pool, and shard map
        /// </summary>
        protected const string TargetGroupObjectSqlDatabaseSet = "Sql Database Input Object Parameter Set";
        protected const string TargetGroupObjectSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Input Object Parameter Set";
        protected const string TargetGroupObjectSqlShardMapSet = "Sql Shard Map Input Object Parameter Set";

        /// <summary>
        /// Parameter sets for target group resource id db, server, pool, and shard map
        /// </summary>
        protected const string TargetGroupResourceIdSqlDatabaseSet = "Sql Database TargetGroupResourceId Parameter Set";
        protected const string TargetGroupResourceIdSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool TargetGroupResourceId Parameter Set";
        protected const string TargetGroupResourceIdSqlShardMapSet = "Sql Shard Map TargetGroupResourceId Parameter Set";

        /// <summary>
        /// The target in question
        /// </summary>
        protected AzureSqlDatabaseAgentTargetModel Target;

        /// <summary>
        /// The existing targets
        /// </summary>
        protected List<AzureSqlDatabaseAgentTargetModel> ExistingTargets;

        /// <summary>
        /// Flag to determine whether an update to targets in target group is needed in this powershell session
        /// </summary>
        protected bool NeedsUpdate;

        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        public virtual string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter for whether or not this target will be excluded.
        /// </summary>
        public virtual SwitchParameter Exclude { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Target Group adapter</returns>
        protected override AzureSqlDatabaseAgentAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentAdapter(DefaultContext);
        }

        /// <summary>
        /// Updates the existing list of targets with the new target if it doesn't already exist in the list.
        /// </summary>
        /// <param name="existingTargets">The list of existing targets in the target group</param>
        /// <returns>An updated list of targets.</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentTargetModel> existingTargets)
        {
            this.Target = new AzureSqlDatabaseAgentTargetModel
            {
                TargetGroupName = this.TargetGroupName,
                MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ?
                    JobTargetGroupMembershipType.Exclude :
                    JobTargetGroupMembershipType.Include,
                TargetType = GetTargetType(),
                TargetServerName = MyInvocation.BoundParameters.ContainsKey("ServerName") ? this.ServerName: null,
                TargetDatabaseName = MyInvocation.BoundParameters.ContainsKey("DatabaseName") ? this.DatabaseName : null,
                TargetElasticPoolName = MyInvocation.BoundParameters.ContainsKey("ElasticPoolName") ? this.ElasticPoolName : null,
                TargetShardMapName = MyInvocation.BoundParameters.ContainsKey("ShardMapName") ? this.ShardMapName : null,
                RefreshCredentialName = MyInvocation.BoundParameters.ContainsKey("RefreshCredentialName") ? CreateCredentialId(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.RefreshCredentialName) : null,
            };

            this.ExistingTargets = existingTargets.ToList();
            this.NeedsUpdate = UpdateExistingTargets();

            // If we don't need to send an update, send back an empty list.
            if (!this.NeedsUpdate)
            {
                return new List<AzureSqlDatabaseAgentTargetModel>();
            }

            return this.ExistingTargets;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates or updates the target if necessary
        /// </summary>
        /// <param name="updatedTargets">The list of updated targets</param>
        /// <returns>The target that was created/updated or null if nothing changed.</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentTargetModel> updatedTargets)
        {
            // If we don't need to update the target group member's return null.
            if (!this.NeedsUpdate)
            {
                return null;
            }

            // Update list of targets
            AzureSqlDatabaseAgentTargetGroupModel model = new AzureSqlDatabaseAgentTargetGroupModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.AgentServerName,
                AgentName = this.AgentName,
                TargetGroupName = this.TargetGroupName,
                Targets = updatedTargets.ToList()
            };

            var resp = ModelAdapter.UpsertTargetGroup(model).Targets.ToList();

            return new List<AzureSqlDatabaseAgentTargetModel> { this.Target };
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
        /// Does a scan over the list of targets and finds the target's index in the list
        /// </summary>
        protected int? FindTarget()
        {
            for (int i = 0; i < this.ExistingTargets.Count; i++)
            {
                AzureSqlDatabaseAgentTargetModel t = this.ExistingTargets[i];

                if (t.TargetServerName == this.Target.TargetServerName &&
                    t.TargetDatabaseName == this.Target.TargetDatabaseName &&
                    t.TargetElasticPoolName == this.Target.TargetElasticPoolName &&
                    t.TargetShardMapName == this.Target.TargetShardMapName &&
                    t.TargetType == this.Target.TargetType &&
                    t.RefreshCredentialName == this.Target.RefreshCredentialName)
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Abstract method when adding or removing targets
        /// </summary>
        /// <returns></returns>
        protected abstract bool UpdateExistingTargets();
    }
}