using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormulaBot.Commands
{
    public class Info : BaseCommandModule
    {
        [Command("driverlist")]
        public async Task GetDriverList(CommandContext ctx, int year)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/drivers.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            if (response.Content != null)
            {
                Console.WriteLine(JObject.Parse(response.Content));
                var driverList = "```";
                var counter = 0;
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                var max = data.MRData.DriverTable.Drivers.Count();
                foreach (Driver driver in data.MRData.DriverTable.Drivers)
                {
                    driverList += driver.givenName + " " + driver.familyName + "\n";
                    counter++;
                    if (counter == max)
                        driverList += "```";
                }

                await ctx.Channel.SendMessageAsync(driverList).ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
