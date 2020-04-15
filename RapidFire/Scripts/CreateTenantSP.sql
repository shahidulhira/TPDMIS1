USE [MTClient]
GO

/****** Object:  StoredProcedure [dbo].[CreateTenantSP]    Script Date: 8/27/2019 4:30:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- EXEC CreateTenantSP 'belayet','dbo'
-- =============================================
CREATE PROCEDURE [dbo].[CreateTenantSP]
	@toSchema nvarchar(max),
	@fromSchema nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE  @object_name nvarchar(max),@object_defination nvarchar(max)


DECLARE curSp CURSOR FAST_FORWARD READ_ONLY LOCAL FOR
    SELECT ROUTINE_NAME,ROUTINE_DEFINITION  FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'  AND SPECIFIC_SCHEMA = @fromSchema AND ROUTINE_NAME NOT IN ( 'CreateTenantWithTable','CreateTenantSP') AND ROUTINE_NAME NOT LIKE 'sp_%'
OPEN curSp

FETCH NEXT FROM curSp INTO @object_name, @object_defination

WHILE @@FETCH_STATUS = 0 BEGIN

    DECLARE @SQL NVARCHAR(MAX) = ''
	DECLARE @DROPSQL NVARCHAR(MAX) = ''

	SELECT @DROPSQL = 'IF EXISTS ('+CHAR(13)+'      SELECT * FROM sys.objects 
	    WHERE object_id = OBJECT_ID(N'''+@toSchema+'.'+@object_name+''') AND type in (N''P'')'+CHAR(13)+'      )'
	+CHAR(13)+'       DROP PROCEDURE '+@toSchema+'.'+@object_name + CHAR(13)


SELECT @SQL = REPLACE(REPLACE(@object_defination,@object_name,@object_name),@fromSchema,@toSchema)+CHAR(13)+''

	PRINT @DROPSQL
	PRINT @SQL
	
	EXEC (@DROPSQL)
	EXEC (@SQL)
	SET @SQL = ''
	SET @DROPSQL= ''
FETCH NEXT FROM curSp INTO @object_name, @object_defination
END

CLOSE curSp
DEALLOCATE curSp
END


GO


