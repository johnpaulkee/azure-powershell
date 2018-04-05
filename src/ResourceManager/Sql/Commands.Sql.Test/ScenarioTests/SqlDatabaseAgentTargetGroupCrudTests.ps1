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
	Tests creating a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-CreateTargetGroup
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"
    $tg1 = "tg1"

    try
    {
        # Test using parameters
        $resp1 = New-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.AgentServerName $a1.AgentServerName
        Assert-AreEqual $resp1.TargetGroupName $tg1
        Assert-AreEqual $resp1.Members.Count 0

        # Test piping
        $tg2 = "tg2"
        $resp2 = $a1 |  New-AzureRmSqlDatabaseAgentTargetGroup -TargetGroupName $tg2
        Assert-AreEqual $resp2.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp2.AgentName $a1.AgentName
        Assert-AreEqual $resp2.AgentServerName $a1.AgentServerName
        Assert-AreEqual $resp2.TargetGroupName $tg2
        Assert-AreEqual $resp2.Members.Count 0
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests getting a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-GetTargetGroup
{
    # Setup
    $rg1 = Create-ResourceGroupForTest
    $s1 = Create-ServerForTest $rg1 "westus2"
    $db1 = Create-DatabaseForTest $rg1 $s1 "db1"
    $a1 = Create-AgentForTest $rg1 $s1 $db1 "agent1"
    $tg1 = Create-TargetGroupForTest $rg1 $s1 $a1 "tg1"
    $tg2 = Create-TargetGroupForTest $rg1 $s1 $a1 "tg2"

    try
    {
        # Test using parameters
        $resp1 = Get-AzureRmSqlDatabaseAgentTargetGroup -ResourceGroupName $rg1.ResourceGroupName -ServerName $s1.ServerName -AgentName $a1.AgentName -TargetGroupName $tg1
        Assert-AreEqual $resp1.ResourceGroupName $rg1.ResourceGroupName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.AgentServerName $s1.AgentServerName
        Assert-AreEqual $resp1.TargetGroupName $tg1.TargetGroupName
        Assert-AreEqual $resp1.Members.Count 0

        # Test piping
        $all = $a1 | Get-AzureRmSqlDatabaseAgentTargetGroup
        Assert-AreEqual $all.Count 2
    }
    finally
    {
        Remove-ResourceGroupForTest $rg1
    }
}

<#
	.SYNOPSIS
	Tests removing a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveTargetGroup
{
	Remove-AzureRmResourceGroup -Name ps659 -Force
    Remove-AzureRmResourceGroup -Name ps8435 -Force
    Remove-AzureRmResourceGroup -Name ps6442 -Force
    Remove-AzureRmResourceGroup -Name ps4804 -Force
    Remove-AzureRmResourceGroup -Name ps6511 -Force
}


<#
	.SYNOPSIS
	Tests removing a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-AddTarget
{
	Remove-AzureRmResourceGroup -Name ps659 -Force
    Remove-AzureRmResourceGroup -Name ps8435 -Force
    Remove-AzureRmResourceGroup -Name ps6442 -Force
    Remove-AzureRmResourceGroup -Name ps4804 -Force
    Remove-AzureRmResourceGroup -Name ps6511 -Force
}

<#
	.SYNOPSIS
	Tests removing a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-RemoveTarget
{
	Remove-AzureRmResourceGroup -Name ps659 -Force
    Remove-AzureRmResourceGroup -Name ps8435 -Force
    Remove-AzureRmResourceGroup -Name ps6442 -Force
    Remove-AzureRmResourceGroup -Name ps4804 -Force
    Remove-AzureRmResourceGroup -Name ps6511 -Force
}

<#
	.SYNOPSIS
	Tests removing a target group
    .DESCRIPTION
	SmokeTest
#>
function Test-Cleanup
{
	Remove-AzureRmResourceGroup -Name ps659 -Force
    Remove-AzureRmResourceGroup -Name ps8435 -Force
    Remove-AzureRmResourceGroup -Name ps6442 -Force
    Remove-AzureRmResourceGroup -Name ps4804 -Force
    Remove-AzureRmResourceGroup -Name ps6511 -Force
}