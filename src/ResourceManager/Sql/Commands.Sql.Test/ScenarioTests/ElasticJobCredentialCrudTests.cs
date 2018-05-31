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
using Microsoft.Azure.ServiceManagemenet.Common.Models;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Azure.Commands.Sql.Test.ScenarioTests
{
    public class ElasticJobCredentialCrudTests : SqlTestsBase
    {
        public ElasticJobCredentialCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialCreateWithDefaultParam()
        {
            RunPowerShellTest("Test-CreateJobCredentialWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialCreateWithAgentObject()
        {
            RunPowerShellTest("Test-CreateJobCredentialWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialCreateWithAgentResourceId()
        {
            RunPowerShellTest("Test-CreateJobCredentialWithAgentResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialCreateWithPiping()
        {
            RunPowerShellTest("Test-CreateJobCredentialWithPiping");
        }
        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialUpdateWithDefaultParam()
        {
            RunPowerShellTest("Test-UpdateJobCredentialWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialUpdateWithInputObject()
        {
            RunPowerShellTest("Test-UpdateJobCredentialWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialUpdateWithResourceId()
        {
            RunPowerShellTest("Test-UpdateJobCredentialWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialUpdateWithPiping()
        {
            RunPowerShellTest("Test-UpdateJobCredentialWithPiping");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobCredentialWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialGetWithAgentObject()
        {
            RunPowerShellTest("Test-GetJobCredentialWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialGetWithAgentResourceId()
        {
            RunPowerShellTest("Test-GetJobCredentialWithAgentResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialGetWithPiping()
        {
            RunPowerShellTest("Test-GetJobCredentialWithPiping");
        }

        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithPiping");
        }

        #endregion
    }
}