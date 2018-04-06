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
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true), OutputType(typeof(JobTarget))]
    public class AddAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// Gets or sets the Refresh Credential Name
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlServerSet)]
        [Parameter(
            Mandatory = true,
            Position = 5,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlElasticPoolSet)]
        [Parameter(
            Mandatory = true,
            Position = 7,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Refresh Credential Name",
            ParameterSetName = SqlShardMapSet)]
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

        private JobTarget Target;

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">The existing target group members</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<JobTarget> ApplyUserInputToModel(IEnumerable<JobTarget> existingTargets)
        {
            string credentialId = GetJobCredentialId(this.RefreshCredentialName);
            this.Target = CreateJobTargetModel(credentialId);

            List<JobTarget> updatedTargets = MergeTargets(existingTargets.ToList(), this.Target);

            // If there were no updates, just return null to indicate that no changes were made.
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
            // If the list of targets weren't updated at this point, just return null;
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
    }
}