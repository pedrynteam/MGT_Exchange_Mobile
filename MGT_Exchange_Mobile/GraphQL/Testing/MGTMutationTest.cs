using MGT_Exchange_Client.GraphQL.Interface;
using MGT_Exchange_Client.GraphQL.Mutation;
using MGT_Exchange_Client.GraphQL.MVC;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MGT_Exchange_Client.GraphQL.Testing
{
    public static class MGTMutationTest
    {
        public static async Task<MutationCreateCompanyAndXUsersTxn_Output> TestMutationCreateCompanyAndXUsersTxn()
        {
            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            MutationCreateCompanyAndXUsersTxn_Input input = new MutationCreateCompanyAndXUsersTxn_Input
            {
                company = new company
                {
                    name = "company10",
                    description = "one",
                    email = "one@one.com",
                    password = "12345"
                },
                userstocreate = 10,
                url = _url,
                token = _token
            };

            MutationCreateCompanyAndXUsersTxn_Output output = 
                await clientMGT.MutationCreateCompanyAndXUsersTxn(input: input);

            return output;
        }
        
        public static async Task<MutationCreateChatTxn_Output> TestMutationCreateChatTxn()
        {
            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            // Pending, when the user is not from this company return: Inner Exception 

            MutationCreateChatTxn_Input input = new MutationCreateChatTxn_Input
            {
                chat = new chat
                {
                    chatId = 0,
                    name = "Chat 1",
                    companyId = "04c6b67c-6f4d-455f-a472-d6206dd769df",
                    participants = new List<participant>
                    {
                        new participant
                        {
                            participantId = 0,
                            chatId = 0,
                            isAdmin = true,
                            userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b"
                        },
                        new participant
                        {
                            participantId = 0,
                            chatId = 0,
                            isAdmin = false,
                            userAppId = "127b0df2-d732-478e-bf84-0b673c48d145"
                        },
                        new participant
                        {
                            participantId = 0,
                            chatId = 0,
                            isAdmin = false,
                            userAppId = "3982349d-289d-4f3a-ab29-f3f6bb3893f4"
                        }
                    },
                },
                url = _url,
                token = _token
            };

            MutationCreateChatTxn_Output output = 
                await clientMGT.MutationCreateChatTxn(input: input);

            return output;
        }
        
        public static async Task<MutationAddCommentToChatTxn_Output> TestMutationAddCommentToChatTxn()
        {
            IMGTExchangeClient clientMGT = new MGTExchangeClient();
            string _url = "http://10.18.24.67:8082/";
            string _token = "token";

            MutationAddCommentToChatTxn_Input input = new MutationAddCommentToChatTxn_Input
            {
                comment = new comment
                {
                    chatId = 3,
                    message = "Hello There",
                    userAppId = "10d3ed1e-ae7d-4d0c-80e0-aa360d76025b"
                },
                url = _url,
                token = _token
            };

            MutationAddCommentToChatTxn_Output output = 
                await clientMGT.MutationAddCommentToChatTxn(input: input);

            return output;
        }
    }
}
