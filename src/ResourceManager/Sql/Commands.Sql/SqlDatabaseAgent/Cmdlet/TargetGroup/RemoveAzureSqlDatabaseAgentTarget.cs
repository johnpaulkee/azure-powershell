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
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true)]
    public class RemoveAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// Check to see if the target group member already exists in the target group.
        /// </summary>
        /// <returns>Null if the target doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<JobTarget> GetEntity()
        {
            return null;
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<JobTarget> ApplyUserInputToModel(IEnumerable<JobTarget> model)
        {
            return new List<JobTarget> { CreateJobTargetModel() }; ;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<JobTarget> PersistChanges(IEnumerable<JobTarget> entity)
        {
            return new List<JobTarget> { this.RemoveTarget(entity.First()) };
        }

        /// <summary>
        /// Adds a new target to the target group
        /// </summary>
        /// <param name="target">The new target to be added.</param>
        /// <returns>The target that was added.</returns>
        protected JobTarget RemoveTarget(JobTarget target)
        {
            // Step 1 : Get Existing Targets
            IList<JobTarget> existingTargets = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName).Members;

            // Step 2 : Remove Target From Existing Targetss
            List<JobTarget> targets = RemoveTargetFromTargets(existingTargets, target);

            // Step 3: Upsert Target Group with Removed Target From Targets
            AzureSqlDatabaseAgentTargetGroupModel model = new AzureSqlDatabaseAgentTargetGroupModel
            {
                ResourceGroupName = this.ResourceGroupName,
                AgentServerName = this.AgentServerName,
                AgentName = this.AgentName,
                TargetGroupName = this.TargetGroupName,
                Members = targets
            };

            AzureSqlDatabaseAgentTargetGroupModel resp = ModelAdapter.UpsertTargetGroup(model);

            return target;
        }

        /// <summary>
        /// This merges the target group members list with the new target that customer wants added.
        /// Throws PSArgumentException if the target for it's target type already exists.s
        /// </summary>
        /// <param name="existingTargets">The existing target group members</param>
        /// <param name="target">The target we want to add to the group</param>
        /// <returns>A merged list of targets if the target doesn't already exist in the group.</returns>
        public List<JobTarget> RemoveTargetFromTargets(
            IList<JobTarget> existingTargets,
            JobTarget target)
        {
            var targets = existingTargets.ToList();
            int initialCount = targets.Count;

            targets.RemoveAll(
                tg => tg.MembershipType == target.MembershipType &&
                tg.Type == target.Type &&
                tg.ServerName == target.ServerName &&
                tg.DatabaseName == target.DatabaseName &&
                tg.ElasticPoolName == target.ElasticPoolName &&
                tg.ShardMapName == target.ShardMapName &&
                tg.RefreshCredential == target.RefreshCredential);

            // Give appropriate error message if target wasn't removed.
            if (targets.Count >= initialCount)
            {
                switch (ParameterSetName)
                {
                    case JobTargetType.SqlServer:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetServerExists, this.ServerName, this.TargetGroupName),
                            "Target");
                    case JobTargetType.SqlDatabase:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetDatabaseExists, this.DatabaseName, this.ServerName, this.TargetGroupName),
                            "Target");
                    case JobTargetType.SqlElasticPool:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetElasticPoolExists, this.ElasticPoolName, this.ServerName, this.TargetGroupName),
                            "Target");
                    case JobTargetType.SqlShardMap:
                        throw new PSArgumentException(
                            string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetShardMapExists, this.ShardMapName, this.ServerName, this.TargetGroupName),
                            "Target");
                    default:
                        break;
                }
            };


            return targets;
        }
    }
}