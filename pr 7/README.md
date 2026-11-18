# Gather — Feedback Management

An ASP.NET Core MVC feedback management module demonstrating shared layouts, reusable partial views, HTML Helpers, and server/client-side validation.

## Features
- Feedback inbox with category, rating, review status, and summary statistics.
- Create, view, edit, and delete feedback.
- Strongly typed form model with Data Annotation validation and category allow-list validation.
- Reusable `_FeedbackForm` partial built with MVC HTML Helpers.
- Shared responsive layout and unobtrusive client-side validation.
- In-memory repository seeded with sample feedback; data resets when the app restarts.

## Run
Requires the .NET 10 SDK. From this folder, run `dotnet run` and open the local URL printed in the terminal. Use `dotnet build` to compile.
