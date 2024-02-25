using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using TestApp.Repo.DataStores;
using TestApp.Repo.Model;
using TestApp.Repo.Repositories;
using TestApp.Services.Contracts.Business;
using TestApp.Services.Contracts.Common;
using TestApp.Services.Impl.Business;
using TestApp.Services.Impl.Common;
using TestApp.Services.Impl.Model;
using CartRule = TestApp.Services.Impl.Model.CartRule;
using Item = TestApp.Services.Impl.Model.Item;

namespace TestApp.Test.Services
{
    [TestFixture]
    public class CheckoutService_DiscountTest
    {
        private ItemService _itemService;
        private CheckoutService _checkoutService;
        private CartService _cartService;
        private string _userId = "CheckoutService_DiscountTest";

        [SetUp]
        public void Setup()
        {
            var binFolder = AppDomain.CurrentDomain.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(binFolder, @"..\..\.."));

            var basePath = projectRoot + "\\Data";
            var store = new JsonDataStore<Repo.Model.Item>(basePath, "items.json");

            var ruleStore = new JsonDataStore<Repo.Model.CartRule>(basePath, "rule.json");

            IMapperService _mapperService = new AutoMapperService();
            _mapperService.AddProfile((new RepoMapperProfile()));
            _itemService =  new ItemService( new ItemRepository(store, _mapperService));

            _cartService = new CartService(new MemoryCacheManager<Cart>());

            _checkoutService = new CheckoutService(_cartService, new RuleRepository(ruleStore, _mapperService));
        }

        [Test]
        public async Task CheckoutOneItem()
        {
            var item = await _itemService.Get('A');
            _cartService.Scan(item, _userId);

            Assert.That(item.Price, Is.EqualTo(await _checkoutService.PriceAsync(_userId)));
        }

        [Test]
        public async Task CheckoutZero()
        {
            Assert.That(0, Is.EqualTo(await _checkoutService.PriceAsync(_userId)));
        }

        [Test]
        public async Task CheckoutOne()
        {
            var item = await _itemService.Get('A');
            _cartService.Scan(item, _userId);

            Assert.That(item.Price, Is.EqualTo(await _checkoutService.PriceAsync(_userId)));
        }

        [TestCase("A", 50)]
        [TestCase("AB", 80)]
        [TestCase("CDBA", 115)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 135)]
        public async Task CheckoutMultiple(string items, double val)
        {
            var item = await _itemService.Get(items);


            _cartService.Scan(item, _userId);

            Assert.That(val, Is.EqualTo(await _checkoutService.PriceAsync(_userId)));
        }
    }

    
}
