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
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using static Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model.AzureSqlDatabaseAgentJobModel;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the Set-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmSqlDatabaseAgentJob",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class SetAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobCmdletBase
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
            ParameterSetName = JobDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultDisableParameterSet,
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
            ParameterSetName = JobDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultDisableParameterSet,
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
            ParameterSetName = JobDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultDisableParameterSet,
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
            ParameterSetName = JobDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultDisableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [ValidateNotNullOrEmpty]
        [Alias("JobName")]
        public override string Name { get; set; }

        /// <summary>
        /// Gets or sets the switch parameter run once
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRunOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The flag to indicate job will be run once")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The flag to indicate job will be run once")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 1,
            HelpMessage = "The flag to indicate job will be run once")]
        public SwitchParameter RunOnce { get; set; }

        /// <summary>
        /// Get or sets the job schedule interval type
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            Position = 4,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectRecurringParameterSet,
            Position = 1,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdRecurringParameterSet,
            Position = 1,
            HelpMessage = "The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month")]
        public JobScheduleReccuringScheduleTypes? IntervalType { get; set; }

        /// <summary>
        /// Gets or sets the job schedule interval count
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultRecurringParameterSet,
            Position = 5,
            HelpMessage = "The recurring schedule interval count")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectRecurringParameterSet,
            Position = 2,
            HelpMessage = "The recurring schedule interval count")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdRecurringParameterSet,
            Position = 2,
            HelpMessage = "The recurring schedule interval count")]
        public uint? IntervalCount { get; set; }

        /// <summary>
        /// Gets or sets the job schedule start time
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobDefaultRunOnceParameterSet,
            Position = 5,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobDefaultRecurringParameterSet,
            Position = 6,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobObjectRunOnceParameterSet,
            Position = 2,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobObjectRecurringParameterSet,
            Position = 3,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobResourceIdRunOnceParameterSet,
            Position = 2,
            HelpMessage = "The job schedule start time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobResourceIdRecurringParameterSet,
            Position = 3,
            HelpMessage = "The job schedule start time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the job schedule end time
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobDefaultRecurringParameterSet,
            Position = 7,
            HelpMessage = "The job schedule end time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobObjectRecurringParameterSet,
            Position = 4,
            HelpMessage = "The job schedule end time")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = JobResourceIdRecurringParameterSet,
            Position = 4,
            HelpMessage = "The job schedule end time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Gets or sets the job description
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The job description")]
        public string Description { get; set; }

        /// <summary>
        /// The flag to enable this job
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The flag to enable this job")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The flag to enable this job")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdEnableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The flag to enable this job")]
        public SwitchParameter Enable { get; set; }

        /// <summary>
        /// The flag to disable this job
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobDefaultDisableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 4,
            HelpMessage = "The flag to disable this job")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectDisableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The flag to disable this job")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdDisableParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The flag to disable this job")]
        public SwitchParameter Disable { get; set; }

        /// <summary>
        /// Gets or sets the agent input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectEnableParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobObjectDisableParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job input object")]
        [ValidateNotNullOrEmpty]
        public override AzureSqlDatabaseAgentJobModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the agent resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdRunOnceParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdRecurringParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdEnableParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = JobResourceIdDisableParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The job resource id")]
        [ValidateNotNullOrEmpty]
        public override string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (ParameterSetName == InputObjectParameterSet)
            {
                InitializeJobProperties(this.InputObject);
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Check to see if the agent already exists in this resource group.
        /// </summary>
        /// <returns>Null if the agent doesn't exist. Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> GetEntity()
        {
            try
            {
                return new List<AzureSqlDatabaseAgentJobModel> {
                    ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name)
                };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The job does not exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobNotExists, this.Name, this.AgentName),
                        "JobName");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobModel> model)
        {
            //var existingEntity = model.First();

            //AzureSqlDatabaseAgentJobModel newEntity = new AzureSqlDatabaseAgentJobModel
            //{
            //    ResourceGroupName = this.ResourceGroupName,
            //    ServerName = this.ServerName,
            //    AgentName = this.AgentName,
            //    JobName = this.Name,
            //    Description = this.Description != null ? this.Description : existingEntity.Description,
            //    Enabled = this.Enabled.IsPresent,
            //    StartTime = this.StartTime != null ? this.StartTime : existingEntity.StartTime,
            //    EndTime = this.EndTime != null ? this.EndTime : existingEntity.EndTime
            //};

            //// Check what flags are set
            //bool onceIsPresent = this.Once.IsPresent;
            //bool recurringIsPresent = this.MonthInterval.HasValue ||
            //                          this.WeekInterval.HasValue ||
            //                          this.DayInterval.HasValue ||
            //                          this.HourInterval.HasValue ||
            //                          this.MinuteInterval.HasValue;

            //// Set up job schedule params
            //if (onceIsPresent)
            //{
            //    // Customer requested job to run once
            //    newEntity.ScheduleType = JobScheduleType.Once;
            //}
            //else if (recurringIsPresent)
            //{
            //    // Customer requested job to be a recurring time interval
            //    // Can monthly, weekly, daily, hourly, or every X minutes.
            //    newEntity.ScheduleType = JobScheduleType.Recurring;

            //    StringBuilder stringBuilder = new StringBuilder();

            //    stringBuilder.Append("P");
            //    if (this.HourInterval.HasValue || this.MinuteInterval.HasValue) stringBuilder.Append("T");

            //    if (this.MonthInterval.HasValue) stringBuilder.Append(this.MonthInterval.Value.ToString() + "M");
            //    if (this.WeekInterval.HasValue) stringBuilder.Append(this.WeekInterval.Value.ToString() + "W");
            //    if (this.DayInterval.HasValue) stringBuilder.Append(this.DayInterval.Value.ToString() + "D");
            //    if (this.HourInterval.HasValue) stringBuilder.Append(this.HourInterval.Value.ToString() + "H");
            //    if (this.MinuteInterval.HasValue) stringBuilder.Append(this.MinuteInterval.Value.ToString() + "M");

            //    string interval = stringBuilder.ToString();

            //    newEntity.Interval = interval;
            //}

            //return new List<AzureSqlDatabaseAgentJobModel> { newEntity };
            return null;
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