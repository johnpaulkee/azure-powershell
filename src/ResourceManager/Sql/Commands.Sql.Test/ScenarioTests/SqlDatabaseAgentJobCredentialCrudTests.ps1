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
    $rg = Create-ResourceGroupForTest
    $server = Create-ServerForTest $rg "westus2"
    $db = Create-DatabaseForTest $rg $server "db1"
    $agent = Create-AgentForTest $rg $server $db "agent"

    try 
    {
    	$credential = New-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName `
            -CredentialName "cred1" -Username "cloudSA" -Password "Yukon900!"

        # Default create agent
        Assert-AreEqual $credential.ResourceGroupName $rg.ResourceGroupName
        Assert-AreEqual $credential.ServerName $server.ServerName
        Assert-AreEqual $credential.CredentialName "cred1"
        Assert-AreEqual $credential.Username "cloudSA"
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
    $credential = Create-JobCredentialForTest $rg $server $agent "cred1" "cloudSA" "Yukon900!"

    try 
    {
        $resp = Get-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName `
            -CredentialName $credential.CredentialName

        Assert-AreEqual $resp.ResourceGroupName $rg.ResourceGroupName
        Assert-AreEqual $resp.ServerName $server.ServerName
        Assert-AreEqual $resp.AgentName $agent.AgentName
        Assert-AreEqual $resp.CredentialName $credential.CredentialName
        Assert-AreEqual $resp.Username $credential.Username
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

    $cred1 = Create-JobCredentialForTest $rg $server $agent "cred1" "cloudSA" "Yukon900!"
    Assert-NotNull $cred1
    $cred2 = Create-JobCredentialForTest $rg $server $agent "cred2" "booyah" "Yukon900!"
    Assert-NotNull $cred2

    try 
    {
        # Remove cred 1
        Remove-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName `
            -CredentialName $cred1.CredentialName -Confirm:$false

        # Remove cred 2 using piping
        $cred2 | Remove-AzureRmSqlDatabaseAgentJobCredential -Force

        $all = $agent | Get-AzureRmSqlDatabaseAgentJobCredential
        Assert-AreEqual $all.Count 0
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
    $credential = Create-JobCredentialForTest $rg $server $agent "cred1" "cloudSA" "Yukon900!"

    try 
    {
        $resp = Set-AzureRmSqlDatabaseAgentJobCredential -ResourceGroupName $rg.ResourceGroupName -ServerName $server.ServerName -AgentName $agent.AgentName `
            -CredentialName $credential.CredentialName -UserName "cloudSA" "passwordTest!"

        Assert-AreEqual $resp.ResourceGroupName $rg.ResourceGroupName
        Assert-AreEqual $resp.ServerName $server.ServerName
        Assert-AreEqual $resp.AgentName $agent.AgentName
        Assert-AreEqual $resp.CredentialName $credential.CredentialName
        Assert-AreEqual $resp.Username $credential.Username
    }
    finally
    {
        Remove-ResourceGroupForTest $rg
    }
}