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
        [HttpPost]
        public ActionResult Index(string searchType, string value)
        {
            List<Client> model = _db.Client.Include(clients => clients.Stylist).ToList();
            if (searchType == "stylists")
            {
                int stylists = Int32.Parse(value);
                model = _db.Client.Where(client => client.StylistId == stylists).Select(stylist => stylist).ToList();
            }
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.StylistId = new SelectList(_db.Stylist, "StylistId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client client)
        {
            _db.Client.Add(client);
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