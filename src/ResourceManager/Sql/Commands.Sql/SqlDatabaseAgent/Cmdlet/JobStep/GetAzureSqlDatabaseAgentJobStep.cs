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

using System;
using System.Management.Automation;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.Job
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class GetAzureSqlDatabaseAgentJobStep : AzureSqlDatabaseAgentJobStepCmdletBase
    {
        /// <summary>
        /// Gets or sets the job input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the job resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job step name
        /// </summary>
        [Parameter(Mandatory = false)]
        [Alias("StepName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the job version
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "The job version")]
        public int? Version { get; set; }

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
                    this.JobName = InputObject.JobName;
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[9];
                    this.JobName = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            // Returns a list of jobs if name is not provided
            if (this.Name == null)
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName), true);
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets a job step from the service.
        /// </summary>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentJobStepModel GetEntity()
        {
            if (this.Version.HasValue)
            {
                return ModelAdapter.GetJobStepByVersion(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Version.Value, this.Name);
            }

            return ModelAdapter.GetJobStep(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Name);
        }
    }
}