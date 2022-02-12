using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102.WepApp.ViewModel
{
    public class InfoViewModel:NotifyViewModelBase<string>
    {
        public InfoViewModel()
        {
            Title = "Bilgilendirme.";
        }
    }
}