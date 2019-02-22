using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Services;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Cmdlet
{
    public abstract class InstancePoolCmdletBase : AzureSqlCmdletBase<IEnumerable<AzureSqlInstancePoolModel>, AzureSqlInstancePoolAdapter>
    {
        /// <summary>
        /// Initializes the instance pool model adapter.
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are in operation with</param>
        /// <returns>The instance pool adapter</returns>
        protected override AzureSqlInstancePoolAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlInstancePoolAdapter(DefaultContext);
        }
    }
}
