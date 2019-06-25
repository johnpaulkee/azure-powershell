using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Commands.ScenarioTest.SqlTests;
using Microsoft.WindowsAzure.Commands.ScenarioTest;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.Azure.Commands.Sql.Test.ScenarioTests
{
    public class InstancePoolCrudTests : SqlTestsBase
    {
        public InstancePoolCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestInstancePoolCreate()
        {
            RunPowerShellTest("Test-CreateInstancePool");
        }

        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestInstancePoolUpdate()
        {
            RunPowerShellTest("Test-UpdateInstancePool");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestInstancePoolRead()
        {
            RunPowerShellTest("Test-GetInstancePool");
        }

        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestInstancePoolRemove()
        {
            RunPowerShellTest("Test-RemoveInstancePool");
        }

        #endregion
    }
}
