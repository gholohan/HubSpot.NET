using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HubSpot.NET.Core.Interfaces;

namespace HubSpot.NET.Api.Product.Dto
{
    /// <summary>
    /// Models the associations of a product to companies & contacts
    /// </summary>
    [DataContract]
    public class ProductHubSpotAssociations
    {
        [DataMember(Name = "associatedCompanyIds")]
        public long[] AssociatedCompany { get; set; }

        [DataMember(Name = "associatedVids")]
        public long[] AssociatedContacts { get; set; }
    }

    /// <summary>
    /// Models a Product entity within HubSpot. Default properties are included here
    /// with the intention that you'd extend this class with properties specific to 
    /// your HubSpot account.
    /// </summary>
    [DataContract]
    public class ProductHubSpotModel : IHubSpotSerializable<ProductHubSpotModel>
    {
        public ProductHubSpotModel()
        {
        }
        /// <summary>
        /// Contacts unique Id in HubSpot
        /// </summary>
        /// 
        [DataMemner(Name = "recurringbillingfrequency")]
        public string BillingFrequency { get; set; }

        [DataMember(Name = "hs_created_by_user_id")]
        public long? Id { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "hs_object_id")]
        [IgnoreDataMember]
        public long? Id { get; set; }

        [DataMember(Name = "hs_sku")]
        public string SKU { get; set; }

        [DataMember(Name = "tax")]
        public double? Tax { get; set; }

        [DataMember(Name = "hs_recurring_billing_period")]
        public string Term { get; set; }

        [DataMember(Name = "hs_cost_of_goods_sold")]
        public double? UnitCost { get; set; }

        [DataMember(Name = "price")]
        public double? UnitPrice { get; set; }

        [IgnoreDataMember]
        public ProductHubSpotAssociations Associations { get; set; }

        public bool IsNameValue => true;

        public virtual void ToHubSpotDataEntity(ref ProductHubSpotModel converted)
        {
            converted.Associations = Associations;
        }

        public virtual void FromHubSpotDataEntity(ProductHubSpotModel hubspotData)
        {
            if (hubspotData.Associations != null)
            {
                Associations.AssociatedDeals = hubspotData.Associations.AssociatedDeals;
            }
        }
    }
}
