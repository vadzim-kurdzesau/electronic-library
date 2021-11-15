CREATE TABLE [dbo].[books] (
    [id]               INT            IDENTITY (1, 1) NOT NULL,
    [name]             NVARCHAR (200) NOT NULL,
    [author]           NVARCHAR (100) NOT NULL,
    [publication_date] DATE           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

