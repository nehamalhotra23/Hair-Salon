using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientController(HairSalonContext db)
    {
      _db = db;
    }
    public ActionResult Index()
        {
            List<Client> model = _db.Client.Include(client => client.Stylist).ToList(); 
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylist, "StylistId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client clients)
        {
            _db.Client.Add(clients);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Client thisClient = _db.Client.FirstOrDefault(client => client.ClientId == id);
            
            return View(thisClient);
        }
  }
}