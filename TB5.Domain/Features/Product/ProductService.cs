using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TB5.WebApi.Database.AppDbContextModels;

namespace TB5.Domain.Features.Product;

public class ProductService
{
    public List<TblProduct> GetProducts()
    {
        AppDbContext db = new AppDbContext();
        List<TblProduct> lst = db.TblProducts.Where(x => !x.IsDelete).ToList();
        return lst;
    }
}
