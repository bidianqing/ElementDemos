using Dapper;
using Dapper.Contrib.Extensions;
using ElementDemos.Entities;
using ElementDemos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneAspNet.Repository.Dapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElementDemos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ConnectionFactory _connectionFactory;

        public HomeController(ILogger<HomeController> logger, ConnectionFactory connectionFactory)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await this.Menus();

            return View(menus);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IEnumerable<Menu>> Menus()
        {
            using var connection = _connectionFactory.CreateConnection();

            var allMenus = (await connection.GetAllAsync<Menu>()).ToList();

            List<Menu> results = new List<Menu>();
            results.AddRange(allMenus.FindAll(u => u.ParentId == 0));

            foreach (var item in results)
            {
                GetChildrenMenus(item);
            }


            void GetChildrenMenus(Menu menu)
            {
                var childrenMenus = allMenus.FindAll(u => u.ParentId == menu.Id);
                menu.Children = childrenMenus;

                foreach (var item in childrenMenus)
                {
                    GetChildrenMenus(item);
                }
            }

            return results;
        }

        public async Task<IEnumerable<Menu>> AllMenu()
        {
            using var connection = _connectionFactory.CreateConnection();

            var allMenu = await connection.GetAllAsync<Menu>();

            return allMenu;
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
