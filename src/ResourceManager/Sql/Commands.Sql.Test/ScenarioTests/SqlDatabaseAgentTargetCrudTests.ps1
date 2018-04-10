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
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddServerTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveServerTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddServerTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveServerTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddServerTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveServerTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddDatabaseTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveDatabaseTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddDatabaseTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveDatabaseTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddDatabaseTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveDatabaseTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddElasticPoolTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1
}


<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveElasticPoolTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ElasticPoolName ep2
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddElasticPoolTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1
}


<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveElasticPoolTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ElasticPoolName ep2
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddElasticPoolTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1
}


<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveElasticPoolTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ElasticPoolName ep2
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddShardMapTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveShardMapTarget
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ShardMapName sm1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddShardMapTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveShardMapTargetWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ShardMapName sm1 -DatabaseName db1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddShardMapTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveShardMapTargetWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $tg = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name tg1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ShardMapName sm1 -DatabaseName db1
}