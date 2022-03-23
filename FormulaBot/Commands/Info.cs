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
                var driverList = "";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Driver driver in data.MRData.DriverTable.Drivers)
                {
                    driverList += $"{driver.givenName} {driver.familyName} \n";
                }

                await ctx.Channel.SendMessageAsync("```" + driverList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("qualifyresult")]
        public async Task GetQualifyingResult(CommandContext ctx, int year, int round)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/{round}/qualifying.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            Console.WriteLine(JObject.Parse(response.Content));

            var qualyList = "";
            var circuitString = "";
            var data = JsonConvert.DeserializeObject<Root>(response.Content);

            if (response.Content != null)
            {
                foreach (Race race in data.MRData.RaceTable.Races)
                {
                    circuitString += $"Circuit Name: {race.Circuit.circuitName} \nCircuit Location: {race.Circuit.Location.country} \nTime : {race.time}\n";
                    await ctx.Channel.SendMessageAsync("```" + circuitString + "```").ConfigureAwait(false);
                    foreach (QualifyingResult qualifying in race.QualifyingResults)
                    {
                        if (qualifying.Q2 == null)
                        {
                            qualyList += $"Driver: {qualifying.Driver.givenName} {qualifying.Driver.familyName} \nQ1: {qualifying.Q1} \nQ2: OUT \nQ3: OUT \n ======> \n\n";
                        }
                        else if (qualifying.Q3 == null)
                        {
                            qualyList += $"Driver: {qualifying.Driver.givenName} {qualifying.Driver.familyName} \nQ1: {qualifying.Q1} \nQ2: {qualifying.Q2} \nQ3: OUT \n ======> \n\n";
                        }
                        else
                        {
                            qualyList += $"Driver: {qualifying.Driver.givenName} {qualifying.Driver.familyName} \nQ1: {qualifying.Q1} \nQ2: {qualifying.Q2} \nQ3: {qualifying.Q3} \n ======> \n\n";
                        }
                    }
                }
                await ctx.Channel.SendMessageAsync("```" + qualyList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}

