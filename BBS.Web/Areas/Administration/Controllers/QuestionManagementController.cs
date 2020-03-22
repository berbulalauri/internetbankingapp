using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Areas.Administration.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Areas.Administration.Controllers
{
    [Area(AdministratorArea.Name)]
    [Authorize(Policy = AdministratorArea.Policy)]
    public class QuestionManagementController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionManagementController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        public async Task<ActionResult> Index(string questionText)
        {
            var model = await _questionService.GetList();
            var result = !string.IsNullOrWhiteSpace(questionText) ? model.Where(x => x.Content.Contains(questionText)) : model;
            if (questionText != null)
            {
                ViewData["DepositTypeId"] = "Search result for " + questionText;
            }
            return View(result);
        }
        public async Task<IActionResult> Create(string question)
        {
            Question question1 = new Question();
            question1.Content = question;
            await _questionService.Add(question1);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _questionService.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Content")] Question question)
        {
            if (question.Content == null)
            {
                return View(question);
            }

            await _questionService.UpdateQuestion(question);

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "QuestionManagement");
            }
            return View(question);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var question = await _questionService.GetQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _questionService.DeleteQuestionService(id);

            return RedirectToAction(nameof(Index));
        }

    }
}