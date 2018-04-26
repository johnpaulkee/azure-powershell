using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services.JobVersion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobVersion
{
    public abstract class AzureSqlDatabaseAgentJobVersionCmdletBase : AzureSqlCmdletBase<AzureSqlDatabaseAgentJobVersionModel, AzureSqlDatabaseAgentJobVersionAdapter>
    {
        protected const string DefaultParameterSet = "Job Default Parameter Set";
        protected const string InputObjectParameterSet = "Input object job default parameter set";
        protected const string ResourceIdParameterSet = "Resource id job default parameter set";

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
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
        public virtual string ServerName { get; set; }

        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultParameterSet,
            ValueFromPipelineByPropertyName = true,
            Position = 2,
            HelpMessage = "The agent name")]
        public virtual string AgentName { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Job adapter</returns>
        protected override AzureSqlDatabaseAgentJobVersionAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentJobVersionAdapter(DefaultContext);
        }
    }
}