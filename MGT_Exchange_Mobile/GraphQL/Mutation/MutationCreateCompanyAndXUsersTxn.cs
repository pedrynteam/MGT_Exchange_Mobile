using MGT_Exchange_Client.GraphQL.MVC;
using MGT_Exchange_Client.GraphQL.Resources;
using Newtonsoft.Json;
using SAHB.GraphQLClient.Executor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Mutation
{
    // 1. Create Model: Input type is used for Mutation, it should be included if needed
    public class MutationCreateCompanyAndXUsersTxn_Input
    {
        public int userstocreate { get; set; }
        public company company { get; set; }
        public string url { get; set; }
        public string token { get; set; }
    }

    public class MutationCreateCompanyAndXUsersTxn_Output
    {
        public resultConfirmation ResultConfirmation { get; set; }
        public company company { get; set; }
    }

    public class MutationCreateCompanyAndXUsersTxn
    {

        public MutationCreateCompanyAndXUsersTxn()
        {
        }

        public async Task<MutationCreateCompanyAndXUsersTxn_Output> Execute(MutationCreateCompanyAndXUsersTxn_Input input)//, IServiceProvider serviceProvider, MVCDbContext contextFatherMVC = null, ApplicationDbContext contextFatherApp = null, bool autoCommit = true)
        {
            MutationCreateCompanyAndXUsersTxn_Output output = new MutationCreateCompanyAndXUsersTxn_Output();

            string queryRaw = @"
mutation {
  createCompanyAndXUsersTxn(
    input: {
        company: {
        name: " + SAHBResource.SetArgumentRaw(input.company.name) + @"
        description: " + SAHBResource.SetArgumentRaw(input.company.description) + @"
        email: " + SAHBResource.SetArgumentRaw(input.company.email) + @"
        password: " + SAHBResource.SetArgumentRaw(input.company.password) + @"
        }
        usersToCreate: " + input.userstocreate + @"
    }
  ) {
    resultConfirmation {
        resultCode
        resultPassed
        resultMessage
        resultDetail
        resultDictionary {
            tag
            value
        }
    }
    company {
        companyId
        loginTokenId
        tokenAuth
        users
        {
            id
            userAppId
            userName
            firstName
            lastName
            nickname
            alias
            loginTokenId
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
                // If no high level errors return the result as it is from server. It may contain errors
                output = stuff.data.createCompanyAndXUsersTxn.ToObject<MutationCreateCompanyAndXUsersTxn_Output>();
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
