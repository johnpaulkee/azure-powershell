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
function Test-CreateJobStepWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$jso1 = Create-JobStepOutputForTest $jc1
	$ct1 = "SELECT 1"

	try
	{
		# Test add job step using minimum default params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.CommandText $ct1
		Assert-Null $js1.Output

		# Test add job step using minimum default params + minimum output params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1 -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.Output.ServerName $jso1.ServerName
		Assert-AreEqual $js1.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $js1.Output.TableName $jso1.TableName
		Assert-AreEqual $js1.Output.Credential $jso1.CredentialName

		# Test add job step using minimum default params + maximum output params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1 -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName -OutputSchemaName $jso1.SchemaName -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $js1.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $js1.Output.ServerName $jso1.ServerName
		Assert-AreEqual $js1.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $js1.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $js1.Output.TableName $jso1.TableName
		Assert-AreEqual $js1.Output.Credential $jso1.CredentialName

		# Test add job step using maximum default params + maximum output params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1 -TimeoutSeconds 1000 -RetryAttempts 100 -InitialRetryIntervalSeconds 10 -MaximumRetryIntervalSeconds 1000 -RetryIntervalBackoffMultiplier 5.0 -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName -OutputSchemaName $jso1.SchemaName -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.ExecutionOptions.TimeoutSeconds 1000
		Assert-AreEqual $js1.ExecutionOptions.RetryAttempts 100
		Assert-AreEqual $js1.ExecutionOptions.InitialRetryIntervalSeconds 10
		Assert-AreEqual $js1.ExecutionOptions.MaximumRetryIntervalSeconds 1000
		Assert-AreEqual $js1.ExecutionOptions.RetryIntervalBackoffMultiplier 5.0
		Assert-AreEqual $js1.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $js1.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $js1.Output.ServerName $jso1.ServerName
		Assert-AreEqual $js1.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $js1.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $js1.Output.TableName $jso1.TableName
		Assert-AreEqual $js1.Output.Credential $jso1.CredentialName
	}
	catch
	{
		#Remove-AzureRmResourceGroup $a1
	}
}

