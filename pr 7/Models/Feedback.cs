using System.ComponentModel.DataAnnotations;

namespace pr_7.Models;

public sealed class Feedback
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "New";
}

public sealed class FeedbackInputModel
{
    [Required(ErrorMessage = "Please enter your name.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 80 characters.")]
    [Display(Name = "Your name")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(160)]
    [Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Choose a feedback category.")]
    [Display(Name = "What is this about?")]
    public string Category { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Select a rating from 1 to 5.")]
    [Display(Name = "Overall experience")]
    public int Rating { get; set; }

    [Required(ErrorMessage = "Please tell us a little more.")]
    [StringLength(1200, MinimumLength = 10, ErrorMessage = "Your message must be between 10 and 1,200 characters.")]
    [Display(Name = "Your feedback")]
    public string Message { get; set; } = string.Empty;

    public static FeedbackInputModel From(Feedback feedback) => new()
    {
        Name = feedback.Name,
        Email = feedback.Email,
        Category = feedback.Category,
        Rating = feedback.Rating,
        Message = feedback.Message
    };
}
