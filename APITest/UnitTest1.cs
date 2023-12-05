using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using SurfboardApi.Controllers.v1;
using SurfboardApi.Data;
using SurfboardApi.Models;

namespace APITest
{

	public class UnitTest1 : IClassFixture<SurfBoardApiContext>
	{
		BookingController _controller;
		private readonly SurfBoardApiContext _context;

		public UnitTest1(SurfBoardApiContext context)
		{
			_context = context;
			_controller = new BookingController(context);
		}


		[Fact]
		public void GetAllv1()
		{
			//Arrange

			//act
			var result = _controller.GetBooking();
			
			//assert
			Assert.NotNull(result);
			Assert.IsType<OkObjectResult>(result);

			var list = result.Result as OkObjectResult;

			Assert.IsType<List<Bookings>>(list.Value);



			var listBooks = list.Value as List<Bookings>;

			Assert.Equal(2, listBooks.Count);
		}
	}
}