function Test-CreateJobStepWithJobObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$jso1 = Create-JobStepOutputForTest $jc1
	$ct1 = "SELECT 1"

	try
	{
		# Test create using resource id with min params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -JobObject $j1 -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.CommandText $ct1
		Assert-Null $js1.Output

		# Test output database object

		# Test output database resource id

		# Test execution options
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-CreateJobStepWithJobResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$jso1 = Create-JobStepOutputForTest $jc1
	$ct1 = "SELECT 1"

	try
	{
		# Test create using resource id with min params
		$jsn1 = Get-JobStepName
		$js1 = Add-AzureRmSqlElasticJobStep -JobResourceId $j1.ResourceId -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.CommandText $ct1
		Assert-Null $js1.Output
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-CreateJobStepWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$jso1 = Create-JobStepOutputForTest $jc1
	$ct1 = "SELECT 1"

	try
	{
		# Test create using resource id with min params
		$jsn1 = Get-JobStepName
		$js1 = $j1 | Add-AzureRmSqlElasticJobStep -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $ct1
		Assert-AreEqual $js1.ResourceGroupName $a1.ResourceGroupName
		Assert-AreEqual $js1.ServerName $a1.ServerName
		Assert-AreEqual $js1.AgentName $a1.AgentName
		Assert-AreEqual $js1.JobName $j1.JobName
		Assert-AreEqual $js1.StepName $jsn1
		Assert-AreEqual $js1.TargetGroupName $tg1.TargetGroupName
		Assert-AreEqual $js1.CredentialName $jc1.CredentialName
		Assert-AreEqual $js1.CommandText $ct1
		Assert-Null $js1.Output
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-UpdateJobStepWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$jc2 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$tg2 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$ct2 = "SELECT 2"
	$jso1 = Create-JobStepOutputForTest $jc1
	$jso2 = Create-JobStepOutputForTest $jc2
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		## TEST DEFAULT PARAMS

		# Test update nothing
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName

		# Test update step target group
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -TargetGroupName $tg2.TargetGroupName
		Assert-AreEqual $resp.TargetGroupName $tg2.TargetGroupName

		# Test update step credential
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -CredentialName $jc2.CredentialName
		Assert-AreEqual $resp.CredentialName $jc2.CredentialName

		# Test update command text
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -CommandText $ct2
		Assert-AreEqual $resp.CommandText $ct2

		# Test add output to step
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputSchemaName $jso1.SchemaName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName
		Assert-AreEqual $resp.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $resp.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $resp.Output.ServerName $jso1.ServerName
		Assert-AreEqual $resp.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $resp.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $resp.Output.TableName $jso1.TableName
		Assert-AreEqual $resp.Output.Credential $jso1.CredentialName

		# Test update output subscription id
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSubscriptionId $jso2.SubscriptionId
		Assert-AreEqual $resp.Output.SubscriptionId $jso2.SubscriptionId

		# Test update output resource group
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputResourceGroupName $jso2.ResourceGroupName
		Assert-AreEqual $resp.Output.ResourceGroupName $jso2.ResourceGroupName

		# Test update output server name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputServerName $jso2.ServerName
		Assert-AreEqual $resp.Output.ServerName $jso2.ServerName

		# Test update output database name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputDatabaseName $jso2.DatabaseName
		Assert-AreEqual $resp.Output.DatabaseName $jso2.DatabaseName

		# Test update output schema name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSchemaName $jso2.SchemaName
		Assert-AreEqual $resp.Output.SchemaName $jso2.SchemaName

		# Test update output table name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputTableName $jso2.TableName
		Assert-AreEqual $resp.Output.TableName $jso2.TableName

		# Test update output credential name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputCredentialName $jso2.CredentialName
		Assert-AreEqual $resp.Output.Credential $jso2.CredentialName

		# Test update job step remove output
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -RemoveOutput
		Assert-Null $resp.Output

		# Test update execution option timeout seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -TimeoutSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.TimeoutSeconds 100

		# Test update execution option retry attempts
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -RetryAttempts 1000
		Assert-AreEqual $resp.ExecutionOptions.RetryAttempts 1000

		# Test update execution option initial retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -InitialRetryIntervalSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.InitialRetryIntervalSeconds 100

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -MaximumRetryIntervalSeconds 1000
		Assert-AreEqual $resp.ExecutionOptions.MaximumRetryIntervalSeconds 1000

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -RetryIntervalBackoffMultiplier 5.2
		Assert-AreEqual $resp.ExecutionOptions.RetryIntervalBackoffMultiplier 5.2
	}
	catch
	{

	}
}

function Test-UpdateJobStepWithInputObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$jc2 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$tg2 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$ct2 = "SELECT 2"
	$jso1 = Create-JobStepOutputForTest $jc1
	$jso2 = Create-JobStepOutputForTest $jc2
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# TEST INPUT OBJECT

		# Test update nothing
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1

		# Test update step target group
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -TargetGroupName $tg2.TargetGroupName
		Assert-AreEqual $resp.TargetGroupName $tg2.TargetGroupName

		# Test update step credential
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -CredentialName $jc2.CredentialName
		Assert-AreEqual $resp.CredentialName $jc2.CredentialName

		# Test update command text
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -CommandText $ct2
		Assert-AreEqual $resp.CommandText $ct2

		# Test add output to step
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputSchemaName $jso1.SchemaName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName
		Assert-AreEqual $resp.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $resp.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $resp.Output.ServerName $jso1.ServerName
		Assert-AreEqual $resp.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $resp.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $resp.Output.TableName $jso1.TableName
		Assert-AreEqual $resp.Output.Credential $jso1.CredentialName

		# Test update output subscription id
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputSubscriptionId $jso2.SubscriptionId
		Assert-AreEqual $resp.Output.SubscriptionId $jso2.SubscriptionId

		# Test update output resource group
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputResourceGroupName $jso2.ResourceGroupName
		Assert-AreEqual $resp.Output.ResourceGroupName $jso2.ResourceGroupName

		# Test update output server name
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputServerName $jso2.ServerName
		Assert-AreEqual $resp.Output.ServerName $jso2.ServerName

		# Test update output database name
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputDatabaseName $jso2.DatabaseName
		Assert-AreEqual $resp.Output.DatabaseName $jso2.DatabaseName

		# Test update output schema name
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputSchemaName $jso2.SchemaName
		Assert-AreEqual $resp.Output.SchemaName $jso2.SchemaName

		# Test update output table name
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputTableName $jso2.TableName
		Assert-AreEqual $resp.Output.TableName $jso2.TableName

		# Test update output credential name
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -OutputCredentialName $jso2.CredentialName
		Assert-AreEqual $resp.Output.Credential $jso2.CredentialName

		# Test update job step remove output
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -RemoveOutput
		Assert-Null $resp.Output

		# Test update execution option timeout seconds
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -TimeoutSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.TimeoutSeconds 100

		# Test update execution option retry attempts
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -RetryAttempts 1000
		Assert-AreEqual $resp.ExecutionOptions.RetryAttempts 1000

		# Test update execution option initial retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -InitialRetryIntervalSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.InitialRetryIntervalSeconds 100

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -MaximumRetryIntervalSeconds 1000
		Assert-AreEqual $resp.ExecutionOptions.MaximumRetryIntervalSeconds 1000

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -InputObject $js1 -RetryIntervalBackoffMultiplier 5.2
		Assert-AreEqual $resp.ExecutionOptions.RetryIntervalBackoffMultiplier 5.2
	}
	catch
	{

	}
}

