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

        [TestMethod]
        public void TestGetCarLocation() 
        {
            IDatabase mockDB = mocks.StrictMock<IDatabase>();
            String carLocation = "Terre Haute";
            String anotherCarLocation = "Chicago";
            Expect.Call(mockDB.getCarLocation(1)).Return(carLocation);
            Expect.Call(mockDB.getCarLocation(20)).Return(anotherCarLocation);
            mockDB.Stub(x => x.getCarLocation(Arg<int>.Is.Anything)).Return("Not Available");

            mocks.ReplayAll();

            Car target = ObjectMother.BMW();
            target.Database = mockDB;
            String result;
            result = target.getCarLocation(1);
            Assert.AreEqual(carLocation, result);
            result = target.getCarLocation(20);
            Assert.AreEqual(anotherCarLocation, result);

            //System.Diagnostics.Debug.WriteLine(target.getRoomOccupant(25));
            result = target.getCarLocation(1024);
            Assert.AreEqual("Not Available", result);

            mocks.VerifyAll();
        }

        [TestMethod()]
        public void TestThatCarDoesGetMilesFromDatabase()
        {
            IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
            int milesInDB = 100;
            Expect.Call(mockDatabase.Miles).PropertyBehavior();

            mocks.ReplayAll();

            mockDatabase.Miles = milesInDB;
            var target = new Car(8888);
            target.Database = mockDatabase;
            int miles = target.Mileage;
            Assert.AreEqual(milesInDB, miles);

            mocks.VerifyAll();
        }
	}
}
