﻿using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(); // Looks for Views/Home/Index.cshtml
    }
}