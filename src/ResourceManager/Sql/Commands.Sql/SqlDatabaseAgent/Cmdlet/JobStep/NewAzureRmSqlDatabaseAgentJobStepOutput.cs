using Microsoft.Azure.Commands.ResourceManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobStep
{
    /// <summary>
    /// Defines the New-AzureRmSqlDatabaseAgentJobStep Cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, "AzureRmSqlDatabaseAgentJobStep",
        SupportsShouldProcess = true)]
    [OutputType(typeof(Management.Sql.Models.JobStepOutput))]
    public class NewAzureRmSqlDatabaseAgentJobStepOutput : AzureRMCmdlet
    {
        public string SubscriptionId;

        public string ResourceGroupName;

        public string ServerName;

        public string DatabaseName;

        public string SchemaName;

        public string TableName;

        public string CredentialName;

        public override void ExecuteCmdlet()
        {
            Management.Sql.Models.JobStepOutput output = new Management.Sql.Models.JobStepOutput
            {
                SubscriptionId = Guid.Parse(SubscriptionId),
                ResourceGroupName = ResourceGroupName, 
                ServerName = ServerName,
                DatabaseName = DatabaseName,
                SchemaName = SchemaName,
                TableName = TableName,
                Credential = CredentialName,
            };

            WriteObject(output);
        }
    }
}
