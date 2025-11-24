using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public class ProductGetDetails
    {
        private readonly IProductData productData;

        public ProductGetDetails(IProductData productData)
        {
            this.productData = productData;
        }

        [FunctionName("ProductGetDetails")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products/{id:int}")] HttpRequest req,
            int id,
            ILogger log)
        {
            var product = await productData.GetProduct(id);
            if (product != null)
            {
                return new OkObjectResult(product);
            }

            return new BadRequestResult();
        }
    }
}
