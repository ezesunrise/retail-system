using RetailSystem.Data;
using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RetailSystem.Test.Controllers
{
    public class SupplierControllerTest
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public async Task GetAllSuppliers_Should_return_IEnumerable_for_businessId_greater_than_zero(int businessId)
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Supplier>>;

            // Act


            //Assert

        }
    }

}
