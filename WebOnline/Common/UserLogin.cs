using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace WebOnline
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string GroupID { set; get; }
        public string UserName { set; get; }
    }
}