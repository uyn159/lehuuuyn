using Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public Category ViewDetail(long id)
        {
            return db.Categories.Find(id);
        }
        public long Insert(Category category)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(category.MetaTitle))
            {
                category.MetaTitle = StringHelper.ToUnsignString(category.Name);//chuyển thành a
            }
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            //Xử lý tag
            return category.ID;
        }
        public bool Edit(Category entity)
        {
            try
            {
                var Slide = db.Categories.Find(entity.ID);
                Slide.DisplayOrder = entity.DisplayOrder;
                Slide.MetaTitle = StringHelper.ToUnsignString(entity.Name);
                Slide.CreatedBy = entity.CreatedBy;
                Slide.Status = entity.Status;
                Slide.CreatedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool ChangeStatus(long id)
        {
            var category = db.Categories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }
}
