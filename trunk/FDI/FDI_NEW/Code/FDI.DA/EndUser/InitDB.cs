using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FDI.Base;


namespace FDI.DA.EndUser
{
    public class InitDB
    {
        #region fields
        public static int TongSoBanGhiSauKhiQuery { get; set; }
        [ThreadStatic]
        private static FDIEntities _instance;
        private static readonly object lockDB = new object();

        #endregion

        #region properties
        public FDIEntities Instance
        {
            get
            {
                lock (lockDB)
                {
                    return _instance ?? (_instance = new FDIEntities());
                }
            }
        }

        #endregion
    }
}
