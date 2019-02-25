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
using System.Text;
using Microsoft.Azure.Commands.Common.Authentication;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Management.Sql;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Services
{
    /// <summary>
    /// This class is responsible for all the REST communication with the audit REST endpoints
    /// </summary>
    public class AzureSqlInstancePoolCommunicator
    {
        /// <summary>
        /// Gets or sets the Azure subscription
        /// </summary>
        internal static IAzureSubscription Subscription { get; private set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public static IAzureContext Context { get; set; }

        /// <summary>
        /// Creates an Azure SQL Instance Pool Communicator
        /// </summary>
        /// <param name="context">The azure context</param>
        public AzureSqlInstancePoolCommunicator(IAzureContext context)
        {
            Context = context;
            if (Context.Subscription != Subscription)
            {
                Subscription = context.Subscription;
            }
        }

        #region Instance pool methods

        public void CreateOrUpdateInstancePool(string resourceGroupName, string instancePoolName)
        {
        }

        #endregion

        /// <summary>
        /// Retrieve the SQL Management client for the currently selected subscription, adding the session and request
        /// id tracing headers for the current cmdlet invocation.
        /// </summary>
        /// <returns>The SQL Management client for the currently selected subscription.</returns>
        public static SqlManagementClient GetCurrentSqlClient()
        {
            var sqlClient = AzureSession.Instance.ClientFactory.CreateArmClient<SqlManagementClient>(Context, AzureEnvironment.Endpoint.ResourceManager);
            return sqlClient;
        }
    }
}
