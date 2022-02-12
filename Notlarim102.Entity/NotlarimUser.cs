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
    [Table("tblNotlarimUsers")]
   public class NotlarimUser : MyEntityBase
    {
        [DisplayName("İsim"), StringLength(30,ErrorMessage ="{0} max. {1} karakter  olmalı")]
        public string Name { get; set; }
        [DisplayName("Soyad"), StringLength(30, ErrorMessage = "{0} max. {1} karakter  olmalı")]
        public string Surname { get; set; }
        [DisplayName("Kullanıcı Adı"), StringLength(30, ErrorMessage = "{0} max. {1} karakter  olmalı"),Required(ErrorMessage ="{0} alanı gereklidir")]
        public string UserName { get; set; }
        [DisplayName("E-posta"), StringLength(100, ErrorMessage = "{0} max. {1} 30 karakter  olmalı"),Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Email { get; set; }
        [DisplayName("Parola"), StringLength(100, ErrorMessage = "{0} max. {1} 30 karakter  olmalı"),Required(ErrorMessage = "{0} alanı gereklidir")]
        public string Password { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFile { get; set; }
        public bool IsActive { get; set; }
        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<Note> Notes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
    }
}
