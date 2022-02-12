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
   public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, DisplayName("Oluşturulma Tarihi"),ScaffoldColumn(false)]
        public DateTime CreatedOn { get; set; }
        [Required, DisplayName("Düzenlenme Tarihi"), ScaffoldColumn(false)]
        public DateTime ModifiedOn { get; set; }
        [Required,StringLength(30), DisplayName("Kullanıcı Adı"), ScaffoldColumn(false)]
        public string ModifiedUserName { get; set; }
    }
}
