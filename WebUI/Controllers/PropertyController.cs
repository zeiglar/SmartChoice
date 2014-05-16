using Source;
using Source.Enums;
using Source.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebUI.Controllers
{
    public class PropertyController : ApiController
    {
        private Gate _Gate = new Gate();

        public IEnumerable<CResult> Get(int behavior = 1, string suburb = "hornsby", string postcode = "2077", string state = "nsw",
            int? minPrice = null, int? maxPrice = null)
        {
            this._Gate.SetSuburb(suburb, postcode, state);
            this._Gate.SetBehavior(behavior);
            this._Gate.SetPriceRange(minPrice, maxPrice);

            return this._Gate.Retrieve();
        }
    }
}