function Test-UpdateJobStepWithResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$jc2 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$tg2 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$ct2 = "SELECT 2"
	$jso1 = Create-JobStepOutputForTest $jc1
	$jso2 = Create-JobStepOutputForTest $jc2
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test update nothing
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId

		# Test update step target group
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -TargetGroupName $tg2.TargetGroupName
		Assert-AreEqual $resp.TargetGroupName $tg2.TargetGroupName

		# Test update step credential
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -CredentialName $jc2.CredentialName
		Assert-AreEqual $resp.CredentialName $jc2.CredentialName

		# Test update command text
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -CommandText $ct2
		Assert-AreEqual $resp.CommandText $ct2

		# Test add output to step
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputSchemaName $jso1.SchemaName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName
		Assert-AreEqual $resp.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $resp.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $resp.Output.ServerName $jso1.ServerName
		Assert-AreEqual $resp.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $resp.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $resp.Output.TableName $jso1.TableName
		Assert-AreEqual $resp.Output.Credential $jso1.CredentialName

		# Test update output subscription id
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputSubscriptionId $jso2.SubscriptionId
		Assert-AreEqual $resp.Output.SubscriptionId $jso2.SubscriptionId

		# Test update output resource group
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputResourceGroupName $jso2.ResourceGroupName
		Assert-AreEqual $resp.Output.ResourceGroupName $jso2.ResourceGroupName

		# Test update output server name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputServerName $jso2.ServerName
		Assert-AreEqual $resp.Output.ServerName $jso2.ServerName

		# Test update output database name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputDatabaseName $jso2.DatabaseName
		Assert-AreEqual $resp.Output.DatabaseName $jso2.DatabaseName

		# Test update output schema name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputSchemaName $jso2.SchemaName
		Assert-AreEqual $resp.Output.SchemaName $jso2.SchemaName

		# Test update output table name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputTableName $jso2.TableName
		Assert-AreEqual $resp.Output.TableName $jso2.TableName

		# Test update output credential name
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -OutputCredentialName $jso2.CredentialName
		Assert-AreEqual $resp.Output.Credential $jso2.CredentialName

		# Test update job step remove output
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -RemoveOutput
		Assert-Null $resp.Output

		# Test update execution option timeout seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -TimeoutSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.TimeoutSeconds 100

		# Test update execution option retry attempts
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -RetryAttempts 1000
		Assert-AreEqual $resp.ExecutionOptions.RetryAttempts 1000

		# Test update execution option initial retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -InitialRetryIntervalSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.InitialRetryIntervalSeconds 100

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -MaximumRetryIntervalSeconds 1000
		Assert-AreEqual $resp.ExecutionOptions.MaximumRetryIntervalSeconds 1000

		# Test update execution option maximum retry interval seconds
		$resp = Set-AzureRmSqlElasticJobStep -ResourceId $js1.ResourceId -RetryIntervalBackoffMultiplier 5.2
		Assert-AreEqual $resp.ExecutionOptions.RetryIntervalBackoffMultiplier 5.2
	}
	catch
	{
	}
}

