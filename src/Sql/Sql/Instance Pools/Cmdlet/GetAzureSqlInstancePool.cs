using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Cmdlet
{
    /// <summary>
    /// Defines the Get-AzRmSqlInstancePool cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Get, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlInstancePool")]
    public class GetAzureSqlInstancePool : InstancePoolCmdletBase
    {
        /// <summary>
        /// Gets or sets the resource group name
        /// </summary>
        [Parameter(
            Mandatory = true,
            Position = 0,
            HelpMessage = "The resource group name.")]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the instance pool name
        /// </summary>
        [Parameter(
            Position = 1,
            HelpMessage = "The managed instance pool name.")]
        [Alias("InstancePoolName")]
        [ResourceNameCompleter("Microsoft.Sql/instancePools", "ResourceGroupName")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets an instance pool from the service.
        /// </summary>
        /// <returns>A single instance pool</returns>
        protected override IEnumerable<AzureSqlInstancePoolModel> GetEntity()
        {
            ICollection<AzureSqlInstancePoolModel> results = null;

            // Returns a list of instance pools
            if (this.Name == null)
            {
                results = ModelAdapter.ListInstancePools(this.ResourceGroupName);
            }
            else
            {
                results = new List<AzureSqlInstancePoolModel>();
                results.Add(ModelAdapter.GetInstancePool(this.ResourceGroupName, this.Name));
            }

            return results;
        }

        /// <summary>
        /// No changes, nothing to persist
        /// </summary>
        /// <param name="entity">The entity retrieved</param>
        /// <returns>The unchanged entity</returns>
        protected override IEnumerable<AzureSqlInstancePoolModel> PersistChanges(
            IEnumerable<AzureSqlInstancePoolModel> entity)
        {
            return entity;
        }

        /// <summary>
        /// No user input to apply to the model
        /// </summary>
        /// <param name="model">The model to modify</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlInstancePoolModel> ApplyUserInputToModel(
            IEnumerable<AzureSqlInstancePoolModel> model)
        {
            return model;
        }
    }
}