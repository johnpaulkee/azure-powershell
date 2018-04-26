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
    [OutputType(typeof(AzureSqlDatabaseAgentJobExecutionModel))]
    [OutputType(typeof(IEnumerable<AzureSqlDatabaseAgentJobExecutionModel>))]
    public class GetAzureSqlDatabaseAgentJobExecution : AzureSqlDatabaseAgentJobExecutionCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the resource group
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of agent server name
        /// </summary>
        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
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
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        [Parameter(ParameterSetName = ListByJob,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByJob,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListStepExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetStepExecution,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetStepExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetTargetExecution,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByJob,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListStepExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetStepExecution,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetStepExecutions,
            Position = 1,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetTargetExecution,
            Position = 1,
            HelpMessage = "The job object")]
        public string JobName { get; set; }

        [Parameter(ParameterSetName = GetRootJobExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListStepExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetStepExecution,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetStepExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetTargetExecution,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListStepExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetStepExecution,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetStepExecutions,
            Position = 2,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetTargetExecution,
            Position = 2,
            HelpMessage = "The job object")]
        public string JobExecutionId { get; set; }

        [Parameter(ParameterSetName = GetStepExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 5,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 5,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 5,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetStepExecution,
            Position = 3,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetStepExecutions,
            Position = 3,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetTargetExecution,
            Position = 3,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetStepExecution,
            Position = 3,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetStepExecutions,
            Position = 3,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetTargetExecution,
            Position = 3,
            HelpMessage = "The job object")]
        public string StepName { get; set; }

        [Parameter(ParameterSetName = GetTargetExecution,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            Position = 6,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetTargetExecution,
            Position = 4,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetTargetExecution,
            Position = 4,
            HelpMessage = "The job object")]
        public string TargetId { get; set; }

        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = InputObjectListStepExecutions,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = ResourceIdListStepExecutions,
            HelpMessage = "The job object")]
        public SwitchParameter Steps { get; set; }

        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "SQL Database Agent Resource Group Name.")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = InputObjectListTargetExecutions,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = InputObjectListTargetStepExecutions,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = ResourceIdListTargetExecutions,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = ResourceIdListTargetStepExecutions,
            HelpMessage = "The job object")]
        public SwitchParameter Targets { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time min")]
        public DateTime? CreateTimeMin { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by create time max")]
        public DateTime? CreateTimeMax { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time min.")]
        public DateTime? EndTimeMin { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter by end time max.")]
        public DateTime? EndTimeMax { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Flag to filter by active executions.")]
        public SwitchParameter Active { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to skip a specified number of executions")]
        public int? Skip { get; set; }

        [Parameter(ParameterSetName = ListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ListByJob,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = InputObjectListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = InputObjectListByJob,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = InputObjectListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = InputObjectListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = InputObjectListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ResourceIdListByAgent,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ResourceIdListByJob,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ResourceIdListStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ResourceIdListTargetExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        [Parameter(ParameterSetName = ResourceIdListTargetStepExecutions,
            Mandatory = false,
            HelpMessage = "Filter to take the top number of executions")]
        public int? Top { get; set; }

        /// <summary>
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByAgent,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListByJob,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetRootJobExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListStepExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetStepExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectListTargetStepExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectGetTargetExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentJobModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the Agent's Control Database Object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByAgent,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListByJob,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetRootJobExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListStepExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetStepExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdListTargetStepExecutions,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdGetTargetExecution,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

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