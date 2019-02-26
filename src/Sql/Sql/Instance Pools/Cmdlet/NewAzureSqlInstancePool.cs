using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Cmdlet
{
    [Cmdlet(VerbsCommon.New, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlInstancePool",
        SupportsShouldProcess = true)]
    public class NewAzureSqlInstancePool : InstancePoolCmdletBase
    {
        [Parameter(Mandatory = true, HelpMessage = "The instance pool resource group")]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the instance pool name
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The instance pool name")]
        [ValidateNotNullOrEmpty]
        [Alias("InstancePoolName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the instance pool location
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The location in which to create the instance pool.")]
        [LocationCompleter("Microsoft.Sql/instancePools")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the instance pool subnet id
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The subnet id to use for the instance pool.")]
        [ValidateNotNullOrEmpty]
        public string SubnetId { get; set; }

        /// <summary>
        /// Gets or sets the instance pool license type
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "Determines the instance pool license type. Possible values are BasePrice (with AHB discount) and LicenseIncluded (without AHB discount).")]
        [PSArgumentCompleter(Constants.LicenseTypeBasePrice, Constants.LicenseTypeLicenseIncluded)]
        public string LicenseType { get; set; }

        /// <summary>
        /// Gets or sets the instance pool vCores
        /// </summary>
        [Parameter(Mandatory = true,
            HelpMessage = "Determines how much vCore to associate with the instance pool.")]
        [ValidateNotNullOrEmpty]
        public int VCore { get; set; }

        /// <summary>
        /// Gets or the sets the instance pool edition
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The instance pool edition.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.GeneralPurposeEdition)]
        public string Edition { get; set; }

        /// <summary>
        /// Gets or sets the instance pool compute generation
        /// </summary>
        [Parameter(Mandatory = true, HelpMessage = "The instance pool compute generation or hardware family.")]
        [ValidateNotNullOrEmpty]
        [PSArgumentCompleter(Constants.ComputeGenerationGen5)]
        public string ComputeGeneration { get; set; }

        /// <summary>
        /// Gets or sets the instance pool tags
        /// </summary>
        [Parameter(Mandatory = false,
            HelpMessage = "The tags to associate with the instance pool")]
        [ValidateNotNullOrEmpty]
        [Alias("Tags")]
        public Hashtable Tag { get; set; }

        protected override IEnumerable<AzureSqlInstancePoolModel> GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("InstancePoolName: {0}", Name);
                ModelAdapter.GetInstancePool(this.ResourceGroupName, this.Name);
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // This is what we want.  We looked and there is no agent with this name.
                    return null;
                }

                // Unexpected exception encountered
                throw;
            }

            throw new PSArgumentException(
                string.Format(Properties.Resources.AzureInstancePoolExists,
                    this.Name), "InstancePoolName");
        }

        protected override IEnumerable<AzureSqlInstancePoolModel> ApplyUserInputToModel
            (IEnumerable<AzureSqlInstancePoolModel> model)
        {
            AzureSqlInstancePoolModel newEntity = new AzureSqlInstancePoolModel
            {
                Location = this.Location,
                ResourceGroupName = this.ResourceGroupName,
                InstancePoolName = this.Name,
                Tags = TagsConversionHelper.CreateTagDictionary(Tag, validate: true),
                Edition = this.Edition,
                HardwareFamily = this.ComputeGeneration,
                LicenseType = this.LicenseType,
                SubnetId = this.SubnetId,
                VCores = this.VCore
            };

            return new List<AzureSqlInstancePoolModel> { newEntity };
        }

        protected override IEnumerable<AzureSqlInstancePoolModel> PersistChanges(
            IEnumerable<AzureSqlInstancePoolModel> entity)
        {
            return new List<AzureSqlInstancePoolModel>
            {
                ModelAdapter.UpsertInstancePool(entity.First())
            };
        }
    }
}