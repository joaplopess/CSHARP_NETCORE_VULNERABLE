using Microsoft.AspNetCore.Mvc;
using System.IO;

public class PathTraversalController : Controller
{
    private readonly string _basePath;

    public PathTraversalController(IConfiguration config)
    {
        _basePath = config["FileStorage:BasePath"];
    }

    public IActionResult Download(string fileName)
    {
        // Path traversal vulnerability
        var filePath = Path.Combine(_basePath, fileName);
        return PhysicalFile(filePath, "application/octet-stream");
    }

    public IActionResult ViewFile(string file)
    {
        // Another path traversal example
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file);
        var content = System.IO.File.ReadAllText(fullPath);
        ViewBag.FileContent = content;
        return View("FileViewer");
    }
}