namespace Lacuna.Genetics.Tests;

public class HttpServiceTests
{
    //[Fact]
    //public void RequestAccessTokenAsync_OnCall_MakeTheProperHttpRequest()
    //{
    //    // Arrange
    //    var expectedUri = new Uri("https://gene.lacuna.cc/api/users/login");

    //    var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
    //    handlerMock
    //        .Protected()
    //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
    //            ItExpr.IsAny<CancellationToken>())
    //        .ReturnsAsync(new HttpResponseMessage
    //        {
    //            StatusCode = HttpStatusCode.OK,
    //            Content = new StringContent("{'accessToken': 'TOKEN', 'code':'Success'}")
    //        })
    //        .Verifiable();

    //    // Act
    //    var result = HttpService.RequestAccessTokenAsync(new User("username", "password"));

    //    // Assert
    //    result.Should().NotBeNull();
    //    handlerMock.Protected().Verify("SendAsync",
    //        Times.Once(),
    //        ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post && req.RequestUri == expectedUri),
    //        ItExpr.IsAny<CancellationToken>());
    //}
}