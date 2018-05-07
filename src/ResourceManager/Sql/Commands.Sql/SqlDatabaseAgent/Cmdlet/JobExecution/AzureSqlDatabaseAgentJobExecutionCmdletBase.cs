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
    public abstract class AzureSqlDatabaseAgentJobExecutionCmdletBase : 
        AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentJobExecutionModel, IEnumerable<AzureSqlDatabaseAgentJobExecutionModel>, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Default parameter sets
        /// </summary>
        protected const string GetRootJobExecution = "GetRootJobExecution Parameter Set";

        /// <summary>
        /// Input object parameter sets
        /// </summary>
        protected const string InputObjectGetRootJobExecution = "Input Object GetRootJobExecution Parameter Set";

        /// <summary>
        /// Resource id parameter sets
        /// </summary>
        protected const string ResourceIdGetRootJobExecution = "Resource Id GetRootJobExecution Parameter Set";

        /// <summary>
        /// Default parameter sets
        /// </summary>
        protected const string GetJobStepExecution = "GetJobStepExecution Parameter Set";

        /// <summary>
        /// Input object parameter sets
        /// </summary>
        protected const string InputObjectGetJobStepExecution = "Input Object GetJobStepExecution Parameter Set";

        /// <summary>
        /// Resource id parameter sets
        /// </summary>
        protected const string ResourceIdGetJobStepExecution = "Resource Id GetJobStepExecution Parameter Set";

        /// <summary>
        /// Default parameter sets
        /// </summary>
        protected const string GetJobTargetExecution = "GetJobTargetExecution Parameter Set";

        /// <summary>
        /// Input object parameter sets
        /// </summary>
        protected const string InputObjectGetJobTargetExecution = "Input Object GetJobTargetExecution Parameter Set";

        /// <summary>
        /// Resource id parameter sets
        /// </summary>
        protected const string ResourceIdGetJobTargetExecution = "Resource Id GetJobTargetExecution Parameter Set";

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