using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace fluffypixi.Models
{
    public class Repository
    {
        static string connectionString1 = @"Server=37.140.192.100;Database=u1292067_fluffypixi;Uid=u1292067_LORD;Pwd=$Hn0l8y7;";
        static string connectionString =
           @"Server=37.140.192.100;
           Initial Catalog=u1292067_fluffypixi;
           Integrated Security=false;
           User Id=u1292067_LORD;
           Password=d2_03uAh;";//37.140.192.100
        public class Profile
        {
            public string Password { get; set; }
            public string Email { get; set; }
            public string FIO { get; set; }
            public string TEL { get; set; }
            public string Adress { get; set; }
            public Byte[] Avatar { get; set; }
            public Profile(string password, string email,int mode)
          {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                switch (mode)
                {
                case 0:
                    db.Execute($"INSERT INTO users VALUES ('{email}','{password}');");
                    Password = password;
                    Email = email;
                break;
                case 1:
                            
                    if (db.Query($"SELECT* FROM users WHERE Email = '{email}' and Password = '{password}'").Count() > 0)
                    {
                        List<users> profile = db.Query<users>($"SELECT * FROM users WHERE Email='{email}' and Password='{password}'").ToList();
                               
                        Password = profile[0].Email;
                        Email = profile[0].Password;
                    }
                    else
                    {
                        Password = null;
                        Email = null;
                    }

                break;
                }
            }
          }
            public static bool AddPostImage(Byte[] uploadedFile, string whosend)
            {
                List<images> images = new List<images>(); 
                int id= 0;
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    images = db.Query<images>($"SELECT * FROM images").ToList();
                }
                foreach(images img in images)
                {
                    id = img.id + 1;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO images (id, data, whosend) VALUES (@id, @data, @whosend)";
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("data", uploadedFile);
                cmd.Parameters.AddWithValue("whosend", whosend);
                cmd.Connection = new SqlConnection(connectionString);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            public static bool UpdateInfo(string Email, string FIO, string TEL, string Adress)
            {
                int i = 0;
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                   i =  db.Execute($"UPDATE users SET FIO = '{FIO}', TEL = '{TEL}', Adress = '{Adress}' WHERE Email = '{Email}'");
                }
                if (i != 0)
                    return true;
                else return false;
            }
        }
        public class FAQ_Model
        {
        public string Email { get; set; }
        public int Id_FAQ { get; set; }
        public string  Message { get; set; }
        public int BranchID { get; set; }
        public int BranchPosition { get; set; }
        public string Time { get; set; }
        }
        static  public void createFAQ(string email,string message,string time,int mode,int branchID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                switch (mode)
                {
                    case 0:
                        List<FAQ_Model> list = db.Query<FAQ_Model>($"select * from FAQ;").ToList();
                        int id = list.Count();
                        foreach (FAQ_Model faq in list)
                        {
                            if (faq.BranchID > branchID) branchID = faq.BranchID;
                        }
                        db.Execute($"INSERT INTO FAQ VALUES ('{email}',{id + 1},'{message}',{branchID+1},{1},'{time}');");
                        break;
                    case 1:
                        id = db.Query<FAQ_Model>($"select * from FAQ;").Count();
                        int branchPosition = db.Query<FAQ_Model>($"select * from FAQ WHERE BranchID={branchID};").Count();
                        db.Execute($"INSERT INTO FAQ VALUES ('{email}',{id+1},'{message}',{branchID},{branchPosition+1},'{time}');");
                    break;

                }
            }
        }
        public static List<FAQ_Model> getMyFAQsBranchs(string email)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FAQ_Model>($"Select * from FAQ where Email={email} and BranchPosition=1;").ToList();
            }
        }
        static public List<FAQ_Model> getFAQs()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FAQ_Model>($"Select * from FAQ where BranchPosition=1;").ToList();
            }
        }
        static public List<FAQ_Model> getFAQ(int branchID)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<FAQ_Model>($"Select * from FAQ where BranchID={branchID};").ToList();
            }
        }
        public class users
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class Invites
        {
            public int ID_invate { get; set; }
            public string Code_invite { get; set; }
        }
        public class images
        {
            public int id { get; set; }
            public byte[] data { get; set; }
            public string whosend { get; set; }
        }
        public class ModAnimal
        {
        public System.Int32 ID { get; set; }
        public System.String Name { get; set; }
        public System.String Date { get; set; }
        public System.String Type { get; set; }
        public System.String Gender { get; set; }
        public System.String Klichka { get; set; }
        public System.String Email { get; set; }
        }
        public class Animals
        {
            public System.Int32 ID { get; set; }
            public System.String Name { get; set; }
            public System.String Date { get; set; }
            public System.String Type { get; set; }
            public System.String Gender { get; set; }
            public System.String Klichka { get; set; }
            public System.String Email { get; set; }
            public Animals(string name, string date, string type, string gender, string klichka,string email)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    ID =  db.Query<List<ModAnimal>>($"Select * from animals;").Count();
                    Name = name;
                    Date = date;
                    Type = type;
                    Gender = gender;
                    Klichka = klichka;
                    Email = email;
                    db.Execute($"INSERT INTO animals VALUES ('{ID + 1}','{Name}','{Date}','{Type}','{Gender}','{Klichka}','{email}');");
                }
            }
            public Animals(int id)
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                ModAnimal a = db.Query<ModAnimal>($"Select * from animals WHERE id={id};").First();
                ID = a.ID;
                Name = a.Name;
                Date = a.Date;
                Type = a.Type;
                Gender = a.Gender;
                Klichka = a.Klichka;
                Email = a.Email;
                }
            }
            static  public List<ModAnimal> GetAnimals(string email)
            {
                List<ModAnimal> anims;
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    anims = db.Query<ModAnimal>($"Select * from animals WHERE Email={email};").ToList();
                }
                return anims;
            }
        }
        public static bool CheckInvite(string code)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
               if( db.Query($"SELECT * FROM invites WHERE Code_invite='{code}'").Count() > 0)
               {
                    return true;
               }
               else
               {
                    return false;
               }
            }
        }
        public static string GenerateInvite()
        {
            Random r = new Random();
            string invateCode = "";
            for(int i=0;i<6;i++)
            invateCode += (char)r.Next(42, 122);
            int id = 0;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
               List<Invites>invites = db.Query<Invites>($"SELECT * FROM invites").ToList();
                foreach (Invites i in invites)
                {
                    id = i.ID_invate + 1;
                }
            }
            
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute($"INSERT INTO invites VALUES ({id},'{invateCode}');");
                }
                return invateCode;
            }
            catch(Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
