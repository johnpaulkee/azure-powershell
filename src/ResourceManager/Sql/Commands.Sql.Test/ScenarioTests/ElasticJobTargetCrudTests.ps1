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
	Tests adding server targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-AddServerTargetWithDefaultParam()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1

	# Server Target Helper Objects
	$st1 =  @{ ServerName = Get-ServerName; }

	try
	{
		# Include st1
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st1.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude st1
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $st1.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude st1 again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include st1 - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st1.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

function Test-AddServerTargetWithTargetGroupObject
{
		# Setup
		$a1 = Create-ElasticJobAgentTestEnvironment
		$jc1 = Create-JobCredentialForTest $a1
		$tg1 = Create-TargetGroupForTest $a1

		$st2 =  @{ ServerName = Get-ServerName; }

    # Include st2
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.TargetServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.TargetType "SqlServer"

    # Exclude st2
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-AreEqual $resp.TargetServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.TargetType "SqlServer"

    # Exclude s2 again - no errors and no resp
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp

    # Include st2 - no errors and resp
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.TargetServerName $st2.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.TargetType "SqlServer"
}

function Test-AddServerTargetWithTargetResourceId
{
			# Setup
		$a1 = Create-ElasticJobAgentTestEnvironment
		$jc1 = Create-JobCredentialForTest $a1
		$tg1 = Create-TargetGroupForTest $a1
		$st3 =  @{ ServerName = Get-ServerName; }
	
    # Include st3
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.TargetServerName $st3.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.TargetType "SqlServer"

    # Exclude s3
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-AreEqual $resp.TargetServerName $st3.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Exclude"
    Assert-AreEqual $resp.TargetType "SqlServer"

    # Exclude s3 again - no errors and no resp
    $resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
    Assert-Null $resp
}

function Test-AddServerTargetWithPiping
{
		# Setup
		$a1 = Create-ElasticJobAgentTestEnvironment
		$jc1 = Create-JobCredentialForTest $a1
		$tg1 = Create-TargetGroupForTest $a1
		$st4 =  @{ ServerName = Get-ServerName; }

    # Add s4 to tg1
    $resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $st4.ServerName -RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.TargetServerName $st4.ServerName
    Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
    Assert-AreEqual $resp.MembershipType "Include"
    Assert-AreEqual $resp.TargetType "SqlServer"
}

<#
	.SYNOPSIS
	Tests removing server targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-RemoveServerTargetWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1

	# Server Target Helper Objects
	$st1 =  @{ ServerName = Get-ServerName; }
	$st2 =  @{ ServerName = Get-ServerName; }
	$st3 =  @{ ServerName = Get-ServerName; }
	$st4 =  @{ ServerName = Get-ServerName; }

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $st1.ServerName -RefreshCredentialName $jc1.CredentialName
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName -Exclude
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $st4.ServerName -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s1
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st1.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $st1.ServerName `
			-RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp

		## Test target group object

		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st2.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $st2.ServerName -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp

		## Test target group resource id

		# Remove s3 - Should be excluded
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st3.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $st3.ServerName -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp

		## Test piping

		# Remove s4 to tg1
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $st4.ServerName -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $st4.ServerName
		Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

function Test-RemoveServerTargetWithTargetGroupObject
{

}

function Test-RemoveServerTargetWithTargetGroupResourceId
{

}

function Test-RemoveServerTargetWithPiping
{

}

<#
	.SYNOPSIS
	Tests adding targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-DatabaseTarget
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1

	# Target Helper Objects
	$dbt1 = @{ ServerName = Get-ServerName; DatabaseName = Get-DatabaseName } # Include
	$dbt2 = @{ ServerName = Get-ServerName; DatabaseName = Get-DatabaseName } # Include
	$dbt3 = @{ ServerName = Get-ServerName; DatabaseName = Get-DatabaseName } # Exclude
	$dbt4 = @{ ServerName = Get-ServerName; DatabaseName = Get-DatabaseName } # Include

	try
	{
		Test-AddDatabaseTarget $a1 $jc1 $tg1 $dbt1 $dbt2 $dbt3 $dbt4
		Test-RemoveDatabaseTarget $a1 $jc1 $tg1 $dbt1 $dbt2 $dbt3 $dbt4
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding database targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-AddDatabaseTarget($a1, $jc1, $tg1, $dbt1, $dbt2, $dbt3, $dbt4)
{
	## --------- Database Target Tests -------------
	## Test default parameters

	# Include db1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName

	Assert-AreEqual $resp.TargetServerName $dbt1.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt1.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName -Exclude

	Assert-AreEqual $resp.TargetServerName $dbt1.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt1.DatabaseName
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db1 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName -Exclude
	Assert-Null $resp

	# Include db1 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $dbt1.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt1.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"
	## Test input object

	# Include db2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName

	Assert-AreEqual $resp.TargetServerName $dbt2.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt2.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName -Exclude

	Assert-AreEqual $resp.TargetServerName $dbt2.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt2.DatabaseName
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db2 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName -Exclude
	Assert-Null $resp

	# Include db2 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $dbt2.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt2.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"
	## Test resource id

	# Include db3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName

	Assert-AreEqual $resp.TargetServerName $dbt3.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt3.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName -Exclude

	Assert-AreEqual $resp.TargetServerName $dbt3.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt3.DatabaseName
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Exclude db3 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName -Exclude
	Assert-Null $resp
	## Test piping

	# Add db4 to tg1
	$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $dbt4.ServerName -DatabaseName $dbt4.DatabaseName
	Assert-AreEqual $resp.TargetServerName $dbt4.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt4.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Add all databases from server in rg1 to tg1 (should be master & 1 user db)
	$added = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName | Get-AzureRmSqlDatabase | Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1
	Assert-AreEqual 2 $added.Count
}

<#
	.SYNOPSIS
	Tests removing database targets from target group
	.DESCRIPTION
	SmokeTest
#>
function Test-RemoveDatabaseTarget($a1, $jc1, $tg1, $dbt1, $dbt2, $dbt3, $dbt4)
{
	## --------- Database Target Tests -------------
	## Test default parameters

	# Remove db1
	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName
	Assert-AreEqual $resp.TargetServerName $dbt1.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt1.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Should have no resp
	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $dbt1.ServerName `
		-DatabaseName $dbt1.DatabaseName
	Assert-Null $resp
	## Test input object

	# Remove db2
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName
	Assert-AreEqual $resp.TargetServerName $dbt2.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt2.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Should have no resp
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $dbt2.ServerName -DatabaseName $dbt2.DatabaseName
	Assert-Null $resp
	## Test resource id

	# Remove db3
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName
	Assert-AreEqual $resp.TargetServerName $dbt3.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt3.DatabaseName
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlDatabase"

	# Should have no resp
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $dbt3.ServerName -DatabaseName $dbt3.DatabaseName
	Assert-Null $resp
	## Test piping

	# Remove db4 to tg1
	$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $dbt4.ServerName -DatabaseName $dbt4.DatabaseName
	Assert-AreEqual $resp.TargetServerName $dbt4.ServerName
	Assert-AreEqual $resp.TargetDatabaseName $dbt4.DatabaseName
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlDatabase"
	# Remove all databases from server in rg1 to tg1 (should be master & 1 user db)
	$removed = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName | Get-AzureRmSqlDatabase | Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1
	Assert-AreEqual 2 $removed.Count
}


