using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models
{
    public class Product
    {
        [Column("product_id")]
        public int ProductId { get; set; }

        [Required]
        [Column("product_name")]
        [Index(IsUnique = true)]
        [MinLength(4, ErrorMessage = "The product name must contain at least 4 characters!"), MaxLength(20, ErrorMessage = "The product name must contain at most 20 characters!")]
        public string ProductName { get; set; }

        [Required]
        [Column("weight")]
        [PositiveNonZeroNumber(ErrorMessage = "The weight must be a positive non-zero number!")]
        public double Weight { get; set; }

        [Required]
        [Column("price")]
        [PositiveNonZeroNumber(ErrorMessage = "The price must be a positive non-zero number!")]
        public double Price { get; set; }

        [Required]
        [Column("country_of_origin")]
        [RegularExpression(@"^[\w\s]{4,20}$", ErrorMessage = "The country name must contain between 4 and 20 alphanumeric character!")]
        public string CountryOfOrigin { get; set; }

        public enum GrindTypeEnum
        {
            Espresso,
            Coarse,
            WholeBean
        }

        [Required]
        [Column("grind_type")]
        public GrindTypeEnum GrindType { get; set; }

        // many-to-many relationship with Supplier
        public virtual ICollection<ProductSupplier> Suppliers { get; set; }

        // many-to-many relationship with Order
        public virtual ICollection<ProductOrder> Orders { get; set; }
    }

    public class Order
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("order_name")]
        [Index(IsUnique = true)]
        [MinLength(4, ErrorMessage = "The order name must contain at least 4 characters!"), MaxLength(20, ErrorMessage = "The order name must contain at most 20 characters!")]
        public string OrderName { get; set; }

        // many-to-many relationship with Product
        public virtual ICollection<ProductOrder> Products { get; set; }

        // one-to-one relationship with OrderInfo
        public virtual OrderInfo OrderInfo { get; set; }

        // many-to-one relationship with ApplicationUser
        [Required]
        [Column("client_id")]
        [ForeignKey("Client")]
        public string ClientId { get; set; }
        public virtual ApplicationUser Client { get; set; }
    }

    public class OrderInfo
    {
        [Column("order_info_id")]
        [ForeignKey("Order")]
        public int OrderInfoId { get; set; }

        [Column("shipping_address")]
        [RegularExpression(@"^[\w\s;\.,-]{10,500}$", ErrorMessage = "The shipping address must contain between 10 and 500 alphanumeric characters, including whitespaces, semicolons, colons,commas and dashes!")]
        public string ShippingAddress { get; set; }

        [Column("total_price")]
        [PositiveNonZeroNumber(ErrorMessage = "The total price must be a positive non-zero number!")]
        public double TotalPrice { get; set; }

        // one-to-one relationship with Order
        public virtual Order Order { get; set; }
    }

    public class ProductOrder
    {
        // many-to-one relationship with Product
        [Key]
        [Column("product_id", Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // many-to-one relationship with Order
        [Key]
        [Column("order_id", Order = 2)]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        [Column("quantity")]
        [PositiveNonZeroNumber(ErrorMessage = "The quantity must be a positive non-zero number!")]
        public int Quantity { get; set; }
    }

    public class Supplier
    {
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [Required]
        [Column("supplier_name")]
        [Index(IsUnique = true)]
        [MinLength(10, ErrorMessage = "The supplier name must contain at least 10 characters!"), MaxLength(50, ErrorMessage = "The supplier name must contain at most 50 characters")]
        public string SupplierName { get; set; }

        [Required]
        [Column("country_of_origin")]
        [RegularExpression(@"^[\w\s]{4,20}$", ErrorMessage = "The country name must contain between 4 and 20 alphanumeric character!")]
        public string CountryOfOrigin { get; set; }

        // many-to-many relationship with Product
        public virtual ICollection<ProductSupplier> Products { get; set; }
    }

    public class ProductSupplier
    {
        // many-to-one relationship with Product
        [Key]
        [Column("product_id", Order = 1)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // many-to-one relationship with Supplier
        [Key]
        [Column("supplier_id", Order = 2)]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Column("quantity")]
        [PositiveNonZeroNumber(ErrorMessage = "The quantity must be a positive non-zero number!")]
        [RegularExpression(@"^\d{1,3}$", ErrorMessage = "The maximum quantity value is 999 !")]
        public int Quantity { get; set; }
    }
}