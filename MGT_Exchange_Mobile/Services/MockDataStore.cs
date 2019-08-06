using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MGT_Exchange_Client.GraphQL.Interface;
using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Query;
using MGT_Exchange_Mobile.Models;

namespace MGT_Exchange_Mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {

            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.7.169:8082/";
            string _token = "token";

            QueryAllUsersByCompany_Input input = new QueryAllUsersByCompany_Input
            {
                company = new company { companyId = "cb276c74-5f89-4362-9645-10f4b07be7be" },
                url = _url,
                token = _token
            };

            QueryAllUsersByCompany_Output output = await clientMGT.QueryAllUsersByCompany(input: input);

            List<Item> newItems = new List<Item>();

            if (output.ResultConfirmation.resultPassed)
            {
                foreach (var user in output.company.users.OrderBy(x => x.nickname))
                {
                    newItems.Add(new Item { Id = user.userAppId, Description = user.nickname, Text = user.firstName });
                }
            }

            return await Task.FromResult(newItems);
            //return await Task.FromResult(items);
        }
    }
}