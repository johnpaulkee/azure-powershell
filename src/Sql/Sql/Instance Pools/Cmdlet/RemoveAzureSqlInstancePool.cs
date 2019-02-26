using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Cmdlet
{
    /// <summary>
    /// Defines the Remove-AzSqlInstancePool cmdlet
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlInstancePool",
        SupportsShouldProcess = true)]
    [OutputType(typeof(AzureSqlInstancePoolModel))]
    public class RemoveAzureSqlInstancePool : InstancePoolCmdletBase
    {
        /// <summary>
        /// Gets or sets the name of the resource group to use.
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 1,
            HelpMessage = "The resource group name.")]
        [ResourceGroupCompleter]
        [ValidateNotNullOrEmpty]
        public override string ResourceGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the instance pool to remove.
        /// </summary>
        [Parameter(Mandatory = true,
            Position = 2,
            HelpMessage = "The instance pool name.")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Defines whether it is ok to skip requesting confirmation before removal
        /// </summary>
        [Parameter(HelpMessage = "Skip confirmation message for performing the action")]
        public SwitchParameter Force { get; set; }

        protected override IEnumerable<AzureSqlInstancePoolModel> GetEntity()
        {
            try
            {
                WriteDebugWithTimestamp("InstancePoolName: {0}", Name);
                return new List<AzureSqlInstancePoolModel>
                {
                    ModelAdapter.GetInstancePool(this.ResourceGroupName, this.Name)
                };
            }
            catch (CloudException ex)
            {
                if (ex.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // The instance pool doesn't exist
                    throw new PSArgumentException(
                        string.Format(Properties.Resources.AzureInstancePoolNotExists, this.Name, "InstancePoolName"));
                }

                // Unexpected exception encountered
                throw;
            }
        }

        /// <summary>
        /// Apply user input. Here nothing to apply.
        /// </summary>
        /// <param name="model">The result of GetEntity</param>
        /// <returns>The input model</returns>
        protected override IEnumerable<AzureSqlInstancePoolModel> ApplyUserInputToModel(
            IEnumerable<AzureSqlInstancePoolModel> model)
        {
            return model;
        }

        /// <summary>
        /// Deletes the instance pool
        /// </summary>
        /// <param name="entity">The instance pool to be deleted</param>
        /// <returns>The instance pool that was deleted</returns>
        protected override IEnumerable<AzureSqlInstancePoolModel> PersistChanges(
            IEnumerable<AzureSqlInstancePoolModel> entity)
        {
            var existingEntity = entity.First();
            ModelAdapter.RemoveInstancePool(existingEntity.ResourceGroupName, existingEntity.InstancePoolName);
            return entity;
        }
    }
}
