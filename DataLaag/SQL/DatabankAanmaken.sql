CREATE TABLE [dbo].[Continent] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Naam]             NVARCHAR (100) NOT NULL,
    [Bevolkingsaantal] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
CREATE TABLE [dbo].[Land] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Naam]             NVARCHAR (150)  NOT NULL,
    [Bevolkingsaantal] INT             NOT NULL,
    [Oppervlakte]      DECIMAL (18, 2) NOT NULL,
    [ContinentId]      INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Land_Continent] FOREIGN KEY ([ContinentId]) REFERENCES [dbo].[Continent] ([Id])
);
GO
CREATE TABLE [dbo].[Stad] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Naam]             NVARCHAR (150)  NOT NULL,
    [Bevolkingsaantal] INT             NOT NULL,
    [LandId]           INT             NOT NULL,
    [IsHoofdstad]      BIT             NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Stad_Land] FOREIGN KEY ([LandId]) REFERENCES [dbo].[Land] ([Id])
);
GO