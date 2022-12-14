

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CsvHelper;
using System.Globalization;
using webscraper2;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;

main();

void main()
{
    Console.Clear();
    Console.WriteLine("Webscraper");
    Console.WriteLine("##########");
    Console.WriteLine("What do you want to scrape?");
    Console.WriteLine("Youtube: press Y");
    Console.WriteLine("ict job: press I");
    Console.WriteLine("Rainbow Six Siege Operators: press R");

    string scrape = Console.ReadLine();

    if (scrape.ToLower() == "y")
    {
        youtube();
    }
    else if (scrape.ToLower() == "i")
    {
        ictjob();
    }
    else if (scrape.ToLower() == "r")
    {
        r6();
    }

}



void youtube()
{
    var list = new List<youtube>();

    String test_url_1 = "https://www.youtube.com/";


    ChromeDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl(test_url_1);
    driver.FindElement(By.XPath("/html/body/ytd-app/ytd-consent-bump-v2-lightbox/tp-yt-paper-dialog/div[4]/div[2]/div[6]/div[1]/ytd-button-renderer[2]/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]")).Click();

    Console.Clear();
    Console.WriteLine("What would u like to search on?");
    string input = Console.ReadLine();

    driver.Navigate().Refresh();
    IWebElement search = driver.FindElement(By.Name("search_query"));
    search.SendKeys(input);
    search.Submit();

    Thread.Sleep(1000);
    IWebElement filter = driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/div/ytd-toggle-button-renderer/yt-button-shape/button/yt-touch-feedback-shape/div/div[2]"));
    filter.Click();
    Thread.Sleep(1000);
    driver.FindElement(By.XPath("/html/body/ytd-app/div[1]/ytd-page-manager/ytd-search/div[1]/ytd-two-column-search-results-renderer/div[2]/div/ytd-section-list-renderer/div[1]/div[2]/ytd-search-sub-menu-renderer/div[1]/iron-collapse/div/ytd-search-filter-group-renderer[5]/ytd-search-filter-renderer[2]/a/div/yt-formatted-string")).Click();

    Thread.Sleep(1000);
    IList<IWebElement> videos = driver.FindElements(By.CssSelector("ytd-video-renderer div[id='dismissible']"));
    Thread.Sleep(1000);

    Console.Clear();
    Console.WriteLine(videos.Count);

    for (int i = 0; i <= 4; i++)
    {
        var channel = videos[i].FindElement(By.CssSelector("div[id='channel-info'] ytd-channel-name a")).Text;
        var title = videos[i].FindElement(By.Id("video-title")).Text;
        var views = videos[i].FindElement(By.CssSelector("div[id='metadata'] div[id='metadata-line'] span")).Text;
        var link = videos[i].FindElement(By.Id("video-title")).GetAttribute("href");
        Console.WriteLine("video " + (i + 1));
        Console.WriteLine("Channel: " + channel);
        Console.WriteLine("Title: " + title);
        Console.WriteLine(views);
        Console.WriteLine("Link: " + link);
        Console.WriteLine("###############################");

        
        
        var youtube = new youtube
        {
            url = link,
            channel = channel,
            title = title,
            views = views,
        };
        list.Add(youtube);
        
    }


    driver.Quit();
    print(list);
}

