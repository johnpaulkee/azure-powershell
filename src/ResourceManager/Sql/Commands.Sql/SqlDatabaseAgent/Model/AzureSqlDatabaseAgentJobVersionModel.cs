using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    public class AzureSqlDatabaseAgentJobVersionModel
    {
        public AzureSqlDatabaseAgentJobVersionModel()
        { }

        public string ResourceGroupName { get; set; }

        public string ServerName { get; set; }

        public string AgentName { get; set; }

        public string JobName { get; set; }

        public int Version { get; set; }

        public string ResourceId;

        public string Type;
    }
}