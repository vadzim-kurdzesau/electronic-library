CREATE TABLE [dbo].[inventory_numbers] (
    [id]      INT          IDENTITY (1, 1) NOT NULL,
    [book_id] INT          NOT NULL,
    [number]  VARCHAR (30) NOT NULL UNIQUE,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_inventory_numbers_to_books] FOREIGN KEY ([book_id]) REFERENCES [dbo].[books] ([id]) ON DELETE CASCADE,
    UNIQUE NONCLUSTERED ([number] ASC)
);

