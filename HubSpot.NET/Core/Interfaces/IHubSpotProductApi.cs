using System.Collections.Generic;
using HubSpot.NET.Api.Product.Dto;

namespace HubSpot.NET.Core.Interfaces
{
    public interface IHubSpotProductApi<T> : ICRUDable<T>
        where T : IHubSpotModel
    {
        ProductListHubSpotModel<T> List(bool includeAssociations, ListRequestOptions opts = null);
        ProductListHubSpotModel<T> ListAssociated(bool includeAssociations, long hubId, ListRequestOptions opts = null, string objectName = "deal");
        ProductRecentListHubSpotModel<T> RecentlyCreated(ProductRecentRequestOptions opts = null);
        ProductRecentListHubSpotModel<T> RecentlyUpdated(ProductRecentRequestOptions opts = null);
    }

    public interface IHubSpotProductApi : IHubSpotProductApi<ProductHubSpotModel> { }
}