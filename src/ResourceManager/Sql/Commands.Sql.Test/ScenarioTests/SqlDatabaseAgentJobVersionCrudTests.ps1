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
	Tests getting job versions for a job
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobVersion
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1

    $ct1 = "SELECT 1"
    $ct2 = "SELECT 2"
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1
    $js2 = Create-JobStepForTest $j1 $tg1 $jc1 $ct2

    # Get with default params
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -ResourceGroupName $j1.ResourceGroupName -ServerName $j1.ServerName -AgentName $j1.AgentName -JobName $j1.JobName -Version 1
    Assert-AreEqual $resp.ResourceGroupName $j1.ResourceGroupName
    Assert-AreEqual $resp.ServerName $j1.ServerName
    Assert-AreEqual $resp.AgentName $j1.AgentName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Version 1

    # Get all with default params
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -ResourceGroupName $j1.ResourceGroupName -ServerName $j1.ServerName -AgentName $j1.AgentName -JobName $j1.JobName
    Assert-AreEqual $resp.Count 2

    # Get with job input object
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -InputObject $j1 -Version 1
    Assert-AreEqual $resp.ResourceGroupName $j1.ResourceGroupName
    Assert-AreEqual $resp.ServerName $j1.ServerName
    Assert-AreEqual $resp.AgentName $j1.AgentName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Version 1

    # Get all with job input object
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -InputObject $j1
    Assert-AreEqual $resp.Count 2

    # Get with job resource id
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -ResourceId $j1.ResourceId -Version 1
    Assert-AreEqual $resp.ResourceGroupName $j1.ResourceGroupName
    Assert-AreEqual $resp.ServerName $j1.ServerName
    Assert-AreEqual $resp.AgentName $j1.AgentName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Version 1

    # Get all with default params
    $resp = Get-AzureRmSqlDatabaseAgentJobVersion -ResourceId $j1.ResourceId
    Assert-AreEqual $resp.Count 2

    # Test piping
    $resp = $j1 | Get-AzureRmSqlDatabaseAgentJobVersion -Version 1
    Assert-AreEqual $resp.ResourceGroupName $j1.ResourceGroupName
    Assert-AreEqual $resp.ServerName $j1.ServerName
    Assert-AreEqual $resp.AgentName $j1.AgentName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Version 1

    # Get all versions with piping from job
    $resp = $j1 | Get-AzureRmSqlDatabaseAgentJobVersion
    Assert-AreEqual $resp.Count 2
}