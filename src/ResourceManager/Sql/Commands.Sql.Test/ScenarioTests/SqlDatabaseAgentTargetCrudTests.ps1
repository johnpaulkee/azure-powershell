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
function Test-AddTarget
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1
    $tg1 = Create-TargetGroupForTest $rg1 $s1 $a1
    
    # Target Helper Objects
    $st1 =  @{ ServerName = "s1"; RefreshCredentialName = $jc1.CredentialName }
    $dbt1 = @{ ServerName = "s1"; DatabaseName = "db1" }
    $ep1 =  @{ ServerName = "s1"; ElasticPoolName = "ep1"; RefreshCredentialName = $jc1.CredentialName }
    $sm1 =  @{ ServerName = "s1"; ShardMapName="sm1"; DatabaseName="db1"; RefreshCredentialName = $jc1.CredentialName }

    try
    {
        # Test default parameters - Server target
        $resp1 = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.ServerName $st1.ServerName
        Assert-AreEqual $resp1.

        # Test input object parameters - Server target

        # Test resource id parameters - Server target

        # Test piping - Server target

        Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -RefreshCredentialName cred1 -Exclude

        Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -RefreshCredentialName cred1
            Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -RefreshCredentialName cred1

                Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -DatabaseName db1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -DatabaseName db1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -DatabaseName db1

        Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

    Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

        Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1
    Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1

        Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1

    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveTarget
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $tg1 = Create-TargetGroupForTest $rg1 $s1 $a1

    try
    {
        Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -RefreshCredentialName cred1

        Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -RefreshCredentialName cred1

        Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -RefreshCredentialName cred1

        
    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -DatabaseName db1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -DatabaseName db1
    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -DatabaseName db1

    
    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

    Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ElasticPoolName ep2 -RefreshCredentialName cred1

    
    Remove-AzureRmSqlDatabaseAgentTarget -ResourceGroupName ps2525 -AgentServerName ps6926 -AgentName agent -TargetGroupName tg1 -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1

    Remove-AzureRmSqlDatabaseAgentTarget -InputObject $tg -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1

        Remove-AzureRmSqlDatabaseAgentTarget -ResourceId $tg.ResourceId -ServerName s1 -ShardMapName sm1 -DatabaseName db1 -RefreshCredentialName cred1
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}