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
    public class ElasticJobTargetGroupCrudTests : SqlTestsBase
    {
        public ElasticJobTargetGroupCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithDefaultParam()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithAgentObject()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithAgentResourceId()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithAgentResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithPiping()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithPiping");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetTargetGroupWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithAgentObject()
        {
            RunPowerShellTest("Test-GetTargetGroupWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithAgentResourceId()
        {
            RunPowerShellTest("Test-GetTargetGroupWithAgentResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithPiping()
        {
            RunPowerShellTest("Test-GetTargetGroupWithPiping");
        }
        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveTargetGroupWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveTargetGroupWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveTargetGroupWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveTargetGroupWithPiping");
        }

        #endregion
    }
}