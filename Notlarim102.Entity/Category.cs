using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.Entity
{
    [Table("tblCategories")]
   public class Category :MyEntityBase
    {
        [StringLength(60),Required,DisplayName("Başlık")]
        public string Title { get; set; }
        [StringLength(150), DisplayName("Açıklama")]
        public string Description { get; set; }
        [DisplayName("Not")]
        public virtual List<Note> Notes { get; set; }
        public Category()
        {
            Notes = new List<Note>();
        }
       
    }
}
