﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Microsoft.Azure.Commands.ScenarioTest.SqlTests;
using Microsoft.Azure.ServiceManagemenet.Common.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Azure.Commands.Sql.Test.ScenarioTests
{
    public class SqlDatabaseAgentCrudTests : SqlTestsBase
    {
        public SqlDatabaseAgentCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentCreate()
        {
            RunPowerShellTest("Test-CreateAgent");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentUpdate()
        {
            RunPowerShellTest("Test-UpdateAgent");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGet()
        {
            RunPowerShellTest("Test-GetAgent");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemove()
        {
            RunPowerShellTest("Test-RemoveAgent");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveByInputObject()
        {
            RunPowerShellTest("Test-RemoveAgentByInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveByResourceId()
        {
            RunPowerShellTest("Test-RemoveAgentByResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGetAllAgents()
        {
            RunPowerShellTest("Test-GetAllAgents");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentSetWithInputObject()
        {
            RunPowerShellTest("Test-SetAgentWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentNewWithInputObject()
        {
            RunPowerShellTest("Test-NewAgentWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentNewWithResourceId()
        {
            RunPowerShellTest("Test-NewAgentWithResourceId");
        }
    }
}