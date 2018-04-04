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
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgentTargetGroup", SupportsShouldProcess = true), OutputType(typeof(AzureSqlDatabaseAgentTargetGroupModel))]
    public class NewAzureSqlDatabaseAgentTargetGroup : AzureSqlDatabaseAgentTargetGroupCmdletBase
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
        /// Check to see if the credential already exists for the agent.
        /// </summary>
        /// <returns>Null if the credential doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetGroupModel> GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("TargetGroupName: {0}", TargetGroupName);
                ModelAdapter.GetTargetGroup(this.ResourceGroupName, this.AgentServerName, this.AgentName, this.TargetGroupName);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no credential with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The credential already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureSqlDatabaseAgentTargetGroupExists, this.TargetGroupName, this.AgentName),
                "TargetGroupName");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetGroupModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentTargetGroupModel> model)
        {
            List<AzureSqlDatabaseAgentTargetGroupModel> targetGroup = new List<AzureSqlDatabaseAgentTargetGroupModel>
            {
                new AzureSqlDatabaseAgentTargetGroupModel
                {
                    ResourceGroupName = this.ResourceGroupName,
                    AgentServerName = this.AgentServerName,
                    AgentName = this.AgentName,
                    TargetGroupName = this.TargetGroupName,
                    Members = new List<Management.Sql.Models.JobTarget>{},
                }
            };

            return targetGroup;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentTargetGroupModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentTargetGroupModel> entity)
        {
            return new List<AzureSqlDatabaseAgentTargetGroupModel>
            {
                ModelAdapter.UpsertTargetGroup(entity.First())
            };
        }
    }
}