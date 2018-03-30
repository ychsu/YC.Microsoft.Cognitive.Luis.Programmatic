using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using YC.Microsoft.Cognitive.Luis.Programmatic;
using YC.Microsoft.Cognitive.Luis.Programmatic.Enums;
using YC.Microsoft.Cognitive.Luis.Programmatic.Models;

namespace Programmatic.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ITrainClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
            var result = client.TrainAsync().GetAwaiter().GetResult();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            ITrainClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
            var result = client.GetTrainStatusAsync().GetAwaiter().GetResult();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void ListEntitiesTest()
        {
            IEntityClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
            var result = client.ListClosedListsAsync().GetAwaiter().GetResult();
            Console.WriteLine(result);
        }

        [TestMethod]
        public void TestMethod3()
        {
            IEntityClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
            var result = client.CreateEntityAsync(new Entity
            {
                Name = "測試建立entity"
            }).GetAwaiter().GetResult();
            client.DeleteEntityAsync(result).Wait();
		}

		[TestMethod]
		public void TestMethod4()
		{
			IEntityClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
			var entity = new ClosedList
			{
				Name = "listEntity2",
				SubLists = new List<ClosedListItem>
				{
					new ClosedListItem
					{
						CanonicalForm = "許育銓",
						List = new List<string>
						{
							"yc",
							"YC",
							"育銓",
							"Saul"
						}
					}
				}
			};
			var result = client.CreateClosedListAsync(entity).GetAwaiter().GetResult();
			Console.WriteLine(result);
		}

		[TestMethod]
		public void TestMethod5()
		{
			IEntityClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
			var entity = new ClosedList
			{
				Id = new Guid("00948157-6e8b-483f-bbb4-878f5fe3a0d1"),
				Name = "listEntity",
				SubLists = new List<ClosedListItem>
				{
					new ClosedListItem
					{
						CanonicalForm = "許育銓",
						List = new List<string>
						{
							"yc",
							"育銓",
							"Saul"
						}
					}
				}
			};
			var str = JsonConvert.SerializeObject(entity);
			var result = client.UpdateClosedListAsync(entity.Id, entity).GetAwaiter().GetResult();
			Console.WriteLine(result);
		}

		[TestMethod]
		public void TestMethod6()
		{
			ITrainClient client = new ProgrammaticClient("f9efec420d174b7f828c91f5aa9e6114", "197c199c-cc35-47df-a275-7cf72244dd1d", "0.1");
			client.PublishAsync(Regions.SoutheastAsia, false).Wait();
		}
	}
}
