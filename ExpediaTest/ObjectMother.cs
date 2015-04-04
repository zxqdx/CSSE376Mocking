using Expedia;
using System;

namespace ExpediaTest
{
	public class ObjectMother
	{
        public static Car Saab()
        {
            return new Car(7) { Name = "Saab 9-5 Sports Sedan" };
        }
        public static Car BMW()
        {
            // This method is used in the test CarTest.TestGetCarLocation()
            return new Car(10) { Name = "BMW x6 xDrive35i" };
        }
	}
}
