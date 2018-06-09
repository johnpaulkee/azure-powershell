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
using Microsoft.Azure.Commands.Sql.ElasticJobs.Services;
using Microsoft.Azure.Commands.Sql.ElasticJobs.Model;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System.Linq;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

namespace Microsoft.Azure.Commands.Sql.ElasticJobs.Cmdlet
{
    /// <summary>
    /// The elastic job target cmdlet base
    /// </summary>
    /// <typeparam name="TInputObject">The input object model</typeparam>
    public abstract class AzureSqlElasticJobTargetCmdletBase<TInputObject> : AzureSqlElasticJobsCmdletBase<TInputObject, IEnumerable<AzureSqlElasticJobTargetModel>, AzureSqlElasticJobAdapter>
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
        protected const string ParentResourceIdSqlDatabaseSet = "Sql Database ParentResourceId Parameter Set";
        protected const string ParentResourceIdSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool ParentResourceId Parameter Set";
        protected const string ParentResourceIdSqlShardMapSet = "Sql Shard Map ParentResourceId Parameter Set";

        /// <summary>
        /// The target in question
        /// </summary>
        protected AzureSqlElasticJobTargetModel Target;

        /// <summary>
        /// The existing targets
        /// </summary>
        protected List<AzureSqlElasticJobTargetModel> ExistingTargets;

        /// <summary>
        /// Flag to determine whether an update to targets in target group is needed in this powershell session
        /// </summary>
        protected bool NeedsUpdate;

        /// <summary>
        /// Gets or sets the switch parameter for whether or not this target will be excluded.
        /// </summary>
        public virtual SwitchParameter Exclude { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure Elastic Job Adapter</returns>
        protected override AzureSqlElasticJobAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlElasticJobAdapter(DefaultContext);
        }

        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(
            Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "Resource Group Name")]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent's server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "The server name.")]
        [Parameter(
            Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "The server name.")]
        [Parameter(Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "The server name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "The agent name.")]
        [Parameter(Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "The agent name.")]
        [Parameter(Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "The agent name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "The target group name.")]
        [Parameter(Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "The target group name.")]
        [Parameter(Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "The target group name.")]
        [ValidateNotNullOrEmpty]
        public override string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the Target Server Name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ParameterSetName = DefaultSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(ParameterSetName = TargetGroupObjectSqlDatabaseSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlShardMapSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ParentResourceIdSqlDatabaseSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ParentResourceIdSqlServerOrElasticPoolSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = ParentResourceIdSqlShardMapSet,
            Mandatory = true,
            Position = 1,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Server Target Name")]
        [ValidateNotNullOrEmpty]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Shard Map Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            HelpMessage = "Shard Map Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Shard Map Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = TargetGroupObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Shard Map Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParentResourceIdSqlShardMapSet)]
        public override string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the Target Database Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            HelpMessage = "Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlDatabaseSet)]
        [Parameter(
            Mandatory = true,
            Position = 6,
            HelpMessage = "Shard Map Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = TargetGroupObjectSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParentResourceIdSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            HelpMessage = "Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = TargetGroupObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            HelpMessage = "Database Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = ParentResourceIdSqlShardMapSet)]
        public override string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the Refresh Credential Name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 5,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = DefaultSqlServerOrElasticPoolSet)]
        [Parameter(
            Mandatory = true,
            Position = 7,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = TargetGroupObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = ParentResourceIdSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = ParentResourceIdSqlShardMapSet)]
        public override string RefreshCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the target group input object.
        /// </summary>
        [Parameter(ParameterSetName = TargetGroupObjectSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The target group object")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The target group object")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlShardMapSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The target group object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlElasticJobTargetGroupModel ParentObject { get; set; }

        /// <summary>
        /// Gets or sets the target group resource id.
        /// </summary>
        [Parameter(ParameterSetName = ParentResourceIdSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [Parameter(ParameterSetName = ParentResourceIdSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [Parameter(ParameterSetName = ParentResourceIdSqlShardMapSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [ValidateNotNullOrEmpty]
        public string ParentResourceId { get; set; }

        /// <summary>
        /// Gets or sets the Target Elastic Pool Name
        /// </summary>
        [Parameter(
            HelpMessage = "Elastic Pool Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet)]
        [Parameter(
            HelpMessage = "Elastic Pool Target Name",
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet)]
        [Parameter(
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = ParentResourceIdSqlServerOrElasticPoolSet)]
        public override string ElasticPoolName { get; set; }

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
                AzureSqlElasticJobTargetModel t = this.ExistingTargets[i];

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

        /// <summary>
        /// Gets the list of existing targets in the target group.
        /// </summary>
        /// <returns>The list of existing targets</returns>
        protected override IEnumerable<AzureSqlElasticJobTargetModel> GetEntity()
        {
            AzureSqlElasticJobTargetGroupModel targetGroup = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName);
            List<AzureSqlElasticJobTargetModel> existingTargets = targetGroup.Targets.ToList();
            return existingTargets;
        }

        /// <summary>
        /// Updates the existing list of targets with the new target if it doesn't already exist in the list.
        /// </summary>
        /// <param name="existingTargets">The list of existing targets in the target group</param>
        /// <returns>An updated list of targets.</returns>
        protected override IEnumerable<AzureSqlElasticJobTargetModel> ApplyUserInputToModel(IEnumerable<AzureSqlElasticJobTargetModel> existingTargets)
        {
            // Reformat target credential id
            foreach (AzureSqlElasticJobTargetModel target in existingTargets)
            {
                target.RefreshCredentialName = CreateCredentialId(target.ResourceGroupName, target.ServerName, target.AgentName, target.RefreshCredentialName);
            }

            this.Target = new AzureSqlElasticJobTargetModel
            {
                TargetGroupName = this.TargetGroupName,
                MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ?
                    JobTargetGroupMembershipType.Exclude :
                    JobTargetGroupMembershipType.Include,
                TargetType = GetTargetType(),
                TargetServerName = this.ServerName,
                TargetDatabaseName = this.DatabaseName,
                TargetElasticPoolName = this.ElasticPoolName,
                TargetShardMapName = this.ShardMapName,
                RefreshCredentialName = this.RefreshCredentialName != null ?
                    CreateCredentialId(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.RefreshCredentialName) : null,
            };

            this.ExistingTargets = existingTargets.ToList();
            this.NeedsUpdate = UpdateExistingTargets();

            // If we don't need to send an update, send back an empty list.
            if (!this.NeedsUpdate)
            {
                return new List<AzureSqlElasticJobTargetModel>();
            }

            return this.ExistingTargets;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates or updates the target if necessary
        /// </summary>
        /// <param name="updatedTargets">The list of updated targets</param>
        /// <returns>The target that was created/updated or null if nothing changed.</returns>
        protected override IEnumerable<AzureSqlElasticJobTargetModel> PersistChanges(IEnumerable<AzureSqlElasticJobTargetModel> updatedTargets)
        {
            // If we don't need to update the target group member's return null.
            if (!this.NeedsUpdate)
            {
                return null;
            }

            // Update list of targets
            AzureSqlElasticJobTargetGroupModel model = new AzureSqlElasticJobTargetGroupModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.AgentServerName,
                AgentName = this.AgentName,
                TargetGroupName = this.TargetGroupName,
                Targets = updatedTargets.ToList()
            };

            var resp = ModelAdapter.UpsertTargetGroup(model).Targets.ToList();

            // Reset target refresh credential back
            this.Target.RefreshCredentialName = this.RefreshCredentialName;

            return new List<AzureSqlElasticJobTargetModel> { this.Target };
        }
    }
}