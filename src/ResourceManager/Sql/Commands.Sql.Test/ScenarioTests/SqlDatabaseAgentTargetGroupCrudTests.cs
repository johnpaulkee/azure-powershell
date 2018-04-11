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
    public class SqlDatabaseAgentTargetGroupCrudTests : SqlTestsBase
    {
        public SqlDatabaseAgentTargetGroupCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreate()
        {
            RunPowerShellTest("Test-CreateTargetGroup");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithInputObject()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupCreateWithResourceId()
        {
            RunPowerShellTest("Test-CreateTargetGroupWithResourceId");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGet()
        {
            RunPowerShellTest("Test-GetTargetGroup");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithInputObject()
        {
            RunPowerShellTest("Test-GetTargetGroupWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupGetWithResourceId()
        {
            RunPowerShellTest("Test-GetTargetGroupWithResourceId");
        }

        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupUpdate()
        {
            RunPowerShellTest("Test-UpdateTargetGroup");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupUpdateWithInputObject()
        {
            RunPowerShellTest("Test-UpdateTargetGroupWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupUpdateWithResourceId()
        {
            RunPowerShellTest("Test-UpdateTargetGroupWithResourceId");
        }

        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetGroupRemove()
        {
            RunPowerShellTest("Test-RemoveTargetGroup");
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

        #endregion
    }
}