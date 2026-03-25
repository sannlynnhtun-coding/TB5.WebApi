using System;
using System.Collections.Generic;

namespace TB5.WebApi.Database.AppDbContextModels;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryName { get; set; } = null!;
}
