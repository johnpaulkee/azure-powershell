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
    public class ElasticJobExecutionCrudTests : SqlTestsBase
    {
        public ElasticJobExecutionCrudTests(ITestOutputHelper output) : base(output)
        {
        }

        #region Start Job Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStartWithDefaultParam()
        {
            RunPowerShellTest("Test-StartJobWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStartWithJobObject()
        {
            RunPowerShellTest("Test-StartJobWithJobObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStartWithJobResourceId()
        {
            RunPowerShellTest("Test-StartJobWithJobResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStartWithPiping()
        {
            RunPowerShellTest("Test-StartJobWithPiping");
        }

        #endregion

        #region Stop Job Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStopWithDefaultParam()
        {
            RunPowerShellTest("Test-StopJobWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStopWithJobExecutionObject()
        {
            RunPowerShellTest("Test-StopJobWithJobExecutionObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStopWithJobExecutionResourceId()
        {
            RunPowerShellTest("Test-StopJobWithJobExecutionResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStopWithPiping()
        {
            RunPowerShellTest("Test-StopJobWithPiping");
        }

        #endregion

        #region Get Job Execution Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobExecutionGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobExecutionWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobExecutionGetWithAgentObject()
        {
            RunPowerShellTest("Test-GetJobExecutionWithAgentObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobExecutionGetWithAgentResourceId()
        {
            RunPowerShellTest("Test-GetJobExecutionWithAgentResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobExecutionGetWithPiping()
        {
            RunPowerShellTest("Test-GetJobExecutionWithPiping");
        }

        #endregion

        #region Get Job Step Execution Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepExecutionGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobStepExecutionWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepExecutionGetWithJobExecutionObject()
        {
            RunPowerShellTest("Test-GetJobStepExecutionWithJobExecutionObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepExecutionGetWithJobExecutionResourceId()
        {
            RunPowerShellTest("Test-GetJobStepExecutionWithJobExecutionResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobStepExecutionGetWithPiping()
        {
            RunPowerShellTest("Test-GetJobStepExecutionWithPiping");
        }

        #endregion

        #region Get Job Target Execution Tests

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobTargetExecutionGetWithDefaultParam()
        {
            RunPowerShellTest("Test-GetJobTargetExecutionWithDefaultParam");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobTargetExecutionGetWithJobExecutionObject()
        {
            RunPowerShellTest("Test-GetJobTargetExecutionWithJobExecutionObject");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobTargetExecutionGetWithJobExecutionResourceId()
        {
            RunPowerShellTest("Test-GetJobTargetExecutionWithJobExecutionResourceId");
        }

        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestJobTargetExecutionGetWithPiping()
        {
            RunPowerShellTest("Test-GetJobTargetExecutionWithPiping");
        }

        #endregion
    }
}