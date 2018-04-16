using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Azure.Commands.Sql.SqlDatabaseAgent.Model
{
    class AzureSqlDatabaseAgentJobExecutionModel
    {
        public string ResourceGroupName;
        public string ServerName;
        public string AgentName;
        public string JobName;
        public string JobVersion;
        public string JobExecutionId;
        public string Lifecycle;
        public string ProvisioningState;
        public DateTime CreateTime;
        public int CurrentAttempts;
        public string LastMessage;
        public string ResourceId;
        public string Type;
        public int StepId;
        public DateTime CurrentAttemptStartTime;

    }
}

                //"jobVersion":1,
                //"stepName":"step1",
                //"stepId":1,
                //"jobExecutionId":"9c20948b-f370-4e4f-b5a4-d07be4309935",
                //"lifecycle":"Succeeded",
                //"provisioningState":"Succeeded",
                //"createTime":"2017-12-03T04:33:17.5133333Z",
                //"startTime":"2017-12-03T04:33:18.1230403Z",
                //"endTime":"2017-12-03T04:33:18.7031029Z",
                //"currentAttempts":1,
                //"currentAttemptStartTime":"2017-12-03T04:33:18.2391013Z",
                //"lastMessage":"Step 1 succeeded execution on target (server 'server1', database 'database1').",
                //"target":{
                //    "type":"SqlDatabase",
                //    "serverName":"server1",
                //    "databaseName":"database1"
                //}