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

using System.Management.Automation;
using Microsoft.Rest.Azure;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using Microsoft.Azure.Management.Sql.Models;
using System.Text;
using static Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.AzureSqlDatabaseAgentJobModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgentJob", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class NewAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobCmdletBase<AzureSqlDatabaseAgentModel>
    {
        /// <summary>
        /// Gets or sets the resource group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [ValidateNotNullOrEmpty]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the agent name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The job name")]
        [ValidateNotNullOrEmpty]
        [Alias("JobName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter run once
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The flag to indicate job will be run once")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 2,
            HelpMessage = "The flag to indicate job will be run once")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 2,
            HelpMessage = "The flag to indicate job will be run once")]
        public SwitchParameter RunOnce { get; set; }

        /// <summary>
        /// Get or sets the job schedule interval type
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            Position = 4,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRecurringParameterSet,
            Position = 2,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            Position = 2,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        public JobScheduleReccuringScheduleTypes? IntervalType { get; set; }

        /// <summary>
        /// Gets or sets the job schedule interval count
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            Position = 5,
            HelpMessage = "The recurring schedule interval count")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRecurringParameterSet,
            Position = 3,
            HelpMessage = "The recurring schedule interval count")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            Position = 3,
            HelpMessage = "The recurring schedule interval count")]
        public uint? IntervalCount { get; set; }

        /// <summary>
        /// Gets or sets the job schedule start time
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentDefaultRunOnceParameterSet,
            Position = 5,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            Position = 6,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentObjectRunOnceParameterSet,
            Position = 3,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentObjectRecurringParameterSet,
            Position = 4,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentResourceIdRunOnceParameterSet,
            Position = 3,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            Position = 4,
            HelpMessage = "The job schedule start time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the job schedule end time
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentDefaultRecurringParameterSet,
            Position = 7,
            HelpMessage = "The job schedule end time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentObjectRecurringParameterSet,
            Position = 5,
            HelpMessage = "The job schedule end time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            Position = 5,
            HelpMessage = "The job schedule end time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the job description
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The job description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the agent input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentObjectRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentModel AgentObject { get; set; }

        /// <summary>
        /// Gets or sets the agent resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = AgentResourceIdRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [ValidateNotNullOrEmpty]
        public string AgentResourceId { get; set; }


        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            InitializeInputObjectProperties(this.AgentObject);
            InitializeResourceIdProperties(this.AgentResourceId);
            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Check to see if the job already exists in this agent.
        /// </summary>
        /// <returns>Null if the job doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> GetEntity()
        {
            try
            {
                ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no agent with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The job already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureSqlDatabaseAgentJobExists, this.Name, this.AgentName),
                "JobName");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the job doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobModel> model)
        {
            AzureSqlDatabaseAgentJobModel newEntity = new AzureSqlDatabaseAgentJobModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.Name,
                Description = this.Description,
                StartTime = this.StartTime != null ? this.StartTime : null,
                EndTime = this.EndTime != null ? this.EndTime : null,
                ScheduleType = this.RunOnce.IsPresent ? JobScheduleType.Once :
                               this.IntervalType.HasValue ? JobScheduleType.Recurring : (JobScheduleType?) null,
            };

            if (newEntity.ScheduleType != null && newEntity.ScheduleType == JobScheduleType.Recurring)
            {
                StringBuilder stringBuilder = new StringBuilder();

                // Create basic ISO 8601 duration - Basic string builder implementation
                // XmlConvert.ToString(timeSpan) only supports up to days. Weeks and months need to be supported
                stringBuilder.Append("P");

                if (this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Hour) || 
                    this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Minute))
                {
                    stringBuilder.Append("T");
                }

                if (this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Month) || 
                    this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Minute))
                {
                    stringBuilder.Append(this.IntervalCount + "M");
                }

                if (this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Week))
                {
                    stringBuilder.Append(this.IntervalCount + "W");
                }

                if (this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Day))
                {
                    stringBuilder.Append(this.IntervalCount + "D");
                }

                if (this.IntervalType.Value.HasFlag(JobScheduleReccuringScheduleTypes.Hour))
                {
                    stringBuilder.Append(this.IntervalCount + "H");
                }

                string interval = stringBuilder.ToString();

                newEntity.Interval = interval;
            }

            return new List<AzureSqlDatabaseAgentJobModel> { newEntity };
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the agent
        /// </summary>
        /// <param name="entity">The agent to create</param>
        /// <returns>The created agent</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobModel> entity)
        {
            return new List<AzureSqlDatabaseAgentJobModel> {
                ModelAdapter.UpsertJob(entity.First())
            };
        }
    }
}