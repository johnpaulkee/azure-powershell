﻿// ----------------------------------------------------------------------------------
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
using Microsoft.Azure.Commands.Sql.ElasticJobs.Model;
using Microsoft.Azure.Commands.Sql.ElasticJobs.Services;
using System.Collections.Generic;

namespace Microsoft.Azure.Commands.Sql.ElasticJobs.Cmdlet.JobExecution
{
    /// <summary>
    /// Defines the elastic job execution cmdlet base
    /// </summary>
    /// <typeparam name="TInputObject">The input object model</typeparam>
    public abstract class AzureSqlElasticJobExecutionCmdletBase<TInputObject> : AzureSqlElasticJobsCmdletBase<TInputObject, IEnumerable<AzureSqlElasticJobExecutionModel>, AzureSqlElasticJobAdapter>
    {
        /// <summary>
        /// Initialize the adapter
        /// </summary>
        /// <param name="subscription"></param>
        /// <returns></returns>
        protected override AzureSqlElasticJobAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlElasticJobAdapter(DefaultContext);
        }
    }
}