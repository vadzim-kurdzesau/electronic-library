CREATE TABLE [dbo].[cities] (
    [id]   INT           NOT NULL,
    [name] NVARCHAR (20) NOT NULL,

    CONSTRAINT [pk_cities_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [ux_cities_name] UNIQUE NONCLUSTERED ([name] ASC),
);
