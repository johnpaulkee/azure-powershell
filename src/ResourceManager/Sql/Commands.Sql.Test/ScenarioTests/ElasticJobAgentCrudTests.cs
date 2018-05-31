// ----------------------------------------------------------------------------------
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
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Azure.Commands.Sql.Test.ScenarioTests
{
    public class ElasticJobAgentCrudTests : SqlTestsBase
    {
        public ElasticJobAgentCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentCreateWithDefaultParam()
        {
            RunPowerShellTest("Test-CreateAgentWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentCreateWithDatabaseObject()
        {
            RunPowerShellTest("Test-CreateAgentWithDatabaseObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentCreateWithDatabaseResourceId()
        {
            RunPowerShellTest("Test-CreateAgentWithDatabaseResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentCreateWithPiping()
        {
            RunPowerShellTest("Test-CreateAgentWithPiping");
        }

        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentUpdateWithDefaultParam()
        {
            RunPowerShellTest("Test-UpdateAgentWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentUpdateWithInputObject()
        {
            RunPowerShellTest("Test-UpdateAgentWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentUpdateWithResourceId()
        {
            RunPowerShellTest("Test-UpdateAgentWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentUpdateWithPiping()
        {
            RunPowerShellTest("Test-UpdateAgentWithPiping");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetAgentWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGetWithServerObject()
        {
            RunPowerShellTest("Test-GetAgentWithServerObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGetWithServerResourceId()
        {
            RunPowerShellTest("Test-GetAgentWithServerResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentGetWithPiping()
        {
            RunPowerShellTest("Test-GetAgentWithPiping");
        }

        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveAgentWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveAgentWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveAgentWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestAgentRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveAgentWithPiping");
        }

        #endregion
    }
}