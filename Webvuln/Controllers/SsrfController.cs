using Microsoft.AspNetCore.Mvc;
using System.Net;

public class SsrfController : Controller
{
    public IActionResult FetchUrl(string url)
    {
        // Basic SSRF vulnerability
        var client = new WebClient();
        var response = client.DownloadString(url);
        ViewBag.Content = response;
        return View("UrlContent");
    }

}