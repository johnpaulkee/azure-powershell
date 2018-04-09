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
    public class SqlDatabaseAgentJobCredentialCrudTests : SqlTestsBase
    {
        public SqlDatabaseAgentJobCredentialCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialCreate()
        {
            RunPowerShellTest("Test-CreateJobCredential");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialGet()
        {
            RunPowerShellTest("Test-GetJobCredential");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialUpdateWithInputObject()
        {
            RunPowerShellTest("Test-GetJobCredentialWithInputObject");
        }


        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialUpdateWithResourceId()
        {
            RunPowerShellTest("Test-GetJobCredentialWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCredentialUpdate()
        {
            RunPowerShellTest("Test-UpdateJobCredential");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialRemove()
        {
            RunPowerShellTest("Test-RemoveJobCredential");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialGetWithInputObject()
        {
            RunPowerShellTest("Test-GetJobCredentialWithInputObject");
        }


        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialGetWithResourceId()
        {
            RunPowerShellTest("Test-GetJobCredentialWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveJobCredentialWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCredentialGetPipingWithInputObject()
        {
            RunPowerShellTest("Test-GetJobCredentialPipingWithInputObject");
        }
    }
}