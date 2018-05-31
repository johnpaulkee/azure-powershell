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
#>
function Test-CreateAgentWithDefaultParam
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $agentName = Get-AgentName

    try
    {
        # Test using default parameters
        $resp = New-AzureRmSqlElasticJobAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -DatabaseName $db1.DatabaseName -AgentName $agentName
        Assert-AreEqual $resp.AgentName $agentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating an agent using database object
#>
function Test-CreateAgentWithDatabaseObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $agentName = Get-AgentName

    try
    {
        # Test using database object
        $resp = New-AzureRmSqlElasticJobAgent -DatabaseObject $db1 -Name $agentName
        Assert-AreEqual $resp.AgentName $agentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating an agent using database resource id
#>
function Test-CreateAgentWithDatabaseResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $agentName = Get-AgentName

    try
    {
        # Test using database resource id
        $resp = New-AzureRmSqlElasticJobAgent -DatabaseResourceId $db1.ResourceId -Name $agentName
        Assert-AreEqual $resp.AgentName $agentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating an agent using piping
#>
function Test-CreateAgentWithPiping
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $agentName = Get-AgentName

    try
    {
        # Test piping - Create using piping
        $resp = $db1 | New-AzureRmSqlElasticJobAgent -Name $agentName
        Assert-AreEqual $resp.AgentName $agentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating an agent with default param
#>
function Test-UpdateAgentWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $tags = @{ Octopus="Agent"}

    try
    {
        # Test using default parameters
        $resp = Set-AzureRmSqlElasticJobAgent -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Tag $tags
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
        Assert-AreEqual $resp.Tags.Octopus "Agent"
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating an agent with input object
#>
function Test-UpdateAgentWithInputObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $tags = @{ Octopus="Agent"}

    try
    {
        # Test using input object
        $resp = Set-AzureRmSqlElasticJobAgent -InputObject $a1 -Tag $tags
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
        Assert-AreEqual $resp.Tags.Octopus "Agent"
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating an agent with resource id
#>
function Test-UpdateAgentWithResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $tags = @{ Octopus="Agent"}

    try
    {
        # Test using resource id
        $resp = Set-AzureRmSqlElasticJobAgent -ResourceId $a1.ResourceId -Tag $tags
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
        Assert-AreEqual $resp.Tags.Octopus "Agent"
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating an agent with piping
#>
function Test-UpdateAgentWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $tags = @{ Octopus="Agent"}

    try
    {
        # Test using piping
        $resp = $a1 | Set-AzureRmSqlElasticJobAgent -Tag $tags
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
        Assert-AreEqual $resp.Tags.Octopus "Agent"
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting an agent
#>
function Test-GetAgentWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment

    try
    {
        # Test using default parameters
        $resp = Get-AzureRmSqlElasticJobAgent -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting an agent with server object
#>
function Test-GetAgentWithServerObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $s1 = Get-AzureRmSqlServer -ServerName $a1.ServerName -ResourceGroupName $a1.ResourceGroupName

    try
    {
        # Test using input object
        $resp = Get-AzureRmSqlElasticJobAgent -ServerObject $s1 -AgentName $a1.AgentName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting an agent with server resource id
#>
function Test-GetAgentWithServerResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $s1 = Get-AzureRmSqlServer -ServerName $a1.ServerName -ResourceGroupName $a1.ResourceGroupName

    try
    {
        # Test using server resource id
        $resp = Get-AzureRmSqlElasticJobAgent -ServerResourceId $s1.ResourceId -AgentName $a1.AgentName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting an agent with piping
#>
function Test-GetAgentWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $s1 = Get-AzureRmSqlServer -ServerName $a1.ServerName -ResourceGroupName $a1.ResourceGroupName
    $db2 = $s1 | New-AzureRmSqlDatabase -Name (Get-DatabaseName)
    $a2 = $db2 | New-AzureRmSqlElasticJobAgent -Name (Get-AgentName)

    try
    {
        # Test using piping
        $resp = $s1 | Get-AzureRmSqlElasticJobAgent -Name $a1.AgentName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.DatabaseName $a1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.Location $a1.Location
        Assert-AreEqual $resp.WorkerCount 100

        $resp = $s1 | Get-AzureRmSqlElasticJobAgent
        Assert-AreEqual $resp.Count 2
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing an agent with default param
#>
function Test-RemoveAgentWithDefaultParam
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        # Test using parameters
        $resp = Remove-AzureRmSqlElasticJobAgent -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing an agent with input object
#>
function Test-RemoveAgentWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        # Test using input object
        $resp = Remove-AzureRmSqlElasticJobAgent -InputObject $a1
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing an agent with resource id
#>
function Test-RemoveAgentWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        # Test using resource id
        $resp = Remove-AzureRmSqlElasticJobAgent -ResourceId $a1.ResourceId
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing an agent with piping
#>
function Test-RemoveAgentWithPiping
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $s1
    $a1 = Create-AgentForTest $db1

    try
    {
        # Test using piping
        $resp = $a1 | Remove-AzureRmSqlElasticJobAgent
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.ServerName $s1.ServerName
        Assert-AreEqual $resp.DatabaseName $db1.DatabaseName
        Assert-AreEqual $resp.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp.Location $s1.Location
        Assert-AreEqual $resp.WorkerCount 100
    }
    finally
    {
        Remove-ResourceGroupForTest $a1
    }
}