using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebPchelki.Models;
using WebPchelki.Models.Entities;

namespace WebPchelki.Controllers
{
    public class HomeController : Controller
    {
        public PchelkiContext db;
        public HomeController(PchelkiContext context) { db = context; }

        [HttpGet]
        public IActionResult Index(string search)
        {
            if (search != null)
            {
                return View(db.Products.Where(p => p.Name.Contains(search)).ToList());
            }
            return View();
        }

        #region Page
        public IActionResult Page(int? page, string search)
        {
            switch (page)
            {
                case 1:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Мёд").ToList());
                case 2:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Воск").ToList());
                case 3:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Перга").ToList());
                case 4:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Забрус").ToList());
                case 5:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Прополис").ToList());
                case 6:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Маточное молочко").ToList());
                case 7:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Пчелиный яд").ToList());
                case 8:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Мерва").ToList());
                case 9:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Пыльца").ToList());
                case 10:
                    return View(db.Products.Where(product => product.TypeProduct.Name == "Гомогенат").ToList());
                default:
                    break;
            }

            if (search != null)
            {
                return View(db.Products.Where(p => p.Name.Contains(search)).ToList());
            }
            return View(db.Products.ToList());
        }
        #endregion

        #region Bee-Beehive-Equipment Page
        public IActionResult BeePage()
        {
            return View(db.Bees.ToList());
        }
        public IActionResult BeehivePage()
        {
            return View(db.Beehives.ToList());
        }
        public IActionResult EquipmentPage()
        {
            return View(db.Equipments.ToList());
        }
        #endregion


