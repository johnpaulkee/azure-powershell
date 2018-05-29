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
	Tests creating a job with min parameters using default parameter sets
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJob
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1
   
    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s
    
    try
    {
        # Test min param
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test enabled
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Enable
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test once
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -RunOnce
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - minute interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - day interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - week interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - month interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters using agent object parameter sets
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithAgentObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1
   
    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s
    
    try
    {
        # Test min param
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test enabled
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Enable
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test once
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -RunOnce
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - minute interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - day interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - week interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - month interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters using agent resource id parameter sets
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobWithAgentResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    try
    {
        # Test min param
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test enabled
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Enable
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test once
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -RunOnce
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - minute interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - day interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

        # Test recurring - week interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    
        # Test recurring - month interval
        $jn1 = Get-JobName
        $resp = New-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $jn1
        Assert-AreEqual $resp.Description $jn1
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests getting a job with min parameters using default parameter sets, input object parameter sets, and resource id parameter sets
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJob
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        $j1 = Create-JobForTest $a1
        $j2 = Create-JobForTest $a1

        # Test using default parameters
        $resp = Get-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-Null $resp.Interval

        # Test get all jobs in a1
        $resp = Get-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName
        Assert-AreEqual $resp.Count 2

        # Test using input object
        $resp = Get-AzureRmSqlDatabaseAgentJob -AgentObject $a1 -Name $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-Null $resp.Interval

        # Test get all using input object
        $resp = Get-AzureRmSqlDatabaseAgentJob -AgentObject $a1
        Assert-AreEqual $resp.Count 2

        # Test using resource id
        $resp = Get-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId -Name $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-Null $resp.Interval

        # Test get all using resource id
        $resp = Get-AzureRmSqlDatabaseAgentJob -AgentResourceId $a1.ResourceId
        Assert-AreEqual $resp.Count 2

        # Test piping
        $resp = $a1 | Get-AzureRmSqlDatabaseAgentJob -Name $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-Null $resp.Interval
    
        # Test get all from agent piping
        $resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
        Assert-AreEqual $resp.Count 2
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing a job with min parameters using default parameter sets, input object parameter sets, and resource id parameter sets
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJob
{
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        $j1 = Create-JobForTest $a1
        $j2 = Create-JobForTest $a1
        $j3 = Create-JobForTest $a1
        $j4 = Create-JobForTest $a1

        # Test get all from agent piping
        $resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
        Assert-AreEqual $resp.Count 4

        # Test remove using default parameters
        $resp = Remove-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
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
        $resp = $a1 | Get-AzureRmSqlDatabaseAgentJob
        Assert-AreEqual $resp.Count 0
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }    
}

function Test-UpdateJob
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    try
    {
        $j1 = Create-JobForTest $a1

        # Test enabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Enable
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test disabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test start and end time
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
        $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
        Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
        Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description ""
    
        # Test description
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName

        # Test once
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -RunOnce
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain

        # Test recurring - minute interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
    
        # Test recurring - day interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"

        # Test recurring - week interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
    
        # Test recurring - month interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

function Test-UpdateJobWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    try
    {
        $j1 = Create-JobForTest $a1

         # Test enabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Enable
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test disabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test start and end time
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
        $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
        Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
        Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description ""
    
        # Test description
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName

        # Test once
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -RunOnce
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain

        # Test recurring - minute interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $j1.JobName -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
    
        # Test recurring - day interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $j1.JobName -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"

        # Test recurring - week interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $j1.JobName -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
    
        # Test recurring - month interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -InputObject $j1 -Description $j1.JobName -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

function Test-UpdateJobWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    $startTime = Get-Date
    $endTime = $startTime.AddHours(5)
    $startTimeIso8601 =  Get-Date $startTime -format s
    $endTimeIso8601 =  Get-Date $endTime -format s

    try
    {
        $j1 = Create-JobForTest $a1

        # Test enabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Enable
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test disabled
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
        Assert-AreEqual $resp.Description ""

        # Test start and end time
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        $respStartTimeIso8601 = Get-Date $resp.StartTime -format s
        $respEndTimeIso8601 = Get-Date $resp.EndTime -format s
        Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
        Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description ""
    
        # Test description
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $j1.JobName
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName

        # Test once
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -RunOnce
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Once"
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain

        # Test recurring - minute interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -IntervalType Minute -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
        Assert-AreEqual $resp.Description $j1.JobName # description should remain
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1M"

        # Test recurring - hour interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $j1.JobName -IntervalType Hour -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "PT1H"
    
        # Test recurring - day interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $j1.JobName -IntervalType Day -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1D"

        # Test recurring - week interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $j1.JobName -IntervalType Week -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1W"
    
        # Test recurring - month interval
        $resp = Set-AzureRmSqlDatabaseAgentJob -ResourceId $j1.ResourceId -Description $j1.JobName -IntervalType Month -IntervalCount 1
        Assert-AreEqual $resp.JobName $j1.JobName
        Assert-AreEqual $resp.Description $j1.JobName
        Assert-AreEqual $resp.ScheduleType "Recurring"
        Assert-AreEqual $resp.Interval "P1M"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}