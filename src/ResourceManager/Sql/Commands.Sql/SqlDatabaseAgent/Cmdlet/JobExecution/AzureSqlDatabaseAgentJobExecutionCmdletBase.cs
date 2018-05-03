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
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobExecution
{
    public abstract class AzureSqlDatabaseAgentJobExecutionCmdletBase : AzureSqlDatabaseAgentCmdletBase<List<AzureSqlDatabaseAgentJobExecutionModel>, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Default parameter sets
        /// </summary>
        protected const string ListByAgent = "ListByAgent Parameter Set";
        protected const string ListByJob = "ListByJob Parameter Set";
        protected const string GetRootJobExecution = "GetRootJobExecution Parameter Set";

        /// <summary>
        /// Input object parameter sets
        /// </summary>
        protected const string InputObjectListByAgent = "Input Object ListByAgent Parameter Set";
        protected const string InputObjectListByJob = "Input Object ListByJob Parameter Set";
        protected const string InputObjectGetRootJobExecution = "Input Object GetRootJobExecution Parameter Set";

        /// <summary>
        /// Resource id parameter sets
        /// </summary>
        protected const string ResourceIdListByAgent = "Resource Id ListByAgent Parameter Set";
        protected const string ResourceIdListByJob = "Resource Id ListByJob Parameter Set";
        protected const string ResourceIdGetRootJobExecution = "Resource Id GetRootJobExecution Parameter Set";



        /// <summary>
        /// Gets or sets the Agent's Control Database Resource Id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The Agent Control Database Resource Id")]
        [ValidateNotNullOrEmpty]
        public virtual string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent name
        /// </summary>
        [Parameter(ParameterSetName = DefaultParameterSet,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public virtual string JobName { get; set; }

        /// <summary>
        /// Initialize the job execution adapter
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentAdapter(DefaultContext);
        }
    }
}