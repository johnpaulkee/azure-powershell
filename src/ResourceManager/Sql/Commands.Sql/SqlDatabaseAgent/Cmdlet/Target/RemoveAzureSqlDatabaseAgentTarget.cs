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
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentTarget", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = SqlDatabaseSet), 
        OutputType(typeof(JobTarget))]
    public class RemoveAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        private JobTarget Target;

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectSqlDatabaseSet:
                case InputObjectSqlServerSet:
                case InputObjectSqlElasticPoolSet:
                case InputObjectSqlShardMapSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.AgentServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    this.TargetGroupName = InputObject.TargetGroupName;
                    break;
                case ResourceIdSqlDatabaseSet:
                case ResourceIdSqlServerSet:
                case ResourceIdSqlElasticPoolSet:
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
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<JobTarget> ApplyUserInputToModel(IEnumerable<JobTarget> existingTargets)
        {
            this.Target = CreateJobTargetModel();
            List<JobTarget> updatedTargets = RemoveTargetFromTargets(existingTargets.ToList(), this.Target);

            if (updatedTargets == null)
            {
                return new List<JobTarget>();
            }

            return updatedTargets;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<JobTarget> PersistChanges(IEnumerable<JobTarget> updatedTargets)
        {
            // If the list of updated targets is 0, then we know nothing was updated, so just return null.
            if (updatedTargets.Count() == 0)
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

            return new List<JobTarget> { this.Target };
        }

        /// <summary>
        /// This merges the target group members list with the new target that customer wants added.
        /// Throws PSArgumentException if the target for it's target type already exists.s
        /// </summary>
        /// <param name="existingTargets">The existing target group members</param>
        /// <param name="target">The target we want to add to the group</param>
        /// <returns>A merged list of targets if the target doesn't already exist in the group or null if there </returns>
        public List<JobTarget> RemoveTargetFromTargets(IList<JobTarget> existingTargets, JobTarget target)
        {
            var targets = existingTargets.ToList();

            int numRemoved = targets.RemoveAll(
                    tg => tg.MembershipType == target.MembershipType &&
                    tg.Type == target.Type &&
                    tg.ServerName == target.ServerName &&
                    tg.DatabaseName == target.DatabaseName &&
                    tg.ElasticPoolName == target.ElasticPoolName &&
                    tg.ShardMapName == target.ShardMapName &&
                    tg.RefreshCredential == target.RefreshCredential);

            // Return an empty list if none were removed.
            if (numRemoved == 0)
            {
                return null;
            };

            return targets;
        }
    }
}