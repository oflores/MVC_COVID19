using NUnit.Framework;
using saonGroup.UI.Controllers;
using saonGroup.UI.Models;

namespace saonGroup.Test
{
    public class Tests
    {
        private string appkey = "82f2a0f1a9msh8004332f2de37cbp16eed0jsna717a9f05f40";
        private string appHost = "covid-19-statistics.p.rapidapi.com";

        [SetUp]
        public void Setup()
        { 
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
        [Test]
        public void TopTenRegions() {
            var result = new BusinessModel(this.appHost,this.appkey).DataRegion();
            Assert.That(result.Count, Is.EqualTo(10));
        }
        [Test]
        public void TopFiveRegions()
        {
            var result = new BusinessModel(this.appHost, this.appkey).DataRegion(5);
            Assert.That(result.Count, Is.EqualTo(5));
        }
        [Test]
        public void TopTenProvince()
        {
            var result = new BusinessModel(this.appHost, this.appkey).DataProvince("USA");
            Assert.That(result, Has.Exactly(10).Items);
        }
        [Test]
        public void CaliforniaProvinceDate()
        {
            var result = new BusinessModel(this.appHost, this.appkey).DataProvince("USA",10, "2020-04-16");
            Assert.That(result[5].confirmed, Is.EqualTo(27677));
            Assert.That(result[5].deaths, Is.EqualTo(956));
        }

    }
}