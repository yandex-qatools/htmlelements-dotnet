using HtmlElements.Test.Browsers;
using HtmlElements.Test.Screens;
using HtmlElements.Test.Services.GoogleScreens.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlElements.Test.Services.GoogleScreens
{
    public abstract class BasicSearchScreen : BasicScreen
    {
        private TopNavBlock navBlock;

        public TopNavBlock TopNav
        {
            get { return navBlock; }
        }

        public BasicSearchScreen(Browser browser)
            : base(browser)
        {
        }

        public ImageSearchPage OpenImageSearchPage()
        {
            TopNav.Navigate("tbm=isch");
            ImageSearchPage isp = new ImageSearchPage(browser);
            browser.Init(isp);
            return isp;
        }
    }
}
