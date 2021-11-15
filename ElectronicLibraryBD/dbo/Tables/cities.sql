CREATE TABLE [dbo].[cities] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [city] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([city] ASC)
);

