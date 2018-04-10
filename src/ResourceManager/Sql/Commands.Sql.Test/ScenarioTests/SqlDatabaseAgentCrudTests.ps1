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
	Tests creating an agent using default parameters
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = "agent1"
    $tags = @{ Dept="Finance" }

    try 
    {
        # Create an agent
    	$resp1 = New-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -DatabaseName $db1.DatabaseName -AgentName $a1 -Tags $tags

        Assert-AreEqual $resp1.AgentName $a1
        Assert-AreEqual $resp1.AgentServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentDatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
        Assert-AreEqual $resp1.Tags.Dept "Finance"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests creating an agent using control database object
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateAgentWithInputObject
{
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = New-AzureRmSqlDatabaseAgent -InputObject $db1 -Name "agent"
    Assert-AreEqual $a1.AgentName "agent"
    Assert-AreEqual $a1.ServerName $s1.ServerName
    Assert-AreEqual $a1.DatabaseName $db1.DatabaseName
    Assert-AreEqual $a1.ResourceGroupName $rg1.ResourceGroupName
    Assert-AreEqual $a1.Location $s1.Location
    Assert-AreEqual $a1.WorkerCount 100
}

<#
	.SYNOPSIS
	Tests creating an agent using control database resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateAgentWithResourceId
{
    New-AzureRmSqlDatabaseAgent -ResourceId /subscriptions/1b132fbc-df61-4dd8-bba0-71d7d4277eda/resourceGroups/ps2525/providers/Microsoft.Sql/servers/ps6926/databases/db1 -Name "agent"
}

<#
	.SYNOPSIS
	Tests updating an agent
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $tags = @{ Dept="Finance" }
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1" @{ Dept="Finance" }
    Assert-NotNull $a1
    Assert-AreEqual $a1.Tags.Dept "Finance"

    try 
    {
        # Test default update tags
        $newTags = @{ Dept="CS"; Test="NewTag" }
    	$resp1 = Set-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Tags $newTags

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.AgentServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentDatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
        Assert-AreEqual $resp1.Tags.Dept "CS"
        Assert-AreEqual $resp1.Tags.Test "NewTag"

        # Test Piping
        $newTags = @{ Dept="Math"; }
        $resp2 = $a1 | Set-AzureRmSqlDatabaseAgent -Tags $newTags
        Assert-AreEqual $resp2.AgentName $a1.AgentName
        Assert-AreEqual $resp2.AgentServerName $s1.ServerName
        Assert-AreEqual $resp2.AgentDatabaseName $db1.DatabaseName
        Assert-AreEqual $resp2.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp2.Location $s1.Location
        Assert-AreEqual $resp2.WorkerCount 100
        Assert-AreEqual $resp2.Tags.Dept "Math"
        Assert-Null $resp2.Tags.Test
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests updating an agent with agent input object
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateAgentWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -Name agent -ServerName ps6926 -ResourceGroupName ps2525
    Set-AzureRmSqlDatabaseAgent -InputObject $agent -Tag @{ Dept="CS" }
}

<#
	.SYNOPSIS
	Tests updating an agent with agent resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateAgentWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -Name agent -ServerName ps6926 -ResourceGroupName ps2525
    Set-AzureRmSqlDatabaseAgent -InputObject $agent -Tag @{ Dept="CS" }
}

<#
	.SYNOPSIS
	Tests getting one or more agents
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $rg2 = Create-ResourceGroupForTest

    # Create 2 servers
    $s1 = Create-ServerForTest $rg1 "westus2"
    $s2 = Create-ServerForTest $rg2 "westus2"

    # Create 3 dbs - 2 in s1 and 1 in s2
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $db2 = Create-DatabaseForTest $rg1 $s1 "db2"
    $db3 = Create-DatabaseForTest $rg2 $s2 "db3"

    # Create 3 agents per control db
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"
    $a2 = Create-AgentForTest $rg1 $s1 $db2 "agent2"
    $a3 = Create-AgentForTest $rg2 $s2 $db3 "agent3"
    
    try {
        # Basic get
        $resp1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100

        # Test basic pipe of agent
        $resp2 = $a3 | Get-AzureRmSqlDatabaseAgent
        Assert-AreEqual $resp2.AgentName $a3.AgentName
        Assert-AreEqual $resp2.ServerName $s2.ServerName
        Assert-AreEqual $resp2.DatabaseName $db3.DatabaseName
        Assert-AreEqual $resp2.ResourceGroupName $rg2.ResourceGroupName
        Assert-AreEqual $resp2.Location $s2.Location
        Assert-AreEqual $resp2.WorkerCount 100

        # Test pipe get agents from server
        $resp3 = $s1 | Get-AzureRmSqlDatabaseAgent
        Assert-AreEqual $resp3.Count 2

        # Test pipe get agents from all servers
        $resp4 = Get-AzureRmSqlServer | Get-AzureRmSqlDatabaseAgent
        Assert-AreEqual $resp4.Count 3
    }
    finally
    {
    	Remove-ResourceGroupForTest $rg1
        Remove-ResourceGroupForTest $rg2
    }
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agent using resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgentWithInputObject
{
    # Setup TODO
    $s1 = Get-AzureRmSqlServer -ResourceGroupName ps2525 -ServerName ps6926
    Get-AzureRmSqlDatabaseAgent -InputObject $s1
    Get-AzureRmSqlDatabaseAgent -InputObject $s1 -Name agent
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agent using resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgentWithResourceId
{
    # Setup TODO
    $s1 = Get-AzureRmSqlServer -ResourceGroupName ps2525 -ServerName ps6926
    Get-AzureRmSqlDatabaseAgent -ResourceId $s1.ResourceId
    Get-AzureRmSqlDatabaseAgent -ResourceId $s1.ResourceId -Name agent
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agent using resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAllAgents
{
    Get-AzureRmSqlDatabaseAgent -AgentServerName sjobaccount35 -ResourceGroupName Job_Account_Test
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agents
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $db2 = Create-DatabaseForTest $rg1 $s1 "db2"

    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"
    $a2 = Create-AgentForTest $rg1 $s1 $db2 "agent2"

    try
    {
        # Test using parameters
        Remove-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName

        # Test using Piping
        $a2 | Remove-AzureRmSqlDatabaseAgent

        $all = Get-AzureRmSqlServer | Get-AzureRmSqlDatabaseAgent
		Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agent using Input Object
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgentByInputObject
{
    # Setup TODO
     $a1 = Get-AzureRmSqlDatabaseAgent -AgentName jpagenttest -AgentServerName ps9823 -ResourceGroupName ps2398
     Remove-AzureRmSqlDatabaseAgent -InputObject $a1
}

<#
	.SYNOPSIS
	Tests removing Azure SQL Database Agent using resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgentByResourceId
{
    # Setup TODO
     $a1 = Get-AzureRmSqlDatabaseAgent -AgentName jpagenttest -AgentServerName ps9823 -ResourceGroupName ps2398
     Remove-AzureRmSqlDatabaseAgent -ResourceId $a1.Id -Force
}