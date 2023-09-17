using RestSharp;
using System.Net;
using TAF.API.Controllers;
using TAF.API.Models.RequestModels;
using TAF.API.Models.ResponseModels.Tech;
using TAF.API.Models.SharedModels.Tech;

namespace TAF.API.Tests
{
    [TestFixture]
    public class TechTests
    {
        [Test]
        public void AllTechResponseWithValidParamsTest()
        {
            var response = new TechController(new CustomRestClient()).GetTechItems<RestResponse>();

            Assert.That(response.Response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Invalid status code ({response.Response.StatusCode})");
        }

        [Test]
        public void AllTechResponseTest()
        {
            var response = new TechController(new CustomRestClient()).GetTechItems<List<TechItemSingleResponseModel>>();

            CollectionAssert.IsNotEmpty(response.Tech, "Any object should be returned");
        }

        [Test]
        public void QuantityOfAllTechItemsTest()
        {
            var currentIdNumber = 13;
            var response = new TechController(new CustomRestClient()).GetTechItems<List<TechItemSingleResponseModel>>().Tech.Select(f => f.Id);

            Assert.That(response.Count, Is.EqualTo(currentIdNumber), $"The number of objects is not equal");
        }

        [Test]
        public void CreateTechItemWithCapacityGBTest()
        {
            var capacity = 1024;

            var techItem = new TechItemRequestModel()
            {
                Name = "iPhone 15",
                Data = new TechData
                {
                    Price = 1789,
                    CapacityGB = 1024
                }
            };

            var createdItem = new TechController(new CustomRestClient()).AddTechItem<TechItemCreatedResponseModel>(techItem).Tech;

            var receivedItem = new TechController(new CustomRestClient()).GetSingleTechItem<TechItemSingleResponseModel>(createdItem.Id).Tech;

            var itemDeletion = new TechController(new CustomRestClient()).DeleteCreatedItem<TechItemDeleteResponseModel>(createdItem.Id).Tech;

            Assert.That(receivedItem.Data.CapacityGB, Is.EqualTo(capacity), "Values should match");
        }

        [Test]
        public void DeleteCreatedTechItemTest()
        {

            var techItem = new TechItemRequestModel()
            {
                Name = "Google Pixel 7",
                Data = new TechData
                {
                    Price = 1400,
                    Generation = "7"
                }
            };

            var createdItem = new TechController(new CustomRestClient()).AddTechItem<TechItemCreatedResponseModel>(techItem).Tech;

            var receivedItem = new TechController(new CustomRestClient()).GetSingleTechItem<TechItemSingleResponseModel>(createdItem.Id).Tech;

            var itemDeletion = new TechController(new CustomRestClient()).DeleteCreatedItem<TechItemDeleteResponseModel>(createdItem.Id).Tech;

            var deletedItem = new TechController(new CustomRestClient()).GetSingleTechItem<TechItemSingleResponseModel>(createdItem.Id).Tech;

            Assert.That(deletedItem.Id, Is.Null, "Item should be deleted");
        }

        [Test]
        public void ReplaceCreatedTechItemWithPutMethodTest()
        {
            var price = 2000;

            var techItem = new TechItemRequestModel()
            {
                Name = "iPhone 14",
                Data = new TechData
                {
                    Price = 1789,
                    CapacityGB = 1024
                }
            };

            var patch = new TechItemRequestModel()
            {
                Name = "iPhone 15",
                Data = new TechData
                {
                    Price = 2000,
                    CapacityGB = 1024
                }
            };

            var createdItem = new TechController(new CustomRestClient()).AddTechItem<TechItemCreatedResponseModel>(techItem).Tech;

            var receivedItem = new TechController(new CustomRestClient()).GetSingleTechItem<TechItemSingleResponseModel>(createdItem.Id).Tech;

            var patchedItem = new TechController(new CustomRestClient()).RecreateTechItem<TechItemPutUpdateResponseModel>(patch, createdItem.Id).Tech;

            var itemDeletion = new TechController(new CustomRestClient()).DeleteCreatedItem<TechItemDeleteResponseModel>(createdItem.Id).Tech;

            Assert.That(patchedItem.Data.Price, Is.EqualTo(price), "Updated value should match");
        }

        [Test]
        public void PathCreatedTechItemTest()
        {
            var newPrice = 2000;

            var techItem = new TechItemRequestModel()
            {
                Name = "iPhone 14",
                Data = new TechData
                {
                    Price = 1789,
                    CapacityGB = 1024
                }
            };

            var changedВata = new
            {
                Data = new 
                {
                    Price = 2000
                }
            };

            var createdItem = new TechController(new CustomRestClient()).AddTechItem<TechItemCreatedResponseModel>(techItem).Tech;

            var receivedItem = new TechController(new CustomRestClient()).GetSingleTechItem<TechItemSingleResponseModel>(createdItem.Id).Tech;

            var patchedItem = new TechController(new CustomRestClient()).PatchTechItem<TechItemPatchUpdateResponseModel>(changedВata, createdItem.Id).Tech;

            var itemDeletion = new TechController(new CustomRestClient()).DeleteCreatedItem<TechItemDeleteResponseModel>(createdItem.Id).Tech;

            Assert.That(patchedItem.Data.Price, Is.EqualTo(newPrice), "Updated value should match");
        }
    }
}