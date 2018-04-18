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
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1

    $j1 = "job1"
    $j2 = "job2"

    $ct = (Get-Date).ToUniversalTime()


    try
    {
        # Test create min with default parameters
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.JobName $j1

        # Test create max with default parameters
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j2 -Description $j2 -StartTime -EndTime -Once
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j2 -Description $j2 -StartTime -EndTime -MinuteInterval 1
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j2 -Description $j2 -StartTime -EndTime -HourInterval 1
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j2 -Description $j2 -StartTime -EndTime -DayInterval 1
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j2 -Description $j2 -StartTime -EndTime -MonthInterval 1


        New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $j1



    }
    finally
    {
    
    }

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