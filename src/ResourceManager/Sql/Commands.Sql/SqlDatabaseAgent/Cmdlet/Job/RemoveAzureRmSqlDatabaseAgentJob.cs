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
using System.Globalization;
using System;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.Job
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "AzureRmSqlDatabaseAgentJob", SupportsShouldProcess = true)]
    public class RemoveAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobCmdletBase
    {
        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [Alias("JobName")]
        public string Name { get; set; }

        /// <summary>
        /// Defines whether it is ok to skip the requesting of rule removal confirmation
        /// </summary>
        [Parameter(HelpMessage = "Skip confirmation message for performing the action")]
        public SwitchParameter Force { get; set; }

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
            HelpMessage = "The agent resource id")]
        [ValidateNotNullOrEmpty]
        public string ResourceId { get; set; }

        /// <summary>
        /// Entry point for the cmdlet
        /// </summary>
        public override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case InputObjectParameterSet:
                    this.ResourceGroupName = InputObject.ResourceGroupName;
                    this.ServerName = InputObject.ServerName;
                    this.AgentName = InputObject.AgentName;
                    this.Name = InputObject.AgentName;
                    break;
                case ResourceIdParameterSet:
                    string[] tokens = ResourceId.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    this.ResourceGroupName = tokens[3];
                    this.ServerName = tokens[7];
                    this.AgentName = tokens[9];
                    this.Name = tokens[tokens.Length - 1];
                    break;
                default:
                    break;
            }

            // Warning confirmation for agent when deleting
            if (!Force.IsPresent &&
                !ShouldProcess(string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentJobDescription, this.Name, this.ServerName),
                               string.Format(CultureInfo.InvariantCulture, Properties.Resources.RemoveSqlDatabaseAgentJobWarning, this.Name, this.ServerName),
                               Properties.Resources.ShouldProcessCaption))
            {
                return;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Gets job to see if it exists before removing
        /// </summary>
        /// <returns></returns>
        protected override AzureSqlDatabaseAgentJobModel GetEntity()
        {
            try
            {
                return ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The job doesn't exists
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureSqlDatabaseAgentJobNotExists, this.Name, this.AgentName),
                        "JobName");
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Apply user input.
        /// </summary>
        /// <param name="model">The result of GetEntity</param>
        /// <returns>The input model</returns>
        protected override AzureSqlDatabaseAgentJobModel ApplyUserInputToModel(AzureSqlDatabaseAgentJobModel model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the job.
        /// </summary>
        /// <param name="entity">The job account being deleted</param>
        /// <returns>The job account that was deleted</returns>
        protected override AzureSqlDatabaseAgentJobModel PersistChanges(AzureSqlDatabaseAgentJobModel entity)
        {
            ModelAdapter.RemoveJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name);
            return entity;
        }
    }
}