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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System.Management.Automation;
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentJobStepCmdletBase : AzureSqlCmdletBase<AzureSqlDatabaseAgentJobStepModel, AzureSqlDatabaseAgentJobStepAdapter>
    {
        /// <summary>
        /// Parameter sets
        /// </summary>
        protected const string DefaultParameterSet = "Job Step Default Parameter Set";
        protected const string InputObjectParameterSet = "Input object job step default parameter set";
        protected const string ResourceIdParameterSet = "Resource id job step default parameter set";

        protected const string AddOutputDefaultParameterSet = "Add output job step default parameter set";
        protected const string AddOutputInputObjectParameterSet = "Add Output Input object job step parameter set";
        protected const string AddOutputResourceIdParameterSet = "Add Output Resource id job step parameter set";

        protected const string RemoveOutputDefaultParameterSet = "Remove output job step default Parameter Set";
        protected const string RemoveOutputInputObjectParameterSet = "Remove output input object job step parameter set";
        protected const string RemoveOutputResourceIdParameterSet = "Remove output resource id job step parameter set";

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        public override string ResourceGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public virtual string ServerName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public virtual string AgentName { get; set; }

        /// <summary>
        /// Output parameters
        /// </summary>
        public virtual string OutputSubscriptionId { get; set; }
        public virtual string OutputResourceGroupName { get; set; }
        public virtual string OutputServerName { get; set; }
        public virtual string OutputDatabaseName { get; set; }
        public virtual string OutputSchemaName { get; set; }
        public virtual string OutputTableName { get; set; }
        public virtual string OutputCredentialName { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Job adapter</returns>
        protected override AzureSqlDatabaseAgentJobStepAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobStepAdapter(DefaultContext);
        }

        protected string CreateTargetGroupId(string targetGroupName)
        {
            return string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/targetGroups/{4}",
                            AzureSqlDatabaseAgentJobStepCommunicator.Subscription.Id,
                            this.ResourceGroupName,
                            this.ServerName,
                            this.AgentName,
                            targetGroupName);
        }

        protected string CreateCredentialId(string credentialName)
        {
            return string.Format("/subscriptions/{0}/resourceGroups/{1}/providers/Microsoft.Sql/servers/{2}/jobAgents/{3}/credentials/{4}",
                            AzureSqlDatabaseAgentJobStepCommunicator.Subscription.Id,
                            this.ResourceGroupName,
                            this.ServerName,
                            this.AgentName,
                            credentialName);
        }

        protected Management.Sql.Models.JobStepOutput CreateJobStepOutputModel()
        {
            var output = new Management.Sql.Models.JobStepOutput();

            if (this.OutputSubscriptionId != null)
            {
                output.SubscriptionId = Guid.Parse(this.OutputSubscriptionId);
            }

            if (this.OutputResourceGroupName != null)
            {
                output.ResourceGroupName = this.OutputResourceGroupName;
            }

            if (this.OutputServerName != null)
            {
                output.ServerName = this.OutputServerName;
            }

            if (this.OutputDatabaseName != null)
            {
                output.DatabaseName = this.OutputDatabaseName;
            }

            if (this.OutputCredentialName != null)
            {
                output.Credential = CreateCredentialId(this.OutputCredentialName);
            }

            if (this.OutputSchemaName != null)
            {
                output.SchemaName = this.OutputSchemaName;
            }

            if (this.OutputTableName != null)
            {
                output.TableName = this.OutputTableName;
            }

            return output;
        }
    }
}