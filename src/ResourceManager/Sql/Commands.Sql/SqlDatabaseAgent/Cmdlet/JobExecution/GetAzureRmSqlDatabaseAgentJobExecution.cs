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

using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobExecution
{
    public class GetAzureRmSqlDatabaseAgentJobExecution : AzureSqlDatabaseAgentJobExecutionCmdletBase
    {
        public AzureSqlDatabaseAgentJobModel InputObject;

        public string ResourceId;


        public DateTime? CreateTimeMin;

        public DateTime? CreateTimeMax;

        public DateTime? EndTimeMin;

        public DateTime? EndTimeMax;

        public SwitchParameter IsActive;

        public string JobExecutionId;

        public string JobName;

        public int? Skip;

        /// <summary>
        /// Cmdlet execution starts here
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectParameterSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            // Returns a list of jobs if name is not provided
            if (this.JobExecutionId == null)
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.ListByJob(
                    this.ResourceGroupName, 
                    this.ServerName, 
                    this.AgentName, 
                    this.JobName,
                    this.CreateTimeMin,
                    this.CreateTimeMax,
                    this.EndTimeMin,
                    this.EndTimeMax,
                    this.IsActive.IsPresent,
                    this.Skip), true);

                return;
            }

            if (this.JobName == null)
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.ListByAgent(
                    this.ResourceGroupName, 
                    this.ServerName, 
                    this.AgentName,
                    this.CreateTimeMin,
                    this.CreateTimeMax,
                    this.EndTimeMin,
                    this.EndTimeMax,
                    this.IsActive.IsPresent,
                    this.Skip), true);
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets a job from the service.
        /// </summary>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentJobExecutionModel GetEntity()
        {
            return ModelAdapter.GetJobExecution(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, Guid.Parse(this.JobExecutionId));
        }
    }
}