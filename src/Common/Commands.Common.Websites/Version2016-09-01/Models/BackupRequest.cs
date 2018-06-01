// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.WebSites.Version2016_09_01.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Description of a backup which will be performed.
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class BackupRequest : ProxyOnlyResource
    {
        /// <summary>
        /// Initializes a new instance of the BackupRequest class.
        /// </summary>
        public BackupRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the BackupRequest class.
        /// </summary>
        /// <param name="backupRequestName">Name of the backup.</param>
        /// <param name="storageAccountUrl">SAS URL to the container.</param>
        /// <param name="id">Resource Id.</param>
        /// <param name="name">Resource Name.</param>
        /// <param name="kind">Kind of resource.</param>
        /// <param name="type">Resource type.</param>
        /// <param name="enabled">True if the backup schedule is enabled (must
        /// be included in that case), false if the backup schedule should be
        /// disabled.</param>
        /// <param name="backupSchedule">Schedule for the backup if it is
        /// executed periodically.</param>
        /// <param name="databases">Databases included in the backup.</param>
        /// <param name="backupRequestType">Type of the backup. Possible values
        /// include: 'Default', 'Clone', 'Relocation', 'Snapshot'</param>
        public BackupRequest(string backupRequestName, string storageAccountUrl, string id = default(string), string name = default(string), string kind = default(string), string type = default(string), bool? enabled = default(bool?), BackupSchedule backupSchedule = default(BackupSchedule), IList<DatabaseBackupSetting> databases = default(IList<DatabaseBackupSetting>), BackupRestoreOperationType? backupRequestType = default(BackupRestoreOperationType?))
            : base(id, name, kind, type)
        {
            BackupRequestName = backupRequestName;
            Enabled = enabled;
            StorageAccountUrl = storageAccountUrl;
            BackupSchedule = backupSchedule;
            Databases = databases;
            BackupRequestType = backupRequestType;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets name of the backup.
        /// </summary>
        [JsonProperty(PropertyName = "properties.name")]
        public string BackupRequestName { get; set; }

        /// <summary>
        /// Gets or sets true if the backup schedule is enabled (must be
        /// included in that case), false if the backup schedule should be
        /// disabled.
        /// </summary>
        [JsonProperty(PropertyName = "properties.enabled")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Gets or sets SAS URL to the container.
        /// </summary>
        [JsonProperty(PropertyName = "properties.storageAccountUrl")]
        public string StorageAccountUrl { get; set; }

        /// <summary>
        /// Gets or sets schedule for the backup if it is executed
        /// periodically.
        /// </summary>
        [JsonProperty(PropertyName = "properties.backupSchedule")]
        public BackupSchedule BackupSchedule { get; set; }

        /// <summary>
        /// Gets or sets databases included in the backup.
        /// </summary>
        [JsonProperty(PropertyName = "properties.databases")]
        public IList<DatabaseBackupSetting> Databases { get; set; }

        /// <summary>
        /// Gets or sets type of the backup. Possible values include:
        /// 'Default', 'Clone', 'Relocation', 'Snapshot'
        /// </summary>
        [JsonProperty(PropertyName = "properties.type")]
        public BackupRestoreOperationType? BackupRequestType { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (BackupRequestName == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "BackupRequestName");
            }
            if (StorageAccountUrl == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "StorageAccountUrl");
            }
            if (BackupSchedule != null)
            {
                BackupSchedule.Validate();
            }
            if (Databases != null)
            {
                foreach (var element in Databases)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
