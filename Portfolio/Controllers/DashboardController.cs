using Microsoft.AspNetCore.Mvc;


namespace Portfolio.Controllers
{
    public class DashboardController : Controller          
    {
        private readonly BlobStorageService _blobStorageService;

        public DashboardController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult AboutMe()
        {
            return View();
        }

        public IActionResult EducationDetails()
        {
            return View();
        }

        public IActionResult SendMessage()
        {
            // Render the Azure Blob Storage image
            string blobName = "illustration-boy-reading-book-white-background-flat-style_1368353-116.png";
            string imageUrl = _blobStorageService.GetBlobUrl(blobName);
            ViewData["ImageUrl"] = imageUrl;

            return View();
        }
    }
}