function Test-UpdateJobStepWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment

	$jc1 = Create-JobCredentialForTest $a1
	$jc2 = Create-JobCredentialForTest $a1

	$tg1 = Create-TargetGroupForTest $a1
	$tg2 = Create-TargetGroupForTest $a1

	$j1 = Create-JobForTest $a1

	$ct1 = "SELECT 1"
	$ct2 = "SELECT 2"
	$jso1 = Create-JobStepOutputForTest $jc1
	$jso2 = Create-JobStepOutputForTest $jc2
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test update step target group
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -TargetGroupName $tg2.TargetGroupName
		Assert-AreEqual $resp.TargetGroupName $tg2.TargetGroupName

		# Test update step credential
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -CredentialName $jc2.CredentialName
		Assert-AreEqual $resp.CredentialName $jc2.CredentialName

		# Test update command text
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -CommandText $ct2
		Assert-AreEqual $resp.CommandText $ct2

		# Test add output to step
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputSubscriptionId $jso1.SubscriptionId -OutputResourceGroupName $jso1.ResourceGroupName -OutputServerName $jso1.ServerName -OutputDatabaseName $jso1.DatabaseName -OutputSchemaName $jso1.SchemaName -OutputTableName $jso1.TableName -OutputCredentialName $jso1.CredentialName
		Assert-AreEqual $resp.Output.SubscriptionId $jso1.SubscriptionId
		Assert-AreEqual $resp.Output.ResourceGroupName $jso1.ResourceGroupName
		Assert-AreEqual $resp.Output.ServerName $jso1.ServerName
		Assert-AreEqual $resp.Output.DatabaseName $jso1.DatabaseName
		Assert-AreEqual $resp.Output.SchemaName $jso1.SchemaName
		Assert-AreEqual $resp.Output.TableName $jso1.TableName
		Assert-AreEqual $resp.Output.Credential $jso1.CredentialName

		# Test update output subscription id
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputSubscriptionId $jso2.SubscriptionId
		Assert-AreEqual $resp.Output.SubscriptionId $jso2.SubscriptionId

		# Test update output resource group
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputResourceGroupName $jso2.ResourceGroupName
		Assert-AreEqual $resp.Output.ResourceGroupName $jso2.ResourceGroupName

		# Test update output server name
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputServerName $jso2.ServerName
		Assert-AreEqual $resp.Output.ServerName $jso2.ServerName

		# Test update output database name
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputDatabaseName $jso2.DatabaseName
		Assert-AreEqual $resp.Output.DatabaseName $jso2.DatabaseName

		# Test update output schema name
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputSchemaName $jso2.SchemaName
		Assert-AreEqual $resp.Output.SchemaName $jso2.SchemaName

		# Test update output table name
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputTableName $jso2.TableName
		Assert-AreEqual $resp.Output.TableName $jso2.TableName

		# Test update output credential name
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -OutputCredentialName $jso2.CredentialName
		Assert-AreEqual $resp.Output.Credential $jso2.CredentialName

		# Test update job step remove output
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -RemoveOutput
		Assert-Null $resp.Output

		# Test update execution option timeout seconds
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -TimeoutSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.TimeoutSeconds 100

		# Test update execution option retry attempts
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -RetryAttempts 1000
		Assert-AreEqual $resp.ExecutionOptions.RetryAttempts 1000

		# Test update execution option initial retry interval seconds
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -InitialRetryIntervalSeconds 100
		Assert-AreEqual $resp.ExecutionOptions.InitialRetryIntervalSeconds 100

		# Test update execution option maximum retry interval seconds
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -MaximumRetryIntervalSeconds 1000
		Assert-AreEqual $resp.ExecutionOptions.MaximumRetryIntervalSeconds 1000

		# Test update execution option maximum retry interval seconds
		$resp = $js1 | Set-AzureRmSqlElasticJobStep -RetryIntervalBackoffMultiplier 5.2
		Assert-AreEqual $resp.ExecutionOptions.RetryIntervalBackoffMultiplier 5.2
	}
	catch
	{

	}
}

