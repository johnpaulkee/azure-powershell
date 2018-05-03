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
    public abstract class AzureSqlDatabaseAgentJobStepCmdletBase : AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentJobStepModel, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Default parameter sets
        /// </summary>
        protected const string AddOutputDefaultParameterSet = "Add output job step default parameter set";
        protected const string RemoveOutputDefaultParameterSet = "Remove output job step default Parameter Set";

        /// <summary>
        /// Input object parameter sets
        /// </summary>
        protected const string AddOutputInputObjectParameterSet = "Add Output Input object job step parameter set";
        protected const string RemoveOutputInputObjectParameterSet = "Remove output input object job step parameter set";

        /// <summary>
        /// Resource id parameter sets
        /// </summary>
        protected const string AddOutputResourceIdParameterSet = "Add Output Resource id job step parameter set";
        protected const string RemoveOutputResourceIdParameterSet = "Remove output resource id job step parameter set";

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 3,
            ParameterSetName = DefaultParameterSet)]
        public virtual string JobName { get; set; }


        /// <summary>
        /// Output target parameters
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
        protected override AzureSqlDatabaseAgentAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentAdapter(DefaultContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputSubscriptionId"></param>
        /// <param name="outputResourceGroupName"></param>
        /// <param name="outputServerName"></param>
        /// <param name="outputDatabaseName"></param>
        /// <param name="outputCredential"></param>
        /// <param name="outputSchemaName"></param>
        /// <param name="outputTableName"></param>
        /// <returns></returns>
        protected Management.Sql.Models.JobStepOutput CreateOrUpdateJobStepOutputModel(
            Management.Sql.Models.JobStepOutput existingOutput = null)
        {
            Management.Sql.Models.JobStepOutput model;

            // Create new output model
            if (existingOutput == null)
            {
                model = new Management.Sql.Models.JobStepOutput
                {
                    SubscriptionId = this.OutputSubscriptionId != null ? Guid.Parse(this.OutputSubscriptionId) : (Guid?) null,
                    ResourceGroupName = this.OutputResourceGroupName != null ? this.OutputResourceGroupName : null,
                    ServerName = this.OutputServerName != null ? this.OutputServerName : null,
                    DatabaseName = this.OutputDatabaseName != null ? this.OutputDatabaseName : null,
                    Credential = this.OutputCredentialName != null ?
                        CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.OutputCredentialName) :
                        null,
                    SchemaName = this.OutputSchemaName != null ? this.OutputSchemaName : null,
                    TableName = this.OutputTableName != null ? this.OutputTableName : null
                };
            }
            else
            {
                // Update existing output model if necessary
                model = new Management.Sql.Models.JobStepOutput
                {
                    SubscriptionId = this.OutputSubscriptionId != null ? Guid.Parse(this.OutputSubscriptionId) : existingOutput.SubscriptionId,
                    ResourceGroupName = this.OutputResourceGroupName != null ? this.OutputResourceGroupName : existingOutput.ResourceGroupName,
                    ServerName = this.OutputServerName != null ? this.OutputServerName : existingOutput.ServerName,
                    DatabaseName = this.OutputDatabaseName != null ? this.OutputDatabaseName : existingOutput.DatabaseName,
                    Credential = this.OutputCredentialName != null ?
                        CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, this.OutputCredentialName) :
                        CreateCredentialId(this.ResourceGroupName, this.ServerName, this.AgentName, existingOutput.Credential),
                    SchemaName = this.OutputSchemaName != null ? this.OutputSchemaName : existingOutput.SchemaName,
                    TableName = this.OutputTableName != null ? this.OutputTableName : existingOutput.TableName
                };
            }

            return model;
        }
    }
}