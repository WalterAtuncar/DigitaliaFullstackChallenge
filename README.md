# DigitaliaFullstackChallenge

#	Descripción del Proyecto
El proyecto "Digitalia.Fullstack.Challenge" es una aplicación de votación y encuestas diseñada con una arquitectura robusta y escalable, utilizando tecnologías modernas como Angular para el frontend y .NET para el backend. La base de datos SQL Server almacena información en cuatro tablas principales: Users para la gestión de usuarios, Surveys para almacenar las encuestas, SurveyOptions para registrar las opciones de cada encuesta, y Votes para conservar los votos de los usuarios. La aplicación permite a los usuarios crear y participar en encuestas, votando por sus opciones preferidas y visualizando los resultados en tiempo real. Se utiliza autenticación JWT para asegurar las interacciones y garantizar la integridad de los datos, mientras que Angular Material proporciona una interfaz de usuario intuitiva y atractiva. El diseño del sistema prioriza la seguridad, la eficiencia y la experiencia del usuario, facilitando la recolección de opiniones y preferencias de manera organizada y accesible.

#	Arquitectura y Patrones Utilizados
	##	N Capas
- Separación de Responsabilidades: Cada capa (Presentación, Negocio, Acceso a Datos) tiene responsabilidades bien definidas, lo que facilita el mantenimiento y la escalabilidad.
- Flexibilidad: Permite cambiar o mejorar una capa sin afectar a las demás.
	##	Repository y Unit of Work
- Abstracción de Acceso a Datos: El patrón Repository proporciona una abstracción del acceso a datos, permitiendo una manera más organizada y mantenible de acceder a la base de datos.
- Consistencia: Unit of Work maneja las transacciones de la base de datos, asegurando la consistencia de los datos.
	##	Elección de .NET 6
- Estabilidad y Rendimiento: .NET 6 es la versión más reciente y estable, ofreciendo mejoras significativas en rendimiento.
- Soporte a Largo Plazo: Garantiza soporte y actualizaciones durante un período extendido.
	##	Documentación con Swagger
	#Prácticas de Desarrollo: TDD y Moq
- Calidad del Código: TDD asegura que el código cumpla con los requisitos antes de su implementación.
- Pruebas Efectivas: Moq se utiliza para mockear dependencias externas en las pruebas, permitiendo pruebas unitarias más aisladas y confiables.
	##	Elección de Dapper
- Rendimiento: Dapper es un ORM ligero que ofrece un rendimiento superior para operaciones de base de datos.
- Simplicidad y Flexibilidad: Proporciona una manera sencilla de mapear resultados de consultas a objetos.

#CREACION DE BADE DE DATOS Y STORE PROCEDURES
##
CREATE DATABASE DigitaliaVotes;
GO

USE [DigitaliaVotes]
GO

-- Creación de la tabla SurveyOptions
CREATE TABLE [dbo].[SurveyOptions](
    [OptionID] [int] IDENTITY(1,1) NOT NULL,
    [OptionText] [varchar](255) NOT NULL,
    PRIMARY KEY CLUSTERED ([OptionID])
)
GO

-- Creación de la tabla Surveys
CREATE TABLE [dbo].[Surveys](
    [SurveyID] [int] IDENTITY(1,1) NOT NULL,
    [UserID] [int] NOT NULL,
    [Title] [varchar](255) NOT NULL,
    [Description] [text] NOT NULL,
    [Question] [varchar](255) NOT NULL,
    [CreationDate] [datetime] NOT NULL,
    [IsActive] [bit] NOT NULL,
    PRIMARY KEY CLUSTERED ([SurveyID])
)
GO

-- Creación de la tabla Users
CREATE TABLE [dbo].[Users](
    [UserID] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [varchar](255) NULL,
    [Email] [varchar](255) NULL,
    [PasswordHash] [varchar](255) NULL,
    [AuthProvider] [varchar](50) NULL,
    [ProviderID] [varchar](255) NULL,
    [ProfilePictureUrl] [varchar](255) NULL,
    [CreationDate] [datetime] NOT NULL,
    [LastAccess] [datetime] NULL,
    PRIMARY KEY CLUSTERED ([UserID])
)
GO

-- Creación de la tabla Votes
CREATE TABLE [dbo].[Votes](
    [VoteID] [int] IDENTITY(1,1) NOT NULL,
    [SurveyID] [int] NOT NULL,
    [OptionID] [int] NOT NULL,
    [UserID] [int] NOT NULL,
    [VoteDate] [datetime] NOT NULL,
    PRIMARY KEY CLUSTERED ([VoteID])
)
GO

-- Definición de las relaciones de clave foránea
ALTER TABLE [dbo].[Surveys] ADD FOREIGN KEY([UserID]) REFERENCES [dbo].[Users] ([UserID])
ALTER TABLE [dbo].[Votes] ADD FOREIGN KEY([OptionID]) REFERENCES [dbo].[SurveyOptions] ([OptionID])
ALTER TABLE [dbo].[Votes] ADD FOREIGN KEY([SurveyID]) REFERENCES [dbo].[Surveys] ([SurveyID])
ALTER TABLE [dbo].[Votes] ADD FOREIGN KEY([UserID]) REFERENCES [dbo].[Users] ([UserID])
GO

