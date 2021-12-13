using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer
{
    public class MyInitializer:CreateDatabaseIfNotExists<NotlarimContext>
    {
        protected override void Seed(NotlarimContext context)
        {
            //Adding AdminUser...
            NotlarimUser admin = new NotlarimUser()
            {
                Name = "enes",
                Surname = "Şenoğlu",
                Email = "enessenoglu954@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin=true,
                UserName="enessenoglu",
                Password="senoglu55",
                CreatedOn=DateTime.Now,
                ModifiedOn=DateTime.Now,
                ModifiedUserName="enessenoglu"
            };
            //adding StandartUser...
            NotlarimUser standartuser = new NotlarimUser()
            {
                Name = "ahmet",
                Surname = "şenoğlu",
                Email = "ahmetsenoglu@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true,
                IsAdmin = false,
                UserName = "ahmetsenoglu",
                Password = "123456",
                CreatedOn = DateTime.Now.AddHours(1),
                ModifiedOn = DateTime.Now.AddMinutes(65),
                ModifiedUserName = "enessenoglu"
            };
            context.NotlarimUsers.Add(admin);
            context.NotlarimUsers.Add(standartuser);
            for (int i = 0; i < 8; i++)
            {
                NotlarimUser user = new NotlarimUser()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Email = FakeData.NetworkData.GetEmail(),
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = true,
                    IsAdmin = false,
                    UserName = $"user-{i}",
                    Password = "123456",
                    CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1),DateTime.Now),
                    ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                    ModifiedUserName = $"user-{i}"
                };
                context.NotlarimUsers.Add(user);
            }
            context.SaveChanges();
            //user list for usning...
            List<NotlarimUser> UserList = context.NotlarimUsers.ToList();
            //adding fake category
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title =FakeData.PlaceData.GetStreetName(),
                    Description=FakeData.PlaceData.GetAddress(),
                    CreatedOn=DateTime.Now,
                    ModifiedOn=DateTime.Now,
                    ModifiedUserName="enessenoglu"

                };
                context.Categories.Add(cat);
                //adding fake Notes...
                for (int k = 0; k < FakeData.NumberData.GetNumber(5,9); k++)
                {
                    NotlarimUser owner = UserList[FakeData.NumberData.GetNumber(0, UserList.Count - 1)];
                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(FakeData.NumberData.GetNumber(5, 25)),
                        Text = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 3)),
                        IsDraft = false,
                        likeCount = FakeData.NumberData.GetNumber(1, 9),
                        Owner = owner,
                        CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                        ModifiedUserName = owner.UserName
                    };
                    cat.Notes.Add(note);
                    //Adding fake comment
                    for (int j = 0; j < FakeData.NumberData.GetNumber(3,5); j++)
                    {
                        NotlarimUser comment_owner = UserList[FakeData.NumberData.GetNumber(0, UserList.Count - 1)];
                        Comment comment = new Comment()
                        {
                            Text=FakeData.TextData.GetSentence(),
                            CreatedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedOn = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-1), DateTime.Now),
                            ModifiedUserName =comment_owner.UserName,
                            Owner=comment_owner
                            
                        };
                        note.Comments.Add(comment);
                        //Adding fake likes
                        for (int m = 0; m < note.likeCount; m++)
                        {
                            Liked liked = new Liked()
                            {
                                LikedUser = UserList[m]
                            };
                            note.Likes.Add(liked);
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
