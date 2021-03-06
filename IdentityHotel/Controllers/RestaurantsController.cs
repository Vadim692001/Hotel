﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IdentityHotel.Models;

namespace IdentityHotel.Controllers
{
    public class RestaurantsController : Controller
    {
        private Hotel_Restor_DiplomEntities db = new Hotel_Restor_DiplomEntities();

        // GET: Restaurants
      
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            var restaurant = db.Restaurant.Include(r => r.Employee).Include(r => r.NameRestaurant);
            return View(restaurant.ToList());
        }

        // GET: Restaurants/Details/5
        [Authorize(Roles = "hairline")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        [Authorize(Roles = "hairline")]
        public ActionResult Create()
        {
            ViewBag.id_Pracivnuka = new SelectList(db.Employee, "id_Pracivnuca", "Sorname_Prac");
            ViewBag.idRestaurant = new SelectList(db.NameRestaurant, "id_Restaurant", "NameRestaurant1");
            return View();
        }

        // POST: Restaurants/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "hairline")]
        public ActionResult Create([Bind(Include = "idRestaurant,EatingTime,Data,id_Pracivnuka,District,NumberPeople")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurant.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Pracivnuka = new SelectList(db.Employee, "id_Pracivnuca", "Sorname_Prac", restaurant.id_Pracivnuka);
            ViewBag.idRestaurant = new SelectList(db.NameRestaurant, "id_Restaurant", "NameRestaurant1", restaurant.idRestaurant);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [Authorize(Roles = "hairline")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Pracivnuka = new SelectList(db.Employee, "id_Pracivnuca", "Sorname_Prac", restaurant.id_Pracivnuka);
            ViewBag.idRestaurant = new SelectList(db.NameRestaurant, "id_Restaurant", "NameRestaurant1", restaurant.idRestaurant);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "hairline")]
        public ActionResult Edit([Bind(Include = "idRestaurant,EatingTime,Data,id_Pracivnuka,District,NumberPeople")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Pracivnuka = new SelectList(db.Employee, "id_Pracivnuca", "Sorname_Prac", restaurant.id_Pracivnuka);
            ViewBag.idRestaurant = new SelectList(db.NameRestaurant, "id_Restaurant", "NameRestaurant1", restaurant.idRestaurant);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = "hairline")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurant.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "hairline")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurant.Find(id);
            db.Restaurant.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
