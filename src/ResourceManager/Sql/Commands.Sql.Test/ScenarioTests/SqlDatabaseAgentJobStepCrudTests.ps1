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
function Test-CreateJobStep
{
    New-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -JobName job1 -StepName step2 -TargetGroupName tg1 -CredentialName cred1 -CommandText "SELECT 1"

    New-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -JobName job1 -StepName step3 -TargetGroupName tg1 -CredentialName cred1 -CommandText "SELECT 1" -StepId 1 -TimeoutSeconds 1234 -RetryAttempts 42 -InitialRetryIntervalSeconds 11 -MaximumRetryIntervalSeconds -222 -RetryIntervalBackoffMultiplier 3.0
}

<#
	.SYNOPSIS
	Tests creating a job with input object min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithInputObject
{
    $a = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent
    New-AzureRmSqlDatabaseAgentJob -InputObject $a -Name job1
}

<#
	.SYNOPSIS
	Tests creating a job with resource id min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithResourceId
{
    $a = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent
    New-AzureRmSqlDatabaseAgentJob -ResourceId $a.ResourceId -Name job1
}


function Test-RemoveJob
{
    Remove-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name job1
}

function Test-UpdateJob
{
    Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name job1
}

function Test-GetJob
{

}