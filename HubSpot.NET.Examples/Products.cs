using HubSpot.NET.Api.Product.Dto;
using HubSpot.NET.Core;
using System;
using System.Collections.Generic;

namespace HubSpot.NET.Examples
{
    public class Products
    {
        public static void Example(HubSpotApi api)
        {
            try
            {
                Tests(api);
                Console.WriteLine("Products tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Products tests failed!");
                Console.WriteLine(ex.ToString());
            }
        }
        private static void Tests(HubSpotApi api)
        {
            /**
             * Create a product
             */
            var product = api.Product.Create(new ProductHubSpotModel()
            {
                UnitPrice = 10000,
                Name = "New Product #1"
            });

            /**
             * Delete a product
             */
            api.Product.Delete(product.Id.Value);

            /**
             * Get all products
             */
            var products = api.Product.List(false,
                new ListRequestOptions(250) { PropertiesToInclude = new List<string> { "name", "price" } });

            /**
             *  Get all products
             *  This is commented in case the HubSpot data has a large quantity of products.
             */
            //var moreResults = true;
            //long offset = 0;
            //while (moreResults)
            //{
            //    var allProducts = api.Product.List<ProductHubSpotModel>(false,
            //        new ListRequestOptions { PropertiesToInclude = new List<string> { "productname", "amount", "hubspot_owner_id", "closedate" }, Limit = 100, Offset = offset });

            //    moreResults = allProducts.MoreResultsAvailable;
            //    if (moreResults) offset = allProducts.ContinuationOffset;
            //}

            /**
             *  Get recently created products since 7 days ago, limited to 10 records
             *  Will default to 30 day if Since is not set.
             *  Using ProductRecentListHubSpotModel to accomodate products returning in the "results" property.
             */
            var currentdatetime = DateTime.SpecifyKind(DateTime.Now.AddDays(-7), DateTimeKind.Utc);
            var since = ((DateTimeOffset)currentdatetime).ToUnixTimeMilliseconds().ToString();

            var recentlyCreatedProducts = api.Product.RecentlyCreated(new ProductRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
                Since = since
            });

            /**
             *  Get recently created products since 7 days ago, limited to 10 records
             *  Will default to 30 day if Since is not set.
             *  Using ProductRecentListHubSpotModel to accomodate products returning in the "results" property.
             */
            var recentlyUpdatedProducts = api.Product.RecentlyCreated(new ProductRecentRequestOptions
            {
                Limit = 10,
                IncludePropertyVersion = false,
                Since = since
            });
        }
    }
}
