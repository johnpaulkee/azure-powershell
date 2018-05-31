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
	$a1 = Create-ElasticJobAgentEnvironmentForTest

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
	$a1 = Create-ElasticJobAgentEnvironmentForTest

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
	$a1 = Create-ElasticJobAgentEnvironmentForTest

	try
	{
		Test-GetJobExecutionWithDefaultParam $a1
		Test-GetJobExecutionWithAgentObject $a1
		Test-GetJobExecutionWithAgentResourceId $a1
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
	$a1 = Create-ElasticJobAgentEnvironmentForTest

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
	$a1 = Create-ElasticJobAgentEnvironmentForTest

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	# Start job - async
	$je = Start-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Wait
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests starting a job with job object
#>
function Test-StartJobWithJobObject ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	# Start job - async
	$je = Start-AzureRmSqlElasticJob -JobObject $j1
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -JobObject $j1 -Wait
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests starting a job with job resource id
#>
function Test-StartJobWithJobResourceId ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	# Start job - async
	$je = Start-AzureRmSqlElasticJob -JobResourceId $j1.ResourceId
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts

	# Start job - sync
	$je = Start-AzureRmSqlElasticJob -JobResourceId $j1.ResourceId -Wait
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests starting a job with piping
#>
function Test-StartJobWithPiping ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	# Start job - async
	$je = $j1 | Start-AzureRmSqlElasticJob
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Created $je.Lifecycle
	Assert-AreEqual Created $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts

	# Start job - sync
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Succeeded $je.Lifecycle
	Assert-AreEqual Succeeded $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests stop job with default param
#>
function Test-StopJobWithDefaultParam ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob

	$je = Stop-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobExecutionId -JobExecutionId $je.JobExecutionId
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests stop job with job execution object
#>
function Test-StopJobWithJobExecutionObject ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob

	$je = Stop-AzureRmSqlElasticJob -JobExecutionObject $je
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
#>
function Test-StopJobWithJobExecutionResourceId ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob

	$je = Stop-AzureRmSqlElasticJob -JobExecutionResourceId $je.ResourceId
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
#>
function Test-StopJobWithPiping ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob

	$je = $je | Stop-AzureRmSqlElasticJob
	Assert-NotNull $je.JobExecutionId
	Assert-AreEqual 1 $je.JobVersion
	Assert-AreEqual Canceled $je.Lifecycle
	Assert-AreEqual Canceled $je.ProvisioningState
	Assert-AreEqual 1 $je.CurrentAttempts
}

<#
	.SYNOPSIS
	Tests get job execution with default param
#>
function Test-GetJobExecutionWithDefaultParam ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

	# Get with min params
	$allExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Count 10
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Count 10
	$jobExecution = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	# Get will all filters
	$allExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecution = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution
}

<#
	.SYNOPSIS
	Tests get job execution with agent object
#>
function Test-GetJobExecutionWithAgentObject ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jobExecution = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution
}

<#
	.SYNOPSIS
	Tests get job execution with agent resource id
#>
function Test-GetJobExecutionWithAgentResourceId ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jobExecution = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution
}

<#
	.SYNOPSIS
	Tests get job execution with piping
#>
function Test-GetJobExecutionWithPiping ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

	$allExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -Count 10
	$jobExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -Count 10
	$jobExecution = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution

	$allExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecutions = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -Count 10 `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	$jobExecution = $a1 | Get-AzureRmSqlElasticJobExecution -JobName $j1.JobName -JobExecutionId $je.JobExecutionId `
		-CreateTimeMin (Get-Date).AddHours(-10) -CreateTimeMax (Get-Date).AddHours(10) -EndTimeMin (Get-Date).AddHours(-3) -EndTimeMax (Get-Date).AddHours(10) -Active
	Assert-NotNull $allExecutions
	Assert-NotNull $jobExecutions
	Assert-NotNull $jobExecution
}

<#
	.SYNOPSIS
	Tests get job step execution with default param
#>
function Test-GetJobStepExecutionWithDefaultParam ($a1)
{
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"
	$je = $j1 | Start-AzureRmSqlElasticJob -Wait

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