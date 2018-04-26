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
function Test-CreateJobStep
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    
    $jsn1 = Get-JobStepName
    $jsn2 = Get-JobStepName
    $jsn3 = Get-JobStepName
    $jsn4 = Get-JobStepName
    $jsn5 = Get-JobStepName
    $script1 = "SELECT 1"

    # Test create using default params
    $js1 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1

    $js2 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn2 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js3 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn3 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js4 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -JobName $j1.JobName -Name $jsn4 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName -TimeoutSeconds 1000 -RetryAttempts 100 -InitialRetryIntervalSeconds 10 -MaximumRetryIntervalSeconds 1000 -RetryIntervalBackoffMultiplier 5.0

    # Test piping
    $js5 = $j1 | Add-AzureRmSqlDatabaseAgentJobStep -Name $jsn5 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1
}

<#
	.SYNOPSIS
	Tests adding job steps with job input object
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobStepWithInputObject
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    
    $jsn1 = Get-JobStepName
    $jsn2 = Get-JobStepName
    $jsn3 = Get-JobStepName
    $jsn4 = Get-JobStepName
    $jsn5 = Get-JobStepName
    $script1 = "SELECT 1"

    # Test create using default params
    $js1 = Add-AzureRmSqlDatabaseAgentJobStep -InputObject $j1 -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1

    $js2 = Add-AzureRmSqlDatabaseAgentJobStep -InputObject $j1 -Name $jsn2 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js3 = Add-AzureRmSqlDatabaseAgentJobStep -InputObject $j1 -Name $jsn3 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js4 = Add-AzureRmSqlDatabaseAgentJobStep -InputObject $j1 -Name $jsn4 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName -TimeoutSeconds 1000 -RetryAttempts 100 -InitialRetryIntervalSeconds 10 -MaximumRetryIntervalSeconds 1000 -RetryIntervalBackoffMultiplier 5.0

    # Test piping
    $js5 = $j1 | Add-AzureRmSqlDatabaseAgentJobStep -Name $jsn5 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1
}

<#
	.SYNOPSIS
	Tests creating a job step with job resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobStepWithResourceId
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    
    $jsn1 = Get-JobStepName
    $jsn2 = Get-JobStepName
    $jsn3 = Get-JobStepName
    $jsn4 = Get-JobStepName
    $jsn5 = Get-JobStepName
    $script1 = "SELECT 1"

    # Test create using default params
    $js1 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceId $j1.ResourceId -Name $jsn1 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1

    $js2 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceId $j1.ResourceId -Name $jsn2 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js3 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceId $j1.ResourceId -Name $jsn3 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName

    $js4 = Add-AzureRmSqlDatabaseAgentJobStep -ResourceId $j1.ResourceId -Name $jsn4 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1 -OutputServerName s1 -OutputSchemaName dbo -OutputDatabaseName db1 -OutputTableName tbl -OutputCredentialName $jc1.CredentialName -TimeoutSeconds 1000 -RetryAttempts 100 -InitialRetryIntervalSeconds 10 -MaximumRetryIntervalSeconds 1000 -RetryIntervalBackoffMultiplier 5.0

    # Test piping
    $js5 = $j1 | Add-AzureRmSqlDatabaseAgentJobStep -Name $jsn5 -TargetGroupName $tg1.TargetGroupName -CredentialName $jc1.CredentialName -CommandText $script1
}

function Test-RemoveJobStep
{
    Remove-AzureRmSqlDatabaseAgentJob -ResourceGroupName powershell -ServerName jppsserver -AgentName jpagent -Name job1
}

function Test-UpdateJobStep
{
    $a1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName powershell -ServerName jppsserver -Name jpagent
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1
    $tg1 = Create-TargetGroupForTest $a1
    $tg2 = Create-TargetGroupForTest $a1
    $j1 = Create-JobForTest $a1
    
    $script1 = "SELECT 1"
    $script2 = "SELECT 2"

    # Test create job step with output object
    $js1 = Create-JobStepForTest $j1 $tg1 $jc1 $script1 $true

    # Test updating existing param at a time

    # Test update target group name
    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -TargetGroupName $tg2.TargetGroupName
    Assert-AreEqual $true $resp.TargetGroup.Contains($tg2.TargetGroupName)

    # Test update credential name
    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -CredentialName $jc2.CredentialName
    Assert-AreEqual $true $resp.Credential.Contains($jc2.CredentialName)

    # Test update script
    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -CommandText $script2
    Assert-AreEqual $resp.Action.Value $script2

    # Test updating output target
    $output = Create-JobStepOutputForTest $jc2

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSubscriptionId $output.SubscriptionId
    Assert-AreEqual $resp.Output.SubscriptionId $output.SubscriptionId

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputResourceGroupName $output.ResourceGroupName
    Assert-AreEqual $resp.Output.ResourceGroupName $output.ResourceGroupName

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputServerName $output.ServerName
    Assert-AreEqual $resp.Output.ServerName $output.ServerName

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputDatabaseName $output.DatabaseName
    Assert-AreEqual $resp.Output.DatabaseName $output.DatabaseName

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSchemaName $output.SchemaName
    Assert-AreEqual $resp.Output.SchemaName $output.SchemaName

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputTableName $output.TableName
    Assert-AreEqual $resp.Output.TableName $output.TableName

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputCredentialName $output.CredentialName
    Assert-AreEqual $true $resp.Output.Credential.Contains($output.CredentialName)

    # Remove output object
    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -RemoveOutput
    Assert-Null $resp.Output

    # Set output object
    # Test updating output target
    $output = Create-JobStepOutputForTest $jc2

    $resp = Set-AzureRmSqlDatabaseAgentJobStep -ResourceGroupName $js1.ResourceGroupName -ServerName $js1.ServerName -AgentName $js1.AgentName -JobName $js1.JobName -Name $js1.StepName -OutputSubscriptionId $output.SubscriptionId -OutputResourceGroupName $output.ResourceGroupName -OutputServerName $output.ServerName -OutputDatabaseName $output.DatabaseName -OutputSchemaName $output.SchemaName -OutputTableName $output.TableName -OutputCredentialName $output.CredentialName
    Assert-NotNull $resp.Output
    
    Assert-AreEqual $resp.Output.SubscriptionId $output.SubscriptionId
    Assert-AreEqual $resp.Output.ServerName $output.ServerName
    Assert-AreEqual $resp.Output.DatabaseName $output.DatabaseName
    Assert-AreEqual $resp.Output.TableName $output.TableName
    Assert-AreEqual $resp.Output.SchemaName $output.SchemaName
}

function Test-GetJobStep
{

}