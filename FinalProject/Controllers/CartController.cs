﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Helpers;
using FinalProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class CartController : Controller
    {
        ApplicationDBContext context;
        public CartController()
        {
            context = new ApplicationDBContext();
        }
        public IActionResult Index()
        {

            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                return View("CartEmpty");
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Laptop.Price * item.Quantity);
                return View();
            }

        }

        public IActionResult Clear()
        {
            SessionHelper.setObjectAsJson(HttpContext.Session, "cart", null);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Buy(int id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Laptop = context.Laptops.Find(id), Quantity = 1 });
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExists(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Laptop = context.Laptops.Find(id), Quantity = 1 });
                }
                SessionHelper.setObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public int isExists(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Laptop.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public IActionResult Checkout()
        {
            if (SessionHelper.GetObjectFromJson<User>(HttpContext.Session, "user") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }

        }
    }
}
