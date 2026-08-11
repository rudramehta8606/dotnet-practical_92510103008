using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NavratriRegistrationPortal
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Registration Registration { get; set; } = new Registration();

        public bool IsSubmitted { get; private set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Registration.RegistrationId = Guid.NewGuid().ToString("N");
            Registration.SubmittedAt = DateTime.Now;
            IsSubmitted = true;
            return Page();
        }
    }

    public class Registration
    {
        [Required(ErrorMessage = "Please enter your full name.")]
        [StringLength(100, ErrorMessage = "Name must be 100 characters or fewer.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Age is required.")]
        [Range(5, 100, ErrorMessage = "Enter an age between 5 and 100.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Select your gender.")]
        public string Gender { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Enter a valid phone number.")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; } = string.Empty;

        public string TeamName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Choose an event category.")]
        public string EventCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Number of participants is required.")]
        [Range(1, 50, ErrorMessage = "Participants must be between 1 and 50.")]
        public int? ParticipantCount { get; set; }

        [Required(ErrorMessage = "Select experience level.")]
        public string ExperienceLevel { get; set; } = string.Empty;

        public string SpecialRequests { get; set; } = string.Empty;

        public string RegistrationId { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; }
    }
}
