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
GO

CREATE TABLE [Actors] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NULL,
    [AvatarURL] nvarchar(max) NULL,
    [Dob] datetime2 NOT NULL,
    [Gender] nvarchar(max) NULL,
    [Description] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Actors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Directors] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NULL,
    [AvatarURL] nvarchar(max) NULL,
    [Dob] datetime2 NOT NULL,
    [Gender] nvarchar(max) NULL,
    [Description] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Directors] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Movies] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(200) NULL,
    [OriginalTitle] nvarchar(200) NULL,
    [Description] nvarchar(1000) NULL,
    [Duration] int NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    [Country] nvarchar(100) NULL,
    [PosterURL] nvarchar(max) NULL,
    [BackgroundURL] nvarchar(max) NULL,
    [TrailerURL] nvarchar(max) NULL,
    [ImdbRating] decimal(18,2) NOT NULL,
    [AgeRestriction] int NOT NULL,
    [Status] nvarchar(max) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Payments] (
    [Id] uniqueidentifier NOT NULL,
    [OrderCode] bigint NOT NULL,
    [Amount] int NOT NULL,
    [Description] nvarchar(500) NULL,
    [TransactionDateTime] datetime2 NOT NULL,
    [PaymentMethod] nvarchar(50) NULL,
    [Status] nvarchar(50) NULL,
    [Currency] nvarchar(5) NULL,
    [BookingId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Rooms] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NULL,
    [Capacity] int NOT NULL,
    [ScreenType] nvarchar(50) NULL,
    [Description] nvarchar(1000) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SeatTypes] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NULL,
    [PriceMutiplier] decimal(18,2) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_SeatTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tags] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(100) NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Tags] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] uniqueidentifier NOT NULL,
    [Username] nvarchar(100) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Role] nvarchar(max) NULL,
    [UserDetailId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovieActors] (
    [Id] uniqueidentifier NOT NULL,
    [MovieId] uniqueidentifier NOT NULL,
    [ActorId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MovieActors] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MovieActors_Actors_ActorId] FOREIGN KEY ([ActorId]) REFERENCES [Actors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieActors_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [MovieDirector] (
    [Id] uniqueidentifier NOT NULL,
    [MovieId] uniqueidentifier NOT NULL,
    [DirectorId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MovieDirector] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MovieDirector_Directors_DirectorId] FOREIGN KEY ([DirectorId]) REFERENCES [Directors] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieDirector_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Reviews] (
    [Id] uniqueidentifier NOT NULL,
    [Rating] int NOT NULL,
    [Content] nvarchar(1000) NULL,
    [MovieId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id])
);
GO

CREATE TABLE [Showtimes] (
    [Id] uniqueidentifier NOT NULL,
    [StartTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [BasePrice] decimal(18,2) NOT NULL,
    [RoomId] uniqueidentifier NOT NULL,
    [MovieId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Showtimes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Showtimes_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]),
    CONSTRAINT [FK_Showtimes_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id])
);
GO

CREATE TABLE [Seats] (
    [Id] uniqueidentifier NOT NULL,
    [SeatRow] nvarchar(50) NULL,
    [SeatNumber] int NOT NULL,
    [Description] nvarchar(200) NULL,
    [SeatTypeId] uniqueidentifier NOT NULL,
    [RoomId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Seats_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]),
    CONSTRAINT [FK_Seats_SeatTypes_SeatTypeId] FOREIGN KEY ([SeatTypeId]) REFERENCES [SeatTypes] ([Id])
);
GO

CREATE TABLE [MovieTags] (
    [Id] uniqueidentifier NOT NULL,
    [MovieId] uniqueidentifier NOT NULL,
    [TagId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_MovieTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MovieTags_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieTags_Tags_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Tags] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bookings] (
    [Id] uniqueidentifier NOT NULL,
    [BookingDate] datetime2 NOT NULL,
    [NumberOfTickets] int NOT NULL,
    [TotalPrice] decimal(18,2) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PaymentId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_Payments_PaymentId] FOREIGN KEY ([PaymentId]) REFERENCES [Payments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
);
GO

CREATE TABLE [UserDetails] (
    [Id] uniqueidentifier NOT NULL,
    [FullName] nvarchar(100) NULL,
    [AvatarURL] nvarchar(max) NULL,
    [Gender] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Dob] date NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_UserDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserDetails_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Tickets] (
    [Id] uniqueidentifier NOT NULL,
    [TicketCode] nvarchar(max) NULL,
    [Price] decimal(18,2) NOT NULL,
    [BookingId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tickets_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [ShowtimeSeats] (
    [Id] uniqueidentifier NOT NULL,
    [ShowtimeId] uniqueidentifier NOT NULL,
    [SeatId] uniqueidentifier NOT NULL,
    [TicketId] uniqueidentifier NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedAt] datetime2 NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [RemovedAt] datetime2 NOT NULL,
    [RemovedBy] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_ShowtimeSeats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShowtimeSeats_Seats_SeatId] FOREIGN KEY ([SeatId]) REFERENCES [Seats] ([Id]),
    CONSTRAINT [FK_ShowtimeSeats_Showtimes_ShowtimeId] FOREIGN KEY ([ShowtimeId]) REFERENCES [Showtimes] ([Id]),
    CONSTRAINT [FK_ShowtimeSeats_Tickets_TicketId] FOREIGN KEY ([TicketId]) REFERENCES [Tickets] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Bookings_PaymentId] ON [Bookings] ([PaymentId]);
