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
    
    public class OrderInfoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderInfoes
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var orderInfoes = db.OrderInfoes.Include(o => o.Order);
            return View(orderInfoes.ToList());
        }

        // GET: OrderInfoes/Details/5
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            return View(orderInfo);
        }

        // GET: OrderInfoes/Create
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create()
        {
            ViewBag.OrderInfoId = new SelectList(db.Orders, "OrderId", "OrderName");
            return View();
        }

        // POST: OrderInfoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create([Bind(Include = "OrderInfoId,ShippingAddress,TotalPrice")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                db.OrderInfoes.Add(orderInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderInfoId = new SelectList(db.Orders, "OrderId", "OrderName", orderInfo.OrderInfoId);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Edit/5
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderInfoId = new SelectList(db.Orders, "OrderId", "OrderName", orderInfo.OrderInfoId);
            return View(orderInfo);
        }

        // POST: OrderInfoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit([Bind(Include = "OrderInfoId,ShippingAddress,TotalPrice")] OrderInfo orderInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderInfoId = new SelectList(db.Orders, "OrderId", "OrderName", orderInfo.OrderInfoId);
            return View(orderInfo);
        }

        // GET: OrderInfoes/Delete/5
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            if (orderInfo == null)
            {
                return HttpNotFound();
            }
            return View(orderInfo);
        }

        // POST: OrderInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderInfo orderInfo = db.OrderInfoes.Find(id);
            db.OrderInfoes.Remove(orderInfo);
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
