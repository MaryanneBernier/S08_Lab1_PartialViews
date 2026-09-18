using Microsoft.AspNetCore.Mvc;
using ZombieParty.Models;
using ZombieParty.Models.Data;

namespace ZombieParty.Controllers
{
    public class HuntingLogController : Controller
    {
        // GET: HuntingLogController
        private ZombiePartyDbContext _baseDonnees { get; set; }
        public HuntingLogController(ZombiePartyDbContext baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }
        public ActionResult Index()
        {
            var huntingLogs = _baseDonnees.HuntingLogs.ToList();

            return View(huntingLogs);
        }

        // GET: HuntingLogController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HuntingLogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HuntingLogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HuntingLogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HuntingLogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HuntingLogController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HuntingLogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HuntingLogController/Upsert
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                // create
                return View(new HuntingLog()); // Envoie un objet vide, car la vue attend un objet HuntingLog
            else
                //update
                return View(_baseDonnees.HuntingLogs.Find(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Upsert(HuntingLog huntingLog)
        {
            if (ModelState.IsValid)
            {
                // Create
                if (huntingLog.Id == 0)
                {
                    // Ajouter à la BD
                    _baseDonnees.HuntingLogs.Add(huntingLog);
                    TempData["Success"] = $"{huntingLog.Title} hunting log added";
                }
                else
                {
                    // Update
                    _baseDonnees.HuntingLogs.Update(huntingLog);
                    TempData["success"] = $"{huntingLog.Title} hunting log updated";
                }
                _baseDonnees.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(huntingLog);
        }
    }
}
