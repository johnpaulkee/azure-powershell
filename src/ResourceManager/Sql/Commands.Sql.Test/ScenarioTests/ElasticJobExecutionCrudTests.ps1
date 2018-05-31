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
	Tests starting a job through all options
#>
function Test-StartJob()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		Test-StartJobWithDefaultParam $a1
		Test-StartJobWithJobObject $a1
		Test-StartJobWithJobResourceId $a1
		Test-StartJobWithPiping $a1
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests stopping a job through all options
#>
function Test-StopJob()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		Test-StopJobWithDefaultParam $a1
		Test-StopJobWithJobExecutionObject $a1
		Test-StopJobWithJobExecutionResourceId $a1
		Test-StopJobWithPiping $a1
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting job executions through all options
#>
function Test-GetJobExecution()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		#Test-GetJobExecutionWithDefaultParam $a1
		#Test-GetJobExecutionWithAgentObject $a1
		#Test-GetJobExecutionWithAgentResourceId $a1
		Test-GetJobExecutionWithPiping $a1
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting job step executions through all options
#>
function Test-GetJobStepExecution()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		Test-GetJobStepExecutionWithDefaultParam $a1
		Test-GetJobStepExecutionWithJobExecutionObject $a1
		Test-GetJobStepExecutionWithJobExecutionResourceId $a1
		Test-GetJobStepExecutionWithPiping $a1
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests getting job target executions through all options
#>
function Test-GetJobTargetExecution()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	try
	{
		Test-GetJobTargetExecutionWithDefaultParam $a1
		Test-GetJobTargetExecutionWithJobExecutionObject $a1
		Test-GetJobTargetExecutionWithJobExecutionResourceId $a1
		Test-GetJobTargetExecutionWithPiping $a1
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests starting a job with default parameters
#>
function Test-StartJobWithDefaultParam ($a1)
{
	$script = "SELECT 1"
	# Setup admin credential for control db server
	$s1 = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName
	$serverLogin = $s1.SqlAdministratorLogin
	$serverPassword = "t357ingP@s5w0rd!"
	$credential = new-object System.Management.Automation.PSCredential($serverLogin, ($serverPassword | ConvertTo-SecureString -asPlainText -Force))
	$jc1 = $a1 | New-AzureRmSqlElasticJobCredential -Name (Get-UserName) -Credential $credential
	$tg1 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name (Get-TargetGroupName)
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Wait
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState

	# # Start job - async
	$je = Start-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests starting a job with job object
#>
function Test-StartJobWithJobObject ($a1)
{
	$script = "SELECT 1"

	# Setup admin credential for control db server
	$s1 = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName
	$serverLogin = $s1.SqlAdministratorLogin
	$serverPassword = "t357ingP@s5w0rd!"
	$credential = new-object System.Management.Automation.PSCredential($serverLogin, ($serverPassword | ConvertTo-SecureString -asPlainText -Force))
	$jc1 = $a1 | New-AzureRmSqlElasticJobCredential -Name (Get-UserName) -Credential $credential
	$tg1 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name (Get-TargetGroupName)
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -JobObject $j1 -Wait
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState

	# Start job - async
	$je = Start-AzureRmSqlElasticJob -JobObject $j1
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests starting a job with job resource id
#>
function Test-StartJobWithJobResourceId ($a1)
{
	$script = "SELECT 1"
	# Setup admin credential for control db server
	$s1 = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName
	$serverLogin = $s1.SqlAdministratorLogin
	$serverPassword = "t357ingP@s5w0rd!"
	$credential = new-object System.Management.Automation.PSCredential($serverLogin, ($serverPassword | ConvertTo-SecureString -asPlainText -Force))
	$jc1 = $a1 | New-AzureRmSqlElasticJobCredential -Name (Get-UserName) -Credential $credential
	$tg1 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name (Get-TargetGroupName)
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -JobResourceId $j1.ResourceId -Wait
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState

	# Start job - async
	$je = Start-AzureRmSqlElasticJob -JobResourceId $j1.ResourceId
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests starting a job with piping
#>
function Test-StartJobWithPiping ($a1)
{
	$script = "SELECT 1"
	# Setup admin credential for control db server
	$s1 = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName
	$serverLogin = $s1.SqlAdministratorLogin
	$serverPassword = "t357ingP@s5w0rd!"
	$credential = new-object System.Management.Automation.PSCredential($serverLogin, ($serverPassword | ConvertTo-SecureString -asPlainText -Force))
	$jc1 = $a1 | New-AzureRmSqlElasticJobCredential -Name (Get-UserName) -Credential $credential
	$tg1 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name (Get-TargetGroupName)
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script

	# Start job - sync
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState

	# Start job - async
	$je = $j1 | Start-AzureRmSqlElasticJob
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests stop job with default param
#>
function Test-StopJobWithDefaultParam ($a1)
{
	$script = "WAITFOR DELAY '00:10:00'"
	$je = Start-JobOnControlDb $a1 $script

	$je = Stop-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests stop job with job execution object
#>
function Test-StopJobWithJobExecutionObject ($a1)
{
	$script = "WAITFOR DELAY '00:10:00'"
	$je = Start-JobOnControlDb $a1 $script

	$je = Stop-AzureRmSqlElasticJob -JobExecutionObject $je
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
#>
function Test-StopJobWithJobExecutionResourceId ($a1)
{
	$script = "WAITFOR DELAY '00:10:00'"
	$je = Start-JobOnControlDb $a1 $script

	$je = Stop-AzureRmSqlElasticJob -JobExecutionResourceId $je.ResourceId
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
#>
function Test-StopJobWithPiping ($a1)
{
	$script = "WAITFOR DELAY '00:10:00'"
	$je = Start-JobOnControlDb $a1 $script

	$je = $je | Stop-AzureRmSqlElasticJob
	Assert-AreEqual $je.ResourceGroupName $a1.ResourceGroupName
	Assert-AreEqual $je.ServerName $a1.ServerName
	Assert-AreEqual $je.AgentName $a1.AgentName
	Assert-AreEqual $je.JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
}

<#
	.SYNOPSIS
	Tests get job execution with default param
#>
function Test-GetJobExecutionWithDefaultParam ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Get with min params
	$allExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Count 10
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Count 10
	$jobExecution = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	# Get will all filters
	$allExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Count 10	-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Count 10 -CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
}

<#
	.SYNOPSIS
	Tests get job execution with agent object
#>
function Test-GetJobExecutionWithAgentObject ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Get with min params
	$allExecutions = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -Count 10
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -JobName $j1.JobName -Count 10
	$jobExecution = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	# Get with all filters
	$allExecutions = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -JobName $j1.JobName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
}

<#
	.SYNOPSIS
	Tests get job execution with agent resource id
#>
function Test-GetJobExecutionWithAgentResourceId ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min params
	$allExecutions = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -Count 10
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -JobName $j1.JobName -Count 10
	$jobExecution = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	# Test with filters
	$allExecutions = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -Count 10
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -JobName $j1.JobName -Count 10
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
}

<#
	.SYNOPSIS
	Tests get job execution with piping
#>
function Test-GetJobExecutionWithPiping ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	$allExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -Count 10
	$jobExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -Count 10
	$jobExecution = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	# Test with filters
	$allExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with default param
#>
function Test-GetJobStepExecutionWithDefaultParam ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions

	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-StepName $js1.StepName -CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with job execution object
#>
function Test-GetJobStepExecutionWithJobExecutionObject ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionObject $je
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionObject $je -StepName $js1.StepName
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions

	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionObject $je `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionObject $je -StepName $js1.StepName `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with job execution resource id
#>
function Test-GetJobStepExecutionWithJobExecutionResourceId ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min params
	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionResourceId $je.ResourceId
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionResourceId $je.ResourceId -StepName $js1.StepName
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions

	# Test with all filters
	$allStepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionResourceId $je.ResourceId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepExecutions = Get-AzureRmSqlElasticJobStepExecution -JobExecutionResourceId $je.ResourceId -StepName $js1.StepName `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with piping
#>
function Test-GetJobStepExecutionWithPiping ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min params
	$allStepExecutions = $je | Get-AzureRmSqlElasticJobStepExecution
	$stepExecutions = $je | Get-AzureRmSqlElasticJobStepExecution -StepName $js1.StepName
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions

	# Test with all filters
	$allStepExecutions = $je | Get-AzureRmSqlElasticJobStepExecution `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepExecutions = $je | Get-AzureRmSqlElasticJobStepExecution -StepName $js1.StepName `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allStepExecutions
	Assert-NotNull $stepExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with default param
#>
function Test-GetJobTargetExecutionWithDefaultParam ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min param
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -Count 10
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId -StepName $js1.StepName -Count 10
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions

	# Test all filters
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-Count 10 -CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId ` -Count 10 -StepName $js1.StepName -CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with job execution object
#>
function Test-GetJobTargetExecutionWithJobExecutionObject ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min param
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionObject $je -Count 10
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionObject $je -StepName $js1.StepName -Count 10
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions

	# Test all filter
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionObject $je -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionObject $je -StepName $js1.StepName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with job execution resource id
#>
function Test-GetJobTargetExecutionWithJobExecutionResourceId ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	# Test min param
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionResourceId $je.ResourceId -Count 10
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionResourceId $je.ResourceId -StepName $js1.StepName -Count 10
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions

	# Test all filter
	$allTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionResourceId $je.ResourceId -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepTargetExecutions = Get-AzureRmSqlElasticJobTargetExecution -JobExecutionResourceId $je.ResourceId -StepName $js1.StepName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions
}

<#
	.SYNOPSIS
	Tests get job step execution with piping
#>
function Test-GetJobTargetExecutionWithPiping ($a1)
{
	$script = "SELECT 1"
	$je = Start-JobOnControlDb $a1 $script $true

	$allTargetExecutions = $je | Get-AzureRmSqlElasticJobTargetExecution -Count 10
	$stepTargetExecutions = $je | Get-AzureRmSqlElasticJobTargetExecution -StepName $js1.StepName -Count 10
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions

	$allTargetExecutions = $je | Get-AzureRmSqlElasticJobTargetExecution -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$stepTargetExecutions = $je | Get-AzureRmSqlElasticJobTargetExecution -StepName $js1.StepName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allTargetExecutions
	Assert-NotNull $stepTargetExecutions
}

<#
	.SYNOPSIS
	Starts a job that targets control db with provided $script
#>
function Start-JobOnControlDb($a1, $script, $wait = $false)
{
	# Setup admin credential for control db server
	$s1 = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName
	$serverLogin = $s1.SqlAdministratorLogin
	$serverPassword = "t357ingP@s5w0rd!"
	$credential = new-object System.Management.Automation.PSCredential($serverLogin, ($serverPassword | ConvertTo-SecureString -asPlainText -Force))
	$jc1 = $a1 | New-AzureRmSqlElasticJobCredential -Name (Get-UserName) -Credential $credential
	$tg1 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name (Get-TargetGroupName)
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script

	if ($wait)
	{
		# Wait for job to complete
		$je = $j1 | Start-AzureRmSqlElasticJob -Wait
	}
	else
	{
		# Run async
		$je = $j1 | Start-AzureRmSqlElasticJob
	}

	return $je
}