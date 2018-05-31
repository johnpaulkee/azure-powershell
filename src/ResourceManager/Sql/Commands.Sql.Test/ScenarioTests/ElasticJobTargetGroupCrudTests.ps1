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
    $a1 = Create-ElasticJobAgentTestEnvironment
    $tgName1 = Get-TargetGroupName
    $tgName2 = Get-TargetGroupName
    $tgName3 = Get-TargetGroupName
    $tgName4 = Get-TargetGroupName

    try
    {
        # Test using default parameters
        $resp1 = New-AzureRmSqlElasticJobTargetGroup -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $tgName1
        Assert-AreEqual $resp1.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $a1.ServerName
        Assert-AreEqual $resp1.TargetGroupName $tgName1
        Assert-AreEqual $resp1.Members.Count 0

        # Test using input object
        $resp2 = New-AzureRmSqlElasticJobTargetGroup -AgentObject $a1 -Name $tgName2
        Assert-AreEqual $resp2.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp2.AgentName $a1.AgentName
        Assert-AreEqual $resp2.ServerName $a1.ServerName
        Assert-AreEqual $resp2.TargetGroupName $tgName2
        Assert-AreEqual $resp2.Members.Count 0

        # Test using resource id
        $resp3 = New-AzureRmSqlElasticJobTargetGroup -AgentResourceId $a1.ResourceId -Name $tgName3
        Assert-AreEqual $resp3.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp3.AgentName $a1.AgentName
        Assert-AreEqual $resp3.ServerName $a1.ServerName
        Assert-AreEqual $resp3.TargetGroupName $tgName3
        Assert-AreEqual $resp3.Members.Count 0

        # Test piping
        $resp4 = $a1 | New-AzureRmSqlElasticJobTargetGroup -Name $tgName4
        Assert-AreEqual $resp4.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp4.AgentName $a1.AgentName
        Assert-AreEqual $resp4.ServerName $a1.ServerName
        Assert-AreEqual $resp4.TargetGroupName $tgName4
        Assert-AreEqual $resp4.Members.Count 0
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
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
    $a1 = Create-ElasticJobAgentTestEnvironment

    $tg1 = Create-TargetGroupForTest $a1
    $tg2 = Create-TargetGroupForTest $a1
    $tg3 = Create-TargetGroupForTest $a1
    $tg4 = Create-TargetGroupForTest $a1

    try
    {
        # Test using default parameters
        $resp1 = Get-AzureRmSqlElasticJobTargetGroup -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $tg1.TargetGroupName
        Assert-AreEqual $resp1.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $a1.ServerName
        Assert-AreEqual $resp1.TargetGroupName $tg1.TargetGroupName
        Assert-AreEqual $resp1.Members.Count 0

        # Test using input object
        $resp2 = Get-AzureRmSqlElasticJobTargetGroup -AgentObject $a1 -Name $tg2.TargetGroupName
        Assert-AreEqual $resp2.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp2.AgentName $a1.AgentName
        Assert-AreEqual $resp2.ServerName $a1.ServerName
        Assert-AreEqual $resp2.TargetGroupName $tg2.TargetGroupName
        Assert-AreEqual $resp2.Members.Count 0

        # Test using resource id
        $resp3 = Get-AzureRmSqlElasticJobTargetGroup -AgentResourceId $a1.ResourceId -Name $tg3.TargetGroupName
        Assert-AreEqual $resp3.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp3.AgentName $a1.AgentName
        Assert-AreEqual $resp3.ServerName $a1.ServerName
        Assert-AreEqual $resp3.TargetGroupName $tg3.TargetGroupName
        Assert-AreEqual $resp3.Members.Count 0

        # Test piping
        $resp4 = $a1 | Get-AzureRmSqlElasticJobTargetGroup -Name $tg4.TargetGroupName
        Assert-AreEqual $resp4.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp4.AgentName $a1.AgentName
        Assert-AreEqual $resp4.ServerName $a1.ServerName
        Assert-AreEqual $resp4.TargetGroupName $tg4.TargetGroupName
        Assert-AreEqual $resp4.Members.Count 0
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
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
    # Setup
    $a1 = Create-ElasticJobAgentTestEnvironment

    $tg1 = Create-TargetGroupForTest $a1
    $tg2 = Create-TargetGroupForTest $a1
    $tg3 = Create-TargetGroupForTest $a1
    $tg4 = Create-TargetGroupForTest $a1

    try
    {
        # Test using default parameters
        $resp1 = Remove-AzureRmSqlElasticJobTargetGroup -ResourceGroupName $a1.ResourceGroupName -ServerName $a1.ServerName -AgentName $a1.AgentName -Name $tg1.TargetGroupName
        Assert-AreEqual $resp1.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp1.AgentName $a1.AgentName
        Assert-AreEqual $resp1.ServerName $a1.ServerName
        Assert-AreEqual $resp1.TargetGroupName $tg1.TargetGroupName
        Assert-AreEqual $resp1.Members.Count 0

        # Test using input object
        $resp2 = Remove-AzureRmSqlElasticJobTargetGroup -InputObject $tg2
        Assert-AreEqual $resp2.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp2.AgentName $a1.AgentName
        Assert-AreEqual $resp2.ServerName $a1.ServerName
        Assert-AreEqual $resp2.TargetGroupName $tg2.TargetGroupName
        Assert-AreEqual $resp2.Members.Count 0

        # Test using resource id
        $resp3 = Remove-AzureRmSqlElasticJobTargetGroup -ResourceId $tg3.ResourceId
        Assert-AreEqual $resp3.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp3.AgentName $a1.AgentName
        Assert-AreEqual $resp3.ServerName $a1.ServerName
        Assert-AreEqual $resp3.TargetGroupName $tg3.TargetGroupName
        Assert-AreEqual $resp3.Members.Count 0

        # Test piping
        $resp4 = $tg4 | Remove-AzureRmSqlElasticJobTargetGroup
        Assert-AreEqual $resp4.ResourceGroupName $a1.ResourceGroupName
        Assert-AreEqual $resp4.AgentName $a1.AgentName
        Assert-AreEqual $resp4.ServerName $a1.ServerName
        Assert-AreEqual $resp4.TargetGroupName $tg4.TargetGroupName
        Assert-AreEqual $resp4.Members.Count 0
    }
    finally
    {
        #Remove-ResourceGroupForTest $a1
    }
}