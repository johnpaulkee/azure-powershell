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

using Microsoft.Azure.Management.Sql.Models;
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobModel
    {
        public AzureSqlDatabaseAgentJobModel()
        {
        }

        /// <summary>
        /// Gets or sets the name of the resource group name
        /// </summary>
        public string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the server
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        ///  Gets or sets the job name
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the description of the job
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the job schedule
        /// </summary>
        public JobSchedule Schedule { get; set; }

        /// <summary>
        /// Gets or sets the job resource id.
        /// </summary>
        public string ResourceId { get; set; }
    }
}
