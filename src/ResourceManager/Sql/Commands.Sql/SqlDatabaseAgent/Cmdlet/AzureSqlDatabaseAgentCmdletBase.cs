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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.Common
{
    /// <summary>
    /// The base class for all Azure Sql database cmdlets
    /// </summary>
    public abstract class AzureSqlDatabaseAgentCmdletBase<M, A> : AzureSqlCmdletBase<M, A>
    {
        protected const string DefaultParameterSet = "Agent Default Parameter Set";
        protected const string InputObjectParameterSet = "Agent Input Object Parameter Set";
        protected const string ResourceIdParameterSet = "Agent Resource Id Parameter Set";

        private const string targetGroupResourceIdTemplate = "/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/targetGroups/{4}";

        /// <summary>
        /// Gets or sets the resource group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The Agent Resource Group Name")]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the agent server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The Agent Server Name")]
        [Alias("AgentServerName")]
        [ValidateNotNullOrEmpty]
        public virtual string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [ValidateNotNullOrEmpty]
        public virtual string AgentName { get; set; }

        #region Helpers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetGroupName"></param>
        /// <returns></returns>
        protected string CreateTargetGroupId(string targetGroupName)
        {
            return string.Format(targetGroupResourceIdTemplate,
                            DefaultContext.Subscription.Id,
                            this.ResourceGroupName,
                            this.ServerName,
                            this.AgentName,
                            targetGroupName);
        }

        protected string CreateCredentialId(string credentialName)
        {
            if (credentialName == null)
            {
                return null;
            }

            return string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                            DefaultContext.Subscription.Id,
                            this.ResourceGroupName,
                            this.ServerName,
                            this.AgentName,
                            credentialName);
        }

        #endregion
    }
}