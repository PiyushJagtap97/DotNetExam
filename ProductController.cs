using DotNetExam.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetExam.Controllers
{


    public class ProductController : Controller
    {

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        // con.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;"

        SqlCommand cmd = new SqlCommand();
        // GET: Product
        public ActionResult Index()
        {
            //select ProductId,ProductName,Rate,Description,CategoryName from Products natural join Categories where Products.CategoryId=Categories.CategoryId
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Product,Categories where Product.CategoryId=Categories.CategoryId";
            List<Product> pds = new List<Product>();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    pds.Add(new Product { ProductId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], Rate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategryName = (string)dr["CategryName"] });

                }
                dr.Close();

            }
            catch (Exception ex)
            {
                ViewBag.err = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return View(pds);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.list = GetCategories();
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Products prd)
        {

            con.Open();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "insert into Product values(@ProductName,@Rate,@Description,@CategoryId)";

            cmd.Parameters.AddWithValue("@ProductName", prd.ProductName);
            cmd.Parameters.AddWithValue("@Rate", prd.Rate);
            cmd.Parameters.AddWithValue("@Description", prd.Description);
            cmd.Parameters.AddWithValue("@CategoryId", prd.CategryId);

            try
            {
                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.err = ex;
                return View();
            }
            finally
            {

                con.Close();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.list = GetCategories();
            con.Open();
            cmd.Connection = con;

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select * from Product where ProductId= @ProductId";
            cmd.Parameters.AddWithValue("@ProductId", id);

            Product pobj = null;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    pobj = new Product { ProductId = (int)dr["ProductId"], ProductName = (string)dr["ProductName"], Rate = (decimal)dr["Rate"], Description = (string)dr["Description"], CategoryId = (int)dr["CategoryId"] };
                dr.Close();
            }
            catch (Exception ex)
            {
                ViewBag.err = ex.Message;
            }
            finally
            {
                con.Close();
            }

            return View(pobj);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product obj)
        {
            con.Open();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "update Product set ProductName=@ProductName,Rate=@Rate,Description=@Description,CategoryId=@CategoryId";
            cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
            cmd.Parameters.AddWithValue("@Rate", obj.Rate);
            cmd.Parameters.AddWithValue("@Description", obj.Description);
            cmd.Parameters.AddWithValue("@CategoryId", obj.CategoryId);

            try
            {
                cmd.ExecuteNonQuery();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            finally
            {
                con.Close();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}