using Source.Enums;
using Source.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Features
{
    public class Target
    {
        #region Fields
        private int Page = 0;
        #endregion

        #region Properties
        public PropertyTypes PropertyType { get; set; }
        public BehaviorTypes BehaviorType { get; set; }

        public int? MinLandSize { get; set; }

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public int? MinBeds { get; set; }
        public int? MaxBeds { get; set; }

        public int? MinBathrooms { get; set; }

        //In Domain it is "Parking"
        public int? MinCarSpaces { get; set; }

        public bool IncludSurrounding { get; set; }

        //The number of page of displaying search results
        public int ListNum 
        {
            get { return this.Page; }
            set { this.Page = value; } 
        }
        #endregion

        #region Constructors
        public Target() { }

        public Target(PropertyTypes propertyType,
            BehaviorTypes behaviorType,
            int? minLandSize,
            int? minPrice, int? maxPrice,
            int? minBeds, int? maxBeds,
            int? minBathrooms,
            int? minParking,
            bool includSurrounding)
        {
            PropertyType = propertyType;
            BehaviorType = behaviorType;
            MinLandSize = minLandSize;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            MinBeds = minBeds;
            MaxBeds = maxBeds;
            MinBathrooms = minBathrooms;
            MinCarSpaces = minParking;
            IncludSurrounding = includSurrounding;
        }
        #endregion

        #region Functions
        public void SetProperties(PropertyTypes propertyType,
            BehaviorTypes behaviorType,
            int? minLandSize,
            int? minPrice, int? maxPrice,
            int? minBeds, int? maxBeds,
            int? minBathrooms,
            int? minParking,
            bool includSurrounding)
        {
            PropertyType = propertyType;
            BehaviorType = behaviorType;
            MinLandSize = minLandSize;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            MinBeds = minBeds;
            MaxBeds = maxBeds;
            MinBathrooms = minBathrooms;
            MinCarSpaces = minParking;
            IncludSurrounding = includSurrounding;
        }
        #endregion
    }
}
