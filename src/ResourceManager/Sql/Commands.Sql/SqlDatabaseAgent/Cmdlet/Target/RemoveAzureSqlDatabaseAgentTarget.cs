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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Remove-AzureRmSqlDatabaseAgentTarget Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentTarget", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultSqlDatabaseSet), 
        OutputType(typeof(JobTarget))]
    public class RemoveAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase<AzureSqlDatabaseAgentTargetGroupModel>
    {
        /// <summary>
        /// Gets or sets the resource group name.
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "Resource Group Name")]
        [Parameter(Mandatory = true,
            Position = 0,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "Resource Group Name")]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent's server name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [Parameter(Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [Parameter(Mandatory = true,
            Position = 1,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "SQL Database Agent Server Name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            Position = 2,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "SQL Database Agent Name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the target group name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlServerOrElasticPoolSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlDatabaseSet,
            HelpMessage = "SQL Database Agent Name.")]
        [Parameter(Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultSqlShardMapSet,
            HelpMessage = "SQL Database Agent Name.")]
        public override string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the Target Server Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ParameterSetName = DefaultSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ParameterSetName = DefaultSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Server Target Name",
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(ParameterSetName = TargetGroupObjectSqlDatabaseSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlShardMapSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlDatabaseSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlServerOrElasticPoolSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlShardMapSet,
            Mandatory = true,
            Position = 1,
            HelpMessage = "Server Target Name")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the Target Elastic Pool Name
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = DefaultSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = false,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = false,
            HelpMessage = "Elastic Pool Target Name",
            ParameterSetName = TargetGroupResourceIdSqlServerOrElasticPoolSet)]
        public override string ElasticPoolName { get; set; }

        /// <summary>
        /// Gets or sets the Shard Map Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = TargetGroupObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Shard Map Target Name",
            ParameterSetName = TargetGroupResourceIdSqlShardMapSet)]
        public override string ShardMapName { get; set; }

        /// <summary>
        /// Gets or sets the Target Database Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            HelpMessage = "Database Target Name",
            ParameterSetName = DefaultSqlDatabaseSet)]
        [Parameter(
            Mandatory = true,
            Position = 6,
            HelpMessage = "Shard Map Database Target Name",
            ParameterSetName = DefaultSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Database Target Name",
            ParameterSetName = TargetGroupObjectSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "Database Target Name",
            ParameterSetName = TargetGroupResourceIdSqlDatabaseSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            HelpMessage = "Database Target Name",
            ParameterSetName = TargetGroupObjectSqlShardMapSet)]
        [Parameter(Mandatory = true,
            Position = 3,
            HelpMessage = "Database Target Name",
            ParameterSetName = TargetGroupResourceIdSqlShardMapSet)]
        public override string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the Refresh Credential Name
        /// </summary>
        [Parameter(Mandatory = true,
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
            ParameterSetName = TargetGroupResourceIdSqlServerOrElasticPoolSet)]
        [Parameter(Mandatory = true,
            Position = 4,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = TargetGroupResourceIdSqlShardMapSet)]
        public override string RefreshCredentialName { get; set; }

        /// <summary>
        /// Gets or sets the target group input object.
        /// </summary>
        [Parameter(ParameterSetName = TargetGroupObjectSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [Parameter(ParameterSetName = TargetGroupObjectSqlShardMapSet,
            Mandatory = true,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The SQL Database Agent Target Group Object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentTargetGroupModel TargetGroupObject { get; set; }

        /// <summary>
        /// Gets or sets the target group resource id.
        /// </summary>
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlDatabaseSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlServerOrElasticPoolSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [Parameter(ParameterSetName = TargetGroupResourceIdSqlShardMapSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The target group resource id")]
        [ValidateNotNullOrEmpty]
        public string TargetGroupResourceId { get; set; }

        /// <summary>
        /// Execution starts here
        /// </summary>
        public override void ExecuteCmdlet()
        {
            InitializeInputObjectProperties(this.TargetGroupObject);
            InitializeResourceIdProperties(this.TargetGroupResourceId);
            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets the list of existing targets in the target group.
        /// </summary>
        /// <returns>The list of existing targets</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetModel> GetEntity()
        {
            IEnumerable<AzureSqlDatabaseAgentTargetModel> existingTargets = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName).Targets.ToList();
            return existingTargets;
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
                TargetServerName = MyInvocation.BoundParameters.ContainsKey("ServerName") ? this.ServerName : null,
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
        /// Updates list of existing targets during remove target scenario
        /// There is 1 scenario where we will need to send an update to server
        /// 1. If target was in list and we removed it.
        /// If this scenarios occurs, we return true and send update to server.
        /// Otherwise, we return empty response to indicate that no changes were made.
        /// </summary>
        /// <returns>True if an update to server is required after updating list of existing targets</returns>
        protected override bool UpdateExistingTargets()
        {
            int? index = FindTarget();
            bool targetExists = index.HasValue;
            bool needsUpdate = false;

            if (targetExists)
            {
                // Update current target type's membership type to existing membership type
                this.Target.MembershipType = this.ExistingTargets[index.Value].MembershipType;

                // Target existed and we want to remove this target
                this.ExistingTargets.RemoveAt(index.Value);
                needsUpdate = true;
            }

            return needsUpdate;
        }
    }
}