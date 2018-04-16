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
function Test-CreateJob
{
    Remove-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name job1
    New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name job1 -Description "testDescription"
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