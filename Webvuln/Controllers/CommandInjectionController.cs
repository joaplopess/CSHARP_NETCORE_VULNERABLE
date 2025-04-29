using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class CommandInjectionController : Controller
{
    public IActionResult Ping(string ipAddress)
    {
        // Command injection vulnerability
        var process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = $"/C ping {ipAddress}";
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        
        ViewBag.PingResult = output;
        return View("PingResult");
    }

}