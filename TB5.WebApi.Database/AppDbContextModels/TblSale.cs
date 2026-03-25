using System;
using System.Collections.Generic;

namespace TB5.WebApi.Database.AppDbContextModels;

public partial class TblSale
{
    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime SaleDate { get; set; }
}
