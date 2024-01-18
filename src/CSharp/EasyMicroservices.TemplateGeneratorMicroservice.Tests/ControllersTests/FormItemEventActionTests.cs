using EasyMicroservices.Cores.Clients;
using EasyMicroservices.IdentityMicroservice.Tests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Tests.Fixtures;
using Identity.GeneratedServices;
using Microsoft.Extensions.DependencyInjection;
using TemplateGenerators.GeneratedServices;
using Xunit;

namespace EasyMicroservices.TemplateGeneratorMicroservice.Tests.ControllersTests;
public class FormItemEventActionTests : IClassFixture<TestFixture>
{
    AuthenticationClient _authenticationClient;
    NoParentFormItemClient _noParentFormItemClient;
    EventClient _eventClient;
    ActionClient _actionClient;
    public FormItemEventActionTests(TestFixture testFixture)
    {
        _authenticationClient = testFixture.ServiceProvider.GetService<AuthenticationClient>();
        _noParentFormItemClient = testFixture.ServiceProvider.GetService<NoParentFormItemClient>();
        _eventClient = testFixture.ServiceProvider.GetService<EventClient>();
        _actionClient = testFixture.ServiceProvider.GetService<ActionClient>();
    }

    async Task<T> InternalLogin<T>(T client)
            where T : CoreSwaggerClientBase
    {
        var token = await LoginTests.GetLoginOwnerAsync(_authenticationClient);
        client.SetBearerToken(token);
        return client;
    }

