using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Resources;
using Newtonsoft.Json;
using SAHB.GraphQLClient.Executor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Query
{

    // 1. Create Model: Input type is used for Mutation, it should be included if needed
    public class QueryAllUsersByCompany_Input
    {
        public company company { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class QueryAllUsersByCompany_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public company company { get; set; }
    }

    public class QueryAllUsersByCompany
    {

        public QueryAllUsersByCompany()
        {
        }

        public async Task<QueryAllUsersByCompany_Output> Execute(QueryAllUsersByCompany_Input input)//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            QueryAllUsersByCompany_Output output = new QueryAllUsersByCompany_Output();


            string queryRaw = @"
query allusersbycompany {
  company(id: " + SAHBResource.SetArgumentRaw(input.company.companyId) + @") {
    companyId
    users {
                id
                userAppId
      firstName
      lastName
      userName
      nickname
      alias
      email
      lastSeen
      department {
                    departmentId
                    name
      }
                groups {
                    groupofId
                    name
                }
            }
        }
    }

";

            string queryToExecute = " {\"query\":\" " + queryRaw + " \"} ";

            IGraphQLHttpExecutor executor = new GraphQLHttpExecutor();
            var result = await executor.ExecuteQuery(query: queryToExecute, url: input.url, method: HttpMethod.Post, authorizationMethod: "Bearer", authorizationToken: input.token);

            dynamic stuff = JsonConvert.DeserializeObject(result.Response);

            // Find a way to see errors
            bool errors = false;
            //foreach (var error in stuff.errors)
            {
                //System.Diagnostics.Debug.WriteLine("Error: " + error["message"]);
            }

            if (!errors)
            {
                output.company = stuff.data.company.ToObject<company>();                
                output.ResultConfirmation = new resultConfirmation { resultPassed = true, resultMessage = "OK", resultDetail = "" };
            }
            else
            {
                output.company = null;
                output.ResultConfirmation = new resultConfirmation { resultPassed = false, resultMessage = "ErrorMessage", resultDetail = "resultDetail" };
            }

            return output;

        }
    }
}
