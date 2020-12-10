using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASPNETCore5Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCore5Demo
{
    public abstract class UscEntityModelBase
    {

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
