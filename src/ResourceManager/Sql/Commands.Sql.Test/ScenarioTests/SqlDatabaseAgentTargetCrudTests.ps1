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
	Tests adding targets to target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddTarget
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    #$ep1 = Create-ElasticPoolForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1 $a1
    $tg1 = Create-TargetGroupForTest $rg1 $s1 $a1
    $tg2 = Create-TargetGroupForTest $rg1 $s1 $a1
    $tg3 = Create-TargetGroupForTest $rg1 $s1 $a1
    $tg4 = Create-TargetGroupForTest $rg1 $s1 $a1

    try
    {
        #Test-AddServerTarget $rg1 $s1 $a1 $jc1 $tg1
        #Test-AddDatabaseTarget $rg1 $s1 $a1 $jc1 $tg2
        #Test-AddElasticPoolTarget $rg1 $s1 $a1 $jc1 $tg3
        Test-AddShardMapTarget $rg1 $s1 $a1 $jc1 $tg4
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests adding server targets to target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddServerTarget($rg1, $s1, $a1, $jc1, $tg1)
{
    # Server Target Helper Objects
    $st1 =  @{ ServerName = "s1"; }
    $st2 =  @{ ServerName = "s2"; }
    $st3 =  @{ ServerName = "s3"; }
    $st4 =  @{ ServerName = "s4"; }

    ## --------- Server Target Tests -------------
    ## Test default parameters 

    # Include s1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $st1.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $st1.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s1 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include s1 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $st1.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 1

    ## Test input object

    # Include s2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
        
    Assert-AreEqual $resp.ServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
        
    Assert-AreEqual $resp.ServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s2 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp
        
    # Include s2 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
        
    Assert-AreEqual $resp.ServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 2

    ## Test resource id

    # Include s3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName
        
    Assert-AreEqual $resp.ServerName $st3.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
        
    Assert-AreEqual $resp.ServerName $st3.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlServer"

    # Exclude s3 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp
        
    # Include s3 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName
        
    Assert-AreEqual $resp.ServerName $st3.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 3

    ## Test piping

    # Add s4 to tg1
    $resp = $tg1 | Add-AzureRmSqlDatabaseAgentTarget -ServerName $st4.ServerName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.ServerName $st4.ServerName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlServer"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 4

    # Add all servers from rg1 to tg1 - Could also add other servers from all resource groups but unsure what count would be otherwise
    $added = Get-AzureRmSqlServer -ResourceGroupName $rg1.ResourceGroupName | Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual 1 $added.Count

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 5
}

<#
	.SYNOPSIS
	Tests adding database targets to target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddDatabaseTarget($rg1, $s1, $a1, $jc1, $tg1)
{
    # Target Helper Objects
    $dbt1 = @{ ServerName = "s1"; DatabaseName = "db1" }
    $dbt2 = @{ ServerName = "s2"; DatabaseName = "db2" }
    $dbt3 = @{ ServerName = "s3"; DatabaseName = "db3" }
    $dbt4 = @{ ServerName = "s4"; DatabaseName = "db4" }

    ## --------- Database Target Tests -------------
    ## Test default parameters

    # Include db1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName -DatabaseName $dbt1.DatabaseName 

    Assert-AreEqual $resp.ServerName $dbt1.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt1.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName -DatabaseName $dbt1.DatabaseName -Exclude

    Assert-AreEqual $resp.ServerName $dbt1.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt1.DatabaseName
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db1 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName -DatabaseName $dbt1.DatabaseName -Exclude
    Assert-Null $resp

    # Include db1 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName -DatabaseName $dbt1.DatabaseName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $dbt1.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt1.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 1

    ## Test input object

    # Include db2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName

    Assert-AreEqual $resp.ServerName $dbt2.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt2.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName -Exclude

    Assert-AreEqual $resp.ServerName $dbt2.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt2.DatabaseName
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db2 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName -Exclude
    Assert-Null $resp

    # Include db2 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $dbt2.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt2.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 2

    ## Test resource id

    # Include db3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName

    Assert-AreEqual $resp.ServerName $dbt3.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt3.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName -Exclude

    Assert-AreEqual $resp.ServerName $dbt3.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt3.DatabaseName
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlDatabase"

    # Exclude db3 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName -Exclude
    Assert-Null $resp

    # Include db3 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $dbt3.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt3.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 3

    ## Test piping

    # Add db4 to tg1
    $resp = $tg1 | Add-AzureRmSqlDatabaseAgentTarget -ServerName $dbt4.ServerName -DatabaseName $dbt4.DatabaseName
    Assert-AreEqual $resp.ServerName $dbt4.ServerName
    Assert-AreEqual $resp.DatabaseName $dbt4.DatabaseName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlDatabase"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 4

    # Add all databases from server in rg1 to tg1 (should be master & 1 user db)
    $added = Get-AzureRmSqlServer -ResourceGroupName $rg1.ResourceGroupName | Get-AzureRmSqlDatabase | Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1
    Assert-AreEqual 2 $added.Count

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 6
}

<#
	.SYNOPSIS
	Tests adding elastic pool targets to target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddElasticPoolTarget($rg1, $s1, $a1, $jc1, $tg1)
{
    # Target Helper Objects
    $ept1 = @{ ServerName = "s1"; ElasticPoolName = "ep1"; }
    $ept2 = @{ ServerName = "s2"; ElasticPoolName = "ep2"; }
    $ept3 = @{ ServerName = "s3"; ElasticPoolName = "ep3"; }
    $ept4 = @{ ServerName = "s4"; ElasticPoolName = "ep4"; }

    ## --------- Elastic Pool Target Tests -------------
    ## Test default parameters

    # Include ep1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName -ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $ept1.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept1.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName -ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $ept1.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept1.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep1 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName -ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include ep1 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName -ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $ept1.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept1.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 1

    ## Test input object

    # Include ep2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $ept2.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept2.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $ept2.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept2.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep2 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include ep2 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $ept2.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept2.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 2

    ## Test resource id

    # Include ep3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $ept3.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept3.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $ept3.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept3.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    # Exclude ep3 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include ep3 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $ept3.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept3.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 3

    ## Test piping

    # Add ep4 to tg1
    $resp = $tg1 | Add-AzureRmSqlDatabaseAgentTarget -ServerName $ept4.ServerName -ElasticPoolName $ept4.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.ServerName $ept4.ServerName
    Assert-AreEqual $resp.ElasticPoolName $ept4.ElasticPoolName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlElasticPool"

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 4

    # Add all elastic pools from rg1 to tg1
    $added = Get-AzureRmSqlServer -ResourceGroupName $rg1.ResourceGroupName | Get-AzureRmSqlElasticPool | Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual 1 $added.Count

    $all = Get-AzureRmSqlDatabaseAgentTargetGroup -InputObject $a1 -Name $tg1.TargetGroupName
    Assert-AreEqual $all.Members.Count 5
}

<#
	.SYNOPSIS
	Tests adding shard map targets to target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddShardMapTarget($rg1, $s1, $a1, $jc1, $tg1)
{
    # Target Helper Objects
    $smt1 = @{ ServerName = "s1"; ShardMapName = "sm1"; DatabaseName = "db1"}
    $smt2 = @{ ServerName = "s1"; ShardMapName = "sm2"; DatabaseName = "db2"}
    $smt3 = @{ ServerName = "s1"; ShardMapName = "sm3"; DatabaseName = "db3"}
    $smt4 = @{ ServerName = "s1"; ShardMapName = "sm4"; DatabaseName = "db4"}

    ## --------- Elastic Pool Target Tests -------------
    ## Test default parameters

    # Include ep1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName -ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $smt1.ServerName
    Assert-AreEqual $resp.ShardMapName $smt1.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt1.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude ep1
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName -ShardMapName $smt1.ShardMapName  -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $smt1.ServerName
    Assert-AreEqual $resp.ShardMapName $smt1.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt1.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude ep1 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName -ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include ep1 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceGroupName $rg1.ResourceGroupName -AgentServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName -ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $smt1.ServerName
    Assert-AreEqual $resp.ShardMapName $smt1.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt1.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    ## Test input object

    # Include db2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $smt2.ServerName
    Assert-AreEqual $resp.ShardMapName $smt2.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt2.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude db2
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName  -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $smt2.ServerName
    Assert-AreEqual $resp.ShardMapName $smt2.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt2.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude db2 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include db2 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -InputObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $smt2.ServerName
    Assert-AreEqual $resp.ShardMapName $smt2.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt2.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    ## Test resource id

    # Include db3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName

    Assert-AreEqual $resp.ServerName $smt3.ServerName
    Assert-AreEqual $resp.ShardMapName $smt3.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt3.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude db3
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

    Assert-AreEqual $resp.ServerName $smt3.ServerName
    Assert-AreEqual $resp.ShardMapName $smt3.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt3.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.Type "SqlShardMap"

    # Exclude db3 again - no errors and no resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include db3 - no errors and resp
    $resp = Add-AzureRmSqlDatabaseAgentTarget -ResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName

    # Test updating back to include shows membership type as Include again.
    Assert-AreEqual $resp.ServerName $smt3.ServerName
    Assert-AreEqual $resp.ShardMapName $smt3.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt3.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"

    ## Test piping

    # Add db4 to tg1
    $resp = $tg1 | Add-AzureRmSqlDatabaseAgentTarget -ServerName $smt4.ServerName -ShardMapName $smt4.ShardMapName -DatabaseName $smt4.DatabaseName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.ServerName $smt4.ServerName
    Assert-AreEqual $resp.ShardMapName $smt4.ShardMapName
    Assert-AreEqual $resp.DatabaseName $smt4.DatabaseName
    Assert-AreEqual $resp.RefreshCredential $jc1.ResourceId
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.Type "SqlShardMap"
}

function Test-Clear()
{
	Remove-AzureRmResourceGroup -Name ps6390 -Force
}