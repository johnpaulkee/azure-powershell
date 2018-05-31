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
    Tests creating a job credential
    .DESCRIPTION
    SmokeTest
#>
function Test-CreateJobCredentialWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try
    {
        # Create using default params
        $resp = New-AzureRmSqlElasticJobCredential -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $cn1 -Credential $c1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.CredentialName $cn1
        Assert-AreEqual $resp.UserName $c1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating a job credential with agent object
    .DESCRIPTION
    SmokeTest
#>
function Test-CreateJobCredentialWithAgentObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try
    {
        # Create using agent input object
        $resp = New-AzureRmSqlElasticJobCredential -AgentObject $a1 -Name $cn1 -Credential $c1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.CredentialName $cn1
        Assert-AreEqual $resp.UserName $c1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating a job credential with resource id
    .DESCRIPTION
    SmokeTest
#>
function Test-CreateJobCredentialWithAgentResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try
    {
        # Create using agent resource id
        $resp = New-AzureRmSqlElasticJobCredential -AgentResourceId $a1.ResourceId -Name $cn1 -Credential $c1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.CredentialName $cn1
        Assert-AreEqual $resp.UserName $c1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests creating a job credential with piping
    .DESCRIPTION
    SmokeTest
#>
function Test-CreateJobCredentialWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $cn1 = Get-JobCredentialName
    $cn1 = Get-JobCredentialName
    $cn1 = Get-JobCredentialName
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try
    {
        # Tests using piping
        $resp = $a1 | New-AzureRmSqlElasticJobCredential -Name $cn1 -Credential $c1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.CredentialName $cn1
        Assert-AreEqual $resp.UserName $c1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating a job credential with default param
    .DESCRIPTION
    SmokeTest
#>
function Test-UpdateJobCredentialWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test default parameters
        $newCred = Get-Credential
        $resp = Set-AzureRmSqlElasticJobCredential -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName -Credential $newCred
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $newCred.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating a job credential with input object
    .DESCRIPTION
    SmokeTest
#>
function Test-UpdateJobCredentialWithInputObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test job credential input object
        $newCred = Get-Credential
        $resp = Set-AzureRmSqlElasticJobCredential -InputObject $jc1 -Credential $newCred
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $newCred.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating a job credential with resource id
    .DESCRIPTION
    SmokeTest
#>
function Test-UpdateJobCredentialWithResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test job credential resource id
        $newCred = Get-Credential
        $resp = Set-AzureRmSqlElasticJobCredential -ResourceId $jc1.ResourceId -Credential $newCred
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $newCred.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests updating a job credential with piping
    .DESCRIPTION
    SmokeTest
#>
function Test-UpdateJobCredentialWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test piping
        $newCred = Get-Credential
        $resp = $jc1 | Set-AzureRmSqlElasticJobCredential -Credential $newCred
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $newCred.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting a job credential
    .DESCRIPTION
    SmokeTest
#>
function Test-GetJobCredentialWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1

    try
    {
        # Test default parameters - get specific credential
        $resp = Get-AzureRmSqlElasticJobCredential -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName

        # Test default parameters - get credentials
        $resp = Get-AzureRmSqlElasticJobCredential -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName
        Assert-True { $resp.Count -ge 2 }
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting a job credential with agent object
    .DESCRIPTION
    SmokeTest
#>
function Test-GetJobCredentialWithAgentObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1 

    try
    {
        # Test job credential input object
        $resp = Get-AzureRmSqlElasticJobCredential -AgentObject $a1 -Name $jc1.CredentialName

        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName

        # Test job credential input object - get credentials
        $resp = Get-AzureRmSqlElasticJobCredential -AgentObject $a1
        Assert-True { $resp.Count -ge 2 }
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting a job credential with agent resource id
    .DESCRIPTION
    SmokeTest
#>
function Test-GetJobCredentialWithAgentResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1

    try
    {
        # Test job credential resource id
        $resp = Get-AzureRmSqlElasticJobCredential -AgentResourceId $a1.ResourceId -Name $jc1.CredentialName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName

        # Test job credential resource id - get credentials
        $resp = Get-AzureRmSqlElasticJobCredential -AgentResourceId $a1
        Assert-True { $resp.Count -ge 2 }
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests getting a job credential with piping
    .DESCRIPTION
    SmokeTest
#>
function Test-GetJobCredentialWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1

    try
    {
        # Test piping - get job credential
        $resp = $a1 | Get-AzureRmSqlElasticJobCredential -Name $jc1.CredentialName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName

        # Test piping - get all credentials
        $resp = $a1 | Get-AzureRmSqlElasticJobCredential
        Assert-True { $resp.Count -ge 2 }
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing a job credential with default param
    .DESCRIPTION
    SmokeTest
#>
function Test-RemoveJobCredentialWithDefaultParam
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test default parameters - Remove credential
        $resp = Remove-AzureRmSqlElasticJobCredential -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing a job credential with input object
    .DESCRIPTION
    SmokeTest
#>
function Test-RemoveJobCredentialWithInputObject
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test input object
        $resp = Remove-AzureRmSqlElasticJobCredential -InputObject $jc1
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing a job credential with resource id
    .DESCRIPTION
    SmokeTest
#>
function Test-RemoveJobCredentialWithResourceId
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1

    try
    {
        # Test resource id
        $resp = Remove-AzureRmSqlElasticJobCredential -ResourceId $jc1.ResourceId
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}

<#
    .SYNOPSIS
    Tests removing a job credential with piping
    .DESCRIPTION
    SmokeTest
#>
function Test-RemoveJobCredentialWithPiping
{
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment
    $jc1 = Create-JobCredentialForTest $a1
    $jc2 = Create-JobCredentialForTest $a1

    try
    {
        # Test piping
        $resp = $jc1 | Remove-AzureRmSqlElasticJobCredential
        Assert-AreEqual $resp.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp.ServerName $a1.ServerName
        Assert-AreEqual $resp.AgentName $a1.AgentName
        Assert-AreEqual $resp.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp.UserName $jc1.UserName

        # Test remove all
        $all = $a1 | Get-AzureRmSqlElasticJobCredential
        $resp = $all | Remove-AzureRmSqlElasticJobCredential
        Assert-IsTrue { $resp.Count -ge 2 }
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}