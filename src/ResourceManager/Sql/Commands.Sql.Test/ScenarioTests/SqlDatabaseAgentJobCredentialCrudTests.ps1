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
    $u1 = "testusername"
	$p1 = "t357ingP@s5w0rd!"
	$c1 = new-object System.Management.Automation.PSCredential($u1, ($p1 | ConvertTo-SecureString -asPlainText -Force)) 

    try 
    {
    	$resp1 = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName `
            -CredentialName $cn1 -Credential $c1

        # Default create agent
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
	Tests getting an Azure Sql Database Agent Job Credential
    .DESCRIPTION
	SmokeTest
#>
function Test-GetJobCredential
{
    # Setup
    $rg = Create-ResourceGroupForTest
    $server = Create-ServerForTest $rg "westus2"
    $db = Create-DatabaseForTest $rg $server "db1"
    $agent = Create-AgentForTest $rg $server $db "agent"

    $credName = "cred1"
    $userName = "testusername"
	$password = "t357ingP@s5w0rd!"
    $cred = new-object System.Management.Automation.PSCredential($userName, ($password | ConvertTo-SecureString -asPlainText -Force))

    $jobCredential = Create-JobCredentialForTest $rg $server $agent $credName $cred

    try 
    {
        $resp = Get-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName `
            -CredentialName $credName

        Assert-AreEqual $resp.ResourceGroupName $rg.ResourceGroupName
        Assert-AreEqual $resp.ServerName $server.ServerName
        Assert-AreEqual $resp.AgentName $agent.AgentName
        Assert-AreEqual $resp.CredentialName $jobCredential.CredentialName
        Assert-AreEqual $resp.UserName $jobCredential.UserName
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
    $rg = Create-ResourceGroupForTest
    $server = Create-ServerForTest $rg "westus2"
    $db = Create-DatabaseForTest $rg $server "db1"
    $agent = Create-AgentForTest $rg $server $db "agent"

    # Create cred 1
    $credName = "cred1"
    $userName = "testusername"
	$password = "t357ingP@s5w0rd!"
    $cred = new-object System.Management.Automation.PSCredential($userName, ($password | ConvertTo-SecureString -asPlainText -Force))

    $jobCredential1 = Create-JobCredentialForTest $rg $server $agent $credName $cred
    Assert-NotNull $jobCredential1

    # Create cred 2
    $credName = "cred2"    
    $jobCredential2 = Create-JobCredentialForTest $rg $server $agent $credName $cred
    Assert-NotNull $cred2

    try
    {
        # Update cred 1
        $newUserName = "newUser"
        $newPassword = "Yukon900!"
        $newCred = new-object System.Management.Automation.PSCredential($jobCredential1.newUserName, ($newPassword | ConvertTo-SecureString -asPlainText -Force))

        $resp1 = Set-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName -CredentialName $cred1.CredentialName -Credential $newCred
        Assert-AreEqual $resp1.UserName $newUserName
        
        # Update cred 2 through piping
        $resp2 = $cred2 | Set-AzureRmSqlDatabaseAgentJobCredential -Credential $newCred
        Assert-AreEqual $resp2.UserName $newUserName
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
    $rg = Create-ResourceGroupForTest
    $server = Create-ServerForTest $rg "westus2"
    $db = Create-DatabaseForTest $rg $server "db1"
    $agent = Create-AgentForTest $rg $server $db "agent"

    # Create cred 1
    $credName = "cred1"
    $userName = "testusername"
	$password = "t357ingP@s5w0rd!"
    $cred = new-object System.Management.Automation.PSCredential($userName, ($password | ConvertTo-SecureString -asPlainText -Force))

    $jobCredential1 = Create-JobCredentialForTest $rg $server $agent $credName $cred
    Assert-NotNull $jobCredential1

    # Create cred 2
    $credName = "cred2"    
    $jobCredential2 = Create-JobCredentialForTest $rg $server $agent $credName $cred
    Assert-NotNull $cred2

    try
    {
        # Remove cred 1
        Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName -CredentialName $cred1.CredentialName
        
        # Remove cred 2 through piping
        $cred2 | Remove-AzureRmSqlDatabaseAgentJobCredential
        
        # Check that credentials are deleted.
        $all = $agent | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 0
    }
    finally 
    {
        Remove-ResourceGroupForTest $rg
    }
}