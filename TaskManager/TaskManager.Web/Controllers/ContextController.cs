using Microsoft.AspNetCore.Mvc;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Context;

namespace TaskManager.Web.Controllers
{
    public class ContextController  : Controller
    {
        private readonly IContextViewModelBuilder _contextViewModelBuilder;

        public ContextController(IContextViewModelBuilder contextViewModelBuilder)
        {
            _contextViewModelBuilder = contextViewModelBuilder;
        }

        // GET: Todo
        public ActionResult Index()
        {
            var model = _contextViewModelBuilder.Index();
            return View(model);
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

            _contextViewModelBuilder.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = _contextViewModelBuilder.Edit(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Edit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _contextViewModelBuilder.Update(model);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(string id)
        {
            _contextViewModelBuilder.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

