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
using System.Collections.Generic;
using Microsoft.Azure.Commands.Sql.Database.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    public abstract class AzureSqlDatabaseAgentJobStepCmdletBase : 
        AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentJobStepModel, IEnumerable<AzureSqlDatabaseAgentJobStepModel>, AzureSqlDatabaseAgentAdapter>
    {
        protected const string DefaultOutputDatabaseObject = "Default job step output database object parameter set";
        protected const string DefaultOutputDatabaseId = "Default job step output database id parameter set";

        protected const string InputObjectOutputDatabaseObject = "Input object job step output database object parameter set";
        protected const string InputObjectOutputDatabaseId = "Input object job step output database id parameter set";

        protected const string ResourceIdOutputDatabaseObject = "Resource id job step output database object parameter set";
        protected const string ResourceIdOutputDatabaseId = "Resource id job step output database id parameter set";


        protected const string DefaultGetVersionParameterSet = "Default get job step version parameter set";
        protected const string JobObjectGetVersionParameterSet = "Job object get job step version parameter set";
        protected const string JobResourceIdGetVersionParameterSet = "Job resource id get job step version parameter set";

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