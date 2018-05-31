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
    public class ElasticJobCrudTests : SqlTestsBase
    {
        public ElasticJobCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Create Tests

        /// <summary>
        /// Tests creating jobs using default parameters
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCreateWithDefaultParam()
        {
            RunPowerShellTest("Test-CreateJobWithDefaultParam");
        }

        /// <summary>
        /// Tests creating new jobs using agent object
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCreateWithAgentObject()
        {
            RunPowerShellTest("Test-CreateJobWithAgentObject");
        }

        /// <summary>
        /// Tests creating jobs with agent resource id
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobCreateWithAgentResourceId()
        {
            RunPowerShellTest("Test-CreateJobWithAgentResourceId");
        }

        #endregion

        #region Update Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobUpdateWithDefaultParam()
        {
            RunPowerShellTest("Test-UpdateJobWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobUpdateWithInputObject()
        {
            RunPowerShellTest("Test-UpdateJobWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobUpdateWithResourceId()
        {
            RunPowerShellTest("Test-UpdateJobWithResourceId");
        }

        #endregion

        #region Get Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobGetWithAgentObject()
        {
            RunPowerShellTest("Test-GetJobWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobGetWithAgentResourceId()
        {
            RunPowerShellTest("Test-GetJobWithAgentResourceId");
        }
        #endregion

        #region Remove Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobRemoveWithDefaultParam()
        {
            RunPowerShellTest("Test-RemoveJobWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobRemoveWithInputObject()
        {
            RunPowerShellTest("Test-RemoveJobWithInputObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobRemoveWithResourceId()
        {
            RunPowerShellTest("Test-RemoveJobWithResourceId");
        }

        #endregion
    }
}