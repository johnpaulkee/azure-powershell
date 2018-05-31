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
#>
function Test-CreateJobWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s

	try
	{
		# Test min param
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test enabled
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Enable
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test once
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -RunOnce
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - minute interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - day interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - week interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - month interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters using agent object parameter sets
#>
function Test-CreateJobWithAgentObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s

	try
	{
		# Test min param
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test enabled
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Enable
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test once
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -RunOnce
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - minute interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - day interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - week interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - month interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentObject $a1 -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters using agent resource id parameter sets
#>
function Test-CreateJobWithAgentResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s

	try
	{
		# Test min param
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test enabled
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Enable
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test once
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -RunOnce
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - minute interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - day interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - week interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - month interval
		$jn1 = Get-JobName
		$resp = New-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters using agent resource id parameter sets
#>
function Test-CreateJobWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s

	try
	{
		# Test min param
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test enabled
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Enable
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test once
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -RunOnce
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Once"   # defaults to once if not specified
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - minute interval
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - day interval
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - week interval
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified

		# Test recurring - month interval
		$jn1 = Get-JobName
		$resp = $a1 | New-AzureRmSqlElasticJob -Name $jn1 -Description $jn1 -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $jn1
		Assert-AreEqual $resp.Description $jn1
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting a job with default params
#>
function Test-GetJobWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test using default parameters
		$resp = Get-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting a job using agent object
#>
function Test-GetJobWithAgentObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test using input object
		$resp = Get-AzureRmSqlElasticJob -AgentObject $a1 -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting job with agent resource id
#>
function Test-GetJobWithAgentResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test using agent resource id
		$resp = Get-AzureRmSqlElasticJob -AgentResourceId $a1.ResourceId -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests get job with piping agent object
#>
function Test-GetJobWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test by piping
		$resp = $a1 | Get-AzureRmSqlElasticJob -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests updating a job with default param
#>
function Test-UpdateJobWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s
	$j1 = Create-JobForTest $a1

	try
	{
		# Test enabled
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Enable
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test disabled
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test start and end time
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		$respStartTimeIso8601 = Get-Date $resp.StartTime -format s
		$respEndTimeIso8601 = Get-Date $resp.EndTime -format s
		Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
		Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description ""

		# Test description
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName

		# Test once
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -RunOnce
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain

		# Test recurring - minute interval
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"

		# Test recurring - day interval
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"

		# Test recurring - week interval
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"

		# Test recurring - month interval
		$resp = Set-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName -Description $j1.JobName -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests updating a job with input object
#>
function Test-UpdateJobWithInputObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s
	$j1 = Create-JobForTest $a1

	try
	{
		 # Test enabled
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Enable
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test disabled
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test start and end time
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		$respStartTimeIso8601 = Get-Date $resp.StartTime -format s
		$respEndTimeIso8601 = Get-Date $resp.EndTime -format s
		Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
		Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description ""

		# Test description
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Description $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName

		# Test once
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -RunOnce
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain

		# Test recurring - minute interval
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Description $j1.JobName -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"

		# Test recurring - day interval
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Description $j1.JobName -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"

		# Test recurring - week interval
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Description $j1.JobName -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"

		# Test recurring - month interval
		$resp = Set-AzureRmSqlElasticJob -InputObject $j1 -Description $j1.JobName -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests updating a job with resource id
#>
function Test-UpdateJobWithResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s

	try
	{
		$j1 = Create-JobForTest $a1

		# Test enabled
		$resp = $j1 | Set-AzureRmSqlElasticJob -Enable
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test disabled
		$resp = $j1 | Set-AzureRmSqlElasticJob
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test start and end time
		$resp = $j1 | Set-AzureRmSqlElasticJob -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		$respStartTimeIso8601 = Get-Date $resp.StartTime -format s
		$respEndTimeIso8601 = Get-Date $resp.EndTime -format s
		Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
		Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description ""

		# Test description
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName

		# Test once
		$resp = $j1 | Set-AzureRmSqlElasticJob -RunOnce
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain

		# Test recurring - minute interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"

		# Test recurring - day interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"

		# Test recurring - week interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"

		# Test recurring - month interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests updating a job with piping
#>
function Test-UpdateJobWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$startTime = Get-Date
	$endTime = $startTime.AddHours(5)
	$startTimeIso8601 =  Get-Date $startTime -format s
	$endTimeIso8601 =  Get-Date $endTime -format s
	$j1 = Create-JobForTest $a1

	try
	{
		# Test enabled
		$resp = $j1 | Set-AzureRmSqlElasticJob -Enable
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $true # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test disabled
		$resp = $j1 | Set-AzureRmSqlElasticJob
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # defaults to false if not specified
		Assert-AreEqual $resp.Description ""

		# Test start and end time
		$resp = $j1 | Set-AzureRmSqlElasticJob -StartTime $startTimeIso8601 -EndTime $endTimeIso8601
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		$respStartTimeIso8601 = Get-Date $resp.StartTime -format s
		$respEndTimeIso8601 = Get-Date $resp.EndTime -format s
		Assert-AreEqual $respStartTimeIso8601 $startTimeIso8601
		Assert-AreEqual $respEndTimeIso8601 $endTimeIso8601
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description ""

		# Test description
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName

		# Test once
		$resp = $j1 | Set-AzureRmSqlElasticJob -RunOnce
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain

		# Test recurring - minute interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -IntervalType Minute -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false # check that enabled was disabled again since Enabled wasn't passed
		Assert-AreEqual $resp.Description $j1.JobName # description should remain
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1M"

		# Test recurring - hour interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Hour -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "PT1H"

		# Test recurring - day interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Day -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1D"

		# Test recurring - week interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Week -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1W"

		# Test recurring - month interval
		$resp = $j1 | Set-AzureRmSqlElasticJob -Description $j1.JobName -IntervalType Month -IntervalCount 1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Description $j1.JobName
		Assert-AreEqual $resp.ScheduleType "Recurring"
		Assert-AreEqual $resp.Interval "P1M"
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing a job with min parameters using default parameter sets, input object parameter sets, and resource id parameter sets
#>
function Test-RemoveJobWithDefaultParam
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test remove using default parameters
		$resp = Remove-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $j1.JobName
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing a job with input object
#>
function Test-RemoveJobWithInputObject
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test remove using default parameters
		$resp = Remove-AzureRmSqlElasticJob -InputObject $j1
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing a job with resource id
#>
function Test-RemoveJobWithResourceId
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$j1 = Create-JobForTest $a1

	try
	{
		# Test remove using resource id
		$resp = Remove-AzureRmSqlElasticJob -ResourceId $j1.ResourceId
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing a job with min parameters using default parameter sets, input object parameter sets, and resource id parameter sets
#>
function Test-RemoveJobWithPiping
{
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		$j1 = Create-JobForTest $a1

		# Test piping
		$resp = $j1 | Remove-AzureRmSqlElasticJob
		Assert-AreEqual $resp.JobName $j1.JobName
		Assert-AreEqual $resp.Enabled $false
		Assert-AreEqual $resp.ScheduleType "Once"
		Assert-Null $resp.Interval
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}