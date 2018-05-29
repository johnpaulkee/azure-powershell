# ----------------------------------------------------------------------------------
#
# Copyright Microsoft Corporation
# Licensed under the Apache License, Version 2.0 (the "License");
# you may not use this file except in compliance with the License.
# You may obtain a copy of the License at
# http://www.apache.org/licenses/LICENSE-2.0
# Unless required by applicable law or agreed to in writing, software
# distributed under the License is distributed on an "AS IS" BASIS,
# WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
# See the License for the specific language governing permissions and
# limitations under the License.
# ----------------------------------------------------------------------------------

<#
	.SYNOPSIS
	Tests creating a job with min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-StartJobExecution
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-StartJobExecutionWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-StartJobExecutionWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobExecution
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
    
    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName

    $all = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Steps
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Targets

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName -Targets
}

function Test-GetJobExecutionWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
    
    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName

    $all = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Steps
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Targets

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName -Targets
}

function Test-GetJobExecutionWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
    
    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName

    $all = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Steps
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Targets

    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName
    $resp = Get-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName -Targets
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-StopJobExecution
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

    $je = Start-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName
    $je = Stop-AzureRmSqlDatabaseAgentJobExecution -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
}