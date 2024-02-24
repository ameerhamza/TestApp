﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services.Impl.Model;

namespace TestApp.Services.Contracts.Business
{
    public interface ICheckoutService
    {
        void Scan(Item item);
        Task<double> PriceAsync();
        void Scan(List<Item> item);
    }
}
