# DBUtility.Core
DBUtility netcore version
# support db
MySql、PostgreSql、Oracle、SQLite、SqlServer
# how to use
```

internal const string EXISTS_DB = @"
    SELECT information_schema.SCHEMATA.SCHEMA_NAME 
    FROM information_schema.SCHEMATA 
    WHERE SCHEMA_NAME=@dbName;";
internal const string PARM_DB = "@dbName";
IDbDataParameter[] PrepareExistsDB(string dbName)
{
    var parms = DbParameterCache.GetCachedParameterSet(dbHelper.ConnectionString, EXISTS_DB);
    if (parms == null)
    {
        parms = new IDbDataParameter[]
        {
           dbHelper.CreateDbParameter(PARM_DB, DbType.AnsiString, ParameterDirection.Input)
        };
        DbParameterCache.CacheParameterSet(dbHelper.ConnectionString, EXISTS_DB, parms);
    }
    parms[0].Value = dbName;
    return parms;
}
bool ExistDb(string dbName)
{
    var parms = PrepareExistsDB(dbName);
    using (var dr = dbHelper.ExecuteReader(System.Data.CommandType.Text, EXISTS_DB, parms))
    {
        return dr.Read();
    }
}
void Read()
{
  var dbHelper = DbHelper.Create("MySql");
  dbHelper.ConnectionString="Server=127.0.0.1;database=siger;charset=utf8;uid=root;password=abcd1";
  var isExists=ExistDb("userdb");
}
```

