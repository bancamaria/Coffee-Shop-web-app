using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        [Authorize(Roles = "Administrator,Client")]
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Client).Include(o => o.OrderInfo);
            return View(orders.ToList());
        }

        // GET: Orders/Details
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Email");
            ViewBag.OrderId = new SelectList(db.OrderInfoes, "OrderInfoId", "OrderInfoId");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create([Bind(Include = "OrderId,OrderName,ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Users, "Id", "Email", order.ClientId);
            ViewBag.OrderId = new SelectList(db.OrderInfoes, "OrderInfoId", "OrderInfoId", order.OrderId);
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Email", order.ClientId);
            ViewBag.OrderId = new SelectList(db.OrderInfoes, "OrderInfoId", "OrderInfoId", order.OrderId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit([Bind(Include = "OrderId,OrderName,ClientId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Users, "Id", "Email", order.ClientId);
            ViewBag.OrderId = new SelectList(db.OrderInfoes, "OrderInfoId", "OrderInfoId", order.OrderId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
