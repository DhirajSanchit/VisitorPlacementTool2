// using System;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using VisitorPlacementTool2.Group;
//
// namespace VIsitorPlacementTests.LogicTests;
//
// [TestClass]
// public class GroupGeneratorTest
// {
//     [TestMethod]
//     public void GenerateVisitorGroup()
//     {
//         //arrange
//         var groupGenerator = new GroupGenerator();
//         //act
//         var result = groupGenerator.GenerateVisitorGroup();
//         //assert
//         Assert.IsNotNull(result.GetVisitors());
//         foreach (var visitor in result.GetVisitors())
//         {
//             Assert.IsNotNull(visitor.Name);
//             Assert.IsNotNull(visitor.DateOfBirth);
//         }
//     }
//
//     [TestMethod]
//     public void GenerateVisitorGroups()
//     {
//         //arrange
//         var groupGenerator = new GroupGenerator();
//         var amount = 10;
//         //act
//         var results = groupGenerator.GenerateVisitorGroups(amount);
//         //assert
//         Assert.AreEqual(10, results.Count);
//         foreach (var result in results)
//         {
//             Assert.IsTrue(result.GetVisitors().Count > 0);
//         }
//     }
//
//     [TestMethod]
//     public void GenerateSingleGroup()
//     {
//         //arrange
//         var groupGenerator = new GroupGenerator();
//         var amount = 1;
//         //act
//         var results = groupGenerator.GenerateVisitorGroups(amount);
//         //assert
//         Assert.AreEqual(1, results.Count);
//         Assert.IsTrue(results[0].GetVisitors().Count > 0);
//     }
//
//     [TestMethod]
//     [ExpectedException(typeof(IndexOutOfRangeException))]
//     public void GenerateNegativeGroupAmount()
//     {
//         //arrange
//         var groupGenerator = new GroupGenerator();
//         var amount = -1;
//         //act
//         //groupGenerator.GenerateVisitorGroups(amount);
//         //assert
//         Assert.Fail();
//     }
//
//     [TestMethod]
//     public void GenerateMaxVisitorGroups()
//     {
//         //arrange
//         var maxVisitors = 250;
//         var groupGenerator = new GroupGenerator();
//         //act
//         var results = groupGenerator.GenerateMaxVisitorGroups(maxVisitors);
//         //assert
//         Assert.IsNotNull(results);
//         foreach (var result in results)
//         {
//             Assert.IsTrue(result.GetVisitors().Count > 0);
//         }
//     }
// }