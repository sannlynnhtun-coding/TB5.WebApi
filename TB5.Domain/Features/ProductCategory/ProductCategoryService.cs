using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB5.Domain.Features.ProductCategory.Models;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.Domain.Features.ProductCategory;

public class ProductCategoryService
{
    public List<TblProductCategory> GetProductCategories()
    {
        AppDbContext db = new AppDbContext();
        return db.TblProductCategories.ToList();
    }

    public TblProductCategory? GetProductCategory(int id)
    {
        AppDbContext db = new AppDbContext();
        return db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
    }

    public ProductCategoryResponse CreateProductCategory(ProductCategoryCreateRequest request)
    {
        AppDbContext db = new AppDbContext();
        TblProductCategory category = new TblProductCategory
        {
            ProductCategoryName = request.ProductCategoryName
        };
        db.TblProductCategories.Add(category);
        int result = db.SaveChanges();
        bool isSuccess = result > 0;
        return new ProductCategoryResponse
        {
            IsSuccess = isSuccess,
            Message = isSuccess ? "Product Category created successfully." : "Failed to create product category."
        };
    }

    public ProductCategoryResponse UpdateProductCategory(int id, ProductCategoryUpdateRequest request)
    {
        AppDbContext db = new AppDbContext();
        TblProductCategory? category = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
        if (category == null)
        {
            return new ProductCategoryResponse { IsSuccess = false, Message = "Product Category not found." };
        }

        category.ProductCategoryName = request.ProductCategoryName;
        db.TblProductCategories.Update(category);
        int result = db.SaveChanges();
        bool isSuccess = result > 0;
        return new ProductCategoryResponse
        {
            IsSuccess = isSuccess,
            Message = isSuccess ? "Product Category updated successfully." : "Failed to update product category."
        };
    }

    public ProductCategoryResponse DeleteProductCategory(int id)
    {
        AppDbContext db = new AppDbContext();
        TblProductCategory? category = db.TblProductCategories.FirstOrDefault(x => x.ProductCategoryId == id);
        if (category == null)
        {
            return new ProductCategoryResponse { IsSuccess = false, Message = "Product Category not found." };
        }

        db.TblProductCategories.Remove(category);
        int result = db.SaveChanges();
        bool isSuccess = result > 0;
        return new ProductCategoryResponse
        {
            IsSuccess = isSuccess,
            Message = isSuccess ? "Product Category deleted successfully." : "Failed to delete product category."
        };
    }
}
