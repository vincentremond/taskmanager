using Microsoft.AspNetCore.Mvc;
using TaskManager.Contract.Business;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoViewModelBuilder _todoViewModelBuilder;
        private readonly ITodoBusiness _todoBusiness;

        public TodoController(ITodoViewModelBuilder todoViewModelBuilder, ITodoBusiness todoBusiness)
        {
            _todoViewModelBuilder = todoViewModelBuilder;
            _todoBusiness = todoBusiness;
        }

        // GET: Todo
        public ActionResult Index()
        {
            var model = _todoViewModelBuilder.Index();
            return View(model);
        }

        public ActionResult Complete(string id)
        {
            _todoBusiness.Complete(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ScoreUp(string id) => ScoreIncrement(id, 1);
        public ActionResult ScoreDown(string id) => ScoreIncrement(id, -1);
        private ActionResult ScoreIncrement(string id, int increment)
        {
            _todoBusiness.IncrementScore(id, increment);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Add model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _todoBusiness.Create(model.Title);

            return RedirectToAction(nameof(Index));
        }
    }
}

