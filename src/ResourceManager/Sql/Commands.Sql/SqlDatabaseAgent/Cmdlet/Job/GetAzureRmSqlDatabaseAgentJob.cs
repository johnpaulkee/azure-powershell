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
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.Job
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgentJob Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJob", 
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    [OutputType(typeof(AzureSqlDatabaseAgentJobModel))]
    [OutputType(typeof(List<AzureSqlDatabaseAgentJobModel>))]
    public class GetAzureSqlDatabaseAgentJob : AzureSqlDatabaseAgentJobCmdletBase<AzureSqlDatabaseAgentModel>
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
        [ValidateNotNullOrEmpty]
        public override string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the server name
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        [ValidateNotNullOrEmpty]
        public override string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the job name
        /// </summary>
        [Parameter(
            Mandatory = false,
            ParameterSetName = InputObjectParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = ResourceIdParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 1,
            HelpMessage = "The job name")]
        [Parameter(
            Mandatory = false,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        [ValidateNotNullOrEmpty]
        [Alias("JobName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the agent input object
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = InputObjectParameterSet,
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
            ValueFromPipelineByPropertyName = true,
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
        /// Gets a job from the service.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> GetEntity()
        {
            try
            {
                // Returns a list of jobs if name is not provided
                if (this.Name == null)
                {
                    return ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName);
                }
                else
                {
                    return new List<AzureSqlDatabaseAgentJobModel> { ModelAdapter.GetJob(this.ResourceGroupName, this.ServerName, this.AgentName, this.Name) };
                }
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
        /// No user input to apply to model.
        /// </summary>
        /// <param name="model">The model to modify</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> ApplyUserInputToModel(IEnumerable<AzureSqlDatabaseAgentJobModel> model)
        {
            return model;
        }

        /// <summary>
        /// No changes, thus nothing to persist.
        /// Note: even though we technically don't need to override this, we want to pass the entity forward so that we can take advantage of
        /// powershell's understanding of a list with one item defaulting to just the item itself.
        /// </summary>
        /// <param name="entity">The entity retrieved</param>
        /// <returns>The unchanged entity</returns>
        protected override IEnumerable<AzureSqlDatabaseAgentJobModel> PersistChanges(IEnumerable<AzureSqlDatabaseAgentJobModel> entity)
        {
            return entity;
        }
    }
}