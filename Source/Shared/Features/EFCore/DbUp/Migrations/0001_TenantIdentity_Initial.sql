IF OBJECT_ID(N'[MigrationHistory_TenantIdentity]') IS NULL
BEGIN
    CREATE TABLE [MigrationHistory_TenantIdentity] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_MigrationHistory_TenantIdentity] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'Identity') IS NULL EXEC(N'CREATE SCHEMA [Identity];');
GO

CREATE TABLE [Identity].[AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [PictureUri] nvarchar(max) NULL,
    [CountOfOpenTabs] int NOT NULL,
    [SelectedTenantId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[TenantSettings] (
    [Id] uniqueidentifier NOT NULL,
    [IconURI] nvarchar(max) NULL,
    [CreatedByUserId] uniqueidentifier NOT NULL,
    [IsSoftDeleted] bit NOT NULL,
    [RowVersion] varbinary(max) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [LastUpdatedAt] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TenantSettings] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[TenantStylings] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedByUserId] uniqueidentifier NOT NULL,
    [IsSoftDeleted] bit NOT NULL,
    [RowVersion] varbinary(max) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [LastUpdatedAt] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TenantStylings] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Identity].[AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [ApplicationUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Identity].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ApplicationUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Identity].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    [ApplicationUserId] uniqueidentifier NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Identity].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[Tenants] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [StylingId] uniqueidentifier NULL,
    [SettingsId] uniqueidentifier NULL,
    [SubscriptionPlanType] int NOT NULL,
    [CreatedByUserId] uniqueidentifier NOT NULL,
    [IsSoftDeleted] bit NOT NULL,
    [RowVersion] varbinary(max) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [LastUpdatedAt] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Tenants] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Tenants_TenantSettings_SettingsId] FOREIGN KEY ([SettingsId]) REFERENCES [Identity].[TenantSettings] ([Id]),
    CONSTRAINT [FK_Tenants_TenantStylings_StylingId] FOREIGN KEY ([StylingId]) REFERENCES [Identity].[TenantStylings] ([Id])
);
GO

CREATE TABLE [Identity].[TenantInvitations] (
    [TenantId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [Role] int NOT NULL,
    CONSTRAINT [FK_TenantInvitations_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TenantInvitations_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Identity].[Tenants] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Identity].[TenantMeberships] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [TenantId] uniqueidentifier NULL,
    [Role] int NOT NULL,
    [ApplicationUserId] uniqueidentifier NULL,
    [CreatedByUserId] uniqueidentifier NOT NULL,
    [IsSoftDeleted] bit NOT NULL,
    [RowVersion] varbinary(max) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [LastUpdatedAt] datetimeoffset NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_TenantMeberships] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TenantMeberships_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [Identity].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_TenantMeberships_Tenants_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [Identity].[Tenants] ([Id])
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [Identity].[AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Identity].[AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_ApplicationUserId] ON [Identity].[AspNetUserClaims] ([ApplicationUserId]);
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [Identity].[AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_ApplicationUserId] ON [Identity].[AspNetUserLogins] ([ApplicationUserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [Identity].[AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [Identity].[AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Identity].[AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Identity].[AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserTokens_ApplicationUserId] ON [Identity].[AspNetUserTokens] ([ApplicationUserId]);
GO

CREATE INDEX [IX_TenantInvitations_TenantId] ON [Identity].[TenantInvitations] ([TenantId]);
GO

CREATE INDEX [IX_TenantInvitations_UserId] ON [Identity].[TenantInvitations] ([UserId]);
GO

CREATE INDEX [IX_TenantMeberships_ApplicationUserId] ON [Identity].[TenantMeberships] ([ApplicationUserId]);
GO

CREATE INDEX [IX_TenantMeberships_TenantId] ON [Identity].[TenantMeberships] ([TenantId]);
GO

CREATE INDEX [IX_Tenants_SettingsId] ON [Identity].[Tenants] ([SettingsId]);
GO

CREATE INDEX [IX_Tenants_StylingId] ON [Identity].[Tenants] ([StylingId]);
GO

INSERT INTO [MigrationHistory_TenantIdentity] ([MigrationId], [ProductVersion])
VALUES (N'20240210092230_initial', N'8.0.1');
GO

COMMIT;
GO