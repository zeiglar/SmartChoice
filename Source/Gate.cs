using Source.Enums;
using Source.Estates;
using Source.Features;
using Source.Webs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source
{
    public class Gate
    {
        private Suburb _Suburb = new Suburb();
        private Target _Target = new Target();

        public Gate() { }

        public void SetSuburb(string name, string postcode, string state)
        {
            this._Suburb.Name = name;
            this._Suburb.Postcode = postcode;
            this._Suburb.State = state;
        }

        public void SetPriceRange(int? minPrice, int? maxPrice)
        {
            this._Target.MinPrice = minPrice;
            this._Target.MaxPrice = maxPrice;
        }

        public void SetBehavior(int behavior)
        {
            if (behavior < 1 || behavior > 6)
                this._Target.BehaviorType = BehaviorTypes.buy;
            else
                this._Target.BehaviorType = (BehaviorTypes)behavior;
        }

        public IList<CResult> Retrieve()
        {
            IList<CResult> cResults = new List<CResult>();
            Web web = new Realestate();
            return web.GetProperties(this._Target, this._Suburb);
        }
        //public Gate()
        //{
        //    _Suburb = new List<Suburb>();
        //    _Suburb.Add(new Suburb()
        //    {
        //        Name = "hornsby",
        //        Postcode = "2077",
        //        State = "nsw"
        //    });
        //}

        //public void Start()
        //{
        //    IList<CResult> cResults = new List<CResult>();
        //    Target target = new Target();
        //    target.BehaviorType = Enums.BehaviorTypes.buy;
        //    Web web = new Realestate();
        //    foreach (Suburb suburb in _Suburb)
        //    {
        //        cResults = web.GetProperties(target, suburb);
        //    }

        //    ShowResults(cResults);
        //}

        //void ShowResults(IList<CResult> cResults)
        //{
        //    foreach (CResult result in cResults)
        //    {
        //        Console.WriteLine(result.Location);
        //        Console.WriteLine(result.PropertyType.ToString());
        //    }

        //    Console.ReadLine();
        //}
    }
}
