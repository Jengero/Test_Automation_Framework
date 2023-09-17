using RestSharp;
using TAF.API.Models.RequestModels;

namespace TAF.API.Controllers
{
    public class TechController : BaseController
    {
            public TechController(CustomRestClient client) : base(client, Service.Tech)
            {

            }

        private const string AllTechResource = "/objects";

        private const string SigleItemResource = "/objects/{0}";

        /// <summary>
        /// Gets list of Bibles from API
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemSingleResponseModel"/> </typeparam>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemSingleResponseModel"/></returns>
        public (RestResponse Response, T? Tech) GetTechItems<T>()    
        {
            return Get<T>(AllTechResource);
        }

        /// <summary>
        /// Receive single item by ID
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemSingleResponseModel"></typeparam>
        /// <param name="itemId"></param>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemSingleResponseModel"/></returns>
        public (RestResponse Response, T? Tech) GetSingleTechItem<T>(string itemId) 
        {
            return Get<T>(string.Format(SigleItemResource, itemId));
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemRequestModel"></typeparam>
        /// <param name="model"></param>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemCreatedResponseModel"</returns>
        public (RestResponse Response, T? Tech) AddTechItem<T>(TechItemRequestModel model) 
        {
            return Post<T, TechItemRequestModel>(AllTechResource, model); 
        }

        /// <summary>
        /// Delete single item by ID
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemRequestModel"></typeparam>
        /// <param name="itemId"></param>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemDeleteResponseModel"</returns>
        public (RestResponse Response, T? Tech) DeleteCreatedItem<T>(string itemId) 
        {
            return Delete<T>(string.Format(SigleItemResource, itemId));
        }

        /// <summary>
        /// Replace created item using ID with Put method
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemRequestModel"></typeparam>
        /// <param name="model"></param>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemPutUpdateResponseModel"</returns>
        public (RestResponse Response, T? Tech) RecreateTechItem<T>(TechItemRequestModel model, string itemId)
        {
            return Put<T, TechItemRequestModel>(string.Format(SigleItemResource,itemId), model);
        }

        /// <summary>
        /// Modify created item by ID with Patch method
        /// </summary>
        /// <typeparam name="T"><see cref="TechItemRequestModel"></typeparam>
        /// <param name="patch"></param>
        /// <returns>response typeof <see cref="RestResponse"/> and <see cref="TechItemPatchUpdateResponseModel"</returns>
        public (RestResponse Response, T? Tech) PatchTechItem<T>(object patch, string itemId)
        {
            return Patch<T, TechItemRequestModel>(string.Format(SigleItemResource, itemId), patch);
        }
    }
}