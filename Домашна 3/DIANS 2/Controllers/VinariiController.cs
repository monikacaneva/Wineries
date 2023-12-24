using DIANS_2.Migrations;
using DIANS_2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace DIANS_2.Controllers
{
    public class VinariiController : Controller
    {

        public ActionResult Contact()
        {
            var wineries = ReadCsvFile("vinarii.csv");

            return View(wineries);
        }


        private List<Winery> ReadCsvFile(string fileName)
        {
            List<Winery> wineries = new List<Winery>();

            string filePath = HostingEnvironment.MapPath($"~/{fileName}");

            if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
            {
                try
                {
                    var lines = System.IO.File.ReadAllLines(filePath);

                    foreach (var line in lines.Skip(1))
                    {
                        var values = line.Split(',');
                        if (values.Length >= 3)
                        {
                            var winery = new Winery
                            {
                                Name = values[0],
                                City = values[1],
                                Address = values[2],
                                Rating = 0, // You can set a default rating here if needed
                            };
                            wineries.Add(winery);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately (e.g., log it)
                }
            }

            return wineries;
        }
        public ActionResult WineryListCity(string city)
        {
            var wineries = ReadCsvFile("vinarii.csv")
                .Where(w => string.Equals(w.City, city, StringComparison.OrdinalIgnoreCase))
                .ToList();

            ViewBag.City = city;

            return View("WineryListCity", wineries);
        }
        [Authorize]
        public ActionResult Edit(string id)
        {
            var winery = ReadCsvFile("vinarii.csv").FirstOrDefault(w => string.Equals(w.Name, id, StringComparison.OrdinalIgnoreCase));

            if (winery == null)
            {
                return HttpNotFound();
            }

            return View(winery);
        }

        [HttpPost]
        public ActionResult Update(Winery winery)
        {
            var wineries = ReadCsvFile("vinarii.csv");

            var existingWinery = wineries.FirstOrDefault(w => string.Equals(w.Name, winery.Name, StringComparison.OrdinalIgnoreCase));
            if (existingWinery != null)
            {
                existingWinery.City = winery.City;
                existingWinery.Address = winery.Address;

                // Save changes to the CSV file
                WriteCsvFile("vinarii.csv", wineries);
            }

            // Redirect to the Contact action to display the updated information
            return RedirectToAction("Contact");
        }
        private void WriteCsvFile(string fileName, List<Winery> wineries)
        {
            string filePath = HostingEnvironment.MapPath($"~/{fileName}");

            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    var lines = new List<string> { "Name,City,Address" };
                    lines.AddRange(wineries.Select(w => $"{w.Name},{w.City},{w.Address}"));
                    System.IO.File.WriteAllLines(filePath, lines);
                }
                catch (Exception ex)
                {
                    // Handle the exception appropriately (e.g., log it)
                }
            }
        }
        [Authorize]

            public ActionResult Remove(string id)
        {
            var winery = ReadCsvFile("vinarii.csv").FirstOrDefault(w => string.Equals(w.Name, id, StringComparison.OrdinalIgnoreCase));

            if (winery == null)
            {
                return HttpNotFound();
            }

            return View(winery);
        }
        [Authorize]

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var wineries = ReadCsvFile("vinarii.csv");

            var wineryToRemove = wineries.FirstOrDefault(w => string.Equals(w.Name, id, StringComparison.OrdinalIgnoreCase));
            if (wineryToRemove != null)
            {
                wineries.Remove(wineryToRemove);

                // Save changes to the CSV file
                WriteCsvFile("vinarii.csv", wineries);

                // Redirect back to the "Contact" action with the city parameter if available
                if (!string.IsNullOrEmpty(wineryToRemove.City))
                {
                    return RedirectToAction("Contact", new { city = wineryToRemove.City });
                }
                else
                {
                    return RedirectToAction("Contact");
                }
            }

            return RedirectToAction("Contact"); // Handle the case when wineryToRemove is null
        }
        [Authorize]

        public ActionResult Rate(string id)
        {
            var winery = ReadCsvFile("vinarii.csv").FirstOrDefault(w => string.Equals(w.Name, id, StringComparison.OrdinalIgnoreCase));

            if (winery == null)
            {
                return HttpNotFound();
            }

            return View(winery);
        }

        [Authorize]

        [HttpPost]
        public ActionResult Rate(Winery updatedWinery)
        {
            if (ModelState.IsValid)
            {
                var wineries = ReadCsvFile("vinarii.csv");

                var existingWinery = wineries.FirstOrDefault(w => string.Equals(w.Name, updatedWinery.Name, StringComparison.OrdinalIgnoreCase));
                if (existingWinery != null)
                {
                    existingWinery.Rating = updatedWinery.Rating;
                }

                // Pass the updated wineries list to the Contact view
                return View("Contact", wineries);
            }

            // If ModelState is not valid, return the view with validation errors
            return View(updatedWinery);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpdateRating(string id, float rating)
        {
            var wineries = ReadCsvFile("vinarii.csv");

            var existingWinery = wineries.FirstOrDefault(w => string.Equals(w.Name, id, StringComparison.OrdinalIgnoreCase));
            if (existingWinery != null)
            {
                existingWinery.Rating = rating;

                WriteCsvFile("vinarii.csv", wineries);
            }

            // Redirect to the Contact action with the city parameter if available
            if (!string.IsNullOrEmpty(existingWinery?.City))
            {
                return RedirectToAction("Contact", new { city = existingWinery.City });
            }

            return RedirectToAction("Contact");
        }

    }
}
