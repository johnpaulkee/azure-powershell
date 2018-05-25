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
    public class DatabaseReplicationTests : SqlTestsBase
    {
        public DatabaseReplicationTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCreateDatabaseCopy()
        {
            RunPowerShellTest("Test-CreateDatabaseCopy");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCreateVcoreDatabaseCopy()
        {
            RunPowerShellTest("Test-CreateVcoreDatabaseCopy");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCreateSecondaryDatabase()
        {
            RunPowerShellTest("Test-CreateSecondaryDatabase");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCreateVcoreDatabaseSecondary()
        {
            RunPowerShellTest("Test-CreateVcoreDatabaseSecondary");
        }

        [Fact]
        [Trait(Category.Sql, Category.CheckIn)]
        public void TestGetReplicationLink()
        {
            RunPowerShellTest("Test-GetReplicationLink");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestRemoveSecondaryDatabase()
        {
            RunPowerShellTest("Test-RemoveSecondaryDatabase");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestFailoverSecondaryDatabase()
        {
            RunPowerShellTest("Test-FailoverSecondaryDatabase");
        }
    }
}
