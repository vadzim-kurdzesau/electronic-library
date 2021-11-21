CREATE TABLE [dbo].[inventory_numbers] (
    [id]      INT          IDENTITY (1, 1) NOT NULL,
    [number]  VARCHAR (30) NOT NULL,
    [book_id] INT          NOT NULL,
    CONSTRAINT [pk_inventory_numbers_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [fk_inventory_numbers_to_books] FOREIGN KEY ([book_id]) REFERENCES [dbo].[books] ([id]) ON DELETE CASCADE,
    CONSTRAINT [ux_inventory_numbers_number] UNIQUE NONCLUSTERED ([number] ASC)
);

