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
    public class CheckoutService_MultiplesTest
    {
        private ItemService _itemService;
        private CheckoutService _checkoutService;
        private CartService _cartService;
        private string _userId = "CheckoutService_MultiplesTest";

        [SetUp]
        public void Setup()
        {
            var binFolder = AppDomain.CurrentDomain.BaseDirectory;
            var projectRoot = Path.GetFullPath(Path.Combine(binFolder, @"..\..\.."));

            var basePath = projectRoot + "\\Data";
            var store = new JsonDataStore<Repo.Model.Item>(basePath, "items.json");

            var ruleStore = new JsonDataStore<Repo.Model.CartRule>(basePath, "rule2.json");

            IMapperService _mapperService = new AutoMapperService();
            _mapperService.AddProfile((new RepoMapperProfile()));
            _itemService = new ItemService(new ItemRepository(store, _mapperService));

            _cartService = new CartService(new MemoryCacheManager<Cart>());

            _checkoutService = new CheckoutService(_cartService, new RuleRepository(ruleStore, _mapperService));
        }


        [TestCase("", 0)]
        [TestCase("A", 50)]
        [TestCase("AB", 80)]
        [TestCase("CDBA", 115)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 130)]
        [TestCase("AAAA", 180)]
        [TestCase("AAAAA", 230)]
        [TestCase("AAAAAA", 260)]
        [TestCase("AAAB", 160)]
        [TestCase("AAABB", 175)]
        [TestCase("AAABBD", 190)]
        [TestCase("DABABA", 190)]
        public async Task CheckoutMultiple(string items, double val)
        {
            var item = await _itemService.Get(items);


            _cartService.Scan(item, _userId);

            Assert.That(val, Is.EqualTo(await _checkoutService.PriceAsync(_userId)));
        }

        [Test]
        public async Task CheckoutMultipleIncremental()
        {
            var item1 = await _itemService.Get('A');
            _cartService.Scan(item1, _userId);

            Assert.That(50, Is.EqualTo(await _checkoutService.TotalAsync(_userId)));

            var item2 = await _itemService.Get('B');
            _cartService.Scan(item2, _userId);

            Assert.That(80, Is.EqualTo(await _checkoutService.TotalAsync(_userId)));

            var item3 = await _itemService.Get('A');
            _cartService.Scan(item3, _userId);

            Assert.That(130, Is.EqualTo(await _checkoutService.TotalAsync(_userId)));

            var item4 = await _itemService.Get('A');
            _cartService.Scan(item4, _userId);

            Assert.That(160, Is.EqualTo(await _checkoutService.TotalAsync(_userId)));

            var item5 = await _itemService.Get('B');
            _cartService.Scan(item5, _userId);

            Assert.That(175, Is.EqualTo(await _checkoutService.TotalAsync(_userId)));
        }
    }

    
}
