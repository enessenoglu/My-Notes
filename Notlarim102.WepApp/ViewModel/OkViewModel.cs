using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102.WepApp.ViewModel
{
    public class OkViewModel:NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Islem Basarılı";
        }
    }
}