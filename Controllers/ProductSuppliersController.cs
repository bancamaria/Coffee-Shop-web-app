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
    public class ProductSuppliersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductSuppliers
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var productSuppliers = db.ProductSuppliers.Include(p => p.Product).Include(p => p.Supplier);
            return View(productSuppliers.ToList());
        }

        // GET: ProductSuppliers/Details?productId={productId}&supplierId={supplierId}
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? productId, int? supplierId)
        {
            if (productId == null || supplierId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier productSupplier = db.ProductSuppliers.Find(productId, supplierId);
            if (productSupplier == null)
            {
                return HttpNotFound();
            }
            return View(productSupplier);
        }

        // GET: ProductSuppliers/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName");
            return View();
        }

        // POST: ProductSuppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ProductId,SupplierId,Quantity")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                db.ProductSuppliers.Add(productSupplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSupplier.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", productSupplier.SupplierId);
            return View(productSupplier);
        }

        // GET: ProductSuppliers/Edit?productId={productId}&supplierId={supplierId}
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? productId, int? supplierId)
        {
            if (productId == null || supplierId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier productSupplier = db.ProductSuppliers.Find(productId, supplierId);
            if (productSupplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSupplier.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", productSupplier.SupplierId);
            return View(productSupplier);
        }

        // POST: ProductSuppliers/Edit?productId={productId}&supplierId={supplierId}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ProductId,SupplierId,Quantity")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSupplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "ProductName", productSupplier.ProductId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "SupplierName", productSupplier.SupplierId);
            return View(productSupplier);
        }

        // GET: ProductSuppliers/Delete?productId={productId}&supplierId={supplierId}
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? productId, int? supplierId)
        {
            if (productId == null || supplierId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier productSupplier = db.ProductSuppliers.Find(productId, supplierId);
            if (productSupplier == null)
            {
                return HttpNotFound();
            }
            return View(productSupplier);
        }

        // POST: ProductSuppliers/Delete?productId={productId}&supplierId={supplierId}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int productId, int supplierId)
        {
            ProductSupplier productSupplier = db.ProductSuppliers.Find(productId, supplierId);
            db.ProductSuppliers.Remove(productSupplier);
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
