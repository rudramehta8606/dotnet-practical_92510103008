namespace AcademicCalendarLeave.Models;

public sealed class AcademicViewModel
{
    public string DisplayName { get; set; } = "Maya Patel";
    public string SelectedMonth { get; set; } = "September 2026";
    public string SelectedFilter { get; set; } = "All events";
    public string LeaveType { get; set; } = "Casual leave";
    public DateTime StartDate { get; set; } = DateTime.Today.AddDays(3);
    public DateTime EndDate { get; set; } = DateTime.Today.AddDays(4);
    public string Reason { get; set; } = string.Empty;
    public bool NotifyFaculty { get; set; } = true;
    public string? Notice { get; set; }
    public List<LeaveRequest> LeaveRequests { get; set; } = [];
    public List<CalendarEvent> Events { get; set; } = [];
}

public sealed record LeaveRequest(string Type, DateTime StartDate, DateTime EndDate, string Status, string Reason);

public sealed record CalendarEvent(string Day, string Month, string Title, string Meta, string Type);