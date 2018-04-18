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

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgentJob", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class NewAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobCmdletBase
    {
        /// <summary>
        /// Gets or sets the agent input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectOnceParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectMinuteParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectHourParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectDayParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectWeekParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectMonthParameterSet,
            ValueFromPipeline = true,
            Position = 0,
            HelpMessage = "The agent input object")]
        [ValidateNotNullOrEmpty]
        public AzureSqlDatabaseAgentModel InputObject { get; set; }

        /// <summary>
        /// Gets or sets the agent resource id
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The agent resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 0,
            HelpMessage = "The resource group name")]
        public override string ResourceGroupName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The server name")]
        public override string ServerName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public override string AgentName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        public string Name { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultOnceParameterSet,
            HelpMessage = "The job will be run once")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectOnceParameterSet,
            HelpMessage = "The job will be run once")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdOnceParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will be run once")]
        public SwitchParameter Once { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultMinuteParameterSet,
            HelpMessage = "The job will execute in minute intervals")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectMinuteParameterSet,
            HelpMessage = "The job will execute in minute intervals")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdMinuteParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will execute in minute intervals")]
        public int MinuteInterval { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultHourParameterSet,
            HelpMessage = "The job will execute in hour intervals")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectHourParameterSet,
            HelpMessage = "The job will execute in hour intervals")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdHourParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will execute in hour intervals")]
        public int HourInterval { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultDayParameterSet,
            HelpMessage = "The job will execute in daily intervals")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectDayParameterSet,
            HelpMessage = "The job will execute in daily intervals")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdDayParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will execute in daily intervals")]
        public int DayInterval { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultWeekParameterSet,
            HelpMessage = "The job will execute in weekly intervals")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectWeekParameterSet,
            HelpMessage = "The job will execute in weekly intervals")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdWeekParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will execute in weekly intervals")]
        public int WeekInterval { get; set; }

        [Parameter(
            Mandatory = true,
            Position = 4,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = DefaultMonthParameterSet,
            HelpMessage = "The job will execute in monthly intervals")]
        [Parameter(
            Mandatory = true,
            Position = 2,
            ValueFromPipelineByPropertyName = true,
            ParameterSetName = InputObjectMonthParameterSet,
            HelpMessage = "The job will execute in monthly intervals")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = ResourceIdMonthParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The job will execute in monthly intervals")]
        public int MonthInterval { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The job description")]
        public string Description { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The job start time")]
        public DateTime StartTime { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The job end time")]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The switch parameter that describes whether this job will be run once. If not provided, job will be recurring.
        /// </summary>
        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Determines whether this job start immediately")]
        SwitchParameter Enabled { get; set; }

        /// <summary>
        /// Check to see if the agent already exists in this resource group.
        /// </summary>
        /// <returns>Null if the agent doesn't exist. Otherwise throws exception</returns>
        protected override AzureSqlDatabaseAgentJobModel GetEntity()
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
        /// <param name="model">This is null since the server doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override AzureSqlDatabaseAgentJobModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobModel model)
        {
            AzureSqlDatabaseAgentJobModel newEntity = new AzureSqlDatabaseAgentJobModel
            {
                ResourceGroupName = this.ResourceGroupName,
                ServerName = this.ServerName,
                AgentName = this.AgentName,
                JobName = this.Name,
                Description = this.Description,
                Type = this.Once.IsPresent ? JobScheduleType.Once : JobScheduleType.Recurring,
                Enabled = this.Enabled.IsPresent
            };

            return newEntity;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the agent
        /// </summary>
        /// <param name="entity">The agent to create</param>
        /// <returns>The created agent</returns>
        protected override AzureSqlDatabaseAgentJobModel PersistChanges(AzureSqlDatabaseAgentJobModel entity)
        {
            return ModelAdapter.UpsertJob(entity);
        }
    }
}