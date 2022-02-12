using Notlarim102.DataAccessLayer;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.BusinessLayer
{
    public class Test
    {
        Repository<NotlarimUser> ruser = new Repository<NotlarimUser>();
        Repository<Category> rcat = new Repository<Category>();
        Repository<Note> rnote = new Repository<Note>();
        Repository<Comment> rcom = new Repository<Comment>();
        Repository<Liked> rliked= new Repository<Liked>();
        public Test()
        {
            //NotlarimContext db = new NotlarimContext();
            //db.Categories.ToList();
            // db.Database.CreateIfNotExists() ---Boş database oluşturmak için
            var test = rcat.List();
            var test1 = rcat.List(x=> x.Id>5);
            
        }
        public void InsertTest()
        {
            int result = ruser.Insert(new NotlarimUser()
            {
                Name = "Abuzer",
                Surname="Kömürcü",
                Email="bakisine@gmail.com",
                ActivateGuid=Guid.NewGuid(),
                IsActive=true,
                IsAdmin=false,
                UserName="abuzer",
                Password="123",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="abuzer"
                
            }
            ) ;
        }
        public void UpdateTest()
        {
            NotlarimUser user = ruser.Find(x => x.UserName == "abuzer");
            if (user!=null)
            {
                user.Password = "1313";
                ruser.Update(user);
            }
        }
        public void DeleteTest()
        {
            NotlarimUser user = ruser.Find(x => x.UserName == "abuzer");
            ruser.Delete(user); 
        }
        public void CommentTest()
        {
            NotlarimUser user = ruser.Find(s => s.Id == 2);
            Note note = rnote.Find(s => s.Id == 3);
            Comment comment = new Comment()
            {
                Text = "Bu bir test Yazisidir",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUserName = "enessenoglu",
                Note = note,
                Owner = user
            };
            rcom.Insert(comment);
        }
    }
}
