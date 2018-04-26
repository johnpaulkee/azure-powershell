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
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test min param
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test enabled
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Enabled
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"
    Assert-AreEqual $resp.Schedule.Enabled $true # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test start and end time
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"
    $respStartTimeIso8601 = Get-Date $resp.Schedule.StartTime -format s
    $respEndTimeIso8601 = Get-Date $resp.Schedule.EndTime -format s
    Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
    Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not 
    Assert-AreEqual $resp.Description ""

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Schedule.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1M"
}

<#
	.SYNOPSIS
	Tests creating a job with input object min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Schedule.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1M"
}

<#
	.SYNOPSIS
	Tests creating a job with input object min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Schedule.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.Schedule.Type "Recurring"
    Assert-AreEqual $resp.Schedule.Interval "P1M"
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