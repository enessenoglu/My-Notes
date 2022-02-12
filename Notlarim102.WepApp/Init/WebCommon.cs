using Notlarim102.Common;
using Notlarim102.Entity;
using Notlarim102.WepApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102.WepApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            if (CurrentSession.User!=null)
            {
                NotlarimUser user = CurrentSession.User as NotlarimUser;
                return user.UserName;
            }
            return "system1";
        }
    }
}