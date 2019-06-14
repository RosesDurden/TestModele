
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/22/2017 15:00:11
-- Generated from EDMX file: C:\dev\dotnet\ModeleMVC\CD67.ModeleMVC.Entity\EntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModeleMVC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TypesVikingVikings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vikings] DROP CONSTRAINT [FK_TypesVikingVikings];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[TypesViking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypesViking];
GO
IF OBJECT_ID(N'[dbo].[Vikings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vikings];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TypesViking'
CREATE TABLE [dbo].[TypesViking] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Libelle] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Vikings'
CREATE TABLE [dbo].[Vikings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nom] nvarchar(100)  NOT NULL,
    [CasqueCornu] bit  NOT NULL,
    [NombreVictoires] int  NULL,
    [Description] nvarchar(max)  NULL,
    [DateCreation] datetime  NOT NULL,
    [DateEdition] datetime  NOT NULL,
    [TypeVikingId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'TypesViking'
ALTER TABLE [dbo].[TypesViking]
ADD CONSTRAINT [PK_TypesViking]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vikings'
ALTER TABLE [dbo].[Vikings]
ADD CONSTRAINT [PK_Vikings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TypeVikingId] in table 'Vikings'
ALTER TABLE [dbo].[Vikings]
ADD CONSTRAINT [FK_TypeVikingViking]
    FOREIGN KEY ([TypeVikingId])
    REFERENCES [dbo].[TypesViking]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeVikingViking'
CREATE INDEX [IX_FK_TypeVikingViking]
ON [dbo].[Vikings]
    ([TypeVikingId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------