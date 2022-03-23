using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using RestSharp;
using System;
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

        [Command("raceresult")]
        public async Task GetLastRaceResults(CommandContext ctx)
        {
            var uri = new Uri($"http://ergast.com/api/f1/current/last/results.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            if (response.Content != null)
            {
                var RaceList = "";
                var circuitString = "";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);

                foreach (Race race in data.MRData.RaceTable.Races)
                {
                    circuitString += $"Circuit Name: {race.Circuit.circuitName} \nCircuit Location: {race.Circuit.Location.country} \nTime: {race.time}\n";
                    await ctx.Channel.SendMessageAsync("```" + circuitString + "```").ConfigureAwait(false);
                    foreach (Result result in race.Results)
                    {
                        RaceList += $"Driver : {result.Driver.givenName} {result.Driver.familyName} \nPosition: {result.position} \nConstructor: {result.Constructor.name} \n =======> \n\n";
                    }
                }
                await ctx.Channel.SendMessageAsync("```" + RaceList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("raceresult")]
        public async Task GetAnyRaceResults(CommandContext ctx, int? year, int? round)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/{round}/results.json");
            
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            var RaceList = "";
            var circuitString = "";
            var data = JsonConvert.DeserializeObject<Root>(response.Content);

            if (response.Content != null)
            {
                foreach (Race race in data.MRData.RaceTable.Races)
                {
                    circuitString += $"Circuit Name: {race.Circuit.circuitName} \nCircuit Location: {race.Circuit.Location.country} \nTime: {race.time}\n";
                    await ctx.Channel.SendMessageAsync("```" + circuitString + "```").ConfigureAwait(false);
                    foreach (Result result in race.Results)
                    {
                        RaceList += $"Driver : {result.Driver.givenName} {result.Driver.familyName} \nPosition: {result.position} \nConstructor: {result.Constructor.name} \n =======> \n\n";
                    }
                }
                await ctx.Channel.SendMessageAsync("```" + RaceList + "```").ConfigureAwait(false);

            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("driverchamp")]
        public async Task GetSeasonResultDriver(CommandContext ctx, int year)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/driverStandings.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            var data = JsonConvert.DeserializeObject<Root>(response.Content);
            var standingString = $"Drivers Championship standings of {year}\n\n";

            foreach (StandingsList standingList in data.MRData.StandingsTable.StandingsLists)
            {
                foreach (DriverStanding driverStanding in standingList.DriverStandings)
                {
                    standingString += $"Driver: {driverStanding.Driver.givenName} {driverStanding.Driver.familyName}\nPosition: {driverStanding.position} \nPoints: {driverStanding.points} \nWins: {driverStanding.wins} \n=======> \n\n";
                }
            }

            await ctx.Channel.SendMessageAsync("```" + standingString + "```").ConfigureAwait(false);

        }

        [Command("constructorchamp")]
        public async Task GetSeasonResultConstructor(CommandContext ctx, int year)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/constructorStandings.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            var data = JsonConvert.DeserializeObject<Root>(response.Content);
            var standingString = $"Constructor championship standings of {year}\n\n";

            foreach (StandingsList standingList in data.MRData.StandingsTable.StandingsLists)
            {
                foreach (ConstructorStanding constructorStanding in standingList.ConstructorStandings)
                {
                    standingString += $"Position: {constructorStanding.position} \nConstructor: {constructorStanding.Constructor.name} \nPoints: {constructorStanding.points} \nWins: {constructorStanding.wins} \n=======> \n\n";
                }
            }

            await ctx.Channel.SendMessageAsync("```" + standingString + "```").ConfigureAwait(false);

        }

        [Command("qualifyresult")]
        public async Task GetQualifyingResult(CommandContext ctx, int year, int round)
        {
            var uri = new Uri($"http://ergast.com/api/f1/{year}/{round}/qualifying.json");
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

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

