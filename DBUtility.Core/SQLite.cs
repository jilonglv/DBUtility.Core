
using System.Data;
using System.Data.Common;
#if NET_FULL
using System.Data.SQLite;
#else
using Microsoft.Data.Sqlite;
#endif

namespace DBUtility
{
    public class SQLite : DbHelper
    {
        public override IDbConnection CreateConnection()
        {
#if NET_FULL
            return new SQLiteConnection(ConnectionString);
#else
            return new SqliteConnection(ConnectionString);
#endif 
        }

        public override IDbCommand CreateCommand()
        {
#if NET_FULL
            return new SQLiteCommand();
#else
            return new SqliteCommand();
#endif 
        }

        public override DbDataAdapter CreateDataAdapter(IDbCommand selectCommand,
            IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand)
        {
#if NET_FULL
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            if (selectCommand != null)
                adapter.SelectCommand = selectCommand as SQLiteCommand;
            if (insertCommand != null)
                adapter.InsertCommand = insertCommand as SQLiteCommand;
            if (deleteCommand != null)
                adapter.DeleteCommand = deleteCommand as SQLiteCommand;
            if (updateCommand != null)
                adapter.UpdateCommand = updateCommand as SQLiteCommand;
#else
            DbDataAdapter adapter = Microsoft.Data.Sqlite.SqliteFactory.Instance.CreateDataAdapter();// new SqliteDataAdapter();
            if (selectCommand != null)
                adapter.SelectCommand = selectCommand as SqliteCommand;
            if (insertCommand != null)
                adapter.InsertCommand = insertCommand as SqliteCommand;
            if (deleteCommand != null)
                adapter.DeleteCommand = deleteCommand as SqliteCommand;
            if (updateCommand != null)
                adapter.UpdateCommand = updateCommand as SqliteCommand;
#endif 

            return adapter;
        }

        public override IDbDataParameter CreateDbParameter(string parameterName)
        {
#if NET_FULL
            IDbDataParameter parameter = new SQLiteParameter();
#else
            IDbDataParameter parameter = new SqliteParameterNetCore();
#endif 
            parameter.ParameterName = parameterName.Replace("?", "@").Replace(":", "@");
            return parameter;
        }

#if !NET_FULL
        class SqliteParameterNetCore : SqliteParameter, System.ICloneable
        {
            //
            // 摘要:
            //     Initializes a new instance of the Microsoft.Data.Sqlite.SqliteParameter class.
            public SqliteParameterNetCore() :base(){ }
            //
            // 摘要:
            //     Initializes a new instance of the Microsoft.Data.Sqlite.SqliteParameter class.
            //
            // 参数:
            //   name:
            //     The name of the parameter.
            //
            //   value:
            //     The value of the parameter. Can be null.
            public SqliteParameterNetCore(string name, object value) :base(name,value){ }
            //
            // 摘要:
            //     Initializes a new instance of the Microsoft.Data.Sqlite.SqliteParameter class.
            //
            // 参数:
            //   name:
            //     The name of the parameter.
            //
            //   type:
            //     The type of the parameter.
            public SqliteParameterNetCore(string name, SqliteType type):base(name,type)
            {
            }
            //
            // 摘要:
            //     Initializes a new instance of the Microsoft.Data.Sqlite.SqliteParameter class.
            //
            // 参数:
            //   name:
            //     The name of the parameter.
            //
            //   type:
            //     The type of the parameter.
            //
            //   size:
            //     The maximum size, in bytes, of the parameter.
            public SqliteParameterNetCore(string name, SqliteType type, int size) : base(name, type, size)
            {
            }
            //
            // 摘要:
            //     Initializes a new instance of the Microsoft.Data.Sqlite.SqliteParameter class.
            //
            // 参数:
            //   name:
            //     The name of the parameter.
            //
            //   type:
            //     The type of the parameter.
            //
            //   size:
            //     The maximum size, in bytes, of the parameter.
            //
            //   sourceColumn:
            //     The source column used for loading the value. Can be null.
            public SqliteParameterNetCore(string name, SqliteType type, int size, string sourceColumn) : base(name, type, size, sourceColumn)
            {
            }
            public object Clone()
            {
                SqliteParameterNetCore destination = new SqliteParameterNetCore();
                // NOTE: _parent is not cloned
                destination.Value = Value;
                destination.Direction = Direction;
                destination.Size = Size;
                //destination. = _offset;
                destination.DbType = DbType;
                destination.SqliteType = SqliteType;
                destination.SourceColumn = SourceColumn;
                destination.SourceVersion = SourceVersion;
                //destination. = _metaType;
                //destination.coll = _collation;
                //destination.ty = _udtTypeName;
                //destination.tyna = _typeName;
                //destination.load = _udtLoadError;
                destination.ParameterName = ParameterName;
                destination.Precision = Precision;
                destination.Scale = Scale;
                destination.SourceColumnNullMapping = SourceColumnNullMapping;
                destination.IsNullable = IsNullable;
                //destination.sql = _sqlBufferReturnValue;
                //destination.inn = _internalMetaType;
                //destination.co = CoercedValue; // copy cached value reference because of XmlReader problem
                //destination.as = _valueAsINullable;

                //SqliteParameter setFlags =
                    //SqliteParameter.IsSqlParameterSqlType |
                    //SqliteParameter.IsNull |
                    //SqliteParameter.IsNullable |
                    //SqliteParameter.CoercedValueIsDataFeed |
                    //SqliteParameter.CoercedValueIsSqlType |
                    //SqliteParameter.SourceColumnNullMapping;
                //destination._flags = (destination._flags & ~setFlags) | (_flags & setFlags);
                //destination._actualSize = _actualSize;
                //if (_xmlSchemaCollection != null)
                //{
                //    destination._xmlSchemaCollection = new SqlMetaDataXmlSchemaCollection();
                //    destination._xmlSchemaCollection.CopyFrom(_xmlSchemaCollection);
                //}
                return destination;
            }
        }
#endif
    }
}
