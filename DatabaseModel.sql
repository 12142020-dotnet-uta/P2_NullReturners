CREATE TABLE [users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [full_name] nvarchar(255),
  [phone] phone,
  [email] email,
  [roleId] nvarchar(255),
  [teamId] int,
  [playerCatId] int
)
GO

CREATE TABLE [roles] (
  [id] int PRIMARY KEY,
  [roleName] nvarchar(255)
)
GO

CREATE TABLE [playbooks] (
  [id] int PRIMARY KEY,
  [teamid] int
)
GO

CREATE TABLE [teams] (
  [id] int PRIMARY KEY,
  [name] nvarchar(255),
  [nickname] nvarchar(255),
  [city] nvarchar(255),
  [wins] int,
  [losses] int,
  [ties] int
)
GO

CREATE TABLE [games] (
  [id] int PRIMARY KEY,
  [homeTeamId] int,
  [awayTeamId] int,
  [winner] int,
  [scoreHome] int,
  [scoreAway] int
)
GO

CREATE TABLE [irList] (
  [id] int PRIMARY KEY,
  [teamid] int,
  [userId] int
)
GO

CREATE TABLE [playerCategories] (
  [id] int PRIMARY KEY,
  [position] nvarchar(255)
)
GO

ALTER TABLE [users] ADD FOREIGN KEY ([roleId]) REFERENCES [roles] ([id])
GO

ALTER TABLE [users] ADD FOREIGN KEY ([teamId]) REFERENCES [teams] ([id])
GO

ALTER TABLE [users] ADD FOREIGN KEY ([playerCatId]) REFERENCES [playerCategories] ([id])
GO

ALTER TABLE [playbooks] ADD FOREIGN KEY ([teamid]) REFERENCES [teams] ([id])
GO

ALTER TABLE [games] ADD FOREIGN KEY ([homeTeamId]) REFERENCES [teams] ([id])
GO

ALTER TABLE [games] ADD FOREIGN KEY ([awayTeamId]) REFERENCES [teams] ([id])
GO

ALTER TABLE [games] ADD FOREIGN KEY ([winner]) REFERENCES [teams] ([id])
GO

ALTER TABLE [irList] ADD FOREIGN KEY ([teamid]) REFERENCES [teams] ([id])
GO

ALTER TABLE [irList] ADD FOREIGN KEY ([userId]) REFERENCES [users] ([id])
GO
