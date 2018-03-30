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

using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgent Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentTarget", SupportsShouldProcess = true)]
    public class RemoveAzureSqlDatabaseAgentTarget : AzureSqlDatabaseAgentTargetCmdletBase
    {
        /// <summary>
        /// Check to see if the target group member already exists in the target group.
        /// </summary>
        /// <returns>Null if the target doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> GetEntity()
        {
            return null;
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> ApplyUserInputToModel(IEnumerable<Management.Sql.Models.JobTarget> model)
        {
            return new List<Management.Sql.Models.JobTarget> { CreateJobTargetModel() }; ;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the job credential
        /// </summary>
        /// <param name="entity">The credential to create</param>
        /// <returns>The created job credential</returns>
        protected override IEnumerable<Management.Sql.Models.JobTarget> PersistChanges(IEnumerable<Management.Sql.Models.JobTarget> entity)
        {
            return new List<Management.Sql.Models.JobTarget> { this.RemoveTarget(entity.First()) };
        }

        /// <summary>
        /// Removes a target from the target group
        /// </summary>
        /// <param name="targetToRemove">The new target to be added.</param>
        /// <returns>The target that was added.</returns>
        public Management.Sql.Models.JobTarget RemoveTarget(
            Management.Sql.Models.JobTarget targetToRemove)
        {
            return null;
        }
    }
}