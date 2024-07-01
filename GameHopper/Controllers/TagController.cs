// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using GameHopper.Models;


// // For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// namespace GameHopper.Controllers
// {
//     public class TagController : Controller
//     {
//         private GameDbContext context;

//         public TagController(GameDbContext dbContext)
//         {
//             context = dbContext;
//         }

//         // GET: /<controller>/
//         public IActionResult Index()
//         {
//             List<Tag> tags = context.Tags.ToList();
//             return View(tags);
//         }

//         [HttpGet]
//         public IActionResult Add()
//         {
//             Tag tag = new Tag();
//             return View(tag);
//         }

//         [HttpPost]
//         public IActionResult Add(Tag tags)
//         {
//             if (ModelState.IsValid)
//             {
//                 context.Skills.Add(Tag);
//                 context.SaveChanges();

//                 return Redirect("/Tags/");
//             }

//             return View("Add", tags);
//         }

//         [HttpGet]
//         public IActionResult AddTag(int id)
//         {
//             Tag theTag = context.Tags.Find(id);
//             List<Tag> possibleTags = context.Tags.ToList();

//             AddTagViewModel addSkillViewModel = new AddTagViewModel(theTag, possibleTags);

//             return View(addTagViewModel);
//         }

//         [HttpPost]
//         public IActionResult AddTag(AddTagViewModel addTagViewModel)
//         {
//             if (ModelState.IsValid)
//             {
//                 int tagId = addTagViewModel.TagId;

//                 Job theJob = context.Jobs.Include(s => s.Skills).Where(j => j.Id == jobId).First();
//                 Skill theSkill = context.Skills.Where(s => s.Id == skillId).First();

//                 theJob.Skills.Add(theSkill);

//                 context.SaveChanges();

//                 return Redirect("/Job/Detail/" + jobId);
//             }

//             return View(addSkillViewModel);
//         }

//         public IActionResult Detail(int id)
//         {
//             Skill theSkill = context.Skills.Include(j => j.Jobs).Where(s => s.Id == id).First();

//             return View(theSkill);
//         }
//     }
// }
