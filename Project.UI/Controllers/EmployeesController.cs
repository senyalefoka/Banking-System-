using Microsoft.AspNetCore.Mvc;
using Project.Domain.Interfaces;
using Project.Domain.Entities;
using System.Threading.Tasks;

namespace Project.UI.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IPersonalDetailsRepository _repo;
        private readonly IAccount _repoAccount;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogin _login;

        public EmployeesController(
            IPersonalDetailsRepository personalDetails,
            ILogin login,
            IAccount account,
            ITransactionRepository transactionRepository)
        {
            _repo = personalDetails;
            _login = login;
            _repoAccount = account;                 // <-- FIXED
            _transactionRepository = transactionRepository;  // <-- FIXED
        }

        public IActionResult Index()
        {
            return View();
        }

        //public async Task<IActionResult> DisplayAllEmployees()
        //{
        //    var employees = await _repo.GetAllAsync();
        //    return View(employees);
        //}

        //---------------------------- REGISTER ----------------------------//

        [HttpGet]
        public IActionResult RegisterView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterView(string name, string password, string email)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _login.Register(name, password, email);

            if (result == null)
            {
                ModelState.AddModelError("", "Registration failed");
                return View();
            }

            TempData["success"] = "Employee registered successfully!";
            return RedirectToAction("Index");
        }

        //---------------------------- LOGIN ----------------------------//

        [HttpGet]
        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginView(string name, string password)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await _login.ValidateUserAsync(name, password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }

            TempData["success"] = "Login successful!";
            return RedirectToAction("LoginLandingView");
        }

        public IActionResult LoginLandingView()
        {
            return View();
        }

        //---------------------------- EDIT ----------------------------//

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var personalDetail = await _repo.GetByIdAsync(id);

            if (personalDetail == null)
                return NotFound();

            return View(personalDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PersonalDetail personalDetail)
        {
            if (id != personalDetail.Code)
                return BadRequest("Code mismatch");

            if (!ModelState.IsValid)
                return View(personalDetail);

            await _repo.UpdateAsync(personalDetail);  // <-- FIXED
            await _repo.SaveAsync(personalDetail);                 // <-- FIXED

            TempData["success"] = "Details updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        //---------------------------- ADD ----------------------------//

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: PersonalDetails/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PersonalDetail personalDetail)
        {
            if (ModelState.IsValid)
            {
                // Generate new GUID if not provided
                if (personalDetail.Code == Guid.Empty)
                    personalDetail.Code = Guid.NewGuid();

                await _repo.AddAsync(personalDetail);
                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed; redisplay form
            return View(personalDetail);
        }

        //---------------------------- ACCOUNTS ----------------------------//

        public async Task<IActionResult> AccountDetail()
        {
            var accountDetails = await _repoAccount.GetAllAsync();
            return View(accountDetails);
        }

        //---------------------------- DETAILS ----------------------------//

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var detail = await _repo.GetByIdAsync(id);
            return View(detail);
        }

        //---------------------------- TRANSACTIONS ----------------------------//

        public async Task<IActionResult> TransactionDetail()
        {
            var transactions = await _transactionRepository.GetAllTaransaction();
            return View(transactions);
        }

        //---------------------------- DETAILS VIEW ----------------------------//

        public async Task<IActionResult> DetailsView()
        {
            var personalDetails = await _repo.GetAllAsync();

            return View(personalDetails.ToList());
        }
    }
}
