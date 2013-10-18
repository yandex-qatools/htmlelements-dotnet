using HtmlElements.Test.Browsers;
using HtmlElements.Test.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElements.Test.Services.GoogleScreens
{
    [Identity("XPath://div[@id='ires']/ol[@id='rso']/li/div[@id='rg']/div[@id='rg_s']", "Id:fsl")]
    public class ImageSearchPage : BasicSearchScreen
    {
        public ImageSearchPage(Browser browser)
            : base(browser)
        {
        }

        public override void Open()
        {
            if (!IsOnCurrentPage())
            {
                OpenImageSearchPage();
            }
        }
    }
}
