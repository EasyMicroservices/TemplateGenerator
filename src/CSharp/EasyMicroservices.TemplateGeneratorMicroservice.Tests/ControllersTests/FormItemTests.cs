using EasyMicroservices.Cores.Clients;
using EasyMicroservices.IdentityMicroservice.Tests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Tests.Fixtures;
using Identity.GeneratedServices;
using Microsoft.Extensions.DependencyInjection;
using TemplateGenerators.GeneratedServices;
using Xunit;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Tests.ControllersTests;
public class FormItemTests : IClassFixture<TestFixture>
{
    AuthenticationClient _authenticationClient;
    NoParentFormItemClient _noParentFormItemClient;
    public FormItemTests(TestFixture testFixture)
    {
        _authenticationClient = testFixture.ServiceProvider.GetService<AuthenticationClient>();
        _noParentFormItemClient = testFixture.ServiceProvider.GetService<NoParentFormItemClient>();
    }

    async Task<T> InternalLogin<T>(T client)
            where T : CoreSwaggerClientBase
    {
        var token = await LoginTests.GetLoginOwnerAsync(_authenticationClient);
        client.SetBearerToken(token);
        return client;
    }

    [Fact]
    public async Task AddFormItem()
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = "test",
            Type = ItemType.TextBox
        }).AsCheckedResult(x => x.Result);

        Assert.True(result > 0);
    }
}
