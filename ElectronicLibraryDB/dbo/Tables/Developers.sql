CREATE TABLE [dbo].[Developers] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (20) NOT NULL,
    [LastName]  NVARCHAR (30) NOT NULL,
    [Email]     VARCHAR (320) NOT NULL,
    [Phone]     VARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC, [Phone] ASC)
);

