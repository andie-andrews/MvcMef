
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/24/2016 22:25:16
-- Generated from EDMX file: C:\Users\andie\Source\Workspaces\MEF_MvcMef_Code_Project\MvcMef.Data\MvcMef.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_Achievements_dbo_Skills_Skill_ID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Achievements] DROP CONSTRAINT [FK_dbo_Achievements_dbo_Skills_Skill_ID];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_DocumentNodeRules_dbo_ActionType_ActionTypeId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Rule] DROP CONSTRAINT [FK_dbo_DocumentNodeRules_dbo_ActionType_ActionTypeId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Skills_dbo_Skills_ParentSkillID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Skills] DROP CONSTRAINT [FK_dbo_Skills_dbo_Skills_ParentSkillID];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Achievements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Achievements];
GO
IF OBJECT_ID(N'[dbo].[ActionType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionType];
GO
IF OBJECT_ID(N'[dbo].[Rule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rule];
GO
IF OBJECT_ID(N'[dbo].[Skills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Skills];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Achievements'
CREATE TABLE [dbo].[Achievements] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Skill_ID] int  NULL
);
GO

-- Creating table 'ActionTypes'
CREATE TABLE [dbo].[ActionTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [EffectiveDateTimeBegin] datetime  NOT NULL,
    [EffectiveDateTimeEnd] datetime  NULL
);
GO

-- Creating table 'Rules'
CREATE TABLE [dbo].[Rules] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ActionTypeId] int  NOT NULL,
    [Name] nvarchar(max)  NULL,
    [SelectNodeXPath] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [NodeAttributeName] nvarchar(max)  NULL,
    [NodeAttributeValue] nvarchar(max)  NULL,
    [NodeAttributeExclusions] nvarchar(max)  NULL,
    [CreateNodeHtml] nvarchar(max)  NULL,
    [EffectiveDateTimeBegin] datetime  NOT NULL,
    [EffectiveDateTimeEnd] datetime  NULL
);
GO

-- Creating table 'Skills'
CREATE TABLE [dbo].[Skills] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [Order] int  NOT NULL,
    [Intellect] int  NOT NULL,
    [Wisdom] int  NOT NULL,
    [Charisma] int  NOT NULL,
    [Fortitude] int  NOT NULL,
    [ParentSkillID] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Achievements'
ALTER TABLE [dbo].[Achievements]
ADD CONSTRAINT [PK_Achievements]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionTypes'
ALTER TABLE [dbo].[ActionTypes]
ADD CONSTRAINT [PK_ActionTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [PK_Rules]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [PK_Skills]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Skill_ID] in table 'Achievements'
ALTER TABLE [dbo].[Achievements]
ADD CONSTRAINT [FK_dbo_Achievements_dbo_Skills_Skill_ID]
    FOREIGN KEY ([Skill_ID])
    REFERENCES [dbo].[Skills]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Achievements_dbo_Skills_Skill_ID'
CREATE INDEX [IX_FK_dbo_Achievements_dbo_Skills_Skill_ID]
ON [dbo].[Achievements]
    ([Skill_ID]);
GO

-- Creating foreign key on [ActionTypeId] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [FK_dbo_DocumentNodeRules_dbo_ActionType_ActionTypeId]
    FOREIGN KEY ([ActionTypeId])
    REFERENCES [dbo].[ActionTypes]
        ([ID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_DocumentNodeRules_dbo_ActionType_ActionTypeId'
CREATE INDEX [IX_FK_dbo_DocumentNodeRules_dbo_ActionType_ActionTypeId]
ON [dbo].[Rules]
    ([ActionTypeId]);
GO

-- Creating foreign key on [ParentSkillID] in table 'Skills'
ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [FK_dbo_Skills_dbo_Skills_ParentSkillID]
    FOREIGN KEY ([ParentSkillID])
    REFERENCES [dbo].[Skills]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Skills_dbo_Skills_ParentSkillID'
CREATE INDEX [IX_FK_dbo_Skills_dbo_Skills_ParentSkillID]
ON [dbo].[Skills]
    ([ParentSkillID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------