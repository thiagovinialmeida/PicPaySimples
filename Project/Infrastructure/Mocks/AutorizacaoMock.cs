namespace PicpaySimples.Project.Infrastructure.Mocks
{

public class AutorizacaoMock
    {
        private static HttpClient _client = new HttpClient();

        public async Task<bool> Autorizacao()
        {
            HttpResponseMessage response =
                await _client.GetAsync(
                    $"https://util.devi.tools/api/v2/authorize");
            string rtn = await response.Content.ReadAsStringAsync();

            string[] autorizacao = rtn.Split(',');
            autorizacao = autorizacao[1].Split('{');
            autorizacao = autorizacao[1].Split(':');

            if (autorizacao[1].Replace('}', ' ') == " true   ")
            {
                return true;
            }

            return false;
        }
    }
}