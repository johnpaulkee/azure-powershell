using Microsoft.Azure.Commands.Sql.Common;
using System.Collections.Generic;
using Microsoft.Azure.Commands.Sql.Usages.Services;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Usages.Models;

namespace Microsoft.Azure.Commands.Sql.Usages.Cmdlet
{
    public abstract class UsageCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlUsageModel>, AzureSqlUsageAdapter>
    {
        /// <summary>
        /// Initializes the usage adapter.
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are in operation with</param>
        /// <returns>The usage adapter</returns>
        protected override AzureSqlUsageAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlUsageAdapter(DefaultContext);
        }
    }
}
