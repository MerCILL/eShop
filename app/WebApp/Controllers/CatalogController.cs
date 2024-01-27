using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Abstractions;

namespace WebApp.Controllers;

public class CatalogController : Controller
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Catalog(int page = 1, int size = 5, string sort = "name")
    {
        try
        {
            if (!string.IsNullOrEmpty(sort))
            {
                HttpContext.Response.Cookies.Append("Sort", sort);
            }

            else if (Request.Cookies["Sort"] != null)
            {
                sort = Request.Cookies["Sort"];
            }

            var catalogItems = await _catalogService.GetCatalogItems(page, size, sort);
            ViewBag.Page = page;
            ViewBag.Size = size;
            return View(catalogItems);
        }
        catch (Exception)
        {
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
