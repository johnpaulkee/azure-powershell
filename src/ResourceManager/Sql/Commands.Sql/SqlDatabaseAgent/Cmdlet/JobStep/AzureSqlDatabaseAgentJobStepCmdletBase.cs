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
    public abstract class AzureSqlDatabaseAgentJobStepCmdletBase : AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentJobStepModel, AzureSqlDatabaseAgentJobStepAdapter>
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
        protected override AzureSqlDatabaseAgentJobStepAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobStepAdapter(DefaultContext);
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
        protected Management.Sql.Models.JobStepOutput CreateJobStepOutputModel(
            Guid? outputSubscriptionId = null,
            string outputResourceGroupName = null,
            string outputServerName = null,
            string outputDatabaseName = null,
            string outputCredential = null,
            string outputSchemaName = null,
            string outputTableName = null)
        {
            return new Management.Sql.Models.JobStepOutput
            {
                SubscriptionId = this.OutputSubscriptionId != null ? Guid.Parse(this.OutputSubscriptionId) : outputSubscriptionId,
                ResourceGroupName = this.OutputResourceGroupName != null ? this.OutputResourceGroupName : outputResourceGroupName,
                ServerName = this.OutputServerName != null ? this.OutputServerName : outputServerName,
                DatabaseName = this.OutputDatabaseName != null ? this.OutputDatabaseName : outputDatabaseName,
                Credential = this.OutputCredentialName != null ? CreateCredentialId(this.OutputCredentialName) : outputCredential,
                SchemaName = this.OutputSchemaName != null ? this.OutputSchemaName : outputSchemaName,
                TableName = this.OutputTableName != null ? this.OutputTableName : outputTableName
            };
        }
    }
}