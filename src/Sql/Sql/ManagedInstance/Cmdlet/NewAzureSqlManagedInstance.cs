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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.ManagedInstance.Model;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Rest.Azure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Security;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Azure.Management.Internal.Resources.Utilities.Models;

namespace Microsoft.Azure.Commands.Sql.ManagedInstance.Cmdlet
{
    /// <summary>
    /// Defines the New-AzSqlInstance cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.New, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlInstance",
        DefaultParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
        SupportsShouldProcess = true),
        OutputType(typeof(AzureSqlManagedInstanceModel))]
    public class NewAzureSqlManagedInstance : ManagedInstanceCmdletBase
    {
        /// <summary>
        /// Parameter sets
        /// </summary>
        protected const string NewInstanceByInstancePoolParentObject = "NewInstanceByInstancePoolParentObject";
        protected const string NewInstanceByInstancePoolResourceIdentifer = "NewInstanceByInstancePoolResourceIdentifier";
        protected const string DefaultNewBySkuNameParameterSet = "DefaultNewBySkuNameParameterSetParameter";
        protected const string DefaultNewByEditionAndComputeGenerationParameterSet = "DefaultNewByEditionAndComputeGenerationParameterSet";

        /// <summary>
        /// Gets or sets the instance pool parent object
        /// </summary>
        [Parameter(
            ParameterSetName = NewInstanceByInstancePoolParentObject,
            Mandatory = true,
            ValueFromPipeline = true,
            HelpMessage = "The instance pool parent object.")]
        [Alias("ParentObject")]
        [ValidateNotNullOrEmpty]
        public AzureSqlInstancePoolModel InstancePool { get; set; }

        /// <summary>
        /// Gets or sets the instance pool resource identifier
        /// </summary>
        [Parameter(
            ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "The instance pool resource identifier.")]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource group to use.
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultNewBySkuNameParameterSet,
            Mandatory = true,
            HelpMessage = "The name of the resource group.")]
        [Parameter(
            ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            Mandatory = true,
            HelpMessage = "The name of the resource group.")]
        [ResourceGroupCompleter]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the instance.
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The name of the instance.")]
        [Alias("InstanceName")]
        [ResourceNameCompleter("Microsoft.Sql/managedInstances", "ResourceGroupName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the admin credential of the instance
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The SQL authentication credential of the instance.")]
        [ValidateNotNull]
        public PSCredential AdministratorCredential { get; set; }

        /// <summary>
        /// The location in which to create the instance
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultNewBySkuNameParameterSet,
            Mandatory = true,
            HelpMessage = "The location in which to create the instance.")]
        [Parameter(
            ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            Mandatory = true,
            HelpMessage = "The location in which to create the instance.")]
        [Parameter(
            ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            Mandatory = true,
            HelpMessage = "The location in which to create the instance.")]
        [LocationCompleter("Microsoft.Sql/locations/managedInstances")]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the instance Subnet Id
        /// </summary>
        [Parameter(Mandatory = true,
            ParameterSetName = DefaultNewBySkuNameParameterSet,
            HelpMessage = "The Subnet Id to use for instance creation.")]
        [Parameter(Mandatory = true,
            ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            HelpMessage = "The Subnet Id to use for instance creation.")]
        [Parameter(Mandatory = true,
            ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            HelpMessage = "The Subnet Id to use for instance creation.")]
        [ValidateNotNullOrEmpty]
        public string SubnetId { get; set; }

        /// <summary>
        /// Gets or sets the instance License Type
        /// </summary>
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultNewBySkuNameParameterSet,
            HelpMessage = "Determines which License Type to use. Possible values are BasePrice (with AHB discount) and LicenseIncluded (without AHB discount).")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            HelpMessage = "Determines which License Type to use. Possible values are BasePrice (with AHB discount) and LicenseIncluded (without AHB discount).")]
        [Parameter(
            Mandatory = true,
            ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            HelpMessage = "Determines which License Type to use. Possible values are BasePrice (with AHB discount) and LicenseIncluded (without AHB discount).")]
        [PSArgumentCompleter(Constants.LicenseTypeBasePrice, Constants.LicenseTypeLicenseIncluded)]
        public string LicenseType { get; set; }

        /// <summary>
        /// Gets or sets the Storage Size in GB for instance
        /// </summary>
        [Parameter(
            Mandatory = true,
            HelpMessage = "Determines how much Storage size to associate with instance.")]
        [ValidateNotNullOrEmpty]
        public int StorageSizeInGB { get; set; }

        /// <summary>
        /// Gets or sets the VCore for instance
        /// </summary>
        [Parameter(
            Mandatory = true,
            HelpMessage = "Determines how much VCore to associate with instance.")]
        [ValidateNotNullOrEmpty]
        public int VCore { get; set; }

        /// <summary>
        /// Gets or sets the instance SKU name
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultNewBySkuNameParameterSet,
            Mandatory = true,
            HelpMessage = "The SKU name for the instance e.g. 'GP_Gen4', 'BC_Gen4'.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.GeneralPurposeGen4, Constants.GeneralPurposeGen5, Constants.BusinessCriticalGen4, Constants.BusinessCriticalGen5)]
        public string SkuName { get; set; }

        /// <summary>
        /// Gets or sets the instance edition
        /// </summary>
        [Parameter(
            ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            Mandatory = true,
            HelpMessage = "The edition for the instance.")]
        [Parameter(
            ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            Mandatory = true,
            HelpMessage = "The edition for the instance.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.GeneralPurposeEdition, Constants.BusinessCriticalEdition)]
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the instance compute generation
        /// </summary>
        [Parameter(ParameterSetName = DefaultNewByEditionAndComputeGenerationParameterSet,
            Mandatory = true,
            HelpMessage = "The compute generation for the instance.")]
        [Parameter(ParameterSetName = NewInstanceByInstancePoolResourceIdentifer,
            Mandatory = true,
            HelpMessage = "The compute generation for the instance.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.ComputeGenerationGen4, Constants.ComputeGenerationGen5)]
        public string ComputeGeneration { get; set; }

        /// <summary>
        /// Gets or sets the instance collation
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "The collation of the Azure SQL Managed Instance to use.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.CollationSqlLatin1, Constants.CollationLatin1)]
        public string Collation { get; set; }

        /// <summary>
        /// Gets or sets the tags to associate with the instance
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "The tags to associate with the instance")]
        [Alias("Tags")]
        public Hashtable Tag { get; set; }

        /// <summary>
        /// Gets or sets whether or not to assign identity for instance
        /// </summary>
        [Parameter(
            Mandatory = false,
            HelpMessage = "Generate and assign an Azure Active Directory Identity for this instance for use with key management services like Azure KeyVault.")]
        public SwitchParameter AssignIdentity { get; set; }

        /// <summary>
        /// Gets or sets whether to place this instance in an instance pool
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "The instance pool name")]
        public string InstancePoolName { get; set; }

        /// <summary>
        /// Gets or sets whether this instance should have public data endpoint enabled
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Flag indicating whether this instance should have a public data endpoint")]
        public SwitchParameter PublicDataEndpointEnabled { get; set; }

        /// <summary>
        /// Gets or sets whether or not to run this cmdlet in the background as a job
        /// </summary>
        [Parameter(Mandatory = false, HelpMessage = "Run cmdlet in the background")]
        public SwitchParameter AsJob { get; set; }

        /// <summary>
        /// Overriding to add warning message
        /// </summary>
        public override void ExecuteCmdlet()
        {
            if (this.InstancePool != null)
            {
                this.ResourceGroupName = this.InstancePool.ResourceGroupName;
                this.InstancePoolName = this.InstancePool.InstancePoolName;
                this.SubnetId = this.InstancePool.SubnetId;
                this.Edition = this.InstancePool.Edition;
                this.ComputeGeneration = this.InstancePool.ComputeGeneration;
                this.Location = this.InstancePool.Location;
                this.LicenseType = this.InstancePool.LicenseType;
            }
            else if (this.ResourceId != null)
            {
                var instancePoolResourceIdentifier = new ResourceIdentifier(this.ResourceId);
                this.ResourceGroupName = instancePoolResourceIdentifier.ResourceGroupName;
                this.InstancePoolName = instancePoolResourceIdentifier.ResourceName;
                var instancePool = ModelAdapter.GetInstancePool(this.ResourceGroupName, this.InstancePoolName);
                this.SubnetId = instancePool.SubnetId;
                this.Edition = instancePool.Edition;
                this.ComputeGeneration = instancePool.ComputeGeneration;
                this.Location = instancePool.Location;
                this.LicenseType = instancePool.LicenseType;
            }

            base.ExecuteCmdlet();
        }

        /// <summary>
        /// Check to see if the instance already exists in this resource group.
        /// </summary>
        /// <returns>Null if the instance doesn't exist.  Otherwise throws exception</returns>
        protected override IEnumerable<AzureSqlManagedInstanceModel> GetEntity()
        {
            try
            {
                ModelAdapter.GetManagedInstance(this.ResourceGroupName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no instance with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            // The instance already exists
            throw new PSArgumentException(
                string.Format(Properties.Resources.ServerNameExists, this.Name),
                "Name");
        }

        /// <summary>
        /// Generates the model from user input.
        /// </summary>
        /// <param name="model">This is null since the instance doesn't exist yet</param>
        /// <returns>The generated model from user input</returns>
        protected override IEnumerable<AzureSqlManagedInstanceModel> ApplyUserInputToModel(IEnumerable<AzureSqlManagedInstanceModel> model)
        {
            List<AzureSqlManagedInstanceModel> newEntity = new List<AzureSqlManagedInstanceModel>();
            Management.Internal.Resources.Models.Sku Sku = new Management.Internal.Resources.Models.Sku();

            if (string.Equals(this.ParameterSetName, DefaultNewBySkuNameParameterSet, System.StringComparison.OrdinalIgnoreCase))
            {
                Sku.Name = SkuName;
            }
            else if (this.ParameterSetName == DefaultNewByEditionAndComputeGenerationParameterSet ||
                     this.ParameterSetName == NewInstanceByInstancePoolParentObject ||
                     this.ParameterSetName == NewInstanceByInstancePoolResourceIdentifer)
            {
                string editionShort = Edition.Equals(Constants.GeneralPurposeEdition) ? "GP" : Edition.Equals(Constants.BusinessCriticalEdition) ? "BC" : "Unknown";
                Sku.Name = editionShort + "_" + ComputeGeneration;
            }

            newEntity.Add(new AzureSqlManagedInstanceModel()
            {
                Location = this.Location,
                ResourceGroupName = this.ResourceGroupName,
                FullyQualifiedDomainName = this.Name,
                AdministratorLogin = this.AdministratorCredential.UserName,
                AdministratorPassword = this.AdministratorCredential.Password,
                Tags = TagsConversionHelper.CreateTagDictionary(Tag, validate: true),
                Identity = ResourceIdentityHelper.GetIdentityObjectFromType(this.AssignIdentity.IsPresent),
                LicenseType = this.LicenseType,
                StorageSizeInGB = this.StorageSizeInGB,
                SubnetId = this.SubnetId,
                VCores = this.VCore,
                Sku = Sku,
                Collation = this.Collation,
                InstancePoolName = this.InstancePoolName,
                PublicDataEndpointEnabled = this.PublicDataEndpointEnabled.IsPresent
            });
            return newEntity;
        }

        /// <summary>
        /// Sends the changes to the service -> Creates the instance
        /// </summary>
        /// <param name="entity">The instance to create</param>
        /// <returns>The created instance</returns>
        protected override IEnumerable<AzureSqlManagedInstanceModel> PersistChanges(IEnumerable<AzureSqlManagedInstanceModel> entity)
        {
            return new List<AzureSqlManagedInstanceModel>() {
                ModelAdapter.UpsertManagedInstance(entity.First())
            };
        }
    }
}
