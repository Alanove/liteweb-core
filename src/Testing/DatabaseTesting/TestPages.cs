using lw.Core.Cte;
using lw.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatabaseTesting;

public class TestPages
{
	private IConfiguration _configuration;
	private IPagesService _pagesService;
	private ITagsService _tagsService;

	[SetUp]
	public void Setup()
	{
		_configuration = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json")
			.Build();
		var serviceCollection = new ServiceCollection();

		serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
		var databaseSettings = _configuration.GetSection(ConfigKeys.DatabaseSettings).Get<DatabaseSettings>();


        serviceCollection.AddDatabase(databaseSettings);
		serviceCollection.RegisterServices();
		var serviceProvider = serviceCollection.BuildServiceProvider();
		_pagesService = serviceProvider.GetRequiredService<IPagesService>();
		_tagsService = serviceProvider.GetRequiredService<ITagsService>();
	}
	[SetUp]
	public void CreateData()
	{
		var page = new Page
		{
			Title = "Test",
			Description = "Test Description",
			Content = "Test Content with #hashtag1 #hashtag2",
			History = "",
			PageProperties = new List<PagePropertyValue> {
				new PagePropertyValue
				{
					Value = "15",
					Property = new PageProperties
					{
						Name = "Price"
					}
				},
				new PagePropertyValue
				{
					Value = "10",
					Property = new PageProperties
					{
						Name = "Discount Price"
					}
				}
			}
		};
		_pagesService.AddPage(page);
		var page1 = new Page
		{
			Title = "Test",
			Description = "Test Description",
			Content = "Test Content with #hashtag1",
			History = "",
			PageProperties = new List<PagePropertyValue> {
				new PagePropertyValue
				{
					Value = "20",
					Property = new PageProperties
					{
						Name = "Price"
					}
				},
				new PagePropertyValue
				{
					Value = "15",
					Property = new PageProperties
					{
						Name = "Discount Price"
					}
				},
				new PagePropertyValue
				{
					Value = "White",
					Property = new PageProperties
					{
						Name = "Color"
					}
				},
				new PagePropertyValue
				{
					Value = "XL",
					Property = new PageProperties
					{
						Name = "Size"
					}
				}
			}
		};
		_pagesService.AddPage(page1);
	}
	[Test]
	public void TestCrud()
	{
		var dbPage = _pagesService.GetPages().Where(p => p.Url == "test").FirstOrDefault();
		if (dbPage == null)
		{
			Assert.Fail();
			return;
		}
		if (dbPage.Tags == null || dbPage.Tags.Count() != 2)
		{
			Assert.Fail();
		}
		
		var dbPage1 = _pagesService.GetPages().Where(p => p.Url == "test-1").FirstOrDefault();
		if (dbPage1 == null)
		{
			Assert.Fail();
			return;
		}

		var tag = _tagsService.GetTag("hashtag1");
		var tag1 = _tagsService.GetTag("hashtag2");
		Assert.Equals(tag.Pages.Count(), 2);
		Assert.Equals(tag1.Pages.Count(), 1);

		Assert.Pass();
	}
	[TearDown]
	public void TeadDown()
	{
		var tag = _tagsService.GetTag("hashtag1");
		var tag1 = _tagsService.GetTag("hashtag2");
		_tagsService.DeleteTag(tag);
		_tagsService.DeleteTag(tag1);
		var dbPage = _pagesService.GetPages().Where(p => p.Url == "test").FirstOrDefault();
		var dbPage1 = _pagesService.GetPages().Where(p => p.Url == "test-1").FirstOrDefault();

		_pagesService.DeletePage(dbPage);
		_pagesService.DeletePage(dbPage1);
	}
}