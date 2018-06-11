USE [master]
GO

/****** Object:  Database [ProductCatalog]    Script Date: 6/11/2018 2:28:45 PM ******/
CREATE DATABASE [ProductCatalog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductCatalog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProductCatalog.mdf' , SIZE = 10240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ProductCatalog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\ProductCatalog_log.ldf' , SIZE = 3840KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [ProductCatalog] SET COMPATIBILITY_LEVEL = 120
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductCatalog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ProductCatalog] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ProductCatalog] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ProductCatalog] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ProductCatalog] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ProductCatalog] SET ARITHABORT OFF 
GO

ALTER DATABASE [ProductCatalog] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ProductCatalog] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ProductCatalog] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ProductCatalog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ProductCatalog] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ProductCatalog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ProductCatalog] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ProductCatalog] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ProductCatalog] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ProductCatalog] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ProductCatalog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ProductCatalog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ProductCatalog] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ProductCatalog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ProductCatalog] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ProductCatalog] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ProductCatalog] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ProductCatalog] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [ProductCatalog] SET  MULTI_USER 
GO

ALTER DATABASE [ProductCatalog] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ProductCatalog] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ProductCatalog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ProductCatalog] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [ProductCatalog] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ProductCatalog] SET  READ_WRITE 
GO


