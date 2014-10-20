using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
namespace QLSV.Data.Utils
{
    public static class ThreadProcess
    {
        private delegate void SetPropertyThreadSafeDelegate<TControl, TTResult>
            (TControl @this, Expression<Func<TControl, TTResult>> property, TTResult value);

        public static void SetPropertyThreadSafe<TControl, TTResult>
            (this TControl @this, Expression<Func<TControl, TTResult>> property, TTResult value)
            where TControl : Control
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("'property' không có body");
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo == null
                || propertyInfo.ReflectedType == null
                //|| (propertyInfo.ReflectedType != null && !@this.GetType().IsSubclassOf(propertyInfo.ReflectedType))
                || @this.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("'property' không có property trong Control.");
            }

            if (@this.InvokeRequired)
            {
                @this.Invoke(new SetPropertyThreadSafeDelegate<TControl, TTResult>(SetPropertyThreadSafe),
                    new object[] { @this, property, value });
            }
            else
            {
                @this.GetType()
                    .InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, @this, new object[] { value });
            }
        }
    }
}
