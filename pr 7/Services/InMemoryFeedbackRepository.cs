using pr_7.Models;

namespace pr_7.Services;

public sealed class InMemoryFeedbackRepository : IFeedbackRepository
{
    private readonly object _sync = new();
    private readonly List<Feedback> _items =
    [
        new() { Id = 1, Name = "Jordan Lee", Email = "jordan@example.com", Category = "Product", Rating = 5, Message = "The new dashboard feels clear and easy to use. The quick filters save me a lot of time.", CreatedAt = DateTime.Today.AddDays(-1), Status = "Reviewed" },
        new() { Id = 2, Name = "Sam Rivera", Email = "sam@example.com", Category = "Support", Rating = 4, Message = "Support was kind and helpful. A short progress update while a request is being handled would be great.", CreatedAt = DateTime.Today.AddDays(-2), Status = "In review" },
        new() { Id = 3, Name = "Taylor Chen", Email = "taylor@example.com", Category = "Idea", Rating = 5, Message = "It would be useful to export a monthly summary for our team meetings.", CreatedAt = DateTime.Today.AddDays(-4), Status = "New" }
    ];
    private int _nextId = 4;

    public IReadOnlyList<Feedback> GetAll()
    {
        lock (_sync)
            return _items.OrderByDescending(item => item.CreatedAt).Select(Copy).ToList();
    }

    public Feedback? GetById(int id)
    {
        lock (_sync)
        {
            var item = _items.FirstOrDefault(feedback => feedback.Id == id);
            return item is null ? null : Copy(item);
        }
    }

    public Feedback Add(FeedbackInputModel input)
    {
        lock (_sync)
        {
            var item = new Feedback
            {
                Id = _nextId++, Name = input.Name.Trim(), Email = input.Email.Trim(),
                Category = input.Category, Rating = input.Rating, Message = input.Message.Trim(),
                CreatedAt = DateTime.Now, Status = "New"
            };
            _items.Add(item);
            return Copy(item);
        }
    }

    public bool Update(int id, FeedbackInputModel input)
    {
        lock (_sync)
        {
            var item = _items.FirstOrDefault(feedback => feedback.Id == id);
            if (item is null) return false;
            item.Name = input.Name.Trim(); item.Email = input.Email.Trim();
            item.Category = input.Category; item.Rating = input.Rating; item.Message = input.Message.Trim();
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_sync)
        {
            var item = _items.FirstOrDefault(feedback => feedback.Id == id);
            return item is not null && _items.Remove(item);
        }
    }

    private static Feedback Copy(Feedback item) => new()
    {
        Id = item.Id, Name = item.Name, Email = item.Email, Category = item.Category,
        Rating = item.Rating, Message = item.Message, CreatedAt = item.CreatedAt, Status = item.Status
    };
}
