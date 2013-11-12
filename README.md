Html Elements framework for .NET
==================
This framework is designed to provide easy-to-use way of interaction with web-page elements in your tests. It may be considered as an extension of WebDriver Page Object.
With the help of Html Elements framework you can group web-page elements into blocks, encapsulate logic of interaction with them and then easily use created blocks in page objects. It also provides a set of helpful matchers to use with web-page elements and blocks.

Create blocks of elements
-------------------------
For example, let's create a block for the search form on the page http://www.yandex.com:

```c#
[Name("Search form")]
[Block(How = How.XPath, Using = "//form")]
public class SearchArrow : HtmlElement {
	[Name("Search request input")]
    [FindsBy(How = How.Id, Using = "searchInput")]
    private TextInput requestInput;

    [Name("Search button")]
    [FindsBy(How = How.ClassName, Using = "b-form-button__input")]
    private Button searchButton;

    public void Search(string request) {
        requestInput.SendKeys(request);
        searchButton.Click();
    }
}
```

Construct page object using created blocks
------------------------------------------
You can easily use created blocks in page objects:

```c#
public class SearchPage {
    private SearchArrow searchArrow;
    // Other blocks and elements here

    public SearchPage(IWebDriver driver) {
        PageFactory.InitElements(new HtmlElementDecorator(driver), this);
    }

    public void Search(string request) {
        searchArrow.Search(request);
    }

    // Other methods here
}
```

Use page objects in your tests
------------------------------
Created page objects can be used in your tests. That makes tests more comprehensive and easy to write.

```c#
[TestFixture]
public class SampleTest {
    private IWebDriver driver = new FirefoxDriver();
    private SearchPage searchPage = new SearchPage(driver);

    [TestFixtureSetUp]
    public void LoadPage() {
        driver.Navigate().GoToUrl("http://www.yandex.com");
    }

    [Test]
    public void SampleTest() {
        searchPage.Search("yandex");
        // Some assertion here
    }

    [TestFixtureTearDown]
    public void CloseDriver() {
        driver.Quit();
    }
}
```