using Common;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class SlideDao
    {
        OnlineShopDbContext db = null;
        public SlideDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Slide> ListAll()
        {
            return db.Slides.OrderBy(y => y.DisplayOther).ToList();
        }
        public Slide ViewDetail(long id)
        {
            return db.Slides.Find(id);
        }
        public long Insert(Slide Slide)
        {
            //Xử lý alias
            Slide.CreatedDate = DateTime.Now;
            db.Slides.Add(Slide);
            db.SaveChanges();
            //Xử lý tag
            return Slide.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var Slide = db.Slides.Find(id);
                db.Slides.Remove(Slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(Slide entity)
        {
            try
            {
                var Slide = db.Slides.Find(entity.ID);
                Slide.Image = entity.Image;
                Slide.CreatedBy = entity.CreatedBy;
                Slide.DisplayOther = entity.DisplayOther;
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
        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status= !slide.Status;
            db.SaveChanges();
            return slide.Status;
        }
    }
}
