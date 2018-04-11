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
function Test-CreateJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    # Credential params
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Name $cn1 -Credential $c1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.CredentialName $cn1
        Assert-AreEqual $resp1.UserName $c1.UserName
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests creating a job credential with agent input object
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobCredentialWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    # Credential params
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgentJobCredential -InputObject $a1 -Name $cn1 -Credential $c1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.CredentialName $cn1
        Assert-AreEqual $resp1.UserName $c1.UserName
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests creating a job credential with agent resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobCredentialWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1

    # Credential params
    $cn1 = Get-JobCredentialName
    $c1 = Get-ServerCredential

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgentJobCredential -ResourceId $a1.ResourceId -Name $cn1 -Credential $c1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.CredentialName $cn1
        Assert-AreEqual $resp1.UserName $c1.UserName
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests updating a job credential
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1

    try
    {
        $newCred = Get-Credential

        # Test parameters
        $resp1 = Set-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName -Credential $newCred

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $newCred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests updating a job credential with job credential input object
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredentialWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1

    try
    {
        $newCred = Get-Credential

        # Test parameters
        $resp1 = Set-AzureRmSqlDatabaseAgentJobCredential -InputObject $jc1 -Credential $newCred

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $newCred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests updating a job credential with job credential resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredentialWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1

    try
    {
        $newCred = Get-Credential

        # Test parameters
        $resp1 = Set-AzureRmSqlDatabaseAgentJobCredential -ResourceId $jc1.ResourceId -Credential $newCred

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $newCred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests getting a job credential
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        # Test parameters
        $resp1 = Get-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests getting a job credential with agent input object
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredentialWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        $resp1 = Get-AzureRmSqlDatabaseAgentJobCredential -InputObject $a1 -Name $jc1.CredentialName

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests getting a job credential with agent resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredentialWithResourceId
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        $resp1 = Get-AzureRmSqlDatabaseAgentJobCredential -ResourceId $a1.ResourceId -Name $jc1.CredentialName

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests removing a job credential
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $jc2 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        # Test parameters
        $resp1 = Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -Name $jc1.CredentialName

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName

        $resp2 = $jc2 | Remove-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $resp2.CredentialName $jc2.CredentialName

   		$all = Get-AzureRmSqlDatabaseAgent -InputObject $s1 -Name $a1.AgentName | Get-AzureRmSqlDatabaseAgentJobCredential
		Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests removing a job credential with job credential input object
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredentialWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        # Test parameters
        $resp1 = Remove-AzureRmSqlDatabaseAgentJobCredential -InputObject $jc1

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName

   		$all = Get-AzureRmSqlDatabaseAgent -InputObject $s1 -Name $a1.AgentName | Get-AzureRmSqlDatabaseAgentJobCredential
		Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests removing a job credential with job credential resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredentialWithInputObject
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1
    $a1 = Create-AgentForTest $rg1 $s1 $db1
    $jc1 = Create-JobCredentialForTest $rg1 $s1, $a1
    $cred = Get-ServerCredential

    try
    {
        # Test parameters
        $resp1 = Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceId $jc1.ResourceId

        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.ServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $cred.UserName

   		$all = Get-AzureRmSqlDatabaseAgent -InputObject $s1 -Name $a1.AgentName | Get-AzureRmSqlDatabaseAgentJobCredential
		Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}