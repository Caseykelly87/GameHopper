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

        public async Task<IActionResult> IndexAsync() {
            List<Rating>? ratingstars = context.Ratings.ToList();
            
            
            return View(ratingstars);
        }
    
    [HttpGet]
      public IActionResult Rate()
    {
        RatingViewModel ratingViewModel = new RatingViewModel();
            return View(ratingViewModel);
    }

    [HttpPost]
        public IActionResult Rate(RatingViewModel ratingViewModel)
    {
        if (ModelState.IsValid)
        {
            Rating newRating = new Rating
            {
                    Id = ratingViewModel.Id,
                    StarRating = ratingViewModel.StarRating
            };
            bool rating = context.Ratings.Any(x => x.StarRating == newRating.StarRating);


                    if (rating)
                    {

                        return Redirect("/Rating/Add/");
                        
                    }

                    else
                    {

                    context.Ratings.Add(newRating);
                    context.SaveChanges();
                    return Redirect("/Rating/");
                    }
        }
                    return View("Add", ratingViewModel);
        
    }
}
}
// public class RatingController : Controller
// {
//     private GameDbContext context;

//         public RatingController(GameDbContext dbContext)
//         {
//             context = dbContext;
//         }

//   public IActionResult CurrentRating()
//     {
//         RatingViewModel ratingViewModel = new RatingViewModel();
//             return View(ratingViewModel);
//     }

    // public IActionResult CurrentRating(RatingViewModel ratingViewModel)
    // {
    //     if (ModelState.IsValid)
    //     {
            
    //     }
        // get { return starRating; }
        // set
        // {
        //     if (value < 0 || value > 5)
        //         throw new ArgumentOutOfRangeException("Rating must be between 0 and 5.");

        //     starRating = value;
        //     UpdateStars();
        // }
    

    // private void InitializeStars()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         PictureBox pb = new PictureBox();
    //         pb.Size = new Size(32, 32); // Adjust size as needed
    //         pb.SizeMode = PictureBoxSizeMode.StretchImage;
    //         pb.Tag = i + 1; // Store the rating value in Tag property
    //         pb.Image = Properties.Resources.star_empty; // Change to your star image
    //         pb.Cursor = Cursors.Hand;
    //         pb.Click += Star_Click;
    //         flowLayoutPanelStars.Controls.Add(pb); // Assuming a FlowLayoutPanel named flowLayoutPanelStars
    //     }
    // }

//     private void UpdateStars()
//     {
//         foreach (Control control in flowLayoutPanelStars.Controls)
//         {
//             if (control is PictureBox pb)
//             {
//                 int ratingValue = (int)pb.Tag;
//                 if (ratingValue <= starRating)
//                     pb.Image = Properties.Resources.star_filled; // Change to your filled star image
//                 else
//                     pb.Image = Properties.Resources.star_empty; // Change to your empty star image
//             }
//         }
//     }

//     private void Star_Click(object sender, EventArgs e)
//     {
//         PictureBox clickedStar = sender as PictureBox;
//         int newRating = (int)clickedStar.Tag;
//         CurrentRating = newRating;
//     }
// }
// }




// public static double GetRating()
//         {
//             int star5 = 12801;
//             int star4 = 4982;
//             int star3 = 1251;
//             int star2 = 429;
//             int star1 = 1265;

//             double rating = (double)(5 * star5 + 4 * star4 + 3 * star3 + 2 * star2 + 1 * star1) / (star1 + star2 + star3 + star4 + star5);

//             rating = Math.Round(rating, 1);

//             return rating;
//         }

//         static void Main(string[] args)
//         {
//             double rating = GetRating();
//             Console.WriteLine("Your product rating: " + rating);
//             Console.ReadKey();
//         }
// }
        //need average rating space on games and DM's