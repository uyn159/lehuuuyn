using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
        public IEnumerable<ProductCategory> ListAllPaging(string searchString, int page, int pageSize)// trả về danh sách bao gồm tất cả user
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));// tìm theo tên và cả user name
            }

            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize); // sắp xếp theo thứ tự tăng hay giảm dần theo ngày thêm sản phẩm
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public long Insert(ProductCategory productcategory)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(productcategory.MetaTitle))
            {
                productcategory.MetaTitle = StringHelper.ToUnsignString(productcategory.Name);//chuyển thành a
            }
            productcategory.CreatedDate = DateTime.Now;
            db.ProductCategories.Add(productcategory);
            db.SaveChanges();
            //Xử lý tag
            return productcategory.ID;
        }
        public bool Delete(int id)
        {
            try
            {
                var productcategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productcategory);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool Edit(ProductCategory entity)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(entity.ID);
                productCategory.ParenID = entity.ParenID;
                productCategory.Name = entity.Name;
                productCategory.CreatedBy = entity.CreatedBy;
                productCategory.DisplayOrder = entity.DisplayOrder;
                productCategory.Status = entity.Status;
                productCategory.ModifiedBy = entity.ModifiedBy;
                productCategory.MetaTitle= StringHelper.ToUnsignString(entity.Name);
                productCategory.CreatedDate = DateTime.Now;
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
            var category = db.ProductCategories.Find(id);
            category.Status = !category.Status;
            db.SaveChanges();
            return category.Status;
        }
    }
}
