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
	Tests creating an Azure Sql Database Agent Job Credential
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"

    # Credential params
    $cn1 = "credName1"
    $cn2 = "credName2"
    $u1 = "testusername"
	$p1 = "t357ingP@s5w0rd!"
	$c1 = new-object System.Management.Automation.PSCredential($u1, ($p1 | ConvertTo-SecureString -asPlainText -Force))

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName `
            -CredentialName $cn1 -Credential $c1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.AgentServerName $s1.ServerName
        Assert-AreEqual $resp1.CredentialName $cn1
        Assert-AreEqual $resp1.UserName $c1.UserName

        # Test piping
        $resp2 = $a1 | New-AzureRmSqlDatabaseAgentJobCredential -CredentialName $cn2 -Credential $c1
        Assert-AreEqual $resp2.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp2.AgentServerName $s1.ServerName
        Assert-AreEqual $resp2.CredentialName $cn2
        Assert-AreEqual $resp2.UserName $c1.UserName
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests getting an Azure Sql Database Agent Job Credential
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"

    # Credential params
    $cn1 = "credName1"
    $cn2 = "credName2"
    $u1 = "testusername"
	$p1 = "t357ingP@s5w0rd!"
	$c1 = new-object System.Management.Automation.PSCredential($u1, ($p1 | ConvertTo-SecureString -asPlainText -Force))

    $jc1 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn1 $c1
    $jc2 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn2 $c1
        
    try 
    {
        # Test parameters
        $resp1 = Get-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName `
            -CredentialName $cn1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.AgentServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $c1.UserName

        # Test piping - Get all credentials in agent
        $all = $a1 | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $resp2.Count 2
		($jc1, $jc2) | ForEach-Object { Assert-True {$_.UserName -in $all.UserName} }
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests removing an Azure Sql Database Agent Job Credential
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"

    # Credential params
    $cn1 = "credName1"
    $cn2 = "credName2"
    $u1 = "testusername"
	$p1 = "t357ingP@s5w0rd!"
	$c1 = new-object System.Management.Automation.PSCredential($u1, ($p1 | ConvertTo-SecureString -asPlainText -Force))

    $jc1 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn1 $c1
    $jc2 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn2 $c1

    try
    {
        $u2 = "newUserName"
        $p2 = "newPassword"
      	$c2 = new-object System.Management.Automation.PSCredential($u2, ($p2 | ConvertTo-SecureString -asPlainText -Force))

        # Test parameters
        $resp1 = Set-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -CredentialName $cn1 -Credential $c2
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.AgentServerName $s1.ServerName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.CredentialName $jc1.CredentialName
        Assert-AreEqual $resp1.UserName $c2.UserName

        # Test piping
        $resp2 = $jc2 | Set-AzureRmSqlDatabaseAgentJobCredential -Credential $c2
        Assert-AreEqual $resp2.UserName $c2.UserName

        $u3 = "oneMoreUserName"
        $p3 = "oneMoreTime"
      	$c3 = new-object System.Management.Automation.PSCredential($u2, ($p2 | ConvertTo-SecureString -asPlainText -Force))

        $all = $a | Get-AzureRmSqlDatabaseAgentJobCredential
        $setAll = $all | Set-AzureRmSqlDatabaseAgentJobCredential -Credential $c3
        Assert-AreEqual $setAll.Count 2
        $setAll | % { Assert-AreEqual $_.UserName $c3.UserName }
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests removing an Azure Sql Database Agent Job Credential
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredential
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"

    # Credential params
    $cn1 = "credName1"
    $cn2 = "credName2"
    $cn3 = "credName3"
    $u1 = "testusername"
	$p1 = "t357ingP@s5w0rd!"
	$c1 = new-object System.Management.Automation.PSCredential($u1, ($p1 | ConvertTo-SecureString -asPlainText -Force))

    $jc1 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn1 $c1
    $jc2 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn2 $c1
    $jc3 = Create-JobCredentialForTest $rg1 $s1 $a1 $cn3 $c1

    try
    {
        # Check 3 credential are left
        $all = $a1 | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 3

        # Test using params
        Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -CredentialName $cn1
        
        # Check only 2 credential are left
        $all = $a1 | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 2

        # Test using piping
        $jc2 | Remove-AzureRmSqlDatabaseAgentJobCredential
        
        # Check only 1 credential is left
        $all = $a1 | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 1

        # Check no more credentials
        $all | Remove-AzureRmSqlDatabaseAgentJobCredential
        $all = $a1 | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}

<#
	.SYNOPSIS
	Tests getting a job credential with input object
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredentialWithInputObject
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name testCred -Credential $c1

    Get-AzureRmSqlDatabaseAgentJobCredential -InputObject $agent
    Get-AzureRmSqlDatabaseAgentJobCredential -InputObject $agent -Name cred1

    Remove-AzureRmSqlDatabaseAgentJobCredential -InputObject $cred
}

<#
	.SYNOPSIS
	Tests getting a job credential with resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredentialWithResourceId
{
    $agent = Get-AzureRmSqlDatabaseAgent -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    $c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name testCred -Credential $c1

    Get-AzureRmSqlDatabaseAgentJobCredential -ResourceId $agent.ResourceId
    Get-AzureRmSqlDatabaseAgentJobCredential -ResourceId $agent.ResourceId -Name cred1

    Remove-AzureRmSqlDatabaseAgentJobCredential -InputObject $cred
}

<#
	.SYNOPSIS
	Tests updating a job credential with input object
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredentialWithInputObject
{
 	$c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = Get-AzureRmSqlDatabaseAgentJobCredential -Name cred1 -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    Set-AzureRmSqlDatabaseAgentJobCredential -InputObject $cred -Credential $c1
}

<#
	.SYNOPSIS
	Tests updating a job credential with resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-UpdateJobCredentialWithResourceId
{
 	$c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = Get-AzureRmSqlDatabaseAgentJobCredential -Name cred1 -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent
    Set-AzureRmSqlDatabaseAgentJobCredential -ResourceId $cred.ResourceId -Credential $c1
}

<#
	.SYNOPSIS
	Tests updating a job credential with input object
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredentialWithInputObject
{
 	$c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name testCred -Credential $c1
    Remove-AzureRmSqlDatabaseAgentJobCredential -InputObject $cred
}

<#
	.SYNOPSIS
	Tests updating a job credential with resource id
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveJobCredentialWithResourceId
{
 	$c1 = new-object System.Management.Automation.PSCredential("newUser", ("password" | ConvertTo-SecureString -asPlainText -Force))
    $cred = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName ps2525 -ServerName ps6926 -AgentName agent -Name testCred -Credential $c1
    Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceId $cred.ResourceId
}

<#
	.SYNOPSIS
	Tests getting all job credentials from all agents in all servers
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredentialPipingWithInputObject
{
    Get-AzureRmSqlServer | Get-AzureRmSqlDatabaseAgent | Get-AzureRmSqlDatabaseAgentJobCredential
}