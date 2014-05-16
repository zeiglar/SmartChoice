using Source.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Estates
{
    /// <summary>
    /// A main search result for clients' tagart: the place that they want to purchase
    /// </summary>
    public class Property
    {
        #region Properties
        #region General Features
        public PropertyTypes PropertyType { get; set; }
        public int BedRooms { get; set; }
        //Bath room has the shower and toilet
        public int BathRooms { get; set; }
        public Land Land { get; set; }
        #endregion

        #region Indoor Features
        public int Ensuite { get; set; }
        public int LivingAreas { get; set; }
        public int Toilets { get; set; }
        public int Study { get; set; }
        public int Workshop { get; set; }
        public int Floorboards { get; set; }
        public int BuildInWardrobes { get; set; }
        public bool HasInternet { get; set; }
        public bool GasHeating { get; set; }
        #endregion

        #region Outdoor Features
        public int Carport { get; set; }
        public int Garage { get; set; }
        public int Balcony { get; set; }
        public int Deck { get; set; }
        public int EntertainingArea { get; set; }
        public int Shed { get; set; }
        public bool IsFullyFenced { get; set; }
        public int SwimmingPool { get; set; }
        #endregion

        #region Other Features
        public string Others { get; set; }
        #endregion

        public Cost Cost { get; set; }
        public Address Address { get; set; }
        public DateTimeOffset RecordedDate { get; set; }
        #endregion
    }
}
