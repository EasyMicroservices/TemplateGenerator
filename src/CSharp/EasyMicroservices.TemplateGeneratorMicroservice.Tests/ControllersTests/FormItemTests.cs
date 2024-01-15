using EasyMicroservices.Cores.Clients;
using EasyMicroservices.IdentityMicroservice.Tests;
using EasyMicroservices.ServiceContracts;
using EasyMicroservices.TemplateGeneratorMicroservice.Tests.Fixtures;
using Identity.GeneratedServices;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
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

    [Theory]
    [InlineData("Test1")]
    [InlineData("Test2")]
    public async Task AddFormItems(string name)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = name,
            Type = ItemType.TextBox,
            Items = new List<CreateFormItemContract>()
            {
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_1",
                    Type = ItemType.OptionList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_1",
                            Type = ItemType.DateOnly,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_2",
                            Type = ItemType.CheckBox,
                        }
                    }
                },
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_2",
                    Type = ItemType.CheckList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_1",
                            Type = ItemType.TextBox,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_2",
                            Type = ItemType.TimeOnly,
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

        Assert.True(find.Items.Count == 2);
        Assert.Contains(find.Items, x => x.Type == ItemType.OptionList);
        Assert.Contains(find.Items, x => x.Type == ItemType.CheckList);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.CheckBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.DateOnly);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TextBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TimeOnly);
        Assert.True(find.Items.SelectMany(x => x.Items).All(x => x.UniqueIdentity.HasValue()));
    }

    [Theory]
    [InlineData("Test1")]
    [InlineData("Test2")]
    public async Task UpdateFormItems(string name)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = name,
            Type = ItemType.TextBox,
            Items = new List<CreateFormItemContract>()
            {
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_1",
                    Type = ItemType.OptionList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_1",
                            Type = ItemType.DateOnly,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_2",
                            Type = ItemType.CheckBox,
                        }
                    }
                },
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_2",
                    Type = ItemType.CheckList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_1",
                            Type = ItemType.TextBox,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_2",
                            Type = ItemType.TimeOnly,
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

        Assert.True(find.Items.Count == 2);
        Assert.Contains(find.Items, x => x.Type == ItemType.OptionList);
        Assert.Contains(find.Items, x => x.Type == ItemType.CheckList);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.CheckBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.DateOnly);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TextBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TimeOnly);

        find.Title = "updated";
        find.Type = ItemType.Label;
        foreach (var item in find.Items)
        {
            item.Type = ItemType.VerticalStack;
            item.Title = "updated1";
            foreach (var item2 in item.Items)
            {
                item2.Type = ItemType.HorizontalStack;
                item2.Title = "updated2";
            }
        }
        find.Items.Add(new FormItemContract()
        {
            Title = name + "add_Inner1_2",
            Type = ItemType.VerticalStack,
            Items = new List<FormItemContract>()
            {
                new FormItemContract()
                {
                    Title = name + "add_Inner1_2_1",
                    Type = ItemType.HorizontalStack,
                },
                new FormItemContract()
                {
                    Title = name + "add_Inner1_2_2",
                    Type = ItemType.HorizontalStack,
                }
            }
        });
        var updated = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find.Id,
            Title = find.Title,
            Type = find.Type,
            Items = find.Items
        }).AsCheckedResult(x => x.Result);

        var find2 = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);

        Assert.True(find2.Items.Count == 3);
        Assert.True(find2.Title == "updated");
        Assert.True(find2.Type == ItemType.Label);
        Assert.True(find2.Items.All(x => x.Type == ItemType.VerticalStack));
        Assert.True(find2.Items.SelectMany(x => x.Items).All(x => x.Type == ItemType.HorizontalStack));
        Assert.True(find2.Items.SelectMany(x => x.Items).All(x => x.UniqueIdentity.HasValue()));
    }

    [Theory]
    [InlineData("Test1")]
    [InlineData("Test2")]
    public async Task DeleteFormItems(string name)
    {
        _noParentFormItemClient = await InternalLogin(_noParentFormItemClient);
        var result = await _noParentFormItemClient.AddAsync(new CreateFormItemRequestContract()
        {
            Title = name,
            Type = ItemType.TextBox,
            Items = new List<CreateFormItemContract>()
            {
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_1",
                    Type = ItemType.OptionList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_1",
                            Type = ItemType.DateOnly,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_1_2",
                            Type = ItemType.CheckBox,
                        }
                    }
                },
                new CreateFormItemContract()
                {
                    Title = name + "_Inner1_2",
                    Type = ItemType.CheckList,
                    Items = new List<CreateFormItemContract>()
                    {
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_1",
                            Type = ItemType.TextBox,
                        },
                        new CreateFormItemContract()
                        {
                            Title = name + "_Inner1_2_2",
                            Type = ItemType.TimeOnly,
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

        Assert.True(find.Items.Count == 2);
        Assert.Contains(find.Items, x => x.Type == ItemType.OptionList);
        Assert.Contains(find.Items, x => x.Type == ItemType.CheckList);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.CheckBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.OptionList).Items, x => x.Type == ItemType.DateOnly);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TextBox);
        Assert.Contains(find.Items.First(x => x.Type == ItemType.CheckList).Items, x => x.Type == ItemType.TimeOnly);

        find.Title = "updated";
        find.Type = ItemType.Label;
        find.Items.Remove(find.Items.First());
        find.Items.First().Items.Remove(find.Items.First().Items.First());

        var updated = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find.Id,
            Title = find.Title,
            Type = find.Type,
            Items = find.Items
        }).AsCheckedResult(x => x.Result);

        var find2 = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);

        Assert.True(find2.Items.Count == 1);
        Assert.True(find2.Title == "updated");
        Assert.True(find2.Type == ItemType.Label);
        Assert.True(find2.Items.SelectMany(x => x.Items).Count() == 1);

        find2.Items.Clear();

        var updated2 = await _noParentFormItemClient.UpdateChangedValuesOnlyAsync(new UpdateFormItemRequestContract()
        {
            Id = find2.Id,
            Title = find2.Title,
            Type = find2.Type,
            Items = find2.Items
        }).AsCheckedResult(x => x.Result);

        var find3 = await _noParentFormItemClient.GetByIdAsync(new Int64GetByIdRequestContract()
        {
            Id = result
        }).AsCheckedResult(x => x.Result);

        Assert.True(find3.Items.Count == 0);

        var all = await _noParentFormItemClient.FilterAsync(new FilterRequestContract()
        {
            Length = 10000
        }).AsCheckedResult(x => x.Result);
        var findFromFilter = all.FirstOrDefault(x => x.Id == result);
        Assert.NotNull(findFromFilter);
        Assert.True(findFromFilter.Items.Count == 0);
    }
}
