/*
EXEC SpGetDropDownListValues @Tablename ='tblTestProduct', @displayValue ='Name', @dataValue = 'ID', 
@WhereClause = 'ID>1', @extraColumn = 'Description', @OrderByClause = 'Name DESC'

tblTestProduct^ID>1^Name^ID^Description^

*/
ALTER PROCEDURE SpGetKeyValuePair          
(          
	@Tablename varchar(1000),          
	@WhereClause varchar(1000) = null,          
	@displayValue varchar(50) = NULL,          
	@dataValue varchar(50)  = NULL,          
	@extraColumn varchar(500) = null,   
	@OrderByClause varchar(500) = null 
)          
AS          
BEGIN
	SET NOCOUNT ON         
	DECLARE @SQL NVARCHAR(1000)          
	DECLARE @SQL1 NVARCHAR(1000)  
	DECLARE @KeyField INT
	DECLARE @ValueField VARCHAR(100) 
	SET @SQL = 'SELECT ' + @displayValue + ', ' + @dataValue + ISNULL(','+@extraColumn,'') + ' FROM ' +  @Tablename 
	
	IF @WhereClause <> ''          
	BEGIN          
		SET @SQL = @SQL +' WHERE ' + @WhereClause +''           
	END 
	
	IF ISNULL(@OrderByClause,'') <> ''  
	BEGIN  
		SET @SQL = @SQL + ' ORDER BY ' + @OrderByClause  
	END 

	PRINT @SQL          
	EXEC (@SQL)          
	SET NOCOUNT OFF          
END 