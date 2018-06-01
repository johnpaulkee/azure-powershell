---
external help file: Microsoft.Azure.Commands.Sql.dll-Help.xml
Module Name: AzureRM.Sql
online version:
schema: 2.0.0
---

# New-AzureRmSqlElasticJob

## SYNOPSIS
Creates a new job

## SYNTAX

### Default Parameter Set (Default)
```
New-AzureRmSqlElasticJob [-ResourceGroupName] <String> [-ServerName] <String> [-AgentName] <String>
 [-Name] <String> [-Description <String>] [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Agent Default Run Once Parameter Set
```
New-AzureRmSqlElasticJob [-ResourceGroupName] <String> [-ServerName] <String> [-AgentName] <String>
 [-Name] <String> [-RunOnce] [-StartTime <DateTime>] [-Description <String>] [-Enable]
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Agent Default Recurring Parameter Set
```
New-AzureRmSqlElasticJob [-ResourceGroupName] <String> [-ServerName] <String> [-AgentName] <String>
 [-Name] <String> [-IntervalType] <JobScheduleReccuringScheduleTypes> [-IntervalCount] <UInt32>
 [-StartTime <DateTime>] [-EndTime <DateTime>] [-Description <String>] [-Enable]
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Input Object Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-Description <String>] [-AgentObject] <AzureSqlElasticJobAgentModel>
 [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Agent Object Run Once Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-RunOnce] [-StartTime <DateTime>] [-Description <String>]
 [-AgentObject] <AzureSqlElasticJobAgentModel> [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Agent Object Recurring Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-IntervalType] <JobScheduleReccuringScheduleTypes>
 [-IntervalCount] <UInt32> [-StartTime <DateTime>] [-EndTime <DateTime>] [-Description <String>]
 [-AgentObject] <AzureSqlElasticJobAgentModel> [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf]
 [-Confirm] [<CommonParameters>]
```

### Resource Id Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-Description <String>] [-AgentResourceId] <String> [-Enable]
 [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm] [<CommonParameters>]
```

### Agent Resource Id Run Once Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-RunOnce] [-StartTime <DateTime>] [-Description <String>]
 [-AgentResourceId] <String> [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
 [<CommonParameters>]
```

### Agent Resource Id Recurring Parameter Set
```
New-AzureRmSqlElasticJob [-Name] <String> [-IntervalType] <JobScheduleReccuringScheduleTypes>
 [-IntervalCount] <UInt32> [-StartTime <DateTime>] [-EndTime <DateTime>] [-Description <String>]
 [-AgentResourceId] <String> [-Enable] [-DefaultProfile <IAzureContextContainer>] [-WhatIf] [-Confirm]
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

### -AgentName
The agent name

```yaml
Type: String
Parameter Sets: Default Parameter Set, Agent Default Run Once Parameter Set, Agent Default Recurring Parameter Set
Aliases:

Required: True
Position: 2
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -AgentObject
The agent input object

```yaml
Type: AzureSqlElasticJobAgentModel
Parameter Sets: Input Object Parameter Set, Agent Object Run Once Parameter Set, Agent Object Recurring Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -AgentResourceId
The agent resource id

```yaml
Type: String
Parameter Sets: Resource Id Parameter Set, Agent Resource Id Run Once Parameter Set, Agent Resource Id Recurring Parameter Set
Aliases:

Required: True
Position: 0
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

### -Description
The job description

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Enable
The flag to indicate customer wants this job to be enabled.

```yaml
Type: SwitchParameter
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -EndTime
The job schedule end time

```yaml
Type: DateTime
Parameter Sets: Agent Default Recurring Parameter Set, Agent Object Recurring Parameter Set, Agent Resource Id Recurring Parameter Set
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IntervalCount
The recurring schedule interval count

```yaml
Type: UInt32
Parameter Sets: Agent Default Recurring Parameter Set, Agent Object Recurring Parameter Set, Agent Resource Id Recurring Parameter Set
Aliases:

Required: True
Position: 5
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -IntervalType
The recurring schedule interval type - Can be Minute, Hour, Day, Week, Month

```yaml
Type: JobScheduleReccuringScheduleTypes
Parameter Sets: Agent Default Recurring Parameter Set, Agent Object Recurring Parameter Set, Agent Resource Id Recurring Parameter Set
Aliases:
Accepted values: Minute, Hour, Day, Week, Month

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Name
The job name

```yaml
Type: String
Parameter Sets: (All)
Aliases: JobName

Required: True
Position: 3
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ResourceGroupName
The resource group name

```yaml
Type: String
Parameter Sets: Default Parameter Set, Agent Default Run Once Parameter Set, Agent Default Recurring Parameter Set
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -RunOnce
The flag to indicate job will be run once

```yaml
Type: SwitchParameter
Parameter Sets: Agent Default Run Once Parameter Set, Agent Object Run Once Parameter Set, Agent Resource Id Run Once Parameter Set
Aliases:

Required: True
Position: 4
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -ServerName
The server name

```yaml
Type: String
Parameter Sets: Default Parameter Set, Agent Default Run Once Parameter Set, Agent Default Recurring Parameter Set
Aliases:

Required: True
Position: 1
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -StartTime
The job schedule start time

```yaml
Type: DateTime
Parameter Sets: Agent Default Run Once Parameter Set, Agent Default Recurring Parameter Set, Agent Object Run Once Parameter Set, Agent Object Recurring Parameter Set, Agent Resource Id Run Once Parameter Set, Agent Resource Id Recurring Parameter Set
Aliases:

Required: False
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
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see about_CommonParameters (http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### Microsoft.Azure.Commands.Sql.ElasticJobs.Model.AzureSqlElasticJobAgentModel
System.String

## OUTPUTS

### System.Object

## NOTES

## RELATED LINKS