void r6()
{
    String test_url_1 = "https://www.ubisoft.com/en-gb/game/rainbow-six/siege/game-info/operators";
    var list = new List<r6>();


    ChromeDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl(test_url_1);

    Thread.Sleep(3000);
    driver.FindElement(By.Id("privacy__modal__accept")).Click();

    


    Console.Clear();
    Console.WriteLine("Attackers: 1");
    Console.WriteLine("Defenderes: 2");
    string atdef = Console.ReadLine();
    if(atdef == "1")
    {
        driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[4]/div[1]/button[1]/span")).Click();

    }
    else
    {
        driver.FindElement(By.XPath("/html/body/div[1]/div[4]/div[4]/div[1]/button[2]/span")).Click();

    }

    IList<IWebElement> operators = driver.FindElements(By.CssSelector("div[class='oplist__cards__wrapper'] div"));
    Console.WriteLine(operators.Count);

    for(int i = 0; i < operators.Count; i++)
    {
        Console.WriteLine(operators[i].FindElement(By.CssSelector("a span")).Text + " :"+i);
    }


    Console.WriteLine("What operator would u like to have details of?");
    string input = Console.ReadLine();
    operators[int.Parse(input)].Click();

    Console.Clear();
    Thread.Sleep(3000);

    IWebElement loadout = driver.FindElement(By.CssSelector("div[class='operator__loadout']"));

    var name = loadout.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[3]/div[1]/h1")).Text;
    var squad = loadout.FindElement(By.XPath("/html/body/div[1]/div[4]/div/div[3]/div[2]/div[1]/div[2]/div[3]/span")).Text;
    var weapons = new List<string>();
    Console.WriteLine("Name: " + name);

    Console.WriteLine("Squad: " + squad);
    Console.WriteLine("weapons: ");
    foreach (IWebElement element in loadout.FindElements(By.CssSelector("div[class='operator__loadout__category__items'] div[class='operator__loadout__weapon']")))
    {
        weapons.Add(element.FindElement(By.CssSelector("div[class='operator__loadout__weapon'] p")).Text);
        Console.WriteLine(element.FindElement(By.CssSelector("div[class='operator__loadout__weapon'] p")).Text);

    }

    var r6 = new r6
    {
        name = name,
        weapons = weapons,
        squad = squad,
    };
    list.Add(r6);
    print(r6);

    driver.Quit();
}

void ictjob()
{
    var list = new List<jobs>();

    String test_url_1 = "https://www.ictjob.be/";


    ChromeDriver driver = new ChromeDriver();
    driver.Navigate().GoToUrl(test_url_1);

    Console.Clear();
    Console.WriteLine("What would u like to search on?");
    string input = Console.ReadLine();

    driver.Navigate().Refresh();
    IWebElement search = driver.FindElement(By.Name("keywords"));
    search.SendKeys(input);
    search.Submit();

    Thread.Sleep(5000);
    IWebElement filter = driver.FindElement(By.Id("sort-by-date"));
    filter.Click();
    Thread.Sleep(1000);
    IList<IWebElement> jobs = driver.FindElements(By.CssSelector("li span[class='job-info']"));
    Thread.Sleep(1000);

    Console.Clear();
    Console.WriteLine(jobs.Count);

    for (int i = 0; i <= 4; i++)
    {
        var title = jobs[i].FindElement(By.CssSelector("a h2[class='job-title']")).Text;
        var company = jobs[i].FindElement(By.CssSelector("span[class='job-company']")).Text;
        var location = jobs[i].FindElement(By.CssSelector("span[class='job-details'] span[class='job-location'] span span")).Text;
        var keywords = jobs[i].FindElement(By.CssSelector("span[class='job-keywords']")).Text;
        var link = jobs[i].FindElement(By.CssSelector("a")).GetAttribute("href");
        Console.WriteLine("Job " + (i + 1));
        Console.WriteLine("Title: " + title);
        Console.WriteLine("Company: " + company);
        Console.WriteLine("Location: " + location);

        Console.WriteLine("keywords: " + keywords);
        Console.WriteLine("Link: " + link);
        Console.WriteLine("###############################");

        var job = new jobs
        {
            link = link,
            company = company,
            title = title,
            location = location,
            keywords = keywords,
        };
        list.Add(job);

    }

    

    driver.Quit();
    print(list);

}

void csv(object obj)
{



    using (var writer = new CsvWriter(File.AppendText("C:/Thomas more/2022-2023/Devops & Sec/csvFile.csv"), CultureInfo.InvariantCulture))
    {
        // Write the object to the CSV file
        writer.WriteRecords((IEnumerable<object>)obj);
    }
    main();
}

void json(object obj)
{
    string filename = "C:/Thomas more/2022-2023/Devops & Sec/jsonFile.json";
    string jsonString = JsonSerializer.Serialize(obj);
    File.WriteAllText(filename, jsonString);
    main();
}

void print(object obj)
{
    Console.WriteLine("Webscraper");
    Console.WriteLine("##########");
    Console.WriteLine("Would u like to download the data?");
    Console.WriteLine("CSV: press C");
    Console.WriteLine("JSON job: press J");
    string print = Console.ReadLine();

    if (print.ToLower() == "c")
    {
        csv(obj);
    }
    else if (print.ToLower() == "j")
        json(obj);
    {
    }
}