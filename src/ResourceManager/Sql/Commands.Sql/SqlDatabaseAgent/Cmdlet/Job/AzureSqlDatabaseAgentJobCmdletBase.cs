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
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentJobCmdletBase<IO> : AzureSqlDatabaseAgentCmdletBase<IO, IEnumerable<AzureSqlDatabaseAgentJobModel>, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Agent default parameter sets
        /// </summary>
        protected const string AgentDefaultRunOnceParameterSet = "Agent Default Run Once Parameter Set";
        protected const string AgentDefaultRecurringParameterSet = "Agent Default Recurring Parameter Set";

        /// <summary>
        /// Agent object parameter sets
        /// </summary>
        protected const string AgentObjectRunOnceParameterSet = "Agent Object Run Once Parameter Set";
        protected const string AgentObjectRecurringParameterSet = "Agent Object Recurring Parameter Set";

        /// <summary>
        /// Agent resource id parameter sets
        /// </summary>
        protected const string AgentResourceIdRunOnceParameterSet = "Agent Resource Id Run Once Parameter Set";
        protected const string AgentResourceIdRecurringParameterSet= "Agent Resource Id Recurring Parameter Set";

        /// <summary>
        /// Job default parameter sets
        /// </summary>
        protected const string JobDefaultRunOnceParameterSet = "Job Default Run Once Parameter Set";
        protected const string JobDefaultRecurringParameterSet = "Job Default Recurring Parameter Set";
        protected const string JobDefaultEnableParameterSet = "Job Default Enable Job Parameter Set";
        protected const string JobDefaultDisableParameterSet = "Job Default Disable Job Parameter Set";

        /// <summary>
        /// Job object parameter sets
        /// </summary>
        protected const string JobObjectRunOnceParameterSet = "Job Object Run Once Parameter Set";
        protected const string JobObjectRecurringParameterSet = "Job Object Recurring Parameter Set";
        protected const string JobObjectEnableParameterSet = "Job Object Enable Parameter Set";
        protected const string JobObjectDisableParameterSet = "Job Object Disable Parameter Set";

        /// <summary>
        /// Job resource id parameter sets
        /// </summary>
        protected const string JobResourceIdRunOnceParameterSet = "Job Resource Id Run Once Parameter Set";
        protected const string JobResourceIdRecurringParameterSet = "Job Resource Id Recurring Parameter Set";
        protected const string JobResourceIdEnableParameterSet = "Job Resource Id Enable Job Parameter Set";
        protected const string JobResourceIdDisableParameterSet = "Job Resource Id Disable Job Parameter Set";

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Job adapter</returns>
        protected override AzureSqlDatabaseAgentAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentAdapter(DefaultContext);
        }
    }
}