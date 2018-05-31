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
    public class ElasticJobTargetCrudTests : SqlTestsBase
    {
        public ElasticJobTargetCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerAddWithDefaultParam()
        {
            RunPowerShellTest("Test-AddServerTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerAddWithTargetGroupObject()
        {
            RunPowerShellTest("Test-AddServerTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerAddWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-AddServerTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerAddWithPiping()
        {
            RunPowerShellTest("Test-AddServerTargetWithPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerRemoveWithTargetGrouObject()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerRemoveWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetServerRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveServerTargetWithPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseAddWithDefaultParam()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseAddWithTargetGroupObject()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseAddWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseAddWithPiping()
        {
            RunPowerShellTest("Test-AddDatabaseTargetWithPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseRemoveWithTargetGroupObject()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseRemoveWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetDatabaseRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveDatabaseTargetWithPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolAddWithDefaultParam()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolAddWithTargetGroupObject()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolAddWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolAddWithPiping()
        {
            RunPowerShellTest("Test-AddElasticPoolTargetPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolRemoveWithTargetGroupObject()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolRemoveWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetElasticPoolRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveElasticPoolTargetPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapAddWithDefaultParam()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapAddWithTargetGroupObject()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapAddWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapAddWithPiping()
        {
            RunPowerShellTest("Test-AddShardMapTargetWithPiping");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapRemoveWithTargetGroupObject()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithTargetGroupObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapRemoveWithTargetGroupResourceId()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithTargetGroupResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestTargetShardMapRemoveWithPiping()
        {
            RunPowerShellTest("Test-RemoveShardMapTargetWithPiping");
        }
    }
}