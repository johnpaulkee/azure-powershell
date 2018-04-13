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
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Add-AzureRmSqlDatabaseAgentTarget Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentTarget", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = SqlDatabaseSet), 
        OutputType(typeof(JobTarget))]
    public class AddAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// The target to add
        /// </summary>
        private JobTarget Target;

        /// <summary>
        /// Flag to check if this target already existed within the group.
        /// </summary>
        private bool TargetExists;

        /// <summary>
        /// Flag to check if this target's membership is being updated.
        /// </summary>
        private bool UpdatedMembership;

        /// <summary>
        /// Gets or sets the flag indicating that we want to exclude this target
        /// </summary>
        [Parameter(Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Excludes a target.")]
        [ValidateNotNullOrEmpty]
        public override SwitchParameter Exclude { get; set; }

        /// <summary>
        /// Updates the existing list of targets with the new target if it doesn't already exist in the list.
        /// </summary>
        /// <param name="existingTargets">The list of existing targets in the target group</param>
        /// <returns>An updated list of targets.</returns>
        protected override IEnumerable<JobTarget> ApplyUserInputToModel(IEnumerable<JobTarget> existingTargets)
        {
            this.Target = CreateJobTargetModel();

            List<JobTarget> updatedTargets = MergeTargets(existingTargets.ToList(), this.Target);

            return updatedTargets;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates or updates the target if necessary
        /// </summary>
        /// <param name="updatedTargets">The list of updated targets</param>
        /// <returns>The target that was created/updated or null if nothing changed.</returns>
        protected override IEnumerable<JobTarget> PersistChanges(IEnumerable<JobTarget> updatedTargets)
        {
            // If this target already existed and it's membership wasn't updated, return null.
            if (this.TargetExists && !this.UpdatedMembership)
            {
                return null;
            }

            AzureSqlDatabaseAgentTargetGroupModel model = new AzureSqlDatabaseAgentTargetGroupModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.AgentServerName,
                AgentName = this.AgentName,
                TargetGroupName = this.TargetGroupName,
                Members = updatedTargets.ToList()
            };

            IList<JobTarget> resp = ModelAdapter.UpsertTargetGroup(model).Members;

            var upsertedTarget = resp.Where(target =>
                target.DatabaseName == this.Target.DatabaseName &&
                target.ServerName == this.Target.ServerName &&
                target.ElasticPoolName == this.Target.ElasticPoolName &&
                target.ShardMapName == this.Target.ShardMapName &&
                target.MembershipType == this.Target.MembershipType &&
                target.Type == this.Target.Type &&
                target.RefreshCredential == this.Target.RefreshCredential).FirstOrDefault();

            return new List<JobTarget> { upsertedTarget };
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
            this.UpdatedMembership = false;
            this.TargetExists = false;

            foreach (JobTarget t in existingTargets)
            {
                if (t.ServerName == target.ServerName &&
                    t.DatabaseName == target.DatabaseName &&
                    t.ElasticPoolName == target.ElasticPoolName &&
                    t.ShardMapName == target.ShardMapName &&
                    t.Type == target.Type &&
                    t.RefreshCredential == target.RefreshCredential)
                {
                    this.TargetExists = true;
                    if (t.MembershipType != target.MembershipType)
                    {
                        this.UpdatedMembership = true;
                        t.MembershipType = target.MembershipType;
                    }
                }
            }

            // If target didn't exist, add this new target
            if (!this.TargetExists)
            {
                existingTargets.Add(target);
                return existingTargets.ToList();
            }

            return existingTargets.ToList();
        }
    }
}