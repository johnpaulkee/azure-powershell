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
        protected const string DefaultParameterSet = "Job Default Parameter Set";
        protected const string InputObjectParameterSet = "Input object job default parameter set";
        protected const string ResourceIdParameterSet = "Resource id job default parameter set";

        protected const string DefaultOnceParameterSet = "Job Default Once Parameter Set";
        protected const string DefaultMinuteParameterSet = "Job Default Minute Parameter Set";
        protected const string DefaultHourParameterSet = "Job Default Hour Parameter Set";
        protected const string DefaultDayParameterSet = "Job Default Day Parameter Set";
        protected const string DefaultWeekParameterSet = "Job Default Week Parameter Set";
        protected const string DefaultMonthParameterSet = "Job Default Month Parameter Set";

        protected const string InputObjectOnceParameterSet = "Job Input Object Once Parameter Set";
        protected const string InputObjectMinuteParameterSet = "Job Input Object Minute Parameter Set";
        protected const string InputObjectHourParameterSet = "Job Input Object Hour Parameter Set";
        protected const string InputObjectDayParameterSet = "Job Input Object Day Parameter Set";
        protected const string InputObjectWeekParameterSet = "Job Input Object Week Parameter Set";
        protected const string InputObjectMonthParameterSet = "Job Input Object Month Parameter Set";

        protected const string ResourceIdOnceParameterSet = "Job Resource Id Once Parameter Set";
        protected const string ResourceIdMinuteParameterSet = "Job Resource Id Minute Parameter Set";
        protected const string ResourceIdHourParameterSet = "Job Resource Id Hour Parameter Set";
        protected const string ResourceIdDayParameterSet = "Job Resource Id Day Parameter Set";
        protected const string ResourceIdWeekParameterSet = "Job Resource Id Week Parameter Set";
        protected const string ResourceIdMonthParameterSet = "Job Resource Id Month Parameter Set";


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
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Job adapter</returns>
        protected override AzureSqlDatabaseAgentJobStepAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobStepAdapter(DefaultContext);
        }
    }
}