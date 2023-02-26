using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.Testing;
using Shedy.Api.IntegrationTests.Helpers;
using Shedy.Api.Requests;

namespace Shedy.Api.IntegrationTests.Controllers;

public class CalendarControllerShould : IClassFixture<ShedyApiFactory>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CalendarControllerShould(ShedyApiFactory factory)
    {
        _factory = factory;
    }

    [Theory]
    [AutoData]
    public async Task CreateCalendar(CreateCalendarRequest request)
    {
        // arrange
        var client = _factory.CreateDefaultClient();
        var uri = "calendar";

        // act
        var result = await client.PostAsJsonAsync(uri, request);

        // assert
        result.EnsureSuccessStatusCode();
        // var db = _factory.Services.GetRequiredService<ShedyDbContext>();
        // var actual = db.Calendars.First(x => x.UserId == request.UserId);
        // actual.Should().NotBeNull();
    }
}