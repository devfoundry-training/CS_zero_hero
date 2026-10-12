// =====================================================================================================
// LessonTests.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// A first look at MSTest, using classes from two lessons. Each test arranges some objects, acts on
// them, then asserts what should be true. Task 11 (tasks/11_testing) takes testing further.
//
// Run (from the repo root):
//     dotnet test src/CS_zero_heroTests --logger "console;verbosity=detailed"
// -----------------------------------------------------------------------------------------------------

namespace CS_zero_heroTests
{
    // datatypes: a class is a reference type, so copying the variable copies the reference
    [TestClass]
    public class DataTypesTests
    {
        [TestMethod]
        public void CopyingAClassVariableSharesTheObject()
        {
            CS_Zero_Hero.datatypes.Person p1 = new CS_Zero_Hero.datatypes.Person("Luke");
            CS_Zero_Hero.datatypes.Person p2 = p1;

            p2.Name = "Leia";

            Assert.AreSame(p1, p2);
            Assert.AreEqual("Leia", p1.Name);
        }

        [TestMethod]
        public void CopyingAnIntCopiesTheValue()
        {
            int a = 10;
            int b = a;

            b = 20;

            Assert.AreEqual(10, a);
            Assert.AreEqual(20, b);
        }
    }

    // oop: the Age property's setter rejects a negative value and keeps the old one
    [TestClass]
    public class OopTests
    {
        [TestMethod]
        public void ValidAgeIsStored()
        {
            CS_Zero_Hero.oop.Person luke = new CS_Zero_Hero.oop.Person("Luke Skywalker", 25);

            luke.Age = 30;

            Assert.AreEqual(30, luke.Age);
        }

        [TestMethod]
        public void NegativeAgeIsRejected()
        {
            CS_Zero_Hero.oop.Person luke = new CS_Zero_Hero.oop.Person("Luke Skywalker", 25);

            luke.Age = -5;

            Assert.AreEqual(25, luke.Age);
        }
    }
}
