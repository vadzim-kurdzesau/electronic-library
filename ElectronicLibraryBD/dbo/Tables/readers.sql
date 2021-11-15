CREATE TABLE [dbo].[readers] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [first_name] NVARCHAR (20)  NOT NULL,
    [last_name]  NVARCHAR (30)  NOT NULL,
    [email]      VARCHAR (320)  NOT NULL,
    [phone]      VARCHAR (20)   NOT NULL,
    [city_id]    INT            NOT NULL,
    [address]    NVARCHAR (100) NOT NULL,
    [zip]        CHAR (6)       NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_readers_to_cities] FOREIGN KEY ([city_id]) REFERENCES [dbo].[cities] ([id]),
    UNIQUE NONCLUSTERED ([email] ASC, [phone] ASC)
);

