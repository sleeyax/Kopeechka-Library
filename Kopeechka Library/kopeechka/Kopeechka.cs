using System;
using System.Net;
using System.Threading.Tasks;
using Kopeechka.Exceptions;
using Kopeechka.Objects;
using System.Text.Json;
using System.Net.Http;

namespace Kopeechka
{
    public class KopeechkaApi
    {

        public const string endpoint = "http://api.kopeechka.store/";

        private string apikey;
        public KopeechkaApi(string api_key)
        {
            apikey = api_key;
        }

        public async Task<OrderRequest> GenerateEmailAsync(string site, string mailType, string sender, string regex, string softId, string subject, string investor = "0", string answerType = "JSON", string clear = "0")
        {
            var requestUrl = $"{endpoint}/mailbox-get-email?site={site}&mail_type={mailType}&password=1&sender={sender}&ex={regex}&token={this.apikey}&soft={softId}&investor={investor}&type={answerType}&subject={subject}&clear={clear}&api=2.0";

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Invalid response: {response.StatusCode}");
            }

            CheckAndThrowException(response.StatusCode, content);
            return JsonSerializer.Deserialize<OrderRequest>(content);
        }

        public async Task<OrderResponse> FetchEmailAsync(string full, string taskId, string type = "JSON")
        {
            var requestUrl = $"{endpoint}/mailbox-get-message?full={full}&id={taskId}&token={this.apikey}&type={type}&api=2.0";

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Invalid response: {response.StatusCode}");
            }

            CheckAndThrowException(response.StatusCode, content);
            return JsonSerializer.Deserialize<OrderResponse>(content);
        }

        public async Task<OrderRequest> ReorderMailAsync(string site, string email, string regex, string subject, string type = "JSON")
        {
            var requestUrl = $"{endpoint}/mailbox-reorder?site={site}&email={email}&regex={regex}&token={this.apikey}&type={type}&subject={subject}&api=2.0";

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Invalid response: {response.StatusCode}");
            }

            CheckAndThrowException(response.StatusCode, content);
            return JsonSerializer.Deserialize<OrderRequest>(content);
        }

        public async Task<OrderCancel> CancelMailAsync(string taskId, string type = "JSON")
        {
            var requestUrl = $"{endpoint}/mailbox-cancel?id={taskId}&token={this.apikey}&type={type}&api=2.0";

            using var httpClient = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

            var response = await httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Invalid response: {response.StatusCode}");
            }

            CheckAndThrowException(response.StatusCode, content);
            return JsonSerializer.Deserialize<OrderCancel>(content);
        }

        private static void CheckAndThrowException(HttpStatusCode statusCode, string content)
        {
            if (statusCode == HttpStatusCode.OK)
            {
                if (content == "no free phones")
                {
                    throw new NoFreePhonesException();
                }
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                switch (content)
                {
                    case "order not found":
                        throw new OrderNotFoundException();
                    case "not enough product qty":
                        throw new NotEnoughProductQuantityException();
                    case "not enough user balance":
                        throw new NotEnoughUserBalanceException();
                    case "not enough rating":
                        throw new NotEnoughRatingException();
                }
            }
            else if (statusCode == HttpStatusCode.NotFound)
            {
                switch (content)
                {
                    case "order not found":
                        throw new OrderNotFoundException();
                    case "order expired":
                        throw new OrderExpiredException();
                    case "order has sms":
                        throw new OrderHasSmsException();
                    case "hosting order":
                        throw new HostingOrderException();
                }
            }
        }
    }
}
