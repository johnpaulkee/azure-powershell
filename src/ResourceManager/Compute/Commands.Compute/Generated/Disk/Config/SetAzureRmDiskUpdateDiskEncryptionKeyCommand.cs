//
// Copyright (c) Microsoft and contributors.  All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//
// See the License for the specific language governing permissions and
// limitations under the License.
//

// Warning: This code was generated by a tool.
//
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using Microsoft.Azure.Commands.Compute.Automation.Models;
using Microsoft.Azure.Management.Compute.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Compute.Automation
{
    [Cmdlet("Set", "AzureRmDiskUpdateDiskEncryptionKey", SupportsShouldProcess = true)]
    [OutputType(typeof(PSDiskUpdate))]
    public partial class SetAzureRmDiskUpdateDiskEncryptionKeyCommand : Microsoft.Azure.Commands.ResourceManager.Common.AzureRMCmdlet
    {
        [Parameter(
            Mandatory = true,
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public PSDiskUpdate DiskUpdate { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 1,
            ValueFromPipelineByPropertyName = true)]
        public string SecretUrl { get; set; }

        [Parameter(
            Mandatory = false,
            Position = 2,
            ValueFromPipelineByPropertyName = true)]
        public string SourceVaultId { get; set; }

        protected override void ProcessRecord()
        {
            if (ShouldProcess("DiskUpdate", "Set"))
            {
                Run();
            }
        }

        private void Run()
        {
            if (this.MyInvocation.BoundParameters.ContainsKey("SecretUrl"))
            {
                // EncryptionSettings
                if (this.DiskUpdate.EncryptionSettings == null)
                {
                    this.DiskUpdate.EncryptionSettings = new Microsoft.Azure.Management.Compute.Models.EncryptionSettings();
                }
                // DiskEncryptionKey
                if (this.DiskUpdate.EncryptionSettings.DiskEncryptionKey == null)
                {
                    this.DiskUpdate.EncryptionSettings.DiskEncryptionKey = new Microsoft.Azure.Management.Compute.Models.KeyVaultAndSecretReference();
                }
                this.DiskUpdate.EncryptionSettings.DiskEncryptionKey.SecretUrl = this.SecretUrl;
            }

            if (this.MyInvocation.BoundParameters.ContainsKey("SourceVaultId"))
            {
                // EncryptionSettings
                if (this.DiskUpdate.EncryptionSettings == null)
                {
                    this.DiskUpdate.EncryptionSettings = new Microsoft.Azure.Management.Compute.Models.EncryptionSettings();
                }
                // DiskEncryptionKey
                if (this.DiskUpdate.EncryptionSettings.DiskEncryptionKey == null)
                {
                    this.DiskUpdate.EncryptionSettings.DiskEncryptionKey = new Microsoft.Azure.Management.Compute.Models.KeyVaultAndSecretReference();
                }
                // SourceVault
                if (this.DiskUpdate.EncryptionSettings.DiskEncryptionKey.SourceVault == null)
                {
                    this.DiskUpdate.EncryptionSettings.DiskEncryptionKey.SourceVault = new Microsoft.Azure.Management.Compute.Models.SourceVault();
                }
                this.DiskUpdate.EncryptionSettings.DiskEncryptionKey.SourceVault.Id = this.SourceVaultId;
            }

            WriteObject(this.DiskUpdate);
        }
    }
}

