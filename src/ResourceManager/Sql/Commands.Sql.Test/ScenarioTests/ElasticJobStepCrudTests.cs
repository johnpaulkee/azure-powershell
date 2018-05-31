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
    public class ElasticJobStepCrudTests : SqlTestsBase
    {
        public ElasticJobStepCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepCreateWithDefaultParam()
        {
            RunPowerShellTest("Test-CreateJobStepWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepCreateWithJobObject()
        {
            RunPowerShellTest("Test-CreateJobStepWithJobObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepCreateWithJobResourceId()
        {
            RunPowerShellTest("Test-CreateJobStepWithJobResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepCreateWithPiping()
        {
            RunPowerShellTest("Test-CreateJobStepWithPiping");
        }

        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepUpdateWithDefaultParam()
        {
            RunPowerShellTest("Test-UpdateJobStepWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepUpdateWithInputObject()
        {
            RunPowerShellTest("Test-UpdateJobStepWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepUpdateWithResourceId()
        {
            RunPowerShellTest("Test-UpdateJobStepWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepUpdateWithPiping()
        {
            RunPowerShellTest("Test-UpdateJobStepWithPiping");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobStepWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepGetWithJobObject()
        {
            RunPowerShellTest("Test-GetJobStepWithJobObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepGetWithJobResourceId()
        {
            RunPowerShellTest("Test-GetJobStepWithJobResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepGetWithPiping()
        {
            RunPowerShellTest("Test-GetJobStepWithPiping");
        }

        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveJobStepWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveJobStepWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveJobStepWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveJobStepWithPiping");
        }

        #endregion
    }
}