using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace SatNavTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void CanGetName()
        {
            Assert.AreEqual("AB", new Edge(new Node("A", 1, 1), new Node("B", 1, 1)).Name);
        }

        [TestMethod]
        public void Can_get_length()
        {
            Assert.AreEqual(3, new Edge(new Node("A", 1, 1), new Node("B", 1, 4)).Length);
            Assert.AreEqual(5, new Edge(new Node("A", 1, 1), new Node("B", 5, 4)).Length);
        }

        [TestMethod]
        public void Can_get_one_route()
        {
            var route = new Map().GetRoute("A", "B");
            Assert.AreEqual(1, route.Count);
            Assert.AreEqual("AB", route.First().Name);
        }

        [TestMethod]
        public void Can_get_two_route()
        {
            var route = new Map().GetRoute("A", "C");
            Assert.AreEqual(2, route.Count);
            Assert.AreEqual("AB", route.First().Name);
            Assert.AreEqual("BC", route.Last().Name);
        }

        [TestMethod]
        public void Can_get_other_route()
        {
            var route = new Map().GetRoute("A", "K");
            Assert.AreEqual(1, route.Count);
            Assert.AreEqual("AK", route.First().Name);
        }

        [TestMethod]
        public void Can_get_other_two_route()
        {
            var route = new Map().GetRoute("A", "L");
            Assert.AreEqual(2, route.Count);
            Assert.AreEqual("AK", route.First().Name);
            Assert.AreEqual("KL", route.Last().Name);
        }

        [TestMethod]
        public void Can_get_three_route()
        {
            var route = new Map().GetRoute("A", "M");
            Assert.AreEqual(3, route.Count);
            Assert.AreEqual("AK", route.First().Name);
            Assert.AreEqual("LM", route.Last().Name);
        }

        [TestMethod]
        public void Can_get_long_route()
        {
            var route = new Map().GetRoute("A", "N");
            Console.WriteLine(string.Join("", route.Select(r => r.Start.Name)));
            Assert.AreEqual("GN", route.Last().Name);
        }
    }
}