-- Insertando las opciones estándar
INSERT INTO SurveyOptions (OptionText) VALUES ('Aprueba');
INSERT INTO SurveyOptions (OptionText) VALUES ('Desaprueba');
INSERT INTO SurveyOptions (OptionText) VALUES ('No precisa');
GO

-- Stores Procedures
USE [DigitaliaVotes]
GO
/****** Object:  StoredProcedure [dbo].[spGetSurveyResults]    Script Date: 17/12/2023 00:36:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- spGetSurveyResults 2
-- =============================================
CREATE PROCEDURE [dbo].[spGetSurveyResults]
    @SurveyID int
AS
BEGIN
    SET NOCOUNT ON;

    -- Suponiendo que las opciones "Aprueba", "Desaprueba", "No precisa" tienen IDs específicos
    WITH OptionCounts AS (
        SELECT 
            so.OptionText, 
            COUNT(v.VoteID) AS TotalVotes
        FROM [dbo].[SurveyOptions] so
        LEFT JOIN [dbo].[Votes] v ON so.OptionID = v.OptionID AND v.SurveyID = @SurveyID
        WHERE so.OptionText IN ('Aprueba', 'Desaprueba', 'No precisa')
        GROUP BY so.OptionText
    )
    SELECT 
        oc.OptionText, 
        ISNULL(oc.TotalVotes, 0) AS TotalVotes
    FROM OptionCounts oc
    ORDER BY oc.OptionText
END
GO

USE [DigitaliaVotes]
GO
/****** Object:  StoredProcedure [dbo].[spValidateLogin]    Script Date: 17/12/2023 00:37:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spValidateLogin]
    @UserName VARCHAR(255),
    @Password VARCHAR(255),
    @ProviderID VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    IF @ProviderID = '' OR @ProviderID IS NULL
    BEGIN
        -- Si @ProviderID es vacío, selecciona con UserName y Password
        SELECT [UserID]
              ,[UserName]
              ,[Email]
              ,[PasswordHash]
              ,[AuthProvider]
              ,[ProviderID]
              ,[ProfilePictureUrl]
              ,[CreationDate]
              ,[LastAccess]
          FROM [DigitaliaVotes].[dbo].[Users]
          WHERE [UserName] = @UserName AND [PasswordHash] = @Password
    END
    ELSE
    BEGIN
        -- Si @ProviderID no es vacío, selecciona con ProviderID
        SELECT [UserID]
              ,[UserName]
              ,[Email]
              ,[PasswordHash]
              ,[AuthProvider]
              ,[ProviderID]
              ,[ProfilePictureUrl]
              ,[CreationDate]
              ,[LastAccess]
          FROM [DigitaliaVotes].[dbo].[Users]
          WHERE [ProviderID] = @ProviderID
    END
END
GO

USE [DigitaliaVotes]
GO
/****** Object:  StoredProcedure [dbo].[spValidateVote]    Script Date: 17/12/2023 00:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spValidateVote]
    @surveyID INT,
    @userID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @optionID INT;

    SELECT @optionID = OptionID FROM Votes
    WHERE SurveyID = @surveyID AND UserID = @userID;

    IF (@optionID IS NULL)
        SET @optionID = 0;

    SELECT @optionID AS OptionID;
END;
GO

	## Configuración del Entorno de Desarrollo
- Instale el .NET 6 SDK desde [Sitio Oficial de Microsoft](https://dotnet.microsoft.com/download/dotnet/6.0).
- Clone el repositorio del proyecto: `git clone https://github.com/WalterAtuncar/DigitaliaFullstackChallenge.git

	## Configuración del Proyecto
- Configure la cadena de conexión en el archivo `appsettings.json`.
- Instale las dependencias necesarias ejecutando `dotnet restore`.

	## Ejecución del Proyecto
	##BACKEND
- Ingresar a la ruta "DigitaliaFullstackChallenge\DigitaliaBackendProject\ApiWeb\Digitalia.Fullstack.Challenge"
- Ejecutar Digitalia.Fullstack.Challenge.sln 
- Compile el proyecto.
- Abrir el Explorador de Pruebas.
- Ejecutar todas las pruebas.
- Ejecute el proyecto Digitalia.Fullstack.Challenge.

  	##FRONTEND
- Ingresar a la ruta "DigitaliaFullstackChallenge\DigitaliaFrontendProject\digitalia-votes"
- Ejecutar git bash el comando para instalar dependencias "npm install"
- Ejecutar el comando para levantar el proyecto "ng serve -o"
- La aplicacion debe correr en la ruta localhost:4200
- Revisar el archivo environment.tsla ruta api debe ser la misma ruta del api web
