using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using pr_7.Models;
using pr_7.Services;

namespace pr_7.Controllers;

public sealed class FeedbackController(IFeedbackRepository repository) : Controller
{
    private static readonly string[] Categories = ["Product", "Support", "Website", "Idea", "Other"];

    public IActionResult Index()
    {
        ViewBag.TotalCount = repository.GetAll().Count;
        return View(repository.GetAll());
    }

    public IActionResult Details(int id)
    {
        var feedback = repository.GetById(id);
        return feedback is null ? NotFound() : View(feedback);
    }

    [HttpGet]
    public IActionResult Create()
    {
        SetCategories();
        return View(new FeedbackInputModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(FeedbackInputModel input)
    {
        if (!Categories.Contains(input.Category))
            ModelState.AddModelError(nameof(input.Category), "Choose a valid category.");
        if (!ModelState.IsValid)
        {
            SetCategories();
            return View(input);
        }

        var created = repository.Add(input);
        TempData["Success"] = "Thanks for sharing. Your feedback has been added.";
        return RedirectToAction(nameof(Details), new { id = created.Id });
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var feedback = repository.GetById(id);
        if (feedback is null) return NotFound();
        SetCategories();
        ViewBag.FeedbackId = id;
        return View(FeedbackInputModel.From(feedback));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, FeedbackInputModel input)
    {
        if (!Categories.Contains(input.Category))
            ModelState.AddModelError(nameof(input.Category), "Choose a valid category.");
        if (!ModelState.IsValid)
        {
            SetCategories();
            ViewBag.FeedbackId = id;
            return View(input);
        }
        if (!repository.Update(id, input)) return NotFound();
        TempData["Success"] = "Feedback updated successfully.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var feedback = repository.GetById(id);
        return feedback is null ? NotFound() : View(feedback);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!repository.Delete(id)) return NotFound();
        TempData["Success"] = "Feedback removed.";
        return RedirectToAction(nameof(Index));
    }

    private void SetCategories() => ViewBag.Categories = Categories
        .Select(category => new SelectListItem(category, category));
}
