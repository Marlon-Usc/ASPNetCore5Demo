using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ASPNETCore5Demo
{
    public abstract class UscEntityModelBase
    {
        //PropertyInfo[] pkProperties;

        public UscEntityModelBase()
        {
            //Ini_pkProperties();
        }

        //void Ini_pkProperties()
        //{
        //    //順序問題如何解決? => 用標記或覆寫回傳PK, 用標記會有該不該繼承的問題, 讓每個 model 自訂會比較活
        //    string[] pk = InnerGetPrimaryKey();
        //    if (pk == null)
        //        return;
        //    pkProperties = new PropertyInfo[pk.Length];
        //    Type myClass = this.GetType();
        //    for (int i=0; i< pk.Length; i++)
        //    {
        //        var p = myClass.GetProperty(pk[i]);
        //        pkProperties[i] = p ?? throw new NullReferenceException($"Primary Key property \"{pk[i]}\" not found!");
        //    }

        //    //SortedList<int, KeyValuePair<string, PropertyInfo>> pkList = new SortedList<int, KeyValuePair<string, PropertyInfo>>();
        //    //Type myClass = this.GetType();
        //    //foreach (var prop in GetType().GetProperties())
        //    //{
        //    //    var myProp = myClass.GetProperty(prop.Name);
        //    //    if (myProp != null)
        //    //    {
        //    //        foreach (UscPrimaryKeyAttribute pkAttr in myProp.GetCustomAttributes(typeof(UscPrimaryKeyAttribute), true))
        //    //        {
        //    //            pkList.Add(pkAttr.Order, new KeyValuePair<string, PropertyInfo>(myProp.Name, myProp));
        //    //        }
        //    //    }
        //    //}
        //    //keyProps = new Dictionary<string, PropertyInfo>(pkList.Values);
        //}

        //public string[] PrimaryKey() => InnerGetPrimaryKey();

        //protected string[] InnerGetPrimaryKey()
        //{
        //    var keyName = Db.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
        //   .Select(x => x.Name).Single();

        //    return (int)entity.GetType().GetProperty(keyName).GetValue(entity, null);

        //}

        //public void SetKeys(params object[] keyValues)
        //{
        //    for(int i=0; i< pkProperties.Length; i++)
        //        pkProperties[i].SetValue(this, keyValues[i]);
        //}

        //public object[] GetKeys()
        //{
        //    if (pkProperties == null)
        //        return null;
        //    object[] values = new object[pkProperties.Length];
        //    for (int i = 0; i < pkProperties.Length; i++)
        //        values[i] = pkProperties[i].GetValue(this);
        //    return values;
        //}

        public void UpdateFrom(object source)
        {
            Type myClass = this.GetType();
            foreach (var prop in source.GetType().GetProperties())
            {
                var myProp = myClass.GetProperty(prop.Name);
                if (myProp != null)
                {
                    object[] attrs = myProp.GetCustomAttributes(typeof(UscFieldAttribute), true);
                    if (attrs.Length == 0 || !((UscFieldAttribute)attrs[0]).IsReadOnly)
                        myProp.SetValue(this, prop.GetValue(source));
                }

            }

        }
    }



}