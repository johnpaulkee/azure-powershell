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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true)]
    public class NewAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// Gets or sets the agent's number of workers
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Target Group Name")]
        public string TargetGroupName { get; set; }

        /// <summary>
        /// Gets or sets the database target name
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "Database Target Name")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the server target name
        /// </summary>
        [Parameter(Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 5,
            HelpMessage = "Server Target Name")]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the flag indicating whether we want to exclude this target.
        /// </summary>
        [Parameter(Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Exclude this target from the target group.")]
        [ValidateNotNullOrEmpty]
        public SwitchParameter Exclude { get; set; }

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
                this.DatabaseName, 
                this.ServerName);
            
            // This is something we don't want. We shouldn't be able to add a new target to group if it already exists.
            if (target != null)
            {
                throw new PSArgumentException(
                    string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetExists, this.DatabaseName, this.ServerName, this.TargetGroupName),
                    "CredentialName");
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
            List<Management.Sql.Models.JobTarget> newTarget = new List<Management.Sql.Models.JobTarget>
            {
                new Management.Sql.Models.JobTarget
                {
                    Type = Management.Sql.Models.JobTargetType.SqlDatabase,
                    ServerName = this.ServerName,
                    DatabaseName = this.DatabaseName,
                    MembershipType = MyInvocation.BoundParameters.ContainsKey("Exclude") ? 
                        Management.Sql.Models.JobTargetGroupMembershipType.Exclude : 
                        Management.Sql.Models.JobTargetGroupMembershipType.Include
                }
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
    }
}