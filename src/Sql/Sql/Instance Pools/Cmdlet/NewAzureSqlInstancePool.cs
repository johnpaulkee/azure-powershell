using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Commands.ResourceManager.Common.Tags;
using Microsoft.Azure.Commands.Sql.Instance_Pools.Model;
using Microsoft.Rest.Azure;

namespace Microsoft.Azure.Commands.Sql.Instance_Pools.Cmdlet
{
    [Cmdlet(VerbsCommon.New, ResourceManager.Common.AzureRMConstants.AzureRMPrefix + "SqlInstancePool",
        SupportsShouldProcess = true)]
    public class NewAzureSqlInstancePool : InstancePoolCmdletBase
    {
        [Parameter] public override string ResourceGroupName { get; set; }

        [Parameter]
        [ValidateNotNullOrEmpty]
        [Alias("InstancePoolName")]
        public string Name { get; set; }

        [Parameter(Mandatory = true,
            HelpMessage = "The location in which to create the instance pool.")]
        [LocationCompleter("Microsoft.Sql/instancePools")]
        public string Location { get; set; }

        [Parameter]
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
                string.Format(Properties.Resources.AzureElasticJobAgentExists,
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
                Tags = TagsConversionHelper.CreateTagDictionary(Tag, validate: true)
            };

            return new List<AzureSqlInstancePoolModel> { };
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