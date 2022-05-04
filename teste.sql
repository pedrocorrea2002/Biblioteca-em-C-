USE [master]
GO
/****** Object:  Database [ControleEmprestimoLivro]    Script Date: 22/04/2022 18:04:25 ******/
CREATE DATABASE [ControleEmprestimoLivro]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ControleEmprestimoLivro', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ControleEmprestimoLivro.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ControleEmprestimoLivro_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ControleEmprestimoLivro_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ControleEmprestimoLivro] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ControleEmprestimoLivro].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ARITHABORT OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET  MULTI_USER 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ControleEmprestimoLivro] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ControleEmprestimoLivro] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ControleEmprestimoLivro] SET QUERY_STORE = OFF
GO
USE [ControleEmprestimoLivro]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 22/04/2022 18:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] NOT NULL IDENTITY,
	[CPF] [varchar](14) NOT NULL,
	[nome] [varchar](100) NOT NULL,
	[endereco] [varchar](50) NOT NULL,
	[cidade] [varchar](50) NOT NULL,
	[bairro] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro]    Script Date: 22/04/2022 18:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro](
	[id] [int] NOT NULL IDENTITY,
	[nome] [varchar](50) NOT NULL,
	[autor] [varchar](100) NOT NULL,
	[editora] [varchar](50) NOT NULL,
	[Emprestado] [bit] NOT NULL,
 CONSTRAINT [PK_Livro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Livro_Cliente_Emprestimo]    Script Date: 22/04/2022 18:04:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Livro_Cliente_Emprestimo](
	[id] [int] NOT NULL IDENTITY,
	[idLivro] [int] NOT NULL,
	[idCliente] [int] NOT NULL,
	[dataEmprestimo] [datetime] NOT NULL,
	[dataDevolucao] [datetime],
 CONSTRAINT [PK_Livro_Cliente_Emprestimo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Livro_Cliente_Emprestimo]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Cliente_Emprestimo_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Livro_Cliente_Emprestimo] CHECK CONSTRAINT [FK_Livro_Cliente_Emprestimo_Cliente]
GO
ALTER TABLE [dbo].[Livro_Cliente_Emprestimo]  WITH CHECK ADD  CONSTRAINT [FK_Livro_Cliente_Emprestimo_Livro] FOREIGN KEY([idLivro])
REFERENCES [dbo].[Livro] ([id])
GO
ALTER TABLE [dbo].[Livro_Cliente_Emprestimo] CHECK CONSTRAINT [FK_Livro_Cliente_Emprestimo_Livro]
GO
USE [master]
GO
ALTER DATABASE [ControleEmprestimoLivro] SET  READ_WRITE 
GO
