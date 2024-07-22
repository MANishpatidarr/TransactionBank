using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TransactionBank.Models;

namespace TransactionBank.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankTransactionDbContext _context;

      public TransactionController(BankTransactionDbContext context)
        {
            _context = context;
        }
        //Get Transaction
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transaction.ToListAsync());
        }
        // Get : Transaction , AddorEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Transaction());
            else
                return View(_context.Transaction.Find(id));
        }

        // POST: Transaction/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if (transaction.TransactionId == 0)
                {
                    transaction.Date = DateTime.Now;
                    _context.Add(transaction);
                }
                else
                    _context.Update(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            _context.Transaction.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
