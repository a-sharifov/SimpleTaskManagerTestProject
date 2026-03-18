using System.Diagnostics;
using Api.Models;
using Api.Models.TaskModels;
using Application.TaskModels.Commands.Create;
using Application.TaskModels.Commands.Delete;
using Application.TaskModels.Commands.Update;
using Application.TaskModels.Queries.Details;
using Application.TaskModels.Queries.OrderByTokens;
using Application.TaskModels.Queries.PrioritiyTokens;
using Application.TaskModels.Queries.SummaryPagedList;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class TaskModelsController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(
        int pageNumber = 1, int pageSize = 10, string orderBy = "DeadlineAsc",
        CancellationToken cancellationToken = default)
    {
        var orderByTokens = await sender.Send(new TaskModelOrderByTokensQuery(), cancellationToken);
        ViewBag.OrderByTokens = orderByTokens.Value;
        ViewBag.CurrentOrderBy = orderBy;

        var result = await sender.Send(
            new GetTaskModelSummaryPagedListQuery(pageNumber, pageSize, orderBy), cancellationToken);

        return View(result.Value);
    }

    public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTaskModelDetailsQuery(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return NotFound();

        return View(result.Value);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
    {
        await LoadPriorityTokensAsync(cancellationToken);
        return View(new CreateTaskModelViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateTaskModelViewModel model, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return await ViewWithPriorityTokensAsync(model, cancellationToken);

        var command = new CreateTaskModelCommand(
            model.Title, model.Description, model.Priority, model.Deadline);

        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            AddValidationErrors(result.ValidationErrors);
            return await ViewWithPriorityTokensAsync(model, cancellationToken);
        }

        return RedirectToAction(nameof(Details), new { id = result.Value });
    }

    public async Task<IActionResult> Edit(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTaskModelDetailsQuery(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return NotFound();

        var detail = result.Value;
        var model = new EditTaskModelViewModel
        {
            Id = detail.Id,
            Title = detail.Title,
            Description = detail.Description,
            Priority = detail.Priority,
            Deadline = detail.Deadline,
            IsCompleted = detail.IsCompleted
        };

        return await ViewWithPriorityTokensAsync(model, cancellationToken);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        Guid id, EditTaskModelViewModel model, CancellationToken cancellationToken = default)
    {
        if (id != model.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return await ViewWithPriorityTokensAsync(model, cancellationToken);

        var command = new UpdateTaskModelCommand(
            model.Id, model.Title, model.Description,
            model.Priority, model.Deadline, model.IsCompleted);

        var result = await sender.Send(command, cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return NotFound();

        if (!result.IsSuccess)
        {
            AddValidationErrors(result.ValidationErrors);
            return await ViewWithPriorityTokensAsync(model, cancellationToken);
        }

        return RedirectToAction(nameof(Details), new { id = model.Id });
    }

    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetTaskModelDetailsQuery(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return NotFound();

        return View(result.Value);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(
        Guid id, CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new DeleteTaskModelCommand(id), cancellationToken);

        if (result.Status == ResultStatus.NotFound)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    private void AddValidationErrors(IEnumerable<ValidationError> errors)
    {
        foreach (var error in errors)
            ModelState.AddModelError(string.Empty, error.ErrorMessage);
    }

    private async Task<ViewResult> ViewWithPriorityTokensAsync(
        object model, CancellationToken cancellationToken)
    {
        await LoadPriorityTokensAsync(cancellationToken);
        return View(model);
    }

    private async Task LoadPriorityTokensAsync(CancellationToken cancellationToken)
    {
        var tokens = await sender.Send(new TaskModelPriorityTokensQuery(), cancellationToken);
        ViewBag.PriorityTokens = tokens.Value;
    }
}
