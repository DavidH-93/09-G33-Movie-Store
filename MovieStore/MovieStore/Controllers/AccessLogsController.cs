using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MovieStore.Data;
using MovieStore.Models;
using MovieStore.Services;
using MovieStore.ViewModels;

namespace MovieStore.Controllers
{
    public class AccessLogsController : Controller
    {
        private readonly MovieStoreDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IAccessLogRepository _accessRepo;

        public AccessLogsController(MovieStoreDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IAccessLogRepository accessRepo)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _accessRepo = accessRepo;
        }

        // GET: AccessLogs
        public async Task<IActionResult> Index(DateTime date)
        {
            var u = await GetCurrentUserAsync();
            var logs = from l in _context.AccessLog select l;

            logs = logs.Where(s => s.UserID == u.Id);

            if (date != DateTime.MinValue)
                logs = logs.Where(s => s.LogTime.Date == date.Date);

            return View(await logs.Select(log => new LogViewModel
            {
                AccessLogID = log.AccessLogID,
                UserID = log.UserID,
                LogTime = log.LogTime,
                AccessType = log.AccessType
            }).ToListAsync());
        }

        // GET: AccessLogs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessLog = await _context.AccessLog
                .FirstOrDefaultAsync(m => m.AccessLogID == id);
            if (accessLog == null)
            {
                return NotFound();
            }

            return View(accessLog);
        }

        /*
        // GET: AccessLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AccessLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccessLogID,UserID,LogTime,AccessType")] AccessLog accessLog)
        {
            if (ModelState.IsValid)
            {
                accessLog.AccessLogID = Guid.NewGuid();
                _context.Add(accessLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accessLog);
        }
        
        // GET: AccessLogs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessLog = await _context.AccessLog.FindAsync(id);
            if (accessLog == null)
            {
                return NotFound();
            }
            return View(accessLog);
        }

        // POST: AccessLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AccessLogID,UserID,LogTime,AccessType")] AccessLog accessLog)
        {
            if (id != accessLog.AccessLogID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessLogExists(accessLog.AccessLogID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accessLog);
        }
        */
        // GET: AccessLogs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessLog = await _context.AccessLog
                .FirstOrDefaultAsync(m => m.AccessLogID == id);
            if (accessLog == null)
            {
                return NotFound();
            }

            return View(accessLog);
        }

        // POST: AccessLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var accessLog = await _context.AccessLog.FindAsync(id);
            _context.AccessLog.Remove(accessLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccessLogExists(Guid id)
        {
            return _context.AccessLog.Any(e => e.AccessLogID == id);
        } 

        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
