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
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobExecution
{
    public abstract class AzureSqlDatabaseAgentJobExecutionCmdletBase : AzureSqlCmdletBase<AzureSqlDatabaseAgentJobExecutionModel, AzureSqlDatabaseAgentJobExecutionAdapter>
    {
        /// <summary>
        /// Parameter sets
        /// </summary>
        protected const string DefaultParameterSet = "Default parameter set";
        protected const string InputObjectParameterSet = "Input object parameter set"; // todo
        protected const string ResourceIdParameterSet = "Resource id parameter set"; // todo

        protected const string ListByAgent = "ListByAgent Parameter Set";
        protected const string ListByJob = "ListByJob Parameter Set";
        protected const string GetRootJobExecution = "GetRootJobExecution Parameter Set";
        protected const string ListStepExecutions = "ListStepExecutions Parameter Set";
        protected const string ListTargetExecutions = "ListTargetExecutions Parameter Set";
        protected const string GetStepExecution = "GetStepExecution Parameter Set";
        protected const string ListTargetStepExecutions = "ListTargetStepExecutions Parameter Set";
        protected const string GetTargetExecution = "GetTargetExecution Parameter Set";

        protected const string InputObjectListByAgent = "Input Object ListByAgent Parameter Set";
        protected const string InputObjectListByJob = "Input Object ListByJob Parameter Set";
        protected const string InputObjectGetRootJobExecution = "Input Object GetRootJobExecution Parameter Set";
        protected const string InputObjectListStepExecutions = "Input Object ListStepExecutions Parameter Set";
        protected const string InputObjectListTargetExecutions = "Input Object ListTargetExecutions Parameter Set";
        protected const string InputObjectGetStepExecution = "Input Object GetStepExecution Parameter Set";
        protected const string InputObjectListTargetStepExecutions = "Input Object ListTargetStepExecutions Parameter Set";
        protected const string InputObjectGetTargetExecution = "Input Object GetTargetExecution Parameter Set";

        protected const string ResourceIdListByAgent = "Resource Id ListByAgent Parameter Set";
        protected const string ResourceIdListByJob = "Resource Id ListByJob Parameter Set";
        protected const string ResourceIdGetRootJobExecution = "Resource Id GetRootJobExecution Parameter Set";
        protected const string ResourceIdListStepExecutions = "Resource Id ListStepExecutions Parameter Set";
        protected const string ResourceIdListTargetExecutions = "Resource Id ListTargetExecutions Parameter Set";
        protected const string ResourceIdGetStepExecution = "Resource Id GetStepExecution Parameter Set";
        protected const string ResourceIdListTargetStepExecutions = "Resource Id ListTargetStepExecutions Parameter Set";
        protected const string ResourceIdGetTargetExecution = "Resource Id GetTargetExecution Parameter Set";

        /// <summary>
        /// Gets or sets the name of the resource group
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of agent server name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentServerName")]
        public virtual string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public virtual string AgentName { get; set; }

        protected override AzureSqlDatabaseAgentJobExecutionAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobExecutionAdapter(DefaultContext);
        }
    }
}
