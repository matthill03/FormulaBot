using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace FormulaBot.Commands
{
    public class InfoCommands : BaseCommandModule
    {
        private RestResponse GetRestResponse(string inputUri)
        {
            var uri = new Uri(inputUri);
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.ExecuteAsync(request).Result;

            return response;
        }

        [Command("driverlist")]
        public async Task GetCurrentDriverList(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current/drivers.json");

            if (response.Content != null)
            {
                var driverList = $"List of drivers from the current season\n\n";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Driver driver in data.MRData.DriverTable.Drivers)
                {
                    driverList += $"Name: {driver.givenName} {driver.familyName} \nNumber: {driver.permanentNumber} \nDOB: {driver.dateOfBirth} \nCode: {driver.code} \n=======> \n\n";
                }

                await ctx.Channel.SendMessageAsync("```" + driverList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("driverlist")]

        public async Task GetAnyDriverListYear(CommandContext ctx, int year)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/drivers.json");

            if (response.Content != null)
            {
                var driverList = $"List of drivers from the {year} season\n\n";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Driver driver in data.MRData.DriverTable.Drivers)
                {
                    driverList += $"Name: {driver.givenName} {driver.familyName} \nNumber: {driver.permanentNumber} \nDOB: {driver.dateOfBirth} \nCode: {driver.code} \n=======> \n\n";
                }

                await ctx.Channel.SendMessageAsync("```" + driverList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("constructorlist")]
        public async Task GetCurrentConstructorList(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current/constructors.json");

            if (response.Content != null)
            {
                var constructorList = $"List of constructors from the current season\n\n";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Constructor constructor in data.MRData.ConstructorTable.Constructors)
                {
                    constructorList += $"Name: {constructor.name} \nNationality: {constructor.nationality} \n=======> \n\n";
                }

                await ctx.Channel.SendMessageAsync("```" + constructorList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("constructorlist")]
        public async Task GetAnyConstructorList(CommandContext ctx, int year)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/constructors.json");

            if (response.Content != null)
            {
                var constructorList = $"List of constructors from the {year} season\n\n";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Constructor constructor in data.MRData.ConstructorTable.Constructors)
                {
                    constructorList += $"Name: {constructor.name} \nNationality: {constructor.nationality} \n=======> \n\n";
                }

                await ctx.Channel.SendMessageAsync("```" + constructorList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("racelist")]
        public async Task GetCurrentRaceList(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current.json");

            if (response.Content != null)
            {
                var scheduleList = "";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Race race in data.MRData.RaceTable.Races)
                {
                    scheduleList += $"Name: {race.Circuit.circuitName} \nLocation: {race.Circuit.Location.country} \nRound: {race.round} \nDate: {race.date} \n=======> \n\n";

                }

                await ctx.Channel.SendMessageAsync("```" + scheduleList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("racelist")]
        public async Task GetAnyRaceList(CommandContext ctx, int year)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}.json");

            if (response.Content != null)
            {
                var scheduleList = "";
                var data = JsonConvert.DeserializeObject<Root>(response.Content);
                foreach (Race race in data.MRData.RaceTable.Races)
                {
                    scheduleList += $"Name: {race.Circuit.circuitName} \nLocation: {race.Circuit.Location.country} \nRound: {race.round} \nDate: {race.date} \n=======> \n\n";

                }

                await ctx.Channel.SendMessageAsync("```" + scheduleList + "```").ConfigureAwait(false);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        [Command("raceresult")]
        public async Task GetLastRaceResults(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current/last/results.json");

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
        public async Task GetAnyRaceResults(CommandContext ctx, int year, int round)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/{round}/results.json");

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

        [Command("driverstandings")]
        public async Task GetCurrentDriverStandings(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current/driverStandings.json");

            var data = JsonConvert.DeserializeObject<Root>(response.Content);
            var standingString = $"Current Drivers Championship Standings\n\n";

            foreach (StandingsList standingList in data.MRData.StandingsTable.StandingsLists)
            {
                foreach (DriverStanding driverStanding in standingList.DriverStandings)
                {
                    standingString += $"Driver: {driverStanding.Driver.givenName} {driverStanding.Driver.familyName}\nPosition: {driverStanding.position} \nPoints: {driverStanding.points} \nWins: {driverStanding.wins} \n=======> \n\n";
                }
            }

            await ctx.Channel.SendMessageAsync("```" + standingString + "```").ConfigureAwait(false);
        }

        [Command("constructorstanding")]
        public async Task GetCurrentConstructorStanding(CommandContext ctx)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/current/constructorStandings.json");

            var data = JsonConvert.DeserializeObject<Root>(response.Content);
            var standingString = $"Current Constructor Championship Standing\n\n";

            foreach (StandingsList standingList in data.MRData.StandingsTable.StandingsLists)
            {
                foreach (ConstructorStanding constructorStanding in standingList.ConstructorStandings)
                {
                    standingString += $"Position: {constructorStanding.position} \nConstructor: {constructorStanding.Constructor.name} \nPoints: {constructorStanding.points} \nWins: {constructorStanding.wins} \n=======> \n\n";
                }
            }

            await ctx.Channel.SendMessageAsync("```" + standingString + "```").ConfigureAwait(false);
        }

        [Command("driverchamp")]
        public async Task GetSeasonResultDriver(CommandContext ctx, int year)
        {
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/driverStandings.json");

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
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/constructorStandings.json");

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
            var response = GetRestResponse($"http://ergast.com/api/f1/{year}/{round}/qualifying.json");

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