GO

CREATE INDEX [IX_Bookings_UserId] ON [Bookings] ([UserId]);
GO

CREATE INDEX [IX_MovieActors_ActorId] ON [MovieActors] ([ActorId]);
GO

CREATE INDEX [IX_MovieActors_MovieId] ON [MovieActors] ([MovieId]);
GO

CREATE INDEX [IX_MovieDirector_DirectorId] ON [MovieDirector] ([DirectorId]);
GO

CREATE INDEX [IX_MovieDirector_MovieId] ON [MovieDirector] ([MovieId]);
GO

CREATE INDEX [IX_MovieTags_MovieId] ON [MovieTags] ([MovieId]);
GO

CREATE INDEX [IX_Reviews_MovieId] ON [Reviews] ([MovieId]);
GO

CREATE INDEX [IX_Seats_RoomId] ON [Seats] ([RoomId]);
GO

CREATE INDEX [IX_Seats_SeatTypeId] ON [Seats] ([SeatTypeId]);
GO

CREATE INDEX [IX_Showtimes_MovieId] ON [Showtimes] ([MovieId]);
GO

CREATE INDEX [IX_Showtimes_RoomId] ON [Showtimes] ([RoomId]);
GO

CREATE INDEX [IX_ShowtimeSeats_SeatId] ON [ShowtimeSeats] ([SeatId]);
GO

CREATE INDEX [IX_ShowtimeSeats_ShowtimeId] ON [ShowtimeSeats] ([ShowtimeId]);
GO

CREATE UNIQUE INDEX [IX_ShowtimeSeats_TicketId] ON [ShowtimeSeats] ([TicketId]);
GO

CREATE INDEX [IX_Tickets_BookingId] ON [Tickets] ([BookingId]);
GO

CREATE UNIQUE INDEX [IX_UserDetails_UserId] ON [UserDetails] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250620184009_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UserDetailId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Users] DROP COLUMN [UserDetailId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250621050041_FixUserFGKey', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UpdatedBy');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Users] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'UpdatedAt');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Users] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RemovedBy');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Users] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'RemovedAt');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Users] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'UpdatedBy');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'UpdatedAt');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'RemovedBy');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'RemovedAt');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'UpdatedBy');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'UpdatedAt');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'RemovedBy');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'RemovedAt');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'UpdatedBy');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Tags] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'UpdatedAt');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Tags] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'RemovedBy');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Tags] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'RemovedAt');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Tags] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'UpdatedBy');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'UpdatedAt');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'RemovedBy');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'RemovedAt');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'UpdatedBy');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'UpdatedAt');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'RemovedBy');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'RemovedAt');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'UpdatedBy');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'UpdatedAt');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'RemovedBy');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'RemovedAt');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'UpdatedBy');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Seats] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'UpdatedAt');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Seats] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'RemovedBy');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Seats] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'RemovedAt');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Seats] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var33 sysname;
SELECT @var33 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'UpdatedBy');
IF @var33 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var33 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var34 sysname;
SELECT @var34 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'UpdatedAt');
IF @var34 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var34 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var35 sysname;
SELECT @var35 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'RemovedBy');
IF @var35 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var35 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var36 sysname;
SELECT @var36 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'RemovedAt');
IF @var36 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var36 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var37 sysname;
SELECT @var37 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'UpdatedBy');
IF @var37 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var37 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var38 sysname;
SELECT @var38 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'UpdatedAt');
IF @var38 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var38 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var39 sysname;
SELECT @var39 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'RemovedBy');
IF @var39 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var39 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var40 sysname;
SELECT @var40 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'RemovedAt');
IF @var40 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var40 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var41 sysname;
SELECT @var41 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'UpdatedBy');
IF @var41 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var41 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var42 sysname;
SELECT @var42 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'UpdatedAt');
IF @var42 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var42 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var43 sysname;
SELECT @var43 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'RemovedBy');
IF @var43 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var43 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var44 sysname;
SELECT @var44 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'RemovedAt');
IF @var44 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var44 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var45 sysname;
SELECT @var45 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedBy');
IF @var45 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var45 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var46 sysname;
SELECT @var46 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedAt');
IF @var46 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var46 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var47 sysname;
SELECT @var47 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'RemovedBy');
IF @var47 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var47 + '];');
ALTER TABLE [Movies] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var48 sysname;
SELECT @var48 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'RemovedAt');
IF @var48 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var48 + '];');
ALTER TABLE [Movies] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var49 sysname;
SELECT @var49 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'UpdatedBy');
IF @var49 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var49 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var50 sysname;
SELECT @var50 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'UpdatedAt');
IF @var50 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var50 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var51 sysname;
SELECT @var51 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'RemovedBy');
IF @var51 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var51 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var52 sysname;
SELECT @var52 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'RemovedAt');
IF @var52 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var52 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var53 sysname;
SELECT @var53 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'UpdatedBy');
IF @var53 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var53 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var54 sysname;
SELECT @var54 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'UpdatedAt');
IF @var54 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var54 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var55 sysname;
SELECT @var55 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'RemovedBy');
IF @var55 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var55 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var56 sysname;
SELECT @var56 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'RemovedAt');
IF @var56 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var56 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var57 sysname;
SELECT @var57 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'UpdatedBy');
IF @var57 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var57 + '];');
ALTER TABLE [Directors] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var58 sysname;
SELECT @var58 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'UpdatedAt');
IF @var58 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var58 + '];');
ALTER TABLE [Directors] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var59 sysname;
SELECT @var59 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'RemovedBy');
IF @var59 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var59 + '];');
ALTER TABLE [Directors] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var60 sysname;
SELECT @var60 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'RemovedAt');
IF @var60 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var60 + '];');
ALTER TABLE [Directors] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var61 sysname;
SELECT @var61 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'UpdatedBy');
IF @var61 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var61 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var62 sysname;
SELECT @var62 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'UpdatedAt');
IF @var62 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var62 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var63 sysname;
SELECT @var63 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'RemovedBy');
IF @var63 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var63 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var64 sysname;
SELECT @var64 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'RemovedAt');
IF @var64 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var64 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

