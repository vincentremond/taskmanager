﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Contract.ViewModel.Builder;
using TaskManager.Contract.ViewModel.Model.Todo;

namespace TaskManager.Web.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoViewModelBuilder _todoViewModelBuilder;
        private readonly IContextViewModelBuilder _contextViewModelBuilder;
        private readonly IMapper _mapper;

        public TodoController(ITodoViewModelBuilder todoViewModelBuilder, IContextViewModelBuilder contextViewModelBuilder, IMapper mapper)
        {
            _todoViewModelBuilder = todoViewModelBuilder;
            _contextViewModelBuilder = contextViewModelBuilder;
            _mapper = mapper;
        }

        // GET: Todo
        public ActionResult Index()
        {
            var model = _todoViewModelBuilder.Index();
            return View(model);
        }

        public ActionResult Complete(string id)
        {
            _todoViewModelBuilder.Complete(id);
            return RedirectToAction(nameof(Index));
        }

        public ActionResult ScoreUp(string id) => ScoreIncrement(id, 1);
        public ActionResult ScoreDown(string id) => ScoreIncrement(id, -1);
        private ActionResult ScoreIncrement(string id, int increment)
        {
            _todoViewModelBuilder.IncrementScore(id, increment);
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

            _todoViewModelBuilder.Create(model.Title);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = _todoViewModelBuilder.Edit(id);
            return EnrichEdit(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Edit model)
        {
            if (!ModelState.IsValid)
            {
                return EnrichEdit(model);
            }

            _todoViewModelBuilder.Update(model);

            return RedirectToAction(nameof(Index));
        }

        private ActionResult EnrichEdit(Edit model)
        {
            var newModel = _mapper.Map<EditViewModel>(model);
            newModel.ViewData = new EditViewModel.Data
            {
                Contexts = new SelectList(_contextViewModelBuilder.GetAll(), "ContextId", "Title", model.ContextId),
            };
            return View(model);
        }
    }
}

