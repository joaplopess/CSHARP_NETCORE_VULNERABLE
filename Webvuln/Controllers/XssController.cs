using Microsoft.AspNetCore.Mvc;

public class XssController : Controller
{
    public IActionResult Comment(string commentText)
    {
        // Stored XSS vulnerability - no output encoding
        ViewBag.Comment = commentText;
        return View("CommentPage");
    }

    public IActionResult Search(string query)
    {
        // Reflected XSS vulnerability
        ViewBag.SearchQuery = query;
        return View("SearchPage");
    }

    public IActionResult Profile(string username)
    {
        // DOM-based XSS vulnerability
        ViewBag.Username = username;
        return View("ProfilePage");
    }
}