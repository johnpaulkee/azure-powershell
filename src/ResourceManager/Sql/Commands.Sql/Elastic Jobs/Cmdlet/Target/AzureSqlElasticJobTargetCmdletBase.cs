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

using Microsoft.Azure.Commands.Common.Authentication.Abstractions;
using Microsoft.Azure.Commands.Sql.Common;
using Microsoft.Azure.Commands.Sql.ElasticJobs.Services;
using Microsoft.Azure.Commands.Sql.ElasticJobs.Model;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Azure.Management.Sql.Models;

namespace Microsoft.Azure.Commands.Sql.ElasticJobs.Cmdlet
{
    /// <summary>
    /// The elastic job target cmdlet base
    /// </summary>
    /// <typeparam name="IO">The input object model</typeparam>
    public abstract class AzureSqlElasticJobTargetCmdletBase<IO> :
        AzureSqlElasticJobsCmdletBase<IO, IEnumerable<AzureSqlElasticJobTargetModel>, AzureSqlElasticJobAdapter>
    {
        /// <summary>
        /// Parameter sets name for default target group db, server, elastic pool, and shard map
        /// </summary>
        protected const string DefaultSqlDatabaseSet = "Sql Database Target Type";
        protected const string DefaultSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Target Type";
        protected const string DefaultSqlShardMapSet = "Sql Shard Map Target Type";

        /// <summary>
        /// Parameter sets for target group object db, server, elastic pool, and shard map
        /// </summary>
        protected const string TargetGroupObjectSqlDatabaseSet = "Sql Database Input Object Parameter Set";
        protected const string TargetGroupObjectSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool Input Object Parameter Set";
        protected const string TargetGroupObjectSqlShardMapSet = "Sql Shard Map Input Object Parameter Set";

        /// <summary>
        /// Parameter sets for target group resource id db, server, pool, and shard map
        /// </summary>
        protected const string TargetGroupResourceIdSqlDatabaseSet = "Sql Database TargetGroupResourceId Parameter Set";
        protected const string TargetGroupResourceIdSqlServerOrElasticPoolSet = "Sql Server or Elastic Pool TargetGroupResourceId Parameter Set";
        protected const string TargetGroupResourceIdSqlShardMapSet = "Sql Shard Map TargetGroupResourceId Parameter Set";

        /// <summary>
        /// The target in question
        /// </summary>
        protected AzureSqlElasticJobTargetModel Target;

        /// <summary>
        /// The existing targets
        /// </summary>
        protected List<AzureSqlElasticJobTargetModel> ExistingTargets;

        /// <summary>
        /// Flag to determine whether an update to targets in target group is needed in this powershell session
        /// </summary>
        protected bool NeedsUpdate;

        /// <summary>
        /// Gets or sets the switch parameter for whether or not this target will be excluded.
        /// </summary>
        public virtual SwitchParameter Exclude { get; set; }

        /// <summary>
        /// Intializes the model adapter
        /// </summary>
        /// <param name="subscription">The subscription the cmdlets are operation under</param>
        /// <returns>The Azure Elastic Job Adapter</returns>
        protected override AzureSqlElasticJobAdapter InitModelAdapter(IAzureSubscription subscription)
        {
            return new AzureSqlElasticJobAdapter(DefaultContext);
        }


        /// <summary>
        /// Helper for determining based on parameter set what target type this target should be.
        /// </summary>
        /// <returns>The target type</returns>
        public string GetTargetType()
        {
            if (this.ShardMapName != null)
            {
                return JobTargetType.SqlShardMap;
            }

            if (this.ElasticPoolName != null)
            {
                return JobTargetType.SqlElasticPool;
            }

            if (this.DatabaseName != null)
            {
                return JobTargetType.SqlDatabase;
            }

            return JobTargetType.SqlServer;
        }

        /// <summary>
        /// Does a scan over the list of targets and finds the target's index in the list
        /// </summary>
        protected int? FindTarget()
        {
            for (int i = 0; i < this.ExistingTargets.Count; i++)
            {
                AzureSqlElasticJobTargetModel t = this.ExistingTargets[i];

                if (t.TargetServerName == this.Target.TargetServerName &&
                    t.TargetDatabaseName == this.Target.TargetDatabaseName &&
                    t.TargetElasticPoolName == this.Target.TargetElasticPoolName &&
                    t.TargetShardMapName == this.Target.TargetShardMapName &&
                    t.TargetType == this.Target.TargetType &&
                    t.RefreshCredentialName == this.Target.RefreshCredentialName)
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Abstract method when adding or removing targets
        /// </summary>
        /// <returns></returns>
        protected abstract bool UpdateExistingTargets();
    }
}