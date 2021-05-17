CREATE TABLE [dbo].[Card]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [PAN] VARCHAR(16) NOT NULL, 
    [ExpirationMonth] INT NOT NULL, 
    [ExpirationYear] INT NOT NULL, 
    [SecurityCode] VARCHAR(3) NOT NULL, 
    [CardName] VARCHAR(100) NOT NULL,
	[Quota] INT NOT NULL,
    [IdClient] INT NOT NULL,
	[Brand] VARCHAR(20) NOT NULL,
	[Active] BIT NOT NULL,
    CONSTRAINT [FK_Card_Client] FOREIGN KEY ([IdClient]) REFERENCES [Client]([Id])
)

GO

CREATE UNIQUE INDEX [IX_Card_PAN] ON [dbo].[Card] ([PAN])
