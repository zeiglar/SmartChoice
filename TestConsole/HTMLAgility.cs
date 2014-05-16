using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using HtmlAgilityPack;

namespace TestConsole
{
    public class HTMLAgility
    {
        public void Test()
        {
            int error = 0;
            string html = GetHTML(GetStrUri(), ref error);
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            HtmlNode node = htmlDoc.DocumentNode;

    //        HtmlNodeCollection items = node.SelectNodes("//div[@class=\"resultBody standard platinum tier1\"]");
            HtmlNodeCollection items = node.SelectNodes("//div[@*[starts-with(@class, 'resultBody ')]]");
            for (int i = 0; i < items.Count; i++)
            {
                //Format is not the same needs to check
                string strPrice = items[i].SelectSingleNode("//p[@class=\"price\"]/span[@class=\"hidden\"]").InnerText;
           //     string type = items[i].SelectSingleNode("//p[@class=\"price\"]/span[@class=\"hidden\"]").InnerText;
                string location = items[i].SelectSingleNode("//a[@class=\"name\"]").InnerText;
                string hyperUri = items[i].SelectSingleNode("//a[@class=\"name\"]").GetAttributeValue("href", null);
                string type = items[i].SelectSingleNode("//span[@class=\"propertyType\"]").InnerText;
                string Baths = "";
                string Beds = "";
                string Parks = "";

                HtmlNodeCollection spaces = items[i].SelectNodes(".//div[@class=\"listingInfo\"]/ul[@class=\"linkList horizontalLinkList propertyFeatures\"]/li");
                for (int j = 0; j < spaces.Count; j++)
                {
                    string num = spaces[j].SelectSingleNode(".//img").GetAttributeValue("alt", null);
                    if (num.Equals("Bedrooms"))
                    {
                        Beds = spaces[j].SelectSingleNode(".//span").InnerText;
                    }
                    else if (num.Equals("Bathrooms"))
                    {
                        Baths = spaces[j].SelectSingleNode(".//span").InnerText;
                    }
                    else if (num.Equals("Car Spaces"))
                    {
                        Parks = spaces[j].SelectSingleNode(".//span").InnerText;
                    }
                }

                //string text = items[i].InnerText;
                //string subHtml = items[i].InnerHtml;
                //HtmlNodeCollection tt = items[i].SelectNodes.ChildNodes;
         //       int ii = 0;

            }
        }

        private string GetStrUri()
        {
            string area = HttpUtility.UrlEncode(string.Format("in-{0}, {1} {2}", "hornsby", "nsw", 2077));
            return string.Format("http://www.realestate.com.au/{0}/{1}/list-{2}?includeSurrounding={3}",
                "buy", area, 1, 0);
        }

        private string GetHTML(string uri, ref int code)
        {
            string html = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    html = client.DownloadString(uri);
                }
                catch (WebException ex)
                {
                    html = ex.ToString();
                    code = 1;
                }
            }

            return html;
        }
    }
}
