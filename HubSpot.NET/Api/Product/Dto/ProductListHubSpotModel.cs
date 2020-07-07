using System.Collections.Generic;
using System.Runtime.Serialization;
using HubSpot.NET.Api.Contact.Dto;
using HubSpot.NET.Core.Abstracts;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Product.Dto
{
    /// <summary>
    /// Models a set of results returned when querying for sets of products
    /// </summary>
    [DataContract]
    public class ProductListHubSpotModel<T> : ListHubSpotModel, IHubSpotModel
    {
        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The contacts.
        /// </value>
        [DataMember(Name = "products")]
        public IList<T> Products { get; set; } = new List<T>();

        public bool IsNameValue => false;
    }
}
