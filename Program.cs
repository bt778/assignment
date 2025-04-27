using System;
using System.Collections.Generic;

abstract class EmergencyUnit
{
    public string Name { get; set; }
    public int Speed { get; set; }

    public EmergencyUnit(string name, int speed)
    {
        Name = name;
        Speed = speed;
    }

    public abstract bool CanHandle(string incidentType);
    public abstract void RespondToIncident(Incident incident, ref int score);
}

class Police : EmergencyUnit
{
    public Police(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType)
    {
        return incidentType == "Crime";
    }

    public override void RespondToIncident(Incident incident, ref int score)
    {
        int time = new Random().Next(1, 6); 
        int difficulty = incident.Type == "Crime" ? 2 : 1; 
        score += (10 - time) * difficulty;
        Console.WriteLine($"{Name} is responding to a crime at {incident.Location}. Response Time: {time}s. Difficulty: {difficulty}");
    }
}

class Firefighter : EmergencyUnit
{
    public Firefighter(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType)
    {
        return incidentType == "Fire";
    }

    public override void RespondToIncident(Incident incident, ref int score)
    {
        int time = new Random().Next(1, 6);
        int difficulty = incident.Type == "Fire" ? 3 : 1;
        score += (10 - time) * difficulty;
        Console.WriteLine($"{Name} is extinguishing a fire at {incident.Location}. Response Time: {time}s. Difficulty: {difficulty}");
    }
}

class Ambulance : EmergencyUnit
{
    public Ambulance(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType)
    {
        return incidentType == "Medical";
    }

    public override void RespondToIncident(Incident incident, ref int score)
    {
        int time = new Random().Next(1, 6);
        int difficulty = incident.Type == "Medical" ? 2 : 1;
        score += (10 - time) * difficulty;
        Console.WriteLine($"{Name} is treating patients at {incident.Location}. Response Time: {time}s. Difficulty: {difficulty}");
    }
}

class Paramedic : EmergencyUnit
{
    public Paramedic(string name, int speed) : base(name, speed) { }

    public override bool CanHandle(string incidentType)
    {
        return incidentType == "Medical";
    }

    public override void RespondToIncident(Incident incident, ref int score)
    {
        int time = new Random().Next(1, 6);
        int difficulty = incident.Type == "Medical" ? 1 : 1;
        score += (10 - time) * difficulty;
        Console.WriteLine($"{Name} is providing medical assistance at {incident.Location}. Response Time: {time}s. Difficulty: {difficulty}");
    }
}

class Incident
{
    public string Type { get; set; }
    public string Location { get; set; }

    public Incident(string type, string location)
    {
        Type = type;
        Location = location;
    }
}

class Program
{
    static void Main()
    {
        List<EmergencyUnit> units = new List<EmergencyUnit>
        {
            new Police("Police Unit 1", 80),
            new Firefighter("Firefighter Unit 1", 70),
            new Ambulance("Ambulance Unit 1", 90),
            new Paramedic("Paramedic Unit 1", 85)
        };

        string[] incidentTypes = { "Fire", "Crime", "Medical", "Natural Disaster" };
        string[] locations = { "City Hall", "Downtown", "Suburbs", "Mall", "Airport" };

        Random rand = new Random();
        int score = 0;

        for (int turn = 1; turn <= 5; turn++)
        {
            Console.WriteLine($"\n*****Turn {turn} *****");

            string randomType = incidentTypes[rand.Next(incidentTypes.Length)];
            string randomLocation = locations[rand.Next(locations.Length)];
            Incident newIncident = new Incident(randomType, randomLocation);

            Console.WriteLine($"Incident: {newIncident.Type} at {newIncident.Location}");

           
            Console.WriteLine("\nChoose a unit to respond to the incident:");
            for (int i = 0; i < units.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {units[i].Name} (Speed: {units[i].Speed})");
            }

            int choice = int.Parse(Console.ReadLine()) - 1;

            if (choice >= 0 && choice < units.Count)
            {
                EmergencyUnit selectedUnit = units[choice];
                if (selectedUnit.CanHandle(newIncident.Type))
                {
                    selectedUnit.RespondToIncident(newIncident, ref score);
                    Console.WriteLine("+ points based on response time and difficulty");
                }
                else
                {
                    Console.WriteLine("This unit cannot handle the incident.");
                    score -= 5; 
                    Console.WriteLine("-5 points");
                }
            }
            else
            {
                Console.WriteLine("Invalid unit selection.");
            }

            Console.WriteLine($"Current Score: {score}");
        }

        Console.WriteLine($"\n******Simulation Complete ******");
        Console.WriteLine($"Final Score: {score}");
        Console.ReadLine();
    }
}
