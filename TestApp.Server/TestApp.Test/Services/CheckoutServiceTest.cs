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
using TestApp.Services.Impl;
using CartRule = TestApp.Services.Impl.CartRule;
using Item = TestApp.Services.Impl.Item;

namespace TestApp.Test.Services
{
    [TestFixture]
    public class CheckoutServiceTest
    {
        private ItemService _itemService;
        private CheckoutService _checkoutService;

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

            _checkoutService = new CheckoutService(new RuleRepository(ruleStore, _mapperService));
        }

        [Test]
        public async Task CheckoutOneItem()
        {
            var item = new Item() { SKU = 'A', Price = 50 };
            _checkoutService.Scan(item);

            Assert.That(100, Is.EqualTo(await _checkoutService.GetTotal()));
        }

        [Test]
        public void CheckoutZero()
        {

            Assert.That(0, Is.EqualTo(_checkoutService.GetTotal()));
        }

        [Test]
        public async Task CheckoutOne()
        {
            var item = await _itemService.Get('A');


            _checkoutService.Scan(item);

            Assert.That(50, Is.EqualTo(_checkoutService.GetTotal()));
        }

        [TestCase("A", 50)]
        [TestCase("AB", 80)]
        [TestCase("CDBA", 115)]
        [TestCase("AA", 100)]
        [TestCase("AAA", 130)]
        public async Task CheckoutMultiple(string items, double val)
        {
            var item = await _itemService.Get(items);


            _checkoutService.Scan(item);

            Assert.That(val, Is.EqualTo(_checkoutService.GetTotal()));
        }
    }

    
}
