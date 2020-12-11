using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;



namespace ASPNETCore5Demo
{
    public static class UscDbContextHelper
    {
        public static void SetKeys<T>(DbContext db, T entity, params object[] keyValues)
        {
            Type tEntity = entity.GetType();
            int i = 0;            
            foreach (var keyName in db.Model.FindEntityType(tEntity).FindPrimaryKey().Properties.Select(x => x.Name))
            {
                tEntity.GetProperty(keyName).SetValue(entity, keyValues[i]);
                i++;
            }            
        }

        public static object[] GetKeys<T>(DbContext db, T entity)
        {
            Type tEntity = entity.GetType();
            Queue<object> values = new Queue<object>();
            foreach (var keyName in db.Model.FindEntityType(tEntity).FindPrimaryKey().Properties.Select(x => x.Name))
            {
                values.Enqueue(tEntity.GetProperty(keyName).GetValue(entity, null));
            }
            return values.ToArray();
        }

    }
}
