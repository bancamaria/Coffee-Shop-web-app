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
    public class ProductOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductOrders
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var productOrders = db.ProductOrders.Include(p => p.Order).Include(p => p.Product);
            return View(productOrders.ToList());
        }

        // GET: ProductOrders/Details?productId={productId}&orderId={orderId}
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Details(int? productId, int? orderId)
        {
            if (productId == null || orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(productId, orderId);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // GET: ProductOrders/Create
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderName");
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: ProductOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Create([Bind(Include = "ProductId,OrderId,Quantity")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                db.ProductOrders.Add(productOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderName", productOrder.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productOrder.ProductId);
            return View(productOrder);
        }

        // GET: ProductOrders/Edit?productId={productId}&orderId={orderId}
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit(int? productId, int? orderId)
        {
            if (productId == null || orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(productId, orderId);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderName", productOrder.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productOrder.ProductId);
            return View(productOrder);
        }

        // POST: ProductOrders/Edit?productId={productId}&orderId={orderId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Edit([Bind(Include = "ProductId,OrderId,Quantity")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderName", productOrder.OrderId);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productOrder.ProductId);
            return View(productOrder);
        }

        // GET: ProductOrders/Delete?productId={productId}&orderId={orderId}
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult Delete(int? productId, int? orderId)
        {
            if (productId == null || orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOrder productOrder = db.ProductOrders.Find(productId, orderId);
            if (productOrder == null)
            {
                return HttpNotFound();
            }
            return View(productOrder);
        }

        // POST: ProductOrders/Delete?productId={productId}&orderId={orderId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client,Administrator")]
        public ActionResult DeleteConfirmed(int productId, int orderId)
        {
            ProductOrder productOrder = db.ProductOrders.Find(productId, orderId);
            db.ProductOrders.Remove(productOrder);
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
