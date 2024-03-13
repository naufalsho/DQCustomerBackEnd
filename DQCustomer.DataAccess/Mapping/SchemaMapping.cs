using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OMS.DataAccess.Mapping
{
    public class SchemaMapping<T> : AutoClassMapper<T> where T : class
    {
        public override void Table(string tableName)
        {
            string[] arrSchema = { "sales","history","file","guest", "cp"};
            string[] schemaClass = tableName.SplitCamelCase();          
            string schema = schemaClass.First().ToLower();
            if(!string.IsNullOrEmpty(Array.Find(arrSchema, c=>c.Equals(schema))))
            {
                tableName = string.Join("", schemaClass.Skip(1));
                SchemaName = schema;
                TableName = tableName;
            }
           
            base.Table(tableName);
        }
        
    }
}
