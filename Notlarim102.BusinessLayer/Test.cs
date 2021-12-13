using Notlarim102.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class Test
    {
        public Test()
        {
            NotlarimContext db = new NotlarimContext();
            db.Categories.ToList();
            // db.Database.CreateIfNotExists() ---Boş database oluşturmak için
        }

    }
}
