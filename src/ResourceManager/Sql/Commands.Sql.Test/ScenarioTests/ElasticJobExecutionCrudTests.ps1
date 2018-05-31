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
	Tests starting a job with default parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-StartJobDefaultParam
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = Start-AzureRmSqlElasticJob -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName
	}
	catch
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests starting a job with job object
	.DESCRIPTION
	SmokeTest
#>
function Test-StartJobWithJobObject
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = Start-AzureRmSqlElasticJob -JobObject $j1
	}
	catch
	{
		#	Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests starting a job with job resource id
	.DESCRIPTION
	SmokeTest
#>
function Test-StartJobWithJobResourceId
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = Start-AzureRmSqlElasticJob -JobResourceId $j1.ResourceId
	}
	catch
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests starting a job with piping
	.DESCRIPTION
	SmokeTest
#>
function Test-StartJobWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = $j1 | Start-AzureRmSqlElasticJob
	}
	catch
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-GetJobExecutionWithDefaultParam
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	$je = $j1 | Start-AzureRmSqlElasticJob
	$all = Get-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Count 10
}

function Test-GetJobExecutionWithAgentObject
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	$je = $j1 | Start-AzureRmSqlElasticJob
	$all = Get-AzureRmSqlElasticJobExecution -AgentObject $a1 -Count 10
}

function Test-GetJobExecutionWithAgentResourceId
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	$je = $j1 | Start-AzureRmSqlElasticJob
	$all = Get-AzureRmSqlElasticJobExecution -AgentResourceId $a1.ResourceId -Count 10
}

function Test-GetJobExecutionWithPiping
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	$je = $j1 | Start-AzureRmSqlElasticJob
	$all = $a1 | Get-AzureRmSqlElasticJobExecution -Count 10
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-StopJobExecutionWithDefaultParam
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	$je = $j1 | Start-AzureRmSqlElasticJob
	$je = Stop-AzureRmSqlElasticJobExecution -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -JobExecutionId $je.JobExecutionId
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-StopJobExecutionWithJobExecutionObject
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = $j1 | Start-AzureRmSqlElasticJob
		$je = Stop-AzureRmSqlElasticJobExecution -JobExecutionObject $je
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-StopJobExecutionWithJobExecutionResourceId
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = $j1 | Start-AzureRmSqlElasticJob
		$je = Stop-AzureRmSqlElasticJobExecution -JobExecutionResourceId $je.ResourceId
	}
	catch
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests creating a job with min parameters
	.DESCRIPTION
	SmokeTest
#>
function Test-StopJobExecutionWithPiping
{
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 "SELECT 1"

	try
	{
		$je = $j1 | Start-AzureRmSqlElasticJob
		$je = $je | Stop-AzuureRmSqlElasticJobExecution
	}
	catch
	{
		# Remove-ResourceGroupForTest $a1
	}
}