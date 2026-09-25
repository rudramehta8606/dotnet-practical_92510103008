using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using AcademicCalendarLeave.Models;

namespace AcademicCalendarLeave.Controllers;

public class HomeController : Controller
{
    private const string LeaveRequestsKey = "leave-requests";

    public IActionResult Index(string? selectedMonth, string? selectedFilter)
    {
        return View(BuildViewModel(new AcademicViewModel
        {
            SelectedMonth = selectedMonth ?? "September 2026",
            SelectedFilter = selectedFilter ?? "All events"
        }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ApplyLeave(AcademicViewModel input)
    {
        if (input.EndDate < input.StartDate)
        {
            input.Notice = "End date must be on or after the start date.";
            return View("Index", BuildViewModel(input));
        }

        if (string.IsNullOrWhiteSpace(input.Reason))
        {
            input.Notice = "Please add a reason before submitting your leave request.";
            return View("Index", BuildViewModel(input));
        }

        var requests = GetRequests();
        requests.Insert(0, new LeaveRequest(input.LeaveType, input.StartDate, input.EndDate, "Pending", input.Reason));
        HttpContext.Session.SetString(LeaveRequestsKey, JsonSerializer.Serialize(requests));
        input.Notice = "Leave request sent to your faculty coordinator.";
        return View("Index", BuildViewModel(input));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetTheme(string theme)
    {
        Response.Cookies.Append("campus-theme", theme == "light" ? "light" : "dark", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(30),
            HttpOnly = false,
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        });
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private AcademicViewModel BuildViewModel(AcademicViewModel? input = null)
    {
        var model = input ?? new AcademicViewModel();
        model.DisplayName = HttpContext.Session.GetString("display-name") ?? "Maya Patel";
        HttpContext.Session.SetString("display-name", model.DisplayName);
        model.LeaveRequests = GetRequests();
        List<CalendarEvent> events =
        [
            new("08", "SEP", "Faculty development day", "All departments · 09:00", "campus"),
            new("14", "SEP", "Mid-semester assessments", "Computer Science · 10:30", "assessment"),
            new("21", "SEP", "Project review window", "Year 3 · 14:00", "deadline"),
            new("28", "SEP", "Student advisory council", "Main hall · 11:00", "meeting"),
            new("05", "OCT", "Research methods workshop", "Library lab · 13:00", "campus"),
            new("19", "OCT", "Assignment submission", "Computer Science · 23:59", "deadline"),
            new("03", "NOV", "Final project presentations", "Innovation hub · 10:00", "assessment")
        ];
        var monthCode = model.SelectedMonth[..3].ToUpperInvariant();
        model.Events = events.Where(calendarEvent => calendarEvent.Month == monthCode).ToList();
        if (model.SelectedFilter != "All events")
        {
            var eventType = model.SelectedFilter switch
            {
                "Assessments" => "assessment",
                "Deadlines" => "deadline",
                "Campus events" => "campus",
                _ => string.Empty
            };
            model.Events = model.Events.Where(calendarEvent => calendarEvent.Type == eventType).ToList();
        }
        return model;
    }

    private List<LeaveRequest> GetRequests()
    {
        var stored = HttpContext.Session.GetString(LeaveRequestsKey);
        return stored is null
            ?
            [
                new("Medical leave", DateTime.Today.AddDays(-12), DateTime.Today.AddDays(-11), "Approved", "Routine appointment"),
                new("Casual leave", DateTime.Today.AddDays(7), DateTime.Today.AddDays(8), "Pending", "Family commitment")
            ]
            : JsonSerializer.Deserialize<List<LeaveRequest>>(stored) ?? [];
    }
}
