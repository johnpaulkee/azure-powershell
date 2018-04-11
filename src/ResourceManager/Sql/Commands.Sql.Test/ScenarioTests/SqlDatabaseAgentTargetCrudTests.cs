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
    public class SqlDatabaseAgentTargetCrudTests : SqlTestsBase
    {
        public SqlDatabaseAgentTargetCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Server Target Type

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddServer()
        {
            RunPowerShellTest("Test-AddServerTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddServerWithInputObject()
        {
            RunPowerShellTest("Test-AddServerTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddServerWithResourceId()
        {
            RunPowerShellTest("Test-AddServerTargetWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveServer()
        {
            RunPowerShellTest("Test-RemoveServerTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveServerWithInputObject()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveServerWithResourceId()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithResourceId");
        }

        #endregion

        #region Database Target Type

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddDatabase()
        {
            RunPowerShellTest("Test-AddDatabaseTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddDatabaseWithInputObject()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddDatabaseWithResourceId()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveDatabase()
        {
            RunPowerShellTest("Test-RemoveDatabaseTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveDatabaseWithInputObject()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveDatabaseWithResourceId()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithResourceId");
        }

        #endregion

        #region Elastic Pool Target Type

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddElasticPool()
        {
            RunPowerShellTest("Test-AddElasticPoolTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddElasticPoolWithInputObject()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddElasticPoolWithResourceId()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveElasticPool()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveElasticPoolWithInputObject()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveElasticPoolWithResourceId()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetWithResourceId");
        }

        #endregion

        #region Shard Map Target Type

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddShardMap()
        {
            RunPowerShellTest("Test-AddShardMapTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddShardMapWithInputObject()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetAddShardMapWithResourceId()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveShardMap()
        {
            RunPowerShellTest("Test-RemoveShardMapTarget");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveShardMapWithInputObject()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetRemoveShardMapWithResourceId()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithResourceId");
        }

        #endregion

    }
}