<#
	.SYNOPSIS
	Tests adding and deleting elastic pool targets
	.DESCRIPTION
	SmokeTest
#>
function Test-ElasticPoolTarget
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1

	# Target Helper Objects
	$ept1 = @{ ServerName = Get-ServerName; ElasticPoolName = Get-ElasticPoolName; }
	$ept2 = @{ ServerName = Get-ServerName; ElasticPoolName = Get-ElasticPoolName; }
	$ept3 = @{ ServerName = Get-ServerName; ElasticPoolName = Get-ElasticPoolName; }
	$ept4 = @{ ServerName = Get-ServerName; ElasticPoolName = Get-ElasticPoolName; }

	try
	{
		Test-AddElasticPoolTarget $a1 $jc1 $tg1 $ept1 $ept2 $ept3 $ept4
		Test-RemoveElasticPoolTarget $a1 $jc1 $tg1 $ept1 $ept2 $ept3 $ept4
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding elastic pool targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-AddElasticPoolTarget($a1, $jc1, $tg1, $ept1, $ept2, $ept3, $ept4)
{
	## --------- Elastic Pool Target Tests -------------
	## Test default parameters

	# Include ep1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept1.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept1.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $ept1.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept1.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep1 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp

	# Include ep1 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $ept1.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept1.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"
	## Test input object

	# Include ep2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept2.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept2.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $ept2.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept2.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep2 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp

	# Include ep2 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $ept2.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept2.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	## Test resource id

	# Include ep3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept3.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept3.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $ept3.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept3.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Exclude ep3 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp

	## Test piping

	# Add ep4 to tg1
	$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $ept4.ServerName -ElasticPoolName $ept4.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $ept4.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept4.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"
}

<#
	.SYNOPSIS
	Tests removing elastic pool targets from target group
	.DESCRIPTION
	SmokeTest
#>
function Test-RemoveElasticPoolTarget($a1, $jc1, $tg1, $ept1, $ept2, $ept3, $ept4)
{
	## --------- Elastic Pool Target Tests -------------
	## Test default parameters

	# Remove ep1
	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept1.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept1.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $ept1.ServerName `
		-ElasticPoolName $ept1.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp

	## Test input object

	# Remove ep2
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept2.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept2.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $ept2.ServerName -ElasticPoolName $ept2.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp

	## Test resource id

	# Remove ep3
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $ept3.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept3.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $ept3.ServerName -ElasticPoolName $ept3.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp

	## Test piping

	# Remove ep4 to tg1
	$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $ept4.ServerName -ElasticPoolName $ept4.ElasticPoolName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $ept4.ServerName
	Assert-AreEqual $resp.TargetElasticPoolName $ept4.ElasticPoolName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlElasticPool"

	# Remove all elastic pools from rg1 to tg1
	$removed = Get-AzureRmSqlServer -ResourceGroupName $a1.ResourceGroupName | Get-AzureRmSqlElasticPool | Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual 1 $removed.Count
}

<#
	.SYNOPSIS
	Tests adding and deleting shard map targets
	.DESCRIPTION
	SmokeTest
#>
function Test-ShardMapTarget
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1

	# Target Helper Objects
	$smt1 = @{ ServerName = "s1"; ShardMapName = "sm1"; DatabaseName = "db1"} # Include
	$smt2 = @{ ServerName = "s1"; ShardMapName = "sm2"; DatabaseName = "db2"} # Include
	$smt3 = @{ ServerName = "s1"; ShardMapName = "sm3"; DatabaseName = "db3"} # Exclude
	$smt4 = @{ ServerName = "s1"; ShardMapName = "sm4"; DatabaseName = "db4"} # Include

	try
	{
		Test-AddShardMapTarget $a1 $jc1 $tg1 $smt1 $smt2 $smt3 $smt4
		Test-RemoveShardMapTarget $a1 $jc1 $tg1 $smt1 $smt2 $smt3 $smt4
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding shard map targets to target group
	.DESCRIPTION
	SmokeTest
#>
function Test-AddShardMapTarget($a1, $jc1, $tg1, $smt1, $smt2, $smt3, $smt4)
{
	## --------- Shard Map Target Tests -------------
	## Test default parameters

	# Include sm1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $smt1.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt1.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt1.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm1
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName  -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $smt1.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt1.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt1.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm1 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp

	# Include sm1 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $smt1.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt1.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt1.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	## Test input object

	# Include sm2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $smt2.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt2.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt2.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm2
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName  -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $smt2.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt2.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt2.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm2 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp

	# Include sm2 - no errors and resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName

	# Test updating back to include shows membership type as Include again.
	Assert-AreEqual $resp.TargetServerName $smt2.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt2.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt2.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	## Test resource id

	# Include sm3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName

	Assert-AreEqual $resp.TargetServerName $smt3.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt3.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt3.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm3
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude

	Assert-AreEqual $resp.TargetServerName $smt3.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt3.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt3.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	# Exclude sm3 again - no errors and no resp
	$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName -Exclude
	Assert-Null $resp
	## Test piping

	# Add sm4 to tg1
	$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $smt4.ServerName -ShardMapName $smt4.ShardMapName -DatabaseName $smt4.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $smt4.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt4.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt4.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"
}

<#
	.SYNOPSIS
	Tests removing shard map targets from target group
	.DESCRIPTION
	SmokeTest
#>
function Test-RemoveShardMapTarget($a1, $jc1, $tg1, $smt1, $smt2, $smt3, $smt4)
{
	## --------- Shard Map Target Tests -------------
	## Test default parameters

	# Remove sm1
	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $smt1.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt1.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt1.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
		$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $smt1.ServerName `
		-ShardMapName $smt1.ShardMapName -DatabaseName $smt1.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp
	## Test input object

	# Remove sm2
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $smt2.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt2.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt2.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $smt2.ServerName -ShardMapName $smt2.ShardMapName -DatabaseName $smt2.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp

	## Test resource id

	# Remove sm3
	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $smt3.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt3.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt3.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Exclude"
	Assert-AreEqual $resp.TargetType "SqlShardMap"

	$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $smt3.ServerName -ShardMapName $smt3.ShardMapName -DatabaseName $smt3.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-Null $resp
	## Test piping

	# Remove sm4
	$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $smt4.ServerName -ShardMapName $smt4.ShardMapName -DatabaseName $smt4.DatabaseName -RefreshCredentialName $jc1.CredentialName
	Assert-AreEqual $resp.TargetServerName $smt4.ServerName
	Assert-AreEqual $resp.TargetShardMapName $smt4.ShardMapName
	Assert-AreEqual $resp.TargetDatabaseName $smt4.DatabaseName
	Assert-AreEqual $resp.RefreshCredentialName $jc1.ResourceId
	Assert-AreEqual $resp.MembershipType "Include"
	Assert-AreEqual $resp.TargetType "SqlShardMap"
}