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

using Microsoft.Azure.Commands.Sql.ManagedInstance.Model;
using Microsoft.Azure.Commands.Sql.ManagedInstance.Services;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.WindowsAzure.Commands.Common;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Services;

namespace Microsoft.Azure.Commands.Sql.ManagedInstance.Adapter
{
    /// <summary>
    /// Adapter for Managed instance operations
    /// </summary>
    public class AzureSqlManagedInstanceAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlManagedInstanceCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Constructs a Managed instance adapter
        /// </summary>
        /// <param name="context">The current azure profile</param>
        public AzureSqlManagedInstanceAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlManagedInstanceCommunicator(Context);
        }

        /// <summary>
        /// Gets a managed instance in a resource group
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <param name="managedInstanceName">The name of the managed instance</param>
        /// <returns>The managed instance</returns>
        public AzureSqlManagedInstanceModel GetManagedInstance(string resourceGroupName, string managedInstanceName)
        {
            var resp = Communicator.Get(resourceGroupName, managedInstanceName);
            return CreateManagedInstanceModelFromResponse(resp);
        }

        /// <summary>
        /// Gets a list of all the managed instances in a subscription
        /// </summary>
        /// <returns>A list of all the managed instances</returns>
        public List<AzureSqlManagedInstanceModel> ListManagedInstances()
        {
            var resp = Communicator.List();

            return resp.Select((s) => CreateManagedInstanceModelFromResponse(s)).ToList();
        }

        /// <summary>
        /// Gets a list of all the managed instances in a resource group
        /// </summary>
        /// <param name="resourceGroupName">The name of the resource group</param>
        /// <returns>A list of all the managed instances</returns>
        public List<AzureSqlManagedInstanceModel> ListManagedInstancesByResourceGroup(string resourceGroupName)
        {
            var resp = Communicator.ListByResourceGroup(resourceGroupName);
            return resp.Select((s) =>
            {
                return CreateManagedInstanceModelFromResponse(s);
            }).ToList();
        }

        /// <summary>
        /// Gets a list of all managed instances in an instance pool
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="instancePoolName">The instance pool name</param>
        /// <returns>A list of managed instances in an instance pool</returns>
        public List<AzureSqlManagedInstanceModel> ListManagedInstancesByInstancePool(string resourceGroupName, string instancePoolName)
        {
            var resp = Communicator.ListByInstancePool(resourceGroupName, instancePoolName);
            return resp.Select(CreateManagedInstanceModelFromResponse).ToList();
        }

        /// <summary>
        /// Upserts a managed instance
        /// </summary>
        /// <param name="model">The managed instance to upsert</param>
        /// <returns>The updated managed instance model</returns>
        public AzureSqlManagedInstanceModel UpsertManagedInstance(AzureSqlManagedInstanceModel model)
        {
            var parameters = new Management.Sql.Models.ManagedInstance()
            {
                Location = model.Location,
                Tags = model.Tags,
                AdministratorLogin = model.AdministratorLogin,
                AdministratorLoginPassword = model.AdministratorPassword != null ? ConversionUtilities.SecureStringToString(model.AdministratorPassword) : null,
                Sku = model.Sku != null ? new Management.Sql.Models.Sku(model.Sku.Name, model.Sku.Tier) : null,
                LicenseType = model.LicenseType,
                StorageSizeInGB = model.StorageSizeInGB,
                SubnetId = model.SubnetId,
                VCores = model.VCores,
                Identity = model.Identity,
                Collation = model.Collation,
                InstancePoolId = model.InstancePoolName != null ?
                    new ResourceIdentifier()
                    {
                        Subscription = Context.Subscription.Id,
                        ResourceGroupName = model.ResourceGroupName,
                        ParentResource = "providers/Microsoft.Sql/instancePools",
                        ResourceName = model.InstancePoolName
                    }.ToString() : null,
                PublicDataEndpointEnabled = model.PublicDataEndpointEnabled,
            };

            var resp = Communicator.CreateOrUpdate(model.ResourceGroupName, model.FullyQualifiedDomainName, parameters);
            return CreateManagedInstanceModelFromResponse(resp);
        }

        /// <summary>
        /// Upserts a managed instance
        /// </summary>
        /// <param name="model">The managed instance to upsert</param>
        /// <returns>The updated managed instance model</returns>
        public AzureSqlManagedInstanceModel UpdateManagedInstance(AzureSqlManagedInstanceModel model)
        {
            var resp = Communicator.Update(model.ResourceGroupName, model.FullyQualifiedDomainName, new Management.Sql.Models.ManagedInstanceUpdate()
            {
                Tags = model.Tags,
                AdministratorLogin = model.AdministratorLogin,
                AdministratorLoginPassword = model.AdministratorPassword != null ? ConversionUtilities.SecureStringToString(model.AdministratorPassword) : null,
                Sku = model.Sku != null ? new Management.Sql.Models.Sku(model.Sku.Name, model.Sku.Tier) : null,
                LicenseType = model.LicenseType,
                StorageSizeInGB = model.StorageSizeInGB,
                SubnetId = model.SubnetId,
                VCores = model.VCores
            });

            return CreateManagedInstanceModelFromResponse(resp);
        }

        /// <summary>
        /// Gets the parent instance pool
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="instancePoolName">The instance pool name</param>
        /// <returns>Gets the instance pool</returns>
        public AzureSqlInstancePoolModel GetInstancePool(string resourceGroupName, string instancePoolName)
        {
            AzureSqlInstancePoolAdapter instancePoolAdapter = new AzureSqlInstancePoolAdapter(Context);
            return instancePoolAdapter.GetInstancePool(resourceGroupName, instancePoolName);
        }

        /// <summary>
        /// Deletes a managed instance
        /// </summary>
        /// <param name="resourceGroupName">The resource group the managed instance is in</param>
        /// <param name="managedInstanceName">The name of the managed instance to delete</param>
        public void RemoveManagedInstance(string resourceGroupName, string managedInstanceName)
        {
            Communicator.Remove(resourceGroupName, managedInstanceName);
        }

        /// <summary>
        /// Convert a Management.Sql.LegacySdk.Models.ManagedInstance to AzureSqlDatabaseManagedInstanceModel
        /// </summary>
        /// <param name="resourceGroupName">The resource group the managed instance is in</param>
        /// <param name="resp">The management client managed instance response to convert</param>
        /// <returns>The converted managed instance model</returns>
        private static AzureSqlManagedInstanceModel CreateManagedInstanceModelFromResponse(Management.Sql.Models.ManagedInstance resp)
        {
            AzureSqlManagedInstanceModel managedInstance = new AzureSqlManagedInstanceModel();
            managedInstance.ResourceGroupName = new ResourceIdentifier(resp.Id).ResourceGroupName;
            managedInstance.ManagedInstanceName = resp.Name;
            managedInstance.Id = resp.Id;
            managedInstance.FullyQualifiedDomainName = resp.FullyQualifiedDomainName;
            managedInstance.AdministratorLogin = resp.AdministratorLogin;
            managedInstance.Location = resp.Location;
            managedInstance.Tags = TagsConversionHelper.CreateTagDictionary(TagsConversionHelper.CreateTagHashtable(resp.Tags), false);
            managedInstance.Identity = resp.Identity;
            managedInstance.FullyQualifiedDomainName = resp.FullyQualifiedDomainName;
            managedInstance.SubnetId = resp.SubnetId;
            managedInstance.LicenseType = resp.LicenseType;
            managedInstance.VCores = resp.VCores;
            managedInstance.StorageSizeInGB = resp.StorageSizeInGB;
            managedInstance.Collation = resp.Collation;
            managedInstance.InstancePoolName = resp.InstancePoolId != null ? new ResourceIdentifier(resp.InstancePoolId).ResourceName : null;
            managedInstance.HardwareFamily = resp.Sku.Family;
            managedInstance.Edition = resp.Sku.Tier;
            Management.Internal.Resources.Models.Sku sku = new Management.Internal.Resources.Models.Sku();
            sku.Name = resp.Sku.Name;
            sku.Tier = resp.Sku.Tier;
            managedInstance.Sku = sku;
            managedInstance.PublicDataEndpointEnabled = resp.PublicDataEndpointEnabled ?? false;

            return managedInstance;
        }
    }
}
