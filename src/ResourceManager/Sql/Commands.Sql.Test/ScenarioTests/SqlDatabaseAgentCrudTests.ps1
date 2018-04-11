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
    $db1 = Create-DatabaseForTest $rg1 $s1
    $agentName = Get-AgentName

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -DatabaseName $db1.DatabaseName -AgentName $agentName

        Assert-AreEqual $resp1.AgentName $agentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
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
    $db1 = Create-DatabaseForTest $rg1 $s1
    $agentName = Get-AgentName

    try
    {
        $a1 = New-AzureRmSqlDatabaseAgent -InputObject $db1 -Name $agentName

        Assert-AreEqual $a1.AgentName $agentName
        Assert-AreEqual $a1.ServerName $s1.ServerName
        Assert-AreEqual $a1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $a1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $a1.Location $s1.Location
        Assert-AreEqual $a1.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests creating an agent using control database resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateAgentWithResourceId
{
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $agentName = Get-AgentName

    try
    {
        $a1 = New-AzureRmSqlDatabaseAgent -ResourceId $db1.ResourceId -Name $agentName

        Assert-AreEqual $a1.AgentName $agentName
        Assert-AreEqual $a1.ServerName $s1.ServerName
        Assert-AreEqual $a1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $a1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $a1.Location $s1.Location
        Assert-AreEqual $a1.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
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
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    $tags1 = @{ Dept="Finance"; AnotherTag="WOOHOO" }

    try
    {
    	$resp1 = Set-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Tag $tags1

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
        Assert-AreEqual $resp1.Tags.Dept "Finance"
        Assert-AreEqual $resp1.Tags.AnotherTag "WOOHOO"
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
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    $tags1 = @{ Dept="Finance"; AnotherTag="WOOHOO" }

    try
    {
    	$resp1 = Set-AzureRmSqlDatabaseAgent -InputObject $a1 -Tag $tags1

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
        Assert-AreEqual $resp1.Tags.Dept "Finance"
        Assert-AreEqual $resp1.Tags.AnotherTag "WOOHOO"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests updating an agent with agent resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateAgentWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    $tags1 = @{ Dept="Finance"; AnotherTag="WOOHOO" }

    try
    {
    	$resp1 = Set-AzureRmSqlDatabaseAgent -ResourceId $a1.ResourceId -Tag $tags1

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
        Assert-AreEqual $resp1.Tags.Dept "Finance"
        Assert-AreEqual $resp1.Tags.AnotherTag "WOOHOO"
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests getting an agent
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    
    try {
        # Basic get
        $resp1 = Get-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
    }
    finally
    {
    	Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests getting an agent with a server input object
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgentWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    
    try {
        # Basic get
        $resp1 = Get-AzureRmSqlDatabaseAgent -InputObject $s1 -AgentName $a1.AgentName

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
    }
    finally
    {
    	Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests getting an agent with a server resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetAgentWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    
    try {
        # Basic get
        $resp1 = Get-AzureRmSqlDatabaseAgent -ResourceId $s1.ResourceId -AgentName $a1.AgentName

        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.Location $s1.Location
        Assert-AreEqual $resp1.WorkerCount 100
    }
    finally
    {
    	Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing an agent
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgent
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    try
    {
        # Test using parameters
        Remove-AzureRmSqlDatabaseAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing an agent with an agent input object
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgentWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    try
    {
        # Test using parameters
        Remove-AzureRmSqlDatabaseAgent -InputObject $a1
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing an agent with an agent resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveAgentWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    try
    {
        # Test using parameters
        Remove-AzureRmSqlDatabaseAgent -ResourceId $a1.ResourceId
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg1
    }
}