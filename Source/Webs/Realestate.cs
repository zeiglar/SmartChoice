using HtmlAgilityPack;
using Source.Estates;
using Source.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Webs
{
    public class Realestate : Web
    {
        #region Fields
        private const string _WebPage = "http://www.realestate.com.au";
        #endregion

        #region Functions
        public string GetSuburbStr(Suburb suburb)
        {
            return string.Format("in-{0}%2c+{1}+{2}", suburb.Name, suburb.State, suburb.Postcode);
        }
        #endregion

        #region Overrides
        protected override string WebPage
        {
            get { return _WebPage; }
        }

        public override string GenerateUri(Target target, Suburb suburb)
        {
            return string.Format("{0}/{1}/{2}{3}/list-{4}?includeSurrounding={5}", WebPage, target.BehaviorType.ToString(),
                GetPriceRange(target), GetSuburbStr(suburb), target.ListNum, target.IncludSurrounding);
        }

        public override IList<CResult> SeekProperties(string html)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode node = htmlDoc.DocumentNode;

            //Get all result body from HTML
            HtmlNodeCollection items = node.SelectNodes("//div[@*[starts-with(@class, 'resultBody ')]]");

            if (items == null)
                return null;


            IList<CResult> results = new List<CResult>();
            for (int i = 0; i < items.Count; i++)
            {
                CResult cResult = new CResult();
                try
                {
                    cResult.Price = items[i].SelectSingleNode(".//span[@class=\"priceText\"]").InnerText;//p[@class=\"price\"]/
                }
                catch
                {
                    cResult.Price = "ContactAgent";
                }
                cResult.Location = items[i].SelectSingleNode(".//a[@class=\"name\"]").InnerText;
                cResult.HyperUri = _WebPage + items[i].SelectSingleNode(".//a[@class=\"name\"]").GetAttributeValue("href", null);
                cResult.PropertyType = items[i].SelectSingleNode(".//span[@class=\"propertyType\"]").InnerText; ;

                HtmlNodeCollection spaces = items[i].SelectNodes(".//div[@class=\"listingInfo\"]/ul[@class=\"linkList horizontalLinkList propertyFeatures\"]/li");
                if (spaces != null)
                    for (int j = 0; j < spaces.Count; j++)
                    {
                        string num = spaces[j].SelectSingleNode(".//img").GetAttributeValue("alt", null);
                        if (num.Equals("Bedrooms"))
                        {
                            cResult.Bedrooms = spaces[j].SelectSingleNode(".//span").InnerText;
                        }
                        else if (num.Equals("Bathrooms"))
                        {
                            cResult.Bathrooms = spaces[j].SelectSingleNode(".//span").InnerText;
                        }
                        else if (num.Equals("Car Spaces"))
                        {
                            cResult.Parkings = spaces[j].SelectSingleNode(".//span").InnerText;
                        }
                    }

                results.Add(cResult);
            }

            return results;
        }
        #endregion

        private string GetPriceRange(Target target)
        {
            string price;

            if (target.MinPrice.HasValue && target.MinPrice.Value != -1)
                if (target.MaxPrice.HasValue && target.MaxPrice.Value != -1)
                    price = string.Format("between-{0}-{1}-", target.MinPrice.Value, target.MaxPrice.Value);
                else
                    price = string.Format("between-{0}-any-", target.MinPrice.Value);
            else if (target.MaxPrice.HasValue && target.MaxPrice.Value != -1)
                price = string.Format("between-0-{0}", target.MaxPrice);
            else
                price = "";

            return price;
        }

        private void CheckResultBody()
        {
        }
    }
}
