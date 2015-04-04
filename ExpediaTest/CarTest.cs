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
            var target = ObjectMother.BMW();
            Assert.AreEqual("It's a BMW!", target.Name);
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
        public void TestThatCarCanGetLocation()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();

            Expect.Call(mockDB.getCarLocation(7)).Return("On the racetrack");
            Expect.Call(mockDB.getCarLocation(13)).Return("Towing from accident");
            Expect.Call(mockDB.getCarLocation(69)).Return("I-70");

            mocks.ReplayAll();

            Car target = new Car(10);
            target.Database = mockDB;

            String result;

            result = target.getCarLocation(7);
            Assert.AreEqual(result, "On the racetrack");

            result = target.getCarLocation(13);
            Assert.AreEqual(result, "Towing from accident");

            result = target.getCarLocation(69);
            Assert.AreEqual(result, "I-70");

            mocks.VerifyAll();
        }
        
        [TestMethod()]
        public void TestThatCarDoesMileageCount()
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            int mileage = 1000;

            Expect.Call(mockDB.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDB.Miles = mileage;

            var target = new Car(10);
            target.Database = mockDB;

            int expectMiles = target.Mileage;
            Assert.AreEqual(expectMiles, mileage);
            mocks.VerifyAll();
        }
	}
}
