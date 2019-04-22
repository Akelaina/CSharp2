CREATE TABLE [dbo].[Employees] (
    [ID]           INT        IDENTITY (1, 1) NOT NULL,
    [Name]         NCHAR (10) NOT NULL,
    [Surname]      NCHAR (10) NOT NULL,
    [Age]          TINYINT    DEFAULT ((18)) NULL,
    [Salary]       MONEY      DEFAULT ((20000)) NULL,
    [DepartmentID] INT        DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Departments] ([ID])
);
GO

CREATE TABLE [dbo].[Departments] (
    [ID]   INT        IDENTITY (1, 1) NOT NULL,
    [Name] NCHAR (20) DEFAULT (N'Новый_отдел_1') NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

ALTER TABLE INSERT INTO Employees (Name, Surname, Age, Salary, DepartmentID) VALUES (N'Имя_0', N'Фамилия_0', 18, 35000, 1)
