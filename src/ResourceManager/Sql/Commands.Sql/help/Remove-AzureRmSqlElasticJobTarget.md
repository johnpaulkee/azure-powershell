---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version:
schema: 2.0.0
---

# Remove-AzureRmSqlElasticJobTarget

## SYNOPSIS
Removes the target from the target group

## SYNTAX

### Sql Database Target Type (Default)
```
Remove-AzureRmSqlElasticJobTarget [-ResourceGroupName] <String> [-AgentServerName] <String>
 [-AgentName] <String> [-TargetGroupName] <String> [-ServerName] <String> [-DatabaseName] <String>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Sql Server or Elastic Pool Target Type
```
Remove-AzureRmSqlElasticJobTarget [-ResourceGroupName] <String> [-AgentServerName] <String>
 [-AgentName] <String> [-TargetGroupName] <String> [-ServerName] <String> [-ElasticPoolName <String>]
 [-RefreshCredentialName] <String> [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Sql Shard Map Target Type
```
Remove-AzureRmSqlElasticJobTarget [-ResourceGroupName] <String> [-AgentServerName] <String>
 [-AgentName] <String> [-TargetGroupName] <String> [-ServerName] <String> [-ShardMapName] <String>
 [-DatabaseName] <String> [-RefreshCredentialName] <String> [-DefaultProfile <IAzureContextContainer>]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Sql Database Input Object Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-DatabaseName] <String>
 [-TargetGroupObject] <AzureSqlElasticJobTargetGroupModel> [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Sql Server or Elastic Pool Input Object Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-ElasticPoolName <String>]
 [-RefreshCredentialName] <String> [-TargetGroupObject] <AzureSqlElasticJobTargetGroupModel>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Sql Shard Map Input Object Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-ShardMapName] <String> [-DatabaseName] <String>
 [-RefreshCredentialName] <String> [-TargetGroupObject] <AzureSqlElasticJobTargetGroupModel>
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Sql Database TargetGroupResourceId Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-DatabaseName] <String>
 [-TargetGroupResourceId] <String> [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Sql Server or Elastic Pool TargetGroupResourceId Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-ElasticPoolName <String>]
 [-RefreshCredentialName] <String> [-TargetGroupResourceId] <String> [-DefaultProfile <IAzureContextContainer>]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Sql Shard Map TargetGroupResourceId Parameter Set
```
Remove-AzureRmSqlElasticJobTarget [-ServerName] <String> [-ShardMapName] <String> [-DatabaseName] <String>
 [-RefreshCredentialName] <String> [-TargetGroupResourceId] <String> [-DefaultProfile <IAzureContextContainer>]
 [-WhatIf] [-Confirm] [<CommonParameters>]
```

## DESCRIPTION
{{Fill in the Description}}

## EXAMPLES

### Example 1
```powershell
PS C:\> {{ Add example code here }}
```

{{ Add example description here }}

## PARAMETERS

### -AgentName
SQL Database Agent Name.

```yaml
Type: String
Parameter Sets: Sql Database Target Type, Sql Server or Elastic Pool Target Type, Sql Shard Map Target Type
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AgentServerName
SQL Database Agent Server Name.

```yaml
Type: String
Parameter Sets: Sql Database Target Type, Sql Server or Elastic Pool Target Type, Sql Shard Map Target Type
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -DatabaseName
Database Target Name

```yaml
Type: String
Parameter Sets: Sql Database Target Type, Sql Shard Map Target Type, Sql Database Input Object Parameter Set, Sql Shard Map Input Object Parameter Set, Sql Database TargetGroupResourceId Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: Sql Shard Map TargetGroupResourceId Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -DefaultProfile
The credentials, account, tenant, and subscription used for communication with Azure.

```yaml
Type: IAzureContextContainer
Parameter Sets: (All)
Aliases: AzureRmContext, AzureCredential

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ElasticPoolName
Elastic Pool Target Name

```yaml
Type: String
Parameter Sets: Sql Server or Elastic Pool Target Type, Sql Server or Elastic Pool Input Object Parameter Set, Sql Server or Elastic Pool TargetGroupResourceId Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -RefreshCredentialName
Refresh Credential Name

```yaml
Type: String
Parameter Sets: Sql Server or Elastic Pool Target Type, Sql Shard Map Target Type, Sql Server or Elastic Pool Input Object Parameter Set, Sql Shard Map Input Object Parameter Set, Sql Server or Elastic Pool TargetGroupResourceId Parameter Set, Sql Shard Map TargetGroupResourceId Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
Resource Group Name

```yaml
Type: String
Parameter Sets: Sql Database Target Type, Sql Server or Elastic Pool Target Type, Sql Shard Map Target Type
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerName
Server Target Name

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: 4
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -ShardMapName
Shard Map Target Name

```yaml
Type: String
Parameter Sets: Sql Shard Map Target Type, Sql Shard Map Input Object Parameter Set, Sql Shard Map TargetGroupResourceId Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -TargetGroupName
SQL Database Agent Name.

```yaml
Type: String
Parameter Sets: Sql Database Target Type, Sql Server or Elastic Pool Target Type, Sql Shard Map Target Type
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -TargetGroupObject
The SQL Database Agent Target Group Object

```yaml
Type: AzureSqlElasticJobTargetGroupModel
Parameter Sets: Sql Database Input Object Parameter Set, Sql Server or Elastic Pool Input Object Parameter Set, Sql Shard Map Input Object Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -TargetGroupResourceId
The target group resource id

```yaml
Type: String
Parameter Sets: Sql Database TargetGroupResourceId Parameter Set, Sql Server or Elastic Pool TargetGroupResourceId Parameter Set, Sql Shard Map TargetGroupResourceId Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -Confirm
Prompts you for confirmation before running the cmdlet.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: cf

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -WhatIf
Shows what would happen if the cmdlet runs.
The cmdlet is not run.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases: wi

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.Commands.Sql.ElasticJobs.Model.AzureSqlElasticJobTargetGroupModel
System.String

## OUTPUTS

### Microsoft.Azure.Management.Sql.Models.JobTarget

## NOTES

## RELATED LINKS
