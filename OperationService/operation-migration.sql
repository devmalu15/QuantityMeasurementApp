IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260331095854_InitialCreate'
)
BEGIN
    CREATE TABLE [QuantityMeasurements] (
        [Id] int NOT NULL IDENTITY,
        [Operation] nvarchar(20) NOT NULL,
        [Operand1] float NOT NULL,
        [Operand2] float NOT NULL,
        [Result] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_QuantityMeasurements] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260331095854_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260331095854_InitialCreate', N'10.0.5');
END;

COMMIT;
GO

