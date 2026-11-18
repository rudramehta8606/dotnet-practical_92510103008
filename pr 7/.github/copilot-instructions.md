# Workspace guidance

- This is a .NET 10 ASP.NET Core MVC feedback-management app.
- Follow MVC separation: controllers coordinate, models validate, views render, and repository services own in-memory data.
- Use the existing shared layout and reusable partial for feedback forms.
- Preserve server-side validation and anti-forgery protection on form posts.
- Run `dotnet build` after C# or Razor changes.
