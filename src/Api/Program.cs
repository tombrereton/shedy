using Shedy.Api;
using Shedy.Api.Middleware;
using Shedy.Core;
using Shedy.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddInfrastructure()
    .AddApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ShedyExceptionMiddleware>();
app.MapControllers();

app.Run();