    [Theory]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData("test3")]
    [InlineData("test4")]
    [InlineData("test5")]
    public async Task AddOrUpdateFormItemEvent(string title)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        _eventClient = await InternalLogin(_eventClient);
        var events = await _eventClient.GetAllAsync().AsCheckedResult(x => x.Result);
        Assert.True(events.Any());
        var eventId = events.First().Id;
        var eventId2 = events.Skip(1).First().Id;
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = title,
            Type = ItemType.TextBox,
            Events = new List<FormItemEventContract>()
            {
                new FormItemEventContract()
                {
                     EventId = eventId,
                }
            }
        }).AsCheckedResult(x => x.Result);

        Assert.True(result > 0);
        var find = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);
        Assert.True(find.Events.Any());
        Assert.True(find.Events.First().EventId == eventId);

        var toRemove = find.Events.First();
        find.Events.Remove(toRemove);
        find.Events.Add(new FormItemEventContract()
        {
            EventId = eventId2,
        });

        var updated = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find.Id,
            Title = find.Title,
            Type = find.Type,
            Events = find.Events
        }).AsCheckedResult(x => x.Result);
        Assert.True(updated != null);
        var find2 = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);
        Assert.True(find2.Events.Any());
        Assert.True(!find2.Events.Any(x => x.EventId == eventId));
        Assert.True(find2.Events.First().EventId == eventId2);
    }

    [Theory]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData("test3")]
    [InlineData("test4")]
    [InlineData("test5")]
    public async Task AddFormItemEventsAction(string title)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        _eventClient = await InternalLogin(_eventClient);
        _actionClient = await InternalLogin(_actionClient);
        var events = await _eventClient.GetAllAsync().AsCheckedResult(x => x.Result);
        Assert.True(events.Any());
        var actions = await _actionClient.GetAllAsync().AsCheckedResult(x => x.Result);
        Assert.True(actions.Any());
        var eventId = events.First().Id;
        var eventId2 = events.Skip(1).First().Id;
        var actionId = actions.First().Id;
        var actionId2 = actions.Skip(1).First().Id;
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = title,
            Type = ItemType.TextBox,
            Events = new List<FormItemEventContract>()
            {
                new FormItemEventContract()
                {
                     EventId = eventId,
                     FormItemEventActions = new List<FormItemEventActionContract>()
                     {
                          new FormItemEventActionContract()
                          {
                               ActionId = actionId,
                               Children = new List<FormItemEventActionContract>()
                               {
                                   new FormItemEventActionContract()
                                   {
                                        ActionId = actionId2
                                   },
                                   new FormItemEventActionContract()
                                   {
                                        ActionId = actionId
                                   },
                                   new FormItemEventActionContract()
                                   {
                                        ActionId = actionId2
                                   }
                               }
                          },
                          new FormItemEventActionContract()
                          {
                               ActionId = actionId2
                          }
                     }
                },
                new FormItemEventContract()
                {
                     EventId = eventId2,
                }
            }
        }).AsCheckedResult(x => x.Result);

        Assert.True(result > 0);
        var find = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);
        Assert.True(find.Events.Any());
        Assert.True(find.Events.First().EventId == eventId);
        Assert.True(find.Events.First().FormItemEventActions.Any());
        Assert.True(find.Events.First().FormItemEventActions.Count == 2);
        Assert.True(find.Events.First().FormItemEventActions.First().Children.Count == 3);

        var children = find.Events.First().FormItemEventActions.First().Children;
        children.Remove(children.FirstOrDefault(x => x.ActionId == actionId2));
        var update = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find.Id,
            Title = find.Title,
            Type = find.Type,
            Events = find.Events
        }).AsCheckedResult(x => x.Result);
        find = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);

        Assert.True(find.Events.Any());
        Assert.True(find.Events.First().EventId == eventId);
        Assert.True(find.Events.First().FormItemEventActions.Any());
        Assert.True(find.Events.First().FormItemEventActions.Count == 2);
        Assert.True(find.Events.First().FormItemEventActions.First().Children.Count == 2);
    }
    [Theory]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData("test3")]
    [InlineData("test4")]
    [InlineData("test5")]
    public async Task AddInnerFormItemEventsAction(string title)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        _eventClient = await InternalLogin(_eventClient);
        _actionClient = await InternalLogin(_actionClient);
        var events = await _eventClient.GetAllAsync().AsCheckedResult(x => x.Result);
        Assert.True(events.Any());
        var actions = await _actionClient.GetAllAsync().AsCheckedResult(x => x.Result);
        Assert.True(actions.Any());
        var eventId = events.First().Id;
        var eventId2 = events.Skip(1).First().Id;
        var actionId = actions.First().Id;
        var actionId2 = actions.Skip(1).First().Id;
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = title,
            Type = ItemType.TextBox,
            Items = new List<CreateFormItemContract>
            {
                new CreateFormItemContract()
                {
                    Title="t1",
                    Type = ItemType.TextBox,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title="t2",
                            Type = ItemType.TextBox,
                            Events = new List<FormItemEventContract>()
                            {
                                new FormItemEventContract()
                                {
                                     EventId = eventId,
                                     FormItemEventActions = new List<FormItemEventActionContract>()
                                     {
                                          new FormItemEventActionContract()
                                          {
                                               ActionId = actionId,
                                               Children = new List<FormItemEventActionContract>()
                                               {
                                                   new FormItemEventActionContract()
                                                   {
                                                        ActionId = actionId2
                                                   },
                                                   new FormItemEventActionContract()
                                                   {
                                                        ActionId = actionId
                                                   },
                                                   new FormItemEventActionContract()
                                                   {
                                                        ActionId = actionId2
                                                   }
                                               }
                                          },
                                          new FormItemEventActionContract()
                                          {
                                               ActionId = actionId2
                                          }
                                     }
                                },
                                new FormItemEventContract()
                                {
                                     EventId = eventId2,
                                }
                            }
                        }
                    }
                }
            }
        }).AsCheckedResult(x => x.Result);

        Assert.True(result > 0);
        var find = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);
        Assert.True(find.Items.First().Items.First().Events.Any());
        Assert.True(find.Items.First().Items.First().Events.First().EventId == eventId);
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.Any());
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.Count == 2);
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.First().Children.Count == 3);

        var children = find.Items.First().Items.First().Events.First().FormItemEventActions.First().Children;
        children.Remove(children.FirstOrDefault(x => x.ActionId == actionId2));
        var update = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find.Id,
            Title = find.Title,
            Type = find.Type,
            Items = find.Items
        }).AsCheckedResult(x => x.Result);
        find = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);

        Assert.True(find.Items.First().Items.First().Events.Any());
        Assert.True(find.Items.First().Items.First().Events.First().EventId == eventId);
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.Any());
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.Count == 2);
        Assert.True(find.Items.First().Items.First().Events.First().FormItemEventActions.First().Children.Count == 2);
    }
}
