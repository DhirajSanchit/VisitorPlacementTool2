// using System;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using VisitorPlacementTool2.Visitors;
//
// namespace VIsitorPlacementTests.LogicTests;
//
// [TestClass]
// public class GenerateVisitorTest
// {
//     [TestMethod]
//     public void GenerateSingleVisitor()
//     {
//         //arrange
//         const int amount = 1;
//         var visitorGenerator = new VisitorGenerator();
//         //act
//         var results = visitorGenerator.GenerateVisitors(amount);
//         //assert
//         Assert.AreEqual(1, results.Count);
//         Assert.IsNotNull(results[0].Name);
//         Assert.IsNotNull(results[0].DateOfBirth);
//     }
//
//     [TestMethod]
//     public void GenerateMultipleVisitors()
//     {
//         //arrange
//         const int amount = 12;
//         var visitorGenerator = new VisitorGenerator();
//         //act
//         var results = visitorGenerator.GenerateVisitors(amount);
//         //assert
//         Assert.AreEqual(12, results.Count);
//         foreach (var result in results)
//         {
//             Assert.IsNotNull(result.Name);
//             Assert.IsNotNull(result.DateOfBirth);
//         }
//     }
//
//     [TestMethod]
//     [ExpectedException(typeof(IndexOutOfRangeException))]
//     public void GenerateNegativeVisitors()
//     {
//         //arrange
//         const int amount = -1;
//         var visitorGenerator = new VisitorGenerator();
//         //act
//         var results = visitorGenerator.GenerateVisitors(amount);
//         //assert
//         Assert.Fail();
//     }
// }