USE [SponsorShipMIS_demo]
GO

/****** Object:  Table [dbo].[AuditTrail]    Script Date: 8/21/2019 10:33:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AuditTrail](
	[RecordId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[ModelName] [nvarchar](250) NULL,
	[OperationType] [nvarchar](50) NULL,
	[AuditDate] [datetime] NULL,
 CONSTRAINT [PK_AuditTrail] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


