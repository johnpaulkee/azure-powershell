using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model;
using Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Services;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Cmdlet.JobVersion
{
    public abstract class AzureSqlDatabaseAgentJobVersionCmdletBase : AzureSqlDatabaseAgentCmdletBase<AzureSqlDatabaseAgentJobVersionModel, AzureSqlDatabaseAgentAdapter>
    {
        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure SQL Database Agent Job adapter</returns>
        protected override AzureSqlDatabaseAgentAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlDatabaseAgentAdapter(DefaultContext);
        }
    }
}