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
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true)]
    public class AddAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        [Parameter(Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Ignores conflicting targets and merges targets distinctly as necessary to to the target group.")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter IgnoreConflicts { get; set; }

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
            return new List<JobTarget> { this.AddTarget(entity.First()) };
        }

        /// <summary>
        /// Adds a new target to the target group
        /// </summary>
        /// <param name="newTarget">The new target to be added.</param>
        /// <returns>The target that was added.</returns>
        protected JobTarget AddTarget(JobTarget newTarget)
        {
            // Step 1 : Get Existing Targets
            IList<JobTarget> existingTargets = ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName).Members;

            // Step 2 : Merge Existing Targets with New Target
            List<JobTarget> targets = MergeTargets(existingTargets, newTarget);

            // Step 3: Upsert Target Group with Merged Targets
            AzureSqlDatabaseAgentTargetGroupModel model = new AzureSqlDatabaseAgentTargetGroupModel
            {
                ResourceGroupName = this.ResourceGroupName,
                AgentServerName = this.AgentServerName,
                AgentName = this.AgentName,
                TargetGroupName = this.TargetGroupName,
                Members = targets
            };
            AzureSqlDatabaseAgentTargetGroupModel resp = ModelAdapter.UpsertTargetGroup(model);

            // Step 4 : Return Upserted Target
            var upsertedTarget = resp.Members.Where(target =>
                target.DatabaseName == newTarget.DatabaseName &&
                target.ServerName == newTarget.ServerName &&
                target.ElasticPoolName == newTarget.ElasticPoolName &&
                target.ShardMapName == newTarget.ShardMapName &&
                target.MembershipType == newTarget.MembershipType &&
                target.Type == newTarget.Type &&
                target.RefreshCredential == newTarget.RefreshCredential).FirstOrDefault();

            return upsertedTarget;
        }

        /// <summary>
        /// This merges the target group members list with the new target that customer wants added.
        /// Throws PSArgumentException if the target for it's target type already exists.s
        /// </summary>
        /// <param name="existingTargets">The existing target group members</param>
        /// <param name="target">The target we want to add to the group</param>
        /// <returns>A merged list of targets if the target doesn't already exist in the group.</returns>
        public List<JobTarget> MergeTargets(
            IList<JobTarget> existingTargets,
            JobTarget target)
        {
            bool targetExists = existingTargets.Where(
                existingTarget => existingTarget.DatabaseName == target.DatabaseName &&
                          existingTarget.ServerName == target.ServerName &&
                          existingTarget.ElasticPoolName == target.ElasticPoolName &&
                          existingTarget.ShardMapName == target.ShardMapName &&
                          existingTarget.RefreshCredential == target.RefreshCredential).Count() > 0;

            // Give appropriate error message if target already exists
            if (!MyInvocation.BoundParameters.ContainsKey("IgnoreConflicts") && targetExists)
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

            // Merge Targets and Remove Duplicates Just In Case
            // https://stackoverflow.com/questions/16983618/how-to-remove-duplicates-from-collection-using-iequalitycomparer-linq-distinct
            var mergedTargets = existingTargets
                .Concat(new List<JobTarget> { target })
                .GroupBy(t => new { t.ServerName, t.DatabaseName, t.ElasticPoolName, t.ShardMapName, t.MembershipType, t.Type, t.RefreshCredential})
                .Select(t => t.First())
                .ToList();

            return mergedTargets;
        }
    }
}