        #region Add
        public IActionResult Add() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Add(string type, string name, int number)
        {
            TypeProduct t = db.TypeProducts.FirstOrDefault(typeP => typeP.Name == type);
            if (t == null)
            {
                TypeProduct typeProduct = new() { Name = type };
                db.TypeProducts.Add(typeProduct);
                await db.SaveChangesAsync();
                type = typeProduct.Name;
            }
;
            Product product = new()
            {
                Name = name,
                Number = number,
                TypeProductId = db.TypeProducts.FirstOrDefault(typeProduct => typeProduct.Name == type).Id
            };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "Page", page = product.TypeProductId });
        }
        #endregion

        #region BeeAdd
        public IActionResult BeeAdd() { return View(); }
        [HttpPost]
        public async Task<IActionResult> BeeAdd(string name, int productivity)
        {
            Bee bee = new()
            {
                Name = name,
                Productivity = productivity
            };
            db.Bees.Add(bee);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "BeePage" });
        }
        #endregion

        #region BeehiveAdd
        public IActionResult BeehiveAdd() { return View(); }
        [HttpPost]
        public async Task<IActionResult> BeehiveAdd(Beehive beehive)
        {
            db.Beehives.Add(beehive);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "BeehivePage" });
        }
        #endregion

        #region EquipmentAdd
        public IActionResult EquipmentAdd() { return View(); }
        [HttpPost]
        public async Task<IActionResult> EquipmentAdd(string name, int number)
        {
            Equipment equipment = new()
            {
                Name = name,
                Number = number
            };
            db.Equipments.Add(equipment);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "EquipmentPage" });
        }
        #endregion


        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.Include(p => p.CommentProducts).FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        #endregion

        #region BeeDetails
        public async Task<IActionResult> BeeDetails(int? id)
        {
            if (id != null)
            {
                Bee bee = await db.Bees.Include(p => p.CommentBees).FirstOrDefaultAsync(p => p.Id == id);
                if (bee != null)
                    return View(bee);
            }
            return NotFound();
        }
        #endregion

        #region BeehiveDetails
        public async Task<IActionResult> BeehiveDetails(int? id)
        {
            if (id != null)
            {
                Beehive beehive = await db.Beehives.Include(p => p.CommentBeehives).FirstOrDefaultAsync(p => p.Id == id);
                if (beehive != null)
                    return View(beehive);
            }
            return NotFound();
        }
        #endregion

        #region EquipmentDetails
        public async Task<IActionResult> EquipmentDetails(int? id)
        {
            if (id != null)
            {
                Equipment equipment = await db.Equipments.Include(p => p.CommentEquipments).FirstOrDefaultAsync(p => p.Id == id);
                if (equipment != null)
                    return View(equipment);
            }
            return NotFound();
        }
        #endregion


        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            db.Products.Update(product);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "Details", id = product.Id });
        }
        #endregion

        #region BeeEdit
        [HttpGet]
        public async Task<IActionResult> BeeEdit(int? id)
        {
            if (id != null)
            {
                Bee bee = await db.Bees.FirstOrDefaultAsync(p => p.Id == id);
                if (bee != null)
                    return View(bee);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> BeeEdit(Bee bee)
        {
            db.Bees.Update(bee);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "BeeDetails", id = bee.Id });
        }
        #endregion

        #region BeehiveEdit
        [HttpGet]
        public async Task<IActionResult> BeehiveEdit(int? id)
        {
            if (id != null)
            {
                Beehive beehive = await db.Beehives.FirstOrDefaultAsync(p => p.Id == id);
                if (beehive != null)
                    return View(beehive);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> BeehiveEdit(Beehive beehive)
        {
            db.Beehives.Update(beehive);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "BeehiveDetails", id = beehive.Id });
        }
        #endregion

        #region EquipmentEdit
        [HttpGet]
        public async Task<IActionResult> EquipmentEdit(int? id)
        {
            if (id != null)
            {
                Equipment equipment = await db.Equipments.FirstOrDefaultAsync(p => p.Id == id);
                if (equipment != null)
                    return View(equipment);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EquipmentEdit(Equipment equipment)
        {
            db.Equipments.Update(equipment);
            await db.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Home", action = "EquipmentDetails", id = equipment.Id });
        }
        #endregion


        #region Delete
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product product = new() { Id = id.Value };
                db.Entry(product).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "Index"});
            }
            return NotFound();
        }
        #endregion

        #region BeeDelete
        [HttpGet]
        [ActionName("BeeDelete")]
        public async Task<IActionResult> ConfirmBeeDelete(int? id)
        {
            if (id != null)
            {
                Bee bee = await db.Bees.FirstOrDefaultAsync(p => p.Id == id);
                if (bee != null)
                    return View(bee);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> BeeDelete(int? id)
        {
            if (id != null)
            {
                Bee bee = new() { Id = id.Value };
                db.Entry(bee).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "BeePage" });
            }
            return NotFound();
        }
        #endregion

        #region BeehiveDelete
        [HttpGet]
        [ActionName("BeehiveDelete")]
        public async Task<IActionResult> ConfirmBeehiveDelete(int? id)
        {
            if (id != null)
            {
                Beehive beehive = await db.Beehives.FirstOrDefaultAsync(p => p.Id == id);
                if (beehive != null)
                    return View(beehive);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> BeehiveDelete(int? id)
        {
            if (id != null)
            {
                Beehive beehive = new() { Id = id.Value };
                db.Entry(beehive).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "BeehivePage" });
            }
            return NotFound();
        }
        #endregion

        #region EquipmentDelete
        [HttpGet]
        [ActionName("EquipmentDelete")]
        public async Task<IActionResult> ConfirmEquipmentDelete(int? id)
        {
            if (id != null)
            {
                Equipment equipment = await db.Equipments.FirstOrDefaultAsync(p => p.Id == id);
                if (equipment != null)
                    return View(equipment);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EquipmentDelete(int? id)
        {
            if (id != null)
            {
                Equipment equipment = new() { Id = id.Value };
                db.Entry(equipment).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "EquipmentPage" });
            }
            return NotFound();
        }
        #endregion


        #region CreateComment
        public async Task<IActionResult> CreateComment(int id, string description)
        {
            CommentProduct commentProduct = new()
            {
                ProductId = id,
                Description = description,
                Date = DateTime.Now
            };
            db.CommentProducts.Add(commentProduct);
            await db.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Home", action = "Details", id = id });
        }
        #endregion

        #region CreateCommentBee
        public async Task<IActionResult> CreateCommentBee(int id, string description)
        {
            CommentBee commentBee = new()
            {
                BeeId = id,
                Description = description,
                Date = DateTime.Now
            };
            db.CommentBees.Add(commentBee);
            await db.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Home", action = "BeeDetails", id = id });
        }
        #endregion

        #region CreateCommentBeehive
        public async Task<IActionResult> CreateCommentBeehive(int id, string description)
        {
            CommentBeehive commentBeehive = new()
            {
                BeehiveId = id,
                Description = description,
                Date = DateTime.Now
            };
            db.CommentBeehives.Add(commentBeehive);
            await db.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Home", action = "BeehiveDetails", id = id });
        }
        #endregion

        #region CreateCommentEquipment
        public async Task<IActionResult> CreateCommentEquipment(int id, string description)
        {
            CommentEquipment commentEquipment = new()
            {
                EquipmentId = id,
                Description = description,
                Date = DateTime.Now
            };
            db.CommentEquipments.Add(commentEquipment);
            await db.SaveChangesAsync();

            return RedirectToRoute(new { controller = "Home", action = "EquipmentDetails", id = id });
        }
        #endregion


        #region DeleteComment
        public async Task<IActionResult> DeleteComment(int? comId, int prodId)
        {
            if (comId != null)
            {
                CommentProduct commentProduct = new() { Id = comId.Value };
                db.Entry(commentProduct).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "Details", id = prodId });
            }
            return NotFound();
        }
        #endregion

        #region DeleteCommentBee
        public async Task<IActionResult> DeleteCommentBee(int? comId, int beeId)
        {
            if (comId != null)
            {
                CommentBee commentBee = new() { Id = comId.Value };
                db.Entry(commentBee).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "BeeDetails", id = beeId });
            }
            return NotFound();
        }
        #endregion

        #region DeleteCommentBeehive
        public async Task<IActionResult> DeleteCommentBeehive(int? comId, int beehId)
        {
            if (comId != null)
            {
                CommentBeehive commentBeehive = new() { Id = comId.Value };
                db.Entry(commentBeehive).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "BeehiveDetails", id = beehId });
            }
            return NotFound();
        }
        #endregion

        #region DeleteCommentEquipment
        public async Task<IActionResult> DeleteCommentEquipment(int? comId, int eqId)
        {
            if (comId != null)
            {
                CommentEquipment commentEquipment = new() { Id = comId.Value };
                db.Entry(commentEquipment).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToRoute(new { controller = "Home", action = "EquipmentDetails", id = eqId });
            }
            return NotFound();
        }
        #endregion
    }
}
