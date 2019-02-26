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

using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Services
{
    /// <summary>
    /// Adapter for instance pool operations
    /// </summary>
    public class AzureSqlInstancePoolAdapter
    {
        /// <summary>
        /// Gets or sets the AzureEndpointsCommunicator which has all the needed management clients
        /// </summary>
        private AzureSqlInstancePoolCommunicator Communicator { get; set; }

        /// <summary>
        /// Gets or sets the Azure profile
        /// </summary>
        public IAzureContext Context { get; set; }

        /// <summary>
        /// Constructor for the instance pool adapter
        /// </summary>
        /// <param name="context">The Azure Context</param>
        public AzureSqlInstancePoolAdapter(IAzureContext context)
        {
            Context = context;
            Communicator = new AzureSqlInstancePoolCommunicator(Context);
        }

        #region Instance Pools

        /// <summary>
        /// Creates or updates an instance pool
        /// </summary>
        /// <param name="model">The instance pool entity</param>
        /// <returns>The created or updated instance pool entity</returns>
        public AzureSqlInstancePoolModel UpsertInstancePool(AzureSqlInstancePoolModel model)
        {
            var parameters = new InstancePool
            {
                Location = model.Location,
                Tags = model.Tags,
                LicenseType = model.LicenseType,
                Sku = new Sku(
                    name: Constants.GeneralPurposeGen5,
                    tier: model.Edition,
                    family: model.HardwareFamily),
                SubnetId = model.SubnetId,
                VCores = model.VCores
            };

            var resp = Communicator.CreateOrUpdateInstancePool(
                resourceGroupName: model.ResourceGroupName,
                instancePoolName: model.InstancePoolName,
                parameters: parameters);

            return CreateInstancePoolModelFromResponse(resp);
        }

        /// <summary>
        /// Updates an existing instance pool
        /// </summary>
        /// <param name="model">The existing instance pool entity</param>
        /// <returns>The updated instance pool entity</returns>
        public AzureSqlInstancePoolModel UpdateInstancePool(AzureSqlInstancePoolModel model)
        {
            var parameters = new InstancePoolUpdate(
                tags: model.Tags);

            var resp = Communicator.UpdateInstancePool(
                resourceGroupName: model.ResourceGroupName,
                instancePoolName: model.InstancePoolName,
                parameters: parameters);

            return CreateInstancePoolModelFromResponse(resp);
        }

        /// <summary>
        /// Gets an instance pool
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="instancePoolName">The instance pool name</param>
        /// <returns>An existing instance pool entity</returns>
        public AzureSqlInstancePoolModel GetInstancePool(string resourceGroupName, string instancePoolName)
        {
            var resp = Communicator.GetInstancePool(resourceGroupName, instancePoolName);
            return CreateInstancePoolModelFromResponse(resp);
        }

        /// <summary>
        /// Gets a list of existing instance pools belonging to the provided resource group
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <returns>A list of instance pool entities</returns>
        public List<AzureSqlInstancePoolModel> ListInstancePools(string resourceGroupName)
        {
            var resp = Communicator.ListInstancePoolsByResourceGroup(resourceGroupName);
            return resp.Select(CreateInstancePoolModelFromResponse).ToList();
        }

        /// <summary>
        /// Removes an existing instance pool
        /// </summary>
        /// <param name="resourceGroupName">The resource group name</param>
        /// <param name="instancePoolName">The instance pool name</param>
        public void RemoveInstancePool(string resourceGroupName, string instancePoolName)
        {
            Communicator.RemoveInstancePool(resourceGroupName, instancePoolName);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates an AzureSqlInstancePoolModel from an InstancePool response
        /// </summary>
        /// <param name="resp">The instance pool response</param>
        /// <returns>An azure sql instance pool model</returns>
        private static AzureSqlInstancePoolModel CreateInstancePoolModelFromResponse(InstancePool resp)
        {
            AzureSqlInstancePoolModel instancePool = new AzureSqlInstancePoolModel();

            string[] segments = resp.Id.Split('/');
            instancePool.ResourceGroupName = segments[4];
            instancePool.InstancePoolName = resp.Name;
            instancePool.Location = resp.Location;
            instancePool.Tags = TagsConversionHelper.CreateTagDictionary(
                TagsConversionHelper.CreateTagHashtable(resp.Tags), false);
            instancePool.SubnetId = resp.SubnetId;
            instancePool.LicenseType = resp.LicenseType;
            instancePool.VCores = resp.VCores;
            instancePool.Type = resp.Type;
            instancePool.ResourceId = resp.Id;
            instancePool.Edition = resp.Sku.Tier;
            instancePool.HardwareFamily = resp.Sku.Family;

            return instancePool;
        }

        #endregion
    }
}