function Test-RemoveJobStepWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with default params
		$resp = Remove-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	catch
	{

	}
}

function Test-RemoveJobStepWithInputObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with input object
		$resp = Remove-AzureRmSqlElasticJobStep -InputObject $js2
		Assert-AreEqual $resp.ResourceGroupName $js2.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js2.ServerName
		Assert-AreEqual $resp.AgentName $js2.AgentName
		Assert-AreEqual $resp.JobName $js2.JobName
		Assert-AreEqual $resp.StepName $js2.StepName
		Assert-AreEqual $resp.TargetGroupName $js2.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js2.CredentialName
	}
	catch
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-RemoveJobStepWithResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with resource id
		$resp = Remove-AzureRmSqlElasticJobStep -ResourceId $js3.ResourceId
		Assert-AreEqual $resp.ResourceGroupName $js3.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js3.ServerName
		Assert-AreEqual $resp.AgentName $js3.AgentName
		Assert-AreEqual $resp.JobName $js3.JobName
		Assert-AreEqual $resp.StepName $js3.StepName
		Assert-AreEqual $resp.TargetGroupName $js3.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js3.CredentialName
	}
	catch
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-RemoveJobStepWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with piping
		$resp = $js1 | Remove-AzureRmSqlElasticJobStep
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-GetJobStepWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with default params
		$resp = Get-AzureRmSqlElasticJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	catch
	{

	}
}

function Test-GetJobStepWithJobObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with input object
		$resp = Get-AzureRmSqlElasticJobStep -JobObject $j1 -Name $js1.StepName
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	catch
	{
		# Remove-ResourceGroupForTest $a1
	}
}

function Test-GetJobStepWithJobResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with resource id
		$resp = Get-AzureRmSqlElasticJobStep -ResourceId $j1.ResourceId -Name $js1.StepName
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	catch
	{

	}
}

function Test-GetJobStepWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$j1 = Create-JobForTest $a1
	$ct1 = "SELECT 1"
	$js1 = Create-JobStepForTest $j1 $tg1 $jc1 $ct1

	try
	{
		# Test with resource id
		$resp = $j1 | Get-AzureRmSqlElasticJobStep -Name $js1.StepName
		Assert-AreEqual $resp.ResourceGroupName $js1.ResourceGroupName
		Assert-AreEqual $resp.ServerName $js1.ServerName
		Assert-AreEqual $resp.AgentName $js1.AgentName
		Assert-AreEqual $resp.JobName $js1.JobName
		Assert-AreEqual $resp.StepName $js1.StepName
		Assert-AreEqual $resp.TargetGroupName $js1.TargetGroupName
		Assert-AreEqual $resp.CredentialName $js1.CredentialName
	}
	catch
	{

	}
}