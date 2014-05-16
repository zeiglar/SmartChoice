using Source.Estates;
using Source.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source.Webs
{
    public class Domain : Web
    {
        #region Fields
        private const string _WebPage = "www.domain.com.au";
        #endregion

        #region Properties
        protected override string WebPage
        {
            get { return _WebPage; }
        }
        #endregion

        public override string GenerateUri(Target target, Suburb suburb)
        {
            throw new NotImplementedException();
        }

        public override IList<CResult> SeekProperties(string html)
        {
            throw new NotImplementedException();
        }
    }
}
