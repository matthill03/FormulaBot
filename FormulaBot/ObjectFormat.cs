
using System.Collections.Generic;

namespace FormulaBot
{
    public class Location
    {
        public string lat { get; set; }
        public string @long { get; set; }
        public string locality { get; set; }
        public string country { get; set; }
    }

    public class Circuit
    {
        public string circuitId { get; set; }
        public string url { get; set; }
        public string circuitName { get; set; }
        public Location Location { get; set; }
    }

    public class Constructor
    {
        public string constructorId { get; set; }
        public string url { get; set; }
        public string name { get; set; }
        public string nationality { get; set; }
    }
    
    public class ConstructorTable
    {
        public string season { get; set; }
        public List<Constructor> Constructors { get; set; }
    }

    public class QualifyingResult
    {
        public string number { get; set; }
        public string position { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
    }

    public class Time
    {
        public string millis { get; set; }
        public string time { get; set; }
    }

    public class AverageSpeed
    {
        public string units { get; set; }
        public string speed { get; set; }
    }

    public class FastestLap
    {
        public string rank { get; set; }
        public string lap { get; set; }
        public Time Time { get; set; }
        public AverageSpeed AverageSpeed { get; set; }
    }

    public class Result
    {
        public string number { get; set; }
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
        public string grid { get; set; }
        public string laps { get; set; }
        public string status { get; set; }
        public Time Time { get; set; }
        public FastestLap FastestLap { get; set; }
    }

    

    public class RaceTable
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<Race> Races { get; set; }
    }

    public class Race
    {
        public string season { get; set; }
        public string round { get; set; }
        public string url { get; set; }
        public string raceName { get; set; }
        public Circuit Circuit { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public List<Result> Results { get; set; }
        public List<QualifyingResult> QualifyingResults { get; set; }
    }

    public class Driver
    {
        public string driverId { get; set; }
        public string permanentNumber { get; set; }
        public string code { get; set; }
        public string url { get; set; }
        public string givenName { get; set; }
        public string familyName { get; set; }
        public string dateOfBirth { get; set; }
        public string nationality { get; set; }
    }

    public class DriverTable
    {
        public string season { get; set; }
        public List<Driver> Drivers { get; set; }
    }
    public class DriverStanding
    {
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public string wins { get; set; }
        public Driver Driver { get; set; }
        public List<Constructor> Constructors { get; set; }
    }
    public class ConstructorStanding
    {
        public string position { get; set; }
        public string positionText { get; set; }
        public string points { get; set; }
        public string wins { get; set; }
        public Constructor Constructor { get; set; }
    }

    public class StandingsList
    {
        public string season { get; set; }
        public string round { get; set; }
        public List<DriverStanding> DriverStandings { get; set; }
        public List<ConstructorStanding> ConstructorStandings { get; set; }
    }

    public class StandingsTable
    {
        public string season { get; set; }
        public List<StandingsList> StandingsLists { get; set; }
    }

    public class MRData
    {
        public string xmlns { get; set; }
        public string series { get; set; }
        public string url { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public string total { get; set; }
        public DriverTable DriverTable { get; set; }
        public RaceTable RaceTable { get; set; }
        public StandingsTable StandingsTable { get; set; }
        public ConstructorTable ConstructorTable { get; set; }
    }

    public class Root
    {
        public MRData MRData { get; set; }
    }
}