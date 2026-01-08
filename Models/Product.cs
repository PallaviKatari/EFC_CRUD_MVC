using System;
using System.Collections.Generic;

namespace WebApp_DFA_EFC.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }
}
