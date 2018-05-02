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
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test min param
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test enabled
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Enabled
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test start and end time
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
    $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
    Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
    Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
    Assert-AreEqual $resp.Enabled $false # defaults to false if not 
    Assert-AreEqual $resp.Description ""

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}

<#
	.SYNOPSIS
	Tests creating a job with input object min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}

<#
	.SYNOPSIS
	Tests creating a job with input object min parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test once
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

    # Test recurring - minute interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $jn1 = Get-JobName
    $resp = New-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}

function Test-GetJob
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    $j1 = Create-JobForTest $a1
    #$j2 = Create-JobForTest $a1

    # Test using default parameters
    $resp = Get-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $j1.JobName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test get all jobs in a1
    #$resp = Get-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    #Assert-AreEqual $resp.Count 2

    # Test using input object
    $resp = Get-AzureRmSqlDatabaseAgentJob -InputObject $a1 -Name $j1.JobName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test get all using input object
    #$resp = Get-AzureRmSqlDatabaseAgentJob -InputObject $a1
    #Assert-AreEqual $resp.Count 2

        # Test using resource id
    $resp = Get-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId -Name $j1.JobName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test get all using resource id
    #$resp = Get-AzureRmSqlDatabaseAgentJob -ResourceId $a1.ResourceId
    #Assert-AreEqual $resp.Count 2

    # Test piping
    $resp = $a1 | Get-AzureRmSqlDatabaseAgentJob -Name $j1.JobName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval
    
    # Test get all from agent piping
    #$resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
    #Assert-AreEqual $resp.Count 2
}

function Test-RemoveJob
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    $j1 = Create-JobForTest $a1
    $j2 = Create-JobForTest $a1
    $j3 = Create-JobForTest $a1
    $j4 = Create-JobForTest $a1

    # Test get all from agent piping
    #$resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
    #Assert-AreEqual $resp.Count 4

    # Test remove using default parameters
    $resp = Remove-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $j1.JobName
    Assert-AreEqual $resp.JobName $j1.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test remove using input object
    $resp = Remove-AzureRmSqlDatabaseAgentJob -InputObject $j2
    Assert-AreEqual $resp.JobName $j2.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test remove using resource id
    $resp = Remove-AzureRmSqlDatabaseAgentJob -ResourceId $j3.ResourceId
    Assert-AreEqual $resp.JobName $j3.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test piping
    $resp = $j4 | Remove-AzureRmSqlDatabaseAgentJob
    Assert-AreEqual $resp.JobName $j4.JobName
    Assert-AreEqual $resp.Enabled $false
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-Null $resp.Interval

    # Test get all from agent piping
    #$resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
    #Assert-AreEqual $resp.Count 0
}

function Test-UpdateJob
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    $j1 = Create-JobForTest $a1
    $jn1 = $j1.JobName

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test enabled
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Enabled
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test start and end time
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
    $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
    Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
    Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description ""
    
    # Test description
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1

    # Test once
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain

    # Test recurring - minute interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent -Name $jn1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}

function Test-UpdateJobWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    $j1 = Create-JobForTest $a1
    $jn1 = $j1.JobName

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test enabled
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Enabled
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test start and end time
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
    $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
    Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
    Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description ""
    
    # Test description
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $jn1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1

    # Test once
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain

    # Test recurring - minute interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}

function Test-UpdateJobWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jpagentserver -AgentName jpagent
    $j1 = Create-JobForTest $a1
    $jn1 = $j1.JobName

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    # Test enabled
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Enabled
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
    Assert-AreEqual $resp.Description ""

    # Test start and end time
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
    $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
    Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
    Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description ""
    
    # Test description
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $jn1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1

    # Test once
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Once
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.ScheduleType "Once"
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain

    # Test recurring - minute interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -MinuteInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
    Assert-AreEqual $resp.Description $jn1 # description should remain
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1M"

    # Test recurring - hour interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $jn1 -HourInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "PT1H"
    
    # Test recurring - day interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $jn1 -DayInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1D"

    # Test recurring - week interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $jn1 -WeekInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1W"
    
    # Test recurring - month interval
    $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $jn1 -MonthInterval 1
    Assert-AreEqual $resp.JobName $jn1
    Assert-AreEqual $resp.Description $jn1
    Assert-AreEqual $resp.ScheduleType "Recurring"
    Assert-AreEqual $resp.Interval "P1M"
}