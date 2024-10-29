using infolog_asp_fab.Data;
using infolog_asp_fab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace infolog_asp_fab.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Afficher la liste des clients
        public async Task<IActionResult> Index()
        {
            var clients = await _context.Client.ToListAsync();
            return View(clients);
        }

        // Récupérer les détails d'un client (GET) pour AJAX
        [HttpGet]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return Json(new { success = false, message = "Client introuvable." });
            }

            return Json(new { success = true, data = client });
        }

        // Ajouter un client (POST) via AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Client.Add(client);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.InnerException?.Message ?? ex.Message });
                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
            return Json(new { success = false, errors });
        }

        /// Modifier un client (POST) via AJAX
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Json(client); // Retourne les données du client en JSON
        }
        //

        [HttpPost]
        public async Task<IActionResult> Edit_clients(Client client)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Les données sont invalides.", errors = errors });
            }

            var existingClient = await _context.Client.FindAsync(client.IdClients);
            if (existingClient == null)
            {
                return Json(new { success = false, message = "Client introuvable." });
            }

            // Met à jour les informations du client
            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.Email = client.Email;
            existingClient.PhoneNumber = client.PhoneNumber;

            try
            {
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return Json(new { success = false, message = "Client introuvable." });
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
