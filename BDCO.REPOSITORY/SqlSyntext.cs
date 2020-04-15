using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Repository
{
  public static  class SqlSyntext
	{
		public static string CheckTables()
		{
			return @"IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppPermission]') AND type in (N'U'))
					BEGIN
					CREATE TABLE [dbo].[AppPermission](
						[Id] [int] IDENTITY(1,1) NOT NULL,
						[UserId] [int] NOT NULL,
						[GroupId] [int] NOT NULL,
						[AppResourceId] [int] NOT NULL,
						[ExecuteInsert] [bit] NOT NULL,
						[ExecuteRead] [bit] NOT NULL,
						[ExecuteUpdate] [bit] NOT NULL,
						[ExecuteDelete] [bit] NOT NULL,
					 CONSTRAINT [PK_AppPermission] PRIMARY KEY CLUSTERED 
					([Id] ASC)
						WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY]
					END
					
					IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppResource]') AND type in (N'U'))
					BEGIN
					CREATE TABLE [dbo].[AppResource](
						[Id] [int] IDENTITY(1,1) NOT NULL,
						[Name] [varchar](100) NOT NULL,
						[RelativePath] [varchar](max) NULL,
						[Active] [bit] NULL,
						[DeployedPath] [varchar](max) NULL,
					 CONSTRAINT [PK_AppResource] PRIMARY KEY CLUSTERED 
					(
						[Id] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
					) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
					END
					
					IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppEventStore]') AND type in (N'U'))
					BEGIN
 
						CREATE TABLE AppEventStore
						(
							AuditLogID INT IDENTITY PRIMARY KEY,
							ReffrenceNo NVARCHAR(200),
							AggregateName NVARCHAR(200),
							OperationType NVARCHAR(50),
							LogDateTime DATETIME,
							UserID NVARCHAR(128)

						)
					END

					

					";
		}
	}
}
