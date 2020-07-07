using System;
using System.Collections.Generic;
using System.Linq;
using Flurl;
using HubSpot.NET.Api.Product.Dto;
using HubSpot.NET.Api.Shared;
using HubSpot.NET.Core;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;
using RestSharp;

namespace HubSpot.NET.Api.Product
{
    public class HubSpotProductApi : ApiRoutable, IHubSpotProductApi
    {
        private readonly IHubSpotClient _client;
        public override string MidRoute => "/products/v1";

        public HubSpotProductApi(IHubSpotClient client)
        {
            _client = client;
            AddRoute<ProductHubSpotModel>("/product");
        }

        /// <summary>
        /// Creates a Product entity
        /// </summary>
        /// <typeparam name="T">Implementation of ProductHubSpotModel</typeparam>
        /// <param name="entity">The entity</param>
        /// <returns>The created entity (with ID set)</returns>
        public ProductHubSpotModel Create(ProductHubSpotModel entity)
        {
            NameTransportModel<ProductHubSpotModel> model = new NameTransportModel<ProductHubSpotModel>();
            model.ToPropertyTransportModel(entity);

            return _client.Execute<ProductHubSpotModel, NameTransportModel<ProductHubSpotModel>>(GetRoute<ProductHubSpotModel>(), model, Method.POST);
        }

        /// <summary>
        /// Gets a single product by ID
        /// </summary>
        /// <param name="productId">ID of the product</param>
        /// <typeparam name="T">Implementation of ProductHubSpotModel</typeparam>
        /// <returns>The product entity</returns>
        public ProductHubSpotModel GetById(long productId)
            => _client.Execute<ProductHubSpotModel>(GetRoute<ProductHubSpotModel>(productId.ToString()));

        /// <summary>
        /// Updates a given product
        /// </summary>
        /// <typeparam name="T">Implementation of ProductHubSpotModel</typeparam>
        /// <param name="entity">The product entity</param>
        /// <returns>The updated product entity</returns>
        public ProductHubSpotModel Update(ProductHubSpotModel entity)
        {
            if (entity.Id < 1)
                throw new ArgumentException("Product entity must have an id set!");

            return _client.Execute<ProductHubSpotModel, ProductHubSpotModel>(GetRoute<ProductHubSpotModel>(entity.Id.ToString()), entity, method: Method.PUT);
        }

        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <typeparam name="T">Implementation of ProductListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of products</returns>
        public ProductListHubSpotModel<ProductHubSpotModel> List(bool includeAssociations, ListRequestOptions opts = null)
        {
            opts = opts ?? new ListRequestOptions(250);

            Url path = GetRoute<ProductListHubSpotModel<ProductHubSpotModel>>("product", "paged").SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("offset", opts.Offset);

            if (includeAssociations)
                path = path.SetQueryParam("includeAssociations", "true");

            if (opts.PropertiesToInclude.Any())
                path = path.SetQueryParam("properties", opts.PropertiesToInclude);

            return _client.Execute<ProductListHubSpotModel<ProductHubSpotModel>, ListRequestOptions>(path, opts);
        }

        /// <summary>
        /// Gets a list of products associated to a hubSpot Object
        /// </summary>
        /// <typeparam name="T">Implementation of ProductListHubSpotModel</typeparam>
        /// <param name="includeAssociations">Bool include associated Ids</param>
        /// <param name="hubId">Long Id of Hubspot object related to products</param>
        /// <param name="objectName">String name of Hubspot object related to products (contact\account)</param>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of products</returns>
        public ProductListHubSpotModel<ProductHubSpotModel> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "contact")
        {
            opts = opts ?? new ListRequestOptions();

            Url path = $"{GetRoute<ProductListHubSpotModel<ProductHubSpotModel>>()}/product/associated/{objectName}/{hubId}/paged"
                .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("offset", opts.Offset);

            if (includeAssociations)
                path = path.SetQueryParam("includeAssociations", "true");

            if (opts.PropertiesToInclude.Any())
                path = path.SetQueryParam("properties", opts.PropertiesToInclude);

            return _client.Execute<ProductListHubSpotModel<ProductHubSpotModel>, ListRequestOptions>(path, opts);
        }

        /// <summary>
        /// Deletes a given product (by ID)
        /// </summary>
        /// <param name="productId">ID of the product</param>
        public void Delete(long productId)
            => _client.ExecuteOnly(GetRoute<ProductHubSpotModel>(productId.ToString()), method: Method.DELETE);

        /// <summary>
        /// Gets a list of recently created products
        /// </summary>
        /// <typeparam name="T">Implementation of ProductListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of products</returns>
        public ProductRecentListHubSpotModel<ProductHubSpotModel> RecentlyCreated(ProductRecentRequestOptions opts = null)
        {
            opts = opts ?? new ProductRecentRequestOptions();

            Url path = $"{GetRoute<ProductRecentListHubSpotModel<ProductHubSpotModel>>()}/product/recent/created"
                            .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("offset", opts.Offset);

            if (opts.IncludePropertyVersion)
                path = path.SetQueryParam("includePropertyVersions", "true");

            if (!string.IsNullOrEmpty(opts.Since))
                path = path.SetQueryParam("since", opts.Since);

            return _client.Execute<ProductRecentListHubSpotModel<ProductHubSpotModel>, ProductRecentRequestOptions>(path, opts);
        }

        /// <summary>
        /// Gets a list of recently modified products
        /// </summary>
        /// <typeparam name="T">Implementation of ProductListHubSpotModel</typeparam>
        /// <param name="opts">Options (limit, offset) relating to request</param>
        /// <returns>List of products</returns>
        public ProductRecentListHubSpotModel<ProductHubSpotModel> RecentlyUpdated(ProductRecentRequestOptions opts = null)
        {
            opts = opts ?? new ProductRecentRequestOptions();

            var path = $"{GetRoute<ProductRecentListHubSpotModel<ProductHubSpotModel>>()}/product/recent/modified"
                .SetQueryParam("limit", opts.Limit);

            if (opts.Offset.HasValue)
                path = path.SetQueryParam("offset", opts.Offset);

            if (opts.IncludePropertyVersion)
                path = path.SetQueryParam("includePropertyVersions", "true");

            if (!string.IsNullOrEmpty(opts.Since))
                path = path.SetQueryParam("since", opts.Since);

            return _client.Execute<ProductRecentListHubSpotModel<ProductHubSpotModel>, ProductRecentRequestOptions>(path, opts);
        }
    }
}
