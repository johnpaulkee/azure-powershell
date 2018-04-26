using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using System;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobVersion
{
    /// <summary>
    /// Defines the Get-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "AzureRmSqlDatabaseAgentJobVersion",
        SupportsShouldProcess = true,
        DefaultParameterSetName = DefaultParameterSet)]
    public class GetAzureSqlDatabaseAgentJobVersion : AzureSqlDatabaseAgentJobVersionCmdletBase
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
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 3,
            HelpMessage = "The job name")]
        public string JobName { get; set; }


        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The job version number")]
        public int Version { get; set; }

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

            // If version is not provided
            if (!this.MyInvocation.BoundParameters.ContainsKey("Version"))
            {
                ModelAdapter = InitModelAdapter(DefaultProfile.DefaultContext.Subscription);
                WriteObject(ModelAdapter.GetJobVersion(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName));
                return;
            }

            base.ExecuteCmdlet();
        }

        protected override AzureSqlDatabaseAgentJobVersionModel GetEntity()
        {
            return ModelAdapter.GetJobVersion(this.ResourceGroupName, this.ServerName, this.AgentName, this.JobName, this.Version);
        }
    }
}