CREATE TABLE [dbo].[cities] (
    [id]   INT           IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

