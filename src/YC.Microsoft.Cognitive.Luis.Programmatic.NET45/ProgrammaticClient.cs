using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using YC.Microsoft.Cognitive.Luis.Programmatic.Enums;
using YC.Microsoft.Cognitive.Luis.Programmatic.Models;
using Newtonsoft.Json;

namespace YC.Microsoft.Cognitive.Luis.Programmatic
{
    public interface ITrainClient
    {
        Task<TrainResult> TrainAsync();

        Task<IEnumerable<TrainingStatusResult>> GetTrainStatusAsync();

		Task PublishAsync(Regions region, bool isStaging);
	}

    public interface IIntentClient
    {
        Task<Guid> CreateAsync(string name);

        Task<IEnumerable<Intent>> ListAsync();

        Task<Intent> GetAsync(Guid id);

        Task UpdateAsync(Guid id, string name);

        Task DeleteAsync(Guid id);
    }

    public interface IEntityClient
    {
        Task<IEnumerable<Entity>> ListEntitiesAsync();

        Task<Entity> GetEntityAsync(Guid id);

        Task<IEnumerable<ClosedList>> ListClosedListsAsync();

        Task<ClosedList> GetClosedListAsync(Guid id);

        Task<Guid> CreateEntityAsync(Entity entity);

        Task<Guid> CreateClosedListAsync(ClosedList list);

        Task<bool> UpdateEntityAsync(Guid id, Entity entity);

        /// <summary>
        /// 修改List的名字, 取代sublists內容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> UpdateClosedListAsync(Guid id, ClosedList list);

        /// <summary>
        /// 修改List Type的Entity中的sublists (僅新增, 不會移除不在列表中的項目)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> PatchClosedListAsync(Guid id, ClosedList list);

        Task<bool> DeleteEntityAsync(Guid id);

        Task<bool> DeleteClosedListAsync(Guid id);
    }

    public class ProgrammaticClient
        : ITrainClient, IEntityClient, IIntentClient
    {
        private static HttpClient httpClient;

        static ProgrammaticClient()
        {
            httpClient = new HttpClient();
        }

        public ProgrammaticClient(string subscriptionKey,
            string appId,
            string versionId,
            EndpointRegions region = EndpointRegions.WestUs)
        {
            SubscriptionKey = subscriptionKey;
            AppId = appId;
            VersionId = versionId;
            Region = region;
        }

        public string SubscriptionKey { get; }
        public string AppId { get; }
        public string VersionId { get; }
        public EndpointRegions Region { get; }

        public Uri Endpoint => new Uri(string.Format(Consts.Endpoint, this.Region.ToString().ToLower(),
            this.AppId,
            this.VersionId));

        async Task<ClosedList> IEntityClient.GetClosedListAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"closedlists/{id}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<ClosedList>(str);
        }

        async Task<Entity> IEntityClient.GetEntityAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"entities/{id}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<Entity>(str);
        }

        async Task<IEnumerable<TrainingStatusResult>> ITrainClient.GetTrainStatusAsync()
        {
            var uri = new Uri(Endpoint, "train");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<IEnumerable<TrainingStatusResult>>(str);
        }

        async Task<IEnumerable<ClosedList>> IEntityClient.ListClosedListsAsync()
        {
            var uri = new Uri(Endpoint, $"closedlists");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<IEnumerable<ClosedList>>(str);
        }

        async Task<IEnumerable<Entity>> IEntityClient.ListEntitiesAsync()
        {
            var uri = new Uri(Endpoint, $"entities");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<IEnumerable<Entity>>(str);
        }

        async Task<TrainResult> ITrainClient.TrainAsync()
        {
            var uri = new Uri(Endpoint, "train");
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}
			return JsonConvert.DeserializeObject<TrainResult>(str);
		}

		async Task ITrainClient.PublishAsync(Regions region, bool isStaging)
		{
            var uri = new Uri(Endpoint, "../../publish");
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
			request.Content = new JsonContent<PuslishRequest>(new PuslishRequest
			{
				Region = region,
				VersionId = this.VersionId,
				IsStaging = isStaging
			});

			var response = await httpClient.SendAsync(request);
			var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}
		}

		async Task<Guid> IEntityClient.CreateClosedListAsync(ClosedList list)
        {
            var uri = new Uri(Endpoint, "closedlists");
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<ClosedList>(list);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<Guid>(str);
        }

        async Task<Guid> IEntityClient.CreateEntityAsync(Entity entity)
        {
            var uri = new Uri(Endpoint, "entities");
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<Entity>(entity);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<Guid>(str);
        }

        async Task<bool> IEntityClient.PatchClosedListAsync(Guid id, ClosedList list)
        {
            var uri = new Uri(Endpoint, $"closedlists/{id}");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<ClosedList>(list);

            var response = await httpClient.SendAsync(request);
			var str = await response.Content.ReadAsStringAsync();

			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return response.IsSuccessStatusCode;
        }

        async Task<bool> IEntityClient.UpdateClosedListAsync(Guid id, ClosedList list)
        {
            var uri = new Uri(Endpoint, $"closedlists/{id}");
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<ClosedList>(list);

            var response = await httpClient.SendAsync(request);
			var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}
			return response.IsSuccessStatusCode;
		}

        async Task<bool> IEntityClient.UpdateEntityAsync(Guid id, Entity entity)
        {
            var uri = new Uri(Endpoint, $"entities/{id}");
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<Entity>(entity);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return response.IsSuccessStatusCode;
		}

        async Task<bool> IEntityClient.DeleteEntityAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"entities/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return response.IsSuccessStatusCode;
		}

        async Task<bool> IEntityClient.DeleteClosedListAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"closedlists/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return response.IsSuccessStatusCode;
		}

        async Task<Guid> IIntentClient.CreateAsync(string name)
        {
            var uri = new Uri(Endpoint, $"intents");
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<object>(new
            {
                name
            });

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<Guid>(str);
        }

        async Task<IEnumerable<Intent>> IIntentClient.ListAsync()
        {
            var uri = new Uri(Endpoint, $"intents");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<IEnumerable<Intent>>(str);
        }

        async Task<Intent> IIntentClient.GetAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"intents/{id}");
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}

			return JsonConvert.DeserializeObject<Intent>(str);
        }

        async Task IIntentClient.UpdateAsync(Guid id, string name)
        {
            var uri = new Uri(Endpoint, $"intents/{id}");
            var request = new HttpRequestMessage(HttpMethod.Put, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);
            request.Content = new JsonContent<object>(new
            {
                name
            });

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}
		}

        async Task IIntentClient.DeleteAsync(Guid id)
        {
            var uri = new Uri(Endpoint, $"intents/{id}");
            var request = new HttpRequestMessage(HttpMethod.Delete, uri);
            request.Headers.Add(Consts.SubscriptionHeaderKey, SubscriptionKey);

            var response = await httpClient.SendAsync(request);
            var str = await response.Content.ReadAsStringAsync();
			if (response.IsSuccessStatusCode == false)
			{
				throw new LuisIntegrateException(str);
			}
		}
	}
}
