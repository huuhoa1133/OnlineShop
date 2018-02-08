using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext db = null;
        public UserDAO()
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public User Login(string username, string password)
        {
            var result = db.User.SingleOrDefault(x => x.UserName == username && x.Password == password);
            return result;
        }
        public IEnumerable<User> GetAll(int page = 1, int pagesize = 10)
        {
            return db.User.OrderBy(x=>x.CreateDate).ToPagedList(page, pagesize);
        }

        public bool Update(User user)
        {
            var detail = db.User.SingleOrDefault(x => x.ID == user.ID);
            if (detail == null)
                return false;
            detail.Name = user.Name;
            detail.Address = user.Address;
            detail.ModifiedBy = user.ModifiedBy;
            detail.ModifiedDate = DateTime.Now;
            db.SaveChanges();
            return true;
        }

        public User FindOne(long ID)
        {
            return db.User.Find(ID);
        }

        public bool DeleteOne(long ID)
        {
            var user = db.User.Find(ID);
            db.User.Remove(user);
            db.SaveChanges();
            return true;
        }
    }
}
