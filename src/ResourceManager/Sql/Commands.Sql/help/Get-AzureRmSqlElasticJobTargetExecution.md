---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version:
schema: 2.0.0
---

# Get-AzureRmSqlElasticJobTargetExecution

## SYNOPSIS
Gets one or more job target executions

## SYNTAX

### Default Parameter Set (Default)
```
Get-AzureRmSqlElasticJobTargetExecution [-ResourceGroupName] <String> [-ServerName] <String>
 [-AgentName] <String> [-JobName] <String> [-JobExecutionId] <String> [-StepName <String>]
 [-CreateTimeMin <DateTime>] [-CreateTimeMax <DateTime>] [-EndTimeMin <DateTime>] [-EndTimeMax <DateTime>]
 [-Active] [-Count] <Int32> [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### GetJobTargetExecution Parameter Set
```
Get-AzureRmSqlElasticJobTargetExecution -StepName <String> [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Input Object Parameter Set
```
Get-AzureRmSqlElasticJobTargetExecution [-StepName <String>] [-CreateTimeMin <DateTime>]
 [-CreateTimeMax <DateTime>] [-EndTimeMin <DateTime>] [-EndTimeMax <DateTime>] [-Active] [-Count] <Int32>
 [-JobExecutionObject] <AzureSqlElasticJobExecutionModel> [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Resource Id Parameter Set
```
Get-AzureRmSqlElasticJobTargetExecution [-StepName <String>] [-CreateTimeMin <DateTime>]
 [-CreateTimeMax <DateTime>] [-EndTimeMin <DateTime>] [-EndTimeMax <DateTime>] [-Active] [-Count] <Int32>
 [-JobExecutionResourceId] <String> [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
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

### -Active
Flag to filter by active executions.

```yaml
Type: SwitchParameter
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AgentName
The agent name.

```yaml
Type: String
Parameter Sets: Default Parameter Set
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Count
Count returns the top number of executions.

```yaml
Type: Int32
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CreateTimeMax
Filter by create time max

```yaml
Type: DateTime
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -CreateTimeMin
Filter by create time min

```yaml
Type: DateTime
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
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

### -EndTimeMax
Filter by end time max.

```yaml
Type: DateTime
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndTimeMin
Filter by end time min.

```yaml
Type: DateTime
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -JobExecutionId
The job execution id.

```yaml
Type: String
Parameter Sets: Default Parameter Set
Aliases:

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -JobExecutionObject
The agent object.

```yaml
Type: AzureSqlElasticJobExecutionModel
Parameter Sets: Input Object Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -JobExecutionResourceId
The job execution resource id.

```yaml
Type: String
Parameter Sets: Resource Id Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByPropertyName)
Accept wildcard characters: False
```

### -JobName
The job name.

```yaml
Type: String
Parameter Sets: Default Parameter Set
Aliases:

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The resource group name.

```yaml
Type: String
Parameter Sets: Default Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerName
The server name.

```yaml
Type: String
Parameter Sets: Default Parameter Set
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StepName
The job step name.

```yaml
Type: String
Parameter Sets: Default Parameter Set, Input Object Parameter Set, Resource Id Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

```yaml
Type: String
Parameter Sets: GetJobTargetExecution Parameter Set
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable.
For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.Commands.Sql.ElasticJobs.Model.AzureSqlElasticJobExecutionModel
System.String


## OUTPUTS

### System.Collections.Generic.IEnumerable`1[[Microsoft.Azure.Commands.Sql.ElasticJobs.Model.AzureSqlElasticJobExecutionModel, Microsoft.Azure.Commands.Sql, Version=4.4.0.0, Culture=neutral, PublicKeyToken=null]]


## NOTES

## RELATED LINKS
