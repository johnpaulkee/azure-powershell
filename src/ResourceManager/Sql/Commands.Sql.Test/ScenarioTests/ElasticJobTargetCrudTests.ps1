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
#>
function Test-AddServerTargetWithDefaultParam()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	try
	{
		# Include targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetServer - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding server targets to target group using target group object
#>
function Test-AddServerTargetWithTargetGroupObject()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	try
	{
		# Include targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetServer - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding server targets to target group using target group resource id
#>
function Test-AddServerTargetWithTargetGroupResourceId()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	try
	{
		# Include targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetServer - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding server targets to target group with piping
#>
function Test-AddServerTargetWithPiping()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	try
	{
		# Include targetServer
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Exclude targetServer again - no errors and no resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetServer - no errors and resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Add all server targets in subscription
		$allServers = Get-AzureRmSqlServer
		$resp = $allServers | Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.Count $allServers.Count
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing server targets to target group using default param
#>
function Test-RemoveServerTargetWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName	$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName $a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing server targets to target group using target group object
#>
function Test-RemoveServerTargetWithTargetGroupObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing server targets to target group using target group resource id
#>
function Test-RemoveServerTargetWithTargetGroupResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName -Exclude

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing server targets to target group with piping
#>
function Test-RemoveServerTargetWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -RefreshCredentialName $jc1.CredentialName # Add agent server

	try
	{
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlServer"

		# Try remove again - should have no resp
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp

		# Try remove all server targets in subscription
		$allServers = Get-AzureRmSqlServer
		$resp = $allServers | Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -RefreshCredentialName $jc1.CredentialName
		Assert-NotNull $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding database targets to target group using default param
#>
function Test-AddDatabaseTargetWithDefaultParam()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-Null $resp

		# Include targetDatabaseName - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding database targets to target group using target group object
#>
function Test-AddDatabaseTargetWithTargetGroupObject()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-Null $resp

		# Include targetDatabaseName - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding database targets to target group using target group resource id
#>
function Test-AddDatabaseTargetWithTargetGroupResourceId()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1 -Exclude
		Assert-Null $resp

		# Include targetDatabaseName - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 `
			-DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding database targets to target group with piping
#>
function Test-AddDatabaseTargetWithPiping()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetDatabaseName
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1 -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Exclude targetDatabaseName again - no errors and no resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1 -Exclude
		Assert-Null $resp

		# Include targetDatabaseName - no errors and resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Add all dbs
		$allDbs = Get-AzureRmSqlServer | Get-AzureRmSqlDatabase
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-NotNull $resp # Assert added dbs
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing database target to target group using default param
#>
function Test-RemoveDatabaseTargetWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName	$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName $a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing database target to target group using target group object
#>
function Test-RemoveDatabaseTargetWithTargetGroupObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing database target to target group using target group resource id
#>
function Test-RemoveDatabaseTargetWithTargetGroupResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing database target to target group with piping
#>
function Test-RemoveDatabaseTargetWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
	$targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $a1.ServerName -DatabaseName $a1.DatabaseName

	try
	{
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.TargetServerName $targetServerName1
		Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlDatabase"

		# Try remove again - should have no resp
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-Null $resp

	 # Remove all dbs
		$allDbs = Get-AzureRmSqlServer | Get-AzureRmSqlDatabase
		$resp = $tg1 | remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -DatabaseName $targetDatabaseName1
		Assert-NotNull $resp # Assert added dbs
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding elastic pool target to target group
#>
function Test-AddElasticPoolTargetWithDefaultParam()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	try
	{
		# Include targetElasticPool
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetElasticPool
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetElasticPool again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetElasticPool - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding elastic pool target to target group using target group object
#>
function Test-AddElasticPoolTargetWithTargetGroupObject()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	try
	{
		# Include targetElasticPool
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetElasticPool - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding elastic pool target to target group using target group resource id
#>
function Test-AddElasticPoolTargetWithTargetGroupResourceId()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	try
	{
		# Include targetElasticPool
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetElasticPool - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding elastic pool target to target group with piping
#>
function Test-AddElasticPoolTargetWithPiping()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$ep1 = Create-ElasticPoolForTest $a1 # create pool on agent's server
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	try
	{
		# Include targetElasticPool
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Exclude targetServer again - no errors and no resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetElasticPool - no errors and resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Add all pools
		$allEps = Get-AzureRmSqlServer | Get-AzureRmSqlElasticPool
		$resp = $allEps | Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -RefreshCredentialName $jc1.CredentialName
		Assert-NotNull $resp
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing elastic pool target to target group using default param
#>
function Test-RemoveElasticPoolTargetWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName	$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName $a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing elastic pool target to target group using target group object
#>
function Test-RemoveElasticPoolTargetWithTargetGroupObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing elastic pool target to target group using target group resource id
#>
function Test-RemoveElasticPoolTargetWithTargetGroupResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName -Exclude

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing elastic pool target to target group with piping
#>
function Test-RemoveElasticPoolTargetWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$ep1 = Create-ElasticPoolForTest $a1	
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetElasticPoolName1 = Get-ElasticPoolName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s2
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetElasticPoolName $targetElasticPoolName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlElasticPool"

		# Try remove again - should have no resp
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ElasticPoolName $targetElasticPoolName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp

		# Remove all pools
		$allEps = Get-AzureRmSqlServer | Get-AzureRmSqlElasticPool
		$resp = $allEps | Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -RefreshCredentialName $jc1.CredentialName
		Assert-NotNull $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding shard map target to target group
#>
function Test-AddShardMapTargetWithDefaultParam()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetShardMap
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetShardMap
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetShardMap again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetShardMap - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName `
			$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 `
			-ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding shard map target to target group using target group object
#>
function Test-AddShardMapTargetWithTargetGroupObject()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetShardMap
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetShardMap - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding shard map target to target group using target group resource id
#>
function Test-AddShardMapTargetWithTargetGroupResourceId()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetShardMap
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer again - no errors and no resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetShardMap - no errors and resp
		$resp = Add-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests adding shard map target to target group with piping
#>
function Test-AddShardMapTargetWithPiping()
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	try
	{
		# Include targetShardMap
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Exclude targetServer again - no errors and no resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude
		Assert-Null $resp

		# Include targetShardMap - no errors and resp
		$resp = $tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"
	}
	finally
	{
		#Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing shard map target to target group using default param
#>
function Test-RemoveShardMapTargetWithDefaultParam
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName	$a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -ResourceGroupName $a1.ResourceGroupName -AgentServerName $a1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1.TargetGroupName -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing shard map target to target group using target group object
#>
function Test-RemoveShardMapTargetWithTargetGroupObject
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupObject $tg1 -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing shard map target to target group using target group resource id
#>
function Test-RemoveShardMapTargetWithTargetGroupResourceId
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName -Exclude

	try
	{
		# Remove s2
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Exclude"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Try remove again - should have no resp
		$resp = Remove-AzureRmSqlElasticJobTarget -TargetGroupResourceId $tg1.ResourceId -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}

<#
	.SYNOPSIS
	Tests removing shard map target to target group with piping
#>
function Test-RemoveShardMapTargetWithPiping
{
	# Setup
	$a1 = Create-ElasticJobAgentTestEnvironment
	$jc1 = Create-JobCredentialForTest $a1
	$tg1 = Create-TargetGroupForTest $a1
	$targetServerName1 = Get-ServerName
  $targetShardMapName1 = Get-ShardMapName
  $targetDatabaseName1 = Get-DatabaseName

	# Add targets
	$tg1 | Add-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName

	try
	{
		# Remove s2
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.TargetServerName $targetServerName1
    Assert-AreEqual $resp.TargetShardMapName $targetShardMapName1
    Assert-AreEqual $resp.TargetDatabaseName $targetDatabaseName1
		Assert-AreEqual $resp.RefreshCredentialName $jc1.CredentialName
		Assert-AreEqual $resp.MembershipType "Include"
		Assert-AreEqual $resp.TargetType "SqlShardMap"

		# Try remove again - should have no resp
		$resp = $tg1 | Remove-AzureRmSqlElasticJobTarget -ServerName $targetServerName1 -ShardMapName $targetShardMapName1 -DatabaseName $targetDatabaseName1 -RefreshCredentialName $jc1.CredentialName
		Assert-Null $resp
	}
	finally
	{
		# Remove-ResourceGroupForTest $a1
	}
}