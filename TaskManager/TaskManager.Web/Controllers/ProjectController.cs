using Microsoft.AspNetCore.Mvc;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Project;

namespace TaskManager.Web.Controllers
{
    public class ProjectController  : Controller
    {
        private readonly IProjectViewModelBuilder _projectViewModelBuilder;

        public ProjectController(IProjectViewModelBuilder projectViewModelBuilder)
        {
            _projectViewModelBuilder = projectViewModelBuilder;
        }

        // GET: Todo
        public ActionResult Index()
        {
            var model = _projectViewModelBuilder.Index();
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

            _projectViewModelBuilder.Create(model.Title);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = _projectViewModelBuilder.Edit(id);
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

            _projectViewModelBuilder.Update(model);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(string id)
        {
            _projectViewModelBuilder.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

