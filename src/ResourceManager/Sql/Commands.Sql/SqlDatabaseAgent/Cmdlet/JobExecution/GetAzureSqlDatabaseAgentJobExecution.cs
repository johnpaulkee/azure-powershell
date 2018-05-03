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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobExecution
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgentJobExecution Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJobExecution",
        SupportsShouldProcess = true,
        DefaultParameterSetName = ListByAgent)]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel>))]
    public class GetAzureSqlDatabaseAgentJobExecution : AzureSqlDatabaseAgentJobExecutionCmdletBase
    {
        /// <summary>
        /// Gets or sets the resource group name
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name.")]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name.")]
        [ValidateNotNullOrEmpty]
        [Alias("AgentServerName")]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent name
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByJob,
            Position = 1,
            HelpMessage = "The job name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            Position = 1,
            HelpMessage = "The job name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByJob,
            Position = 1,
            HelpMessage = "The job name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            Position = 1,
            HelpMessage = "The job name.")]
        public override string JobName { get; set; }

        /// <summary>
        /// Gets or sets the job execution id
        /// </summary>
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The job execution id.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            Position = 2,
            HelpMessage = "The job execution id.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            Position = 2,
            HelpMessage = "The job execution id.")]
        public string JobExecutionId { get; set; }

        /// <summary>
        /// Gets or sets the min create time
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        public DateTime? CreateTimeMin { get; set; }

        /// <summary>
        /// Gets or sets the max create time
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        public DateTime? CreateTimeMax { get; set; }

        /// <summary>
        /// Gets or sets the min end time
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        public DateTime? EndTimeMin { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        public DateTime? EndTimeMax { get; set; }

        /// <summary>
        /// Gets or sets the active switch parameter. Filters by active/in progress executions
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        public SwitchParameter Active { get; set; }

        /// <summary>
        /// Gets or sets the top executions to return in the response
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = true,
            HelpMessage = "Count returns the top number of executions.")]
        public int? Count { get; set; }

        /// <summary>
        /// Gets or sets the agent object input model
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByAgent,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByJob,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the agent resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByAgent,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByJob,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [ValidateNotNullOrEmpty]
        public override string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectListByAgent:
                case InputObjectListByJob:
                case InputObjectGetRootJobExecution:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    break;
                case ResourceIdListByAgent:
                case ResourceIdListByJob:
                case ResourceIdGetRootJobExecution:
                    var resourceInfo = new ResourceIdentifier(ResourceId);
                    this.ResourceGroupName = resourceInfo.ResourceGroupName;
                    this.ServerName = ResourceIdentifier.GetTypeFromResourceType(resourceInfo.ParentResource);
                    this.AgentName = resourceInfo.ResourceName;
                    break;
                default:
                    break;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets job execution(s) from the service.
        /// </summary>
        /// <returns></returns>
        protected override List<AzureSqlDatabaseAgentJobExecutionModel> GetEntity()
        {
            switch (ParameterSetName)
            {
                case ListByAgent:
                case InputObjectListByAgent:
                case ResourceIdListByAgent:

                    return ModelAdapter.ListByAgent(
                        resourceGroupName: this.ResourceGroupName,
                        serverName: this.ServerName,
                        agentName: this.AgentName,
                        createTimeMin: this.CreateTimeMin,
                        createTimeMax: this.CreateTimeMax,
                        endTimeMin: this.EndTimeMin,
                        endTimeMax: this.EndTimeMax, 
                        isActive: this.Active.IsPresent ? this.Active : (bool?) null,
                        top: this.Count);

                case ListByJob:
                case InputObjectListByJob:
                case ResourceIdListByJob:

                    return ModelAdapter.ListByJob(
                        resourceGroupName: this.ResourceGroupName,
                        serverName: this.ServerName,
                        agentName: this.AgentName,
                        jobName: this.JobName,
                        createTimeMin: this.CreateTimeMin,
                        createTimeMax: this.CreateTimeMax,
                        endTimeMin: this.EndTimeMin,
                        endTimeMax: this.EndTimeMax,
                        isActive: this.Active.IsPresent ? this.Active : (bool?)null,
                        top: this.Count);

                case GetRootJobExecution:
                case InputObjectGetRootJobExecution:
                case ResourceIdGetRootJobExecution:

                    var rootJobExecution = ModelAdapter.GetJobExecution(
                        resourceGroupName: this.ResourceGroupName,
                        serverName: this.ServerName,
                        agentName: this.AgentName,
                        jobName: this.JobName,
                        jobExecutionId: Guid.Parse(this.JobExecutionId));

                    return new List<AzureSqlDatabaseAgentJobExecutionModel> { rootJobExecution };

                default:
                    throw new PSArgumentException();
            }
        }
    }
}