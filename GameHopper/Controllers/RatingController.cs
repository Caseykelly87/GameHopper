  using GameHopper.Models;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using GameHopper.ViewModels;
  namespace GameHopper.Controllers
{
    public  class RatingController : Controller
    {

        private GameDbContext context;

        public RatingController(GameDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Rating> ratings = context.Ratings.ToList();
            return View(ratings);
        }
    
    [HttpGet]
      public IActionResult Add()
    {
        RatingViewModel ratingViewModel = new RatingViewModel();
            return View(ratingViewModel);
    }

    [HttpPost]
        public IActionResult Add(RatingViewModel ratingViewModel)
    {
        if (ModelState.IsValid)
        {
            Rating newRating = new Rating
            {
                    Id = ratingViewModel.Id,
                    StarRating = ratingViewModel.StarRating
            };

                    context.Ratings.Add(newRating);
                    context.SaveChanges();
                    return Redirect("/Rating/");
                    
        }
                    return View("Add", ratingViewModel);
        
    }
}
}
