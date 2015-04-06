using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
        public void TestThatCarDoesGetLocation() 
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Narnia";
            String anotherCarLocation = "Compton";
            Expect.Call(mockDB.getCarLocation(1)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(2)).Return(anotherCarLocation);
            mocks.ReplayAll();
            Car target = new Car(10);
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(1);
            Assert.AreEqual(carLocation, result);
            result = target.getCarLocation(2);
            Assert.AreEqual(anotherCarLocation, result);
            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestThatCarDoesGetMileage()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            List<Int32> Miles = new List<int>();
            for(var i=0;i<100;i++)
            {
                Miles.add(i);
            }
            Expect.Call(mockDatabase.Miles).PropertyBehavior();
            mocks.ReplayAll();
            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;
            int milesCount = target.Mileage;
            Assert.AreEqual(milesCount, Miles.Count);
            mocks.VerifyAll();
        }

        [TestMethod]
        public void TestThatBMWHasTenDays()
        {
            var target = ObjectMother.BMW();
            Assert.AreEqual(target.Name, "");
        }
	}
}