DECLARE @var65 sysname;
SELECT @var65 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'UpdatedBy');
IF @var65 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var65 + '];');
ALTER TABLE [Actors] ALTER COLUMN [UpdatedBy] uniqueidentifier NULL;
GO

DECLARE @var66 sysname;
SELECT @var66 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'UpdatedAt');
IF @var66 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var66 + '];');
ALTER TABLE [Actors] ALTER COLUMN [UpdatedAt] datetime2 NULL;
GO

DECLARE @var67 sysname;
SELECT @var67 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'RemovedBy');
IF @var67 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var67 + '];');
ALTER TABLE [Actors] ALTER COLUMN [RemovedBy] uniqueidentifier NULL;
GO

DECLARE @var68 sysname;
SELECT @var68 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'RemovedAt');
IF @var68 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var68 + '];');
ALTER TABLE [Actors] ALTER COLUMN [RemovedAt] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250621050914_FixAuditType', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var69 sysname;
SELECT @var69 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'Dob');
IF @var69 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var69 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [Dob] date NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250621055123_changeUserDetailDobToNullable', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var70 sysname;
SELECT @var70 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CreatedBy');
IF @var70 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var70 + '];');
ALTER TABLE [Users] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var71 sysname;
SELECT @var71 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'CreatedAt');
IF @var71 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var71 + '];');
ALTER TABLE [Users] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var72 sysname;
SELECT @var72 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'CreatedBy');
IF @var72 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var72 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var73 sysname;
SELECT @var73 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserDetails]') AND [c].[name] = N'CreatedAt');
IF @var73 IS NOT NULL EXEC(N'ALTER TABLE [UserDetails] DROP CONSTRAINT [' + @var73 + '];');
ALTER TABLE [UserDetails] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var74 sysname;
SELECT @var74 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'CreatedBy');
IF @var74 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var74 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var75 sysname;
SELECT @var75 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tickets]') AND [c].[name] = N'CreatedAt');
IF @var75 IS NOT NULL EXEC(N'ALTER TABLE [Tickets] DROP CONSTRAINT [' + @var75 + '];');
ALTER TABLE [Tickets] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var76 sysname;
SELECT @var76 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'CreatedBy');
IF @var76 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var76 + '];');
ALTER TABLE [Tags] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var77 sysname;
SELECT @var77 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Tags]') AND [c].[name] = N'CreatedAt');
IF @var77 IS NOT NULL EXEC(N'ALTER TABLE [Tags] DROP CONSTRAINT [' + @var77 + '];');
ALTER TABLE [Tags] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var78 sysname;
SELECT @var78 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'CreatedBy');
IF @var78 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var78 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var79 sysname;
SELECT @var79 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShowtimeSeats]') AND [c].[name] = N'CreatedAt');
IF @var79 IS NOT NULL EXEC(N'ALTER TABLE [ShowtimeSeats] DROP CONSTRAINT [' + @var79 + '];');
ALTER TABLE [ShowtimeSeats] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var80 sysname;
SELECT @var80 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'CreatedBy');
IF @var80 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var80 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var81 sysname;
SELECT @var81 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Showtimes]') AND [c].[name] = N'CreatedAt');
IF @var81 IS NOT NULL EXEC(N'ALTER TABLE [Showtimes] DROP CONSTRAINT [' + @var81 + '];');
ALTER TABLE [Showtimes] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var82 sysname;
SELECT @var82 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'CreatedBy');
IF @var82 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var82 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var83 sysname;
SELECT @var83 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SeatTypes]') AND [c].[name] = N'CreatedAt');
IF @var83 IS NOT NULL EXEC(N'ALTER TABLE [SeatTypes] DROP CONSTRAINT [' + @var83 + '];');
ALTER TABLE [SeatTypes] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var84 sysname;
SELECT @var84 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'CreatedBy');
IF @var84 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var84 + '];');
ALTER TABLE [Seats] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var85 sysname;
SELECT @var85 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Seats]') AND [c].[name] = N'CreatedAt');
IF @var85 IS NOT NULL EXEC(N'ALTER TABLE [Seats] DROP CONSTRAINT [' + @var85 + '];');
ALTER TABLE [Seats] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var86 sysname;
SELECT @var86 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'CreatedBy');
IF @var86 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var86 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var87 sysname;
SELECT @var87 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'CreatedAt');
IF @var87 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var87 + '];');
ALTER TABLE [Rooms] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var88 sysname;
SELECT @var88 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CreatedBy');
IF @var88 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var88 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var89 sysname;
SELECT @var89 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CreatedAt');
IF @var89 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var89 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var90 sysname;
SELECT @var90 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'CreatedBy');
IF @var90 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var90 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var91 sysname;
SELECT @var91 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieTags]') AND [c].[name] = N'CreatedAt');
IF @var91 IS NOT NULL EXEC(N'ALTER TABLE [MovieTags] DROP CONSTRAINT [' + @var91 + '];');
ALTER TABLE [MovieTags] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var92 sysname;
SELECT @var92 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedBy');
IF @var92 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var92 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var93 sysname;
SELECT @var93 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedAt');
IF @var93 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var93 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var94 sysname;
SELECT @var94 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'CreatedBy');
IF @var94 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var94 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var95 sysname;
SELECT @var95 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieDirector]') AND [c].[name] = N'CreatedAt');
IF @var95 IS NOT NULL EXEC(N'ALTER TABLE [MovieDirector] DROP CONSTRAINT [' + @var95 + '];');
ALTER TABLE [MovieDirector] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var96 sysname;
SELECT @var96 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'CreatedBy');
IF @var96 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var96 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var97 sysname;
SELECT @var97 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[MovieActors]') AND [c].[name] = N'CreatedAt');
IF @var97 IS NOT NULL EXEC(N'ALTER TABLE [MovieActors] DROP CONSTRAINT [' + @var97 + '];');
ALTER TABLE [MovieActors] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var98 sysname;
SELECT @var98 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'CreatedBy');
IF @var98 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var98 + '];');
ALTER TABLE [Directors] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var99 sysname;
SELECT @var99 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Directors]') AND [c].[name] = N'CreatedAt');
IF @var99 IS NOT NULL EXEC(N'ALTER TABLE [Directors] DROP CONSTRAINT [' + @var99 + '];');
ALTER TABLE [Directors] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var100 sysname;
SELECT @var100 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'CreatedBy');
IF @var100 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var100 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var101 sysname;
SELECT @var101 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Bookings]') AND [c].[name] = N'CreatedAt');
IF @var101 IS NOT NULL EXEC(N'ALTER TABLE [Bookings] DROP CONSTRAINT [' + @var101 + '];');
ALTER TABLE [Bookings] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

DECLARE @var102 sysname;
SELECT @var102 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'CreatedBy');
IF @var102 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var102 + '];');
ALTER TABLE [Actors] ALTER COLUMN [CreatedBy] uniqueidentifier NULL;
GO

DECLARE @var103 sysname;
SELECT @var103 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Actors]') AND [c].[name] = N'CreatedAt');
IF @var103 IS NOT NULL EXEC(N'ALTER TABLE [Actors] DROP CONSTRAINT [' + @var103 + '];');
ALTER TABLE [Actors] ALTER COLUMN [CreatedAt] datetime2 NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250621061351_changeBaseToNUllable', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Users] ADD [IsVerify] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250708074855_AddIsVerifyAndIsActiveToUser', N'8.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250708075014_UpdateIsActiveDefaultValue', N'8.0.0');
GO

COMMIT;
GO

