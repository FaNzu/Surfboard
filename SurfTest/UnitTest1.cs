using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SurfboardApi.Controllers.v1;
using SurfboardApi.Data;
using SurfboardApi.Models.ViewModels;

namespace SurfTest
{
	[TestClass]
	public class BookingControllerTests
	{

		[TestMethod]
		public async Task GetBooking_ReturnsOkResult()
		{
			// Arrange
			var contextMock = new Mock<SurfBoardApiContext>();
			contextMock.Setup(c => c.Bookings).Returns(MockDbSet(new List<Bookings>()));

			var controller = new BookingController(contextMock.Object);

			// Act
			var result = await controller.GetBooking();

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[TestMethod]
		public async Task CreateBooking_WithValidModel_ReturnsOkResult()
		{
			// Arrange
			var contextMock = new Mock<SurfBoardApiContext>();
			contextMock.Setup(c => c.Bookings).Returns(MockDbSet(new List<Bookings>()));

			var controller = new BookingController(contextMock.Object);

			var validBookingRequest = new BookingRequestVM
			{
				// Provide valid booking request data here
			};

			// Act
			var result = await controller.CreateBooking(validBookingRequest);

			// Assert
			Assert.IsType<OkObjectResult>(result);
		}

		[TestMethod]
		public async Task CreateBooking_WithInvalidModel_ReturnsBadRequestResult()
		{
			// Arrange
			var contextMock = new Mock<SurfBoardApiContext>();
			var controller = new BookingController(contextMock.Object);
			controller.ModelState.AddModelError("key", "error message");

			var invalidBookingRequest = new BookingRequestVM
			{
				// Provide invalid booking request data here
			};

			// Act
			var result = await controller.CreateBooking(invalidBookingRequest);

			// Assert
			Assert.IsType<BadRequestObjectResult>(result);
		}


		// Add more test cases as needed...

		private static IQueryable<T> MockDbSet<T>(List<T> data) where T : class
		{
			var queryableData = data.AsQueryable();
			var dbSetMock = new Mock<IQueryable<T>>();
			dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
			dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
			return dbSetMock.Object;
		}
	}
}