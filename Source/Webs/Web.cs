using Source.Enums;
using Source.Estates;
using Source.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Source.Webs
{
    public abstract class Web
    {
        #region Properties
        #endregion

        #region Abstract Funcions
        public abstract string GenerateUri(Target target, Suburb suburb);
        public abstract IList<CResult> SeekProperties(string html);
        #endregion

        #region Virtual Functions
        protected virtual string WebPage { get; set; }

        public virtual IList<CResult> GetProperties(Target target, Suburb suburb)
        {
            List<CResult> results = new List<CResult>();

            while (true)
            {
                target.ListNum++;
                IList<CResult> result = GetPropertiesFromSinglePage(target, suburb);
                if (result == null || result.Count == 0)
                    break;
                else
                    results.AddRange(result);
            }

            return results;
        }

        public virtual IList<CResult> GetPropertiesFromSinglePage(Target target, Suburb suburb)
        {
            int code = 0;
            string html = this.GetHTML(this.GenerateUri(target, suburb), ref code);

            if (code == 0)
                return this.SeekProperties(html);
            else
            {
                //TODO: add error log
                return null;
            }
        }

        //public virtual string GetHTML(string link, ref int code)
        //{
        //    Uri uri;
        //    if (Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out uri))
        //    {
        //        return GetHTML(uri, ref code);
        //    }

        //    return ReturnTypes.Failure.ToString();
        //}

        public virtual string GetHTML(string uri, ref int code)
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
                    code = (int)ErrorCodes.Error_WebPageDownloading;
                }
                catch (Exception ex)
                {
                    html = ex.ToString();
                    code = (int)ErrorCodes.Error_WebPageDownloading;
                }
            }

            return html;
        }
        #endregion
    }
}
