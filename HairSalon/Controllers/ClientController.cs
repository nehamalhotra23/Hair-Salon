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
        

        public ActionResult Create(int id)
        {
            ViewBag.StylistId = id;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            _db.Client.Add(client);
            _db.SaveChanges();
            return RedirectToAction("Details", "Stylist", new { id = client.StylistId });
        }

        public ActionResult Details(int id)
        {
            Client thisClient = _db.Client.Include(client => client.Stylist).FirstOrDefault(stylist => stylist.ClientId == id);
             Console.WriteLine(thisClient);

            return View(thisClient);
        }
    }
}