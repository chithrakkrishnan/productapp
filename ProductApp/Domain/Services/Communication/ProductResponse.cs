using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse<Product>
    {
        public ProductResponse(Product resource)
            : base(resource)
        {
        }

        public ProductResponse(string message)
            : base(message)
        {
        }
    }
}
