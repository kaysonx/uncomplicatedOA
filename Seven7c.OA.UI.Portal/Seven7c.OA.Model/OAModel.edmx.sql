
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/23/2015 08:47:02
-- Generated from EDMX file: G:\BaiduYunDownload\OA\Seven7c.OA.UI.Portal\Seven7c.OA.Model\OAModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Seven7cOADB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Rel_UserInfo_OrderInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderInfo] DROP CONSTRAINT [FK_Rel_UserInfo_OrderInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_TempWF_Instance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_Instance] DROP CONSTRAINT [FK_WF_TempWF_Instance];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_InstanceWF_Step]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_Step] DROP CONSTRAINT [FK_WF_InstanceWF_Step];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoMesssage]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messsage] DROP CONSTRAINT [FK_UserInfoMesssage];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[OrderInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoExt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoExt];
GO
IF OBJECT_ID(N'[dbo].[WF_Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Temp];
GO
IF OBJECT_ID(N'[dbo].[WF_Instance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Instance];
GO
IF OBJECT_ID(N'[dbo].[WF_Step]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Step];
GO
IF OBJECT_ID(N'[dbo].[Messsage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messsage];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(32)  NOT NULL,
    [Pwd] nvarchar(32)  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [Email] nvarchar(128)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'OrderInfo'
CREATE TABLE [dbo].[OrderInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(32)  NOT NULL,
    [UserInfoId] int  NOT NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModifiedOn] datetimeoffset  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActionName] nvarchar(32)  NOT NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethod] nvarchar(32)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ActionInfoType] smallint  NOT NULL,
    [Remark] nvarchar(128)  NOT NULL,
    [IconUrl] nvarchar(512)  NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IsPass] bit  NOT NULL,
    [UserInfoId] int  NOT NULL,
    [ActionInfoId] int  NOT NULL
);
GO

-- Creating table 'UserInfoExt'
CREATE TABLE [dbo].[UserInfoExt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Address] nvarchar(64)  NOT NULL,
    [UserInfoId] int  NOT NULL
);
GO

-- Creating table 'WF_Temp'
CREATE TABLE [dbo].[WF_Temp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TempName] nvarchar(32)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(512)  NULL,
    [Description] nvarchar(max)  NOT NULL,
    [TempForm] nvarchar(max)  NULL,
    [WFActivity] nvarchar(max)  NULL
);
GO

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WF_TempId] int  NOT NULL,
    [InstName] nvarchar(32)  NOT NULL,
    [WFApplicationId] uniqueidentifier  NOT NULL,
    [SubBy] int  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [EndTime] datetime  NOT NULL,
    [Status] smallint  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(512)  NULL,
    [InstanceForm] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WF_Step'
CREATE TABLE [dbo].[WF_Step] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WF_InstanceId] int  NOT NULL,
    [StepName] nvarchar(32)  NOT NULL,
    [ProcessBy] int  NULL,
    [Status] smallint  NOT NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [Result] nvarchar(32)  NULL,
    [Comment] nvarchar(512)  NULL,
    [SubTime] datetime  NOT NULL,
    [CheckTime] datetime  NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(512)  NULL
);
GO

-- Creating table 'Messsage'
CREATE TABLE [dbo].[Messsage] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(32)  NULL,
    [Content] nvarchar(max)  NOT NULL,
    [FileUrl] nvarchar(128)  NULL,
    [SendTime] datetime  NOT NULL,
    [SendTo] int  NULL,
    [SendRoles] int  NULL,
    [UserInfoId] int  NOT NULL,
    [IsRead] bit  NOT NULL,
    [DelFlagSend] smallint  NOT NULL,
    [DelFlagGet] smallint  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [UserInfo_Id] int  NOT NULL,
    [RoleInfo_Id] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionInfo'
CREATE TABLE [dbo].[RoleInfoActionInfo] (
    [RoleInfo_Id] int  NOT NULL,
    [ActionInfo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [PK_OrderInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInfoExt'
ALTER TABLE [dbo].[UserInfoExt]
ADD CONSTRAINT [PK_UserInfoExt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Temp'
ALTER TABLE [dbo].[WF_Temp]
ADD CONSTRAINT [PK_WF_Temp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [PK_WF_Instance]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [PK_WF_Step]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messsage'
ALTER TABLE [dbo].[Messsage]
ADD CONSTRAINT [PK_Messsage]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserInfo_Id], [RoleInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([UserInfo_Id], [RoleInfo_Id] ASC);
GO

-- Creating primary key on [RoleInfo_Id], [ActionInfo_Id] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [PK_RoleInfoActionInfo]
    PRIMARY KEY CLUSTERED ([RoleInfo_Id], [ActionInfo_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfoId] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [FK_Rel_UserInfo_OrderInfo]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Rel_UserInfo_OrderInfo'
CREATE INDEX [IX_FK_Rel_UserInfo_OrderInfo]
ON [dbo].[OrderInfo]
    ([UserInfoId]);
GO

-- Creating foreign key on [UserInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_Id])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_RoleInfo]
ON [dbo].[UserInfoRoleInfo]
    ([RoleInfo_Id]);
GO

-- Creating foreign key on [RoleInfo_Id] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionInfo_Id] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_Id])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionInfo_ActionInfo'
CREATE INDEX [IX_FK_RoleInfoActionInfo_ActionInfo]
ON [dbo].[RoleInfoActionInfo]
    ([ActionInfo_Id]);
GO

-- Creating foreign key on [UserInfoId] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfoId]);
GO

-- Creating foreign key on [ActionInfoId] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoId])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfoId]);
GO

-- Creating foreign key on [WF_TempId] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WF_TempWF_Instance]
    FOREIGN KEY ([WF_TempId])
    REFERENCES [dbo].[WF_Temp]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_TempWF_Instance'
CREATE INDEX [IX_FK_WF_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WF_TempId]);
GO

-- Creating foreign key on [WF_InstanceId] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [FK_WF_InstanceWF_Step]
    FOREIGN KEY ([WF_InstanceId])
    REFERENCES [dbo].[WF_Instance]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_Step'
CREATE INDEX [IX_FK_WF_InstanceWF_Step]
ON [dbo].[WF_Step]
    ([WF_InstanceId]);
GO

-- Creating foreign key on [UserInfoId] in table 'Messsage'
ALTER TABLE [dbo].[Messsage]
ADD CONSTRAINT [FK_UserInfoMesssage]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoMesssage'
CREATE INDEX [IX_FK_UserInfoMesssage]
ON [dbo].[Messsage]
    ([UserInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------