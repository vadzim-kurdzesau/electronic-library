CREATE TABLE [dbo].[borrow_history] (
    [id]                  INT  IDENTITY (1, 1) NOT NULL,
    [reader_id]           INT  NOT NULL,
    [inventory_number_id] INT  NOT NULL,
    [borrow_date]         DATE NOT NULL,
    [return_date]         DATE NULL,

    CONSTRAINT [pk_borrow_history_id] PRIMARY KEY  CLUSTERED ([id] ASC),
    CONSTRAINT [fk_borrow_history_inventory_numbers_id] FOREIGN KEY ([inventory_number_id]) REFERENCES [dbo].[inventory_numbers] ([id]),
    CONSTRAINT [fk_borrow_history_readers_id] FOREIGN KEY ([reader_id]) REFERENCES [dbo].[readers] ([id]),
    CONSTRAINT [ux_borrow_history_inventory_number_id] UNIQUE NONCLUSTERED ([inventory_number_id] ASC),
);

