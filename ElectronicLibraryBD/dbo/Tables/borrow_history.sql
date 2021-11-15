CREATE TABLE [dbo].[borrow_history] (
    [id]                  INT  IDENTITY (1, 1) NOT NULL,
    [reader_id]           INT  NOT NULL,
    [inventory_number_id] INT  NOT NULL,
    [borrow_date]         DATE NOT NULL,
    [return_date]         DATE NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_borrow_history_to_inventory_numbers] FOREIGN KEY ([inventory_number_id]) REFERENCES [dbo].[inventory_numbers] ([id]),
    CONSTRAINT [FK_borrow_history_to_readers] FOREIGN KEY ([reader_id]) REFERENCES [dbo].[readers] ([id]),
    UNIQUE NONCLUSTERED ([inventory_number_id] ASC)
);

