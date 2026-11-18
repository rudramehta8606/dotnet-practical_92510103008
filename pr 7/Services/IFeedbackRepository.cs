using pr_7.Models;

namespace pr_7.Services;

public interface IFeedbackRepository
{
    IReadOnlyList<Feedback> GetAll();
    Feedback? GetById(int id);
    Feedback Add(FeedbackInputModel input);
    bool Update(int id, FeedbackInputModel input);
    bool Delete(int id);
}
