using Lacuna.Genetics.Core;
using Lacuna.Genetics.Core.Models;

var user = new User("filimor", "zkdvz3dA3!nBJcn94y**");

var doJob = true;
var jobsHandler = new JobsHandler(user, new Laboratory(), new HttpService());

while (doJob)
{
    try
    {
        var (response, result) = jobsHandler.DoJobAsync().Result;

        Console.WriteLine($"JOB ID: {response.Job!.Id}\n");
        Console.WriteLine($"JOB TYPE: {response.Job!.Type}");
        Console.WriteLine($"JOB RESPONSE: {response.Code} {(!string.IsNullOrEmpty(response.Message) ? '-' : ' ')} {response.Message}");

        Console.WriteLine(response.Job!.Type == "EncodeStrand"
            ? $"STRAND:\n{response.Job?.Strand}"
            : $"STRAND ENCODED:\n{response.Job?.StrandEncoded}");

        if (response.Job!.Type == "CheckGene")
        {
            Console.WriteLine($"\nGENE ENCODED:\n{response.Job?.GeneEncoded}");
        }

        switch (response.Job!.Type)
        {
            case "DecodeStrand":
                Console.WriteLine($"\nSTRAND:\n{result.Strand}\n");
                break;
            case "EncodeStrand":
                Console.WriteLine($"\nSTRAND ENCODED:\n{result.StrandEncoded}\n");
                break;
            case "CheckGene":
                Console.WriteLine($"\nIS ACTIVATED:\n{result.IsActivated}\n");
                break;
        }

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine(e.InnerException?.Message);
        Console.WriteLine(e.StackTrace);
        Console.WriteLine(e.Source);
        Console.WriteLine();
    }

    Console.Write("Do another job? (y/n) ");
    doJob = Console.ReadLine() == "y";
    Console.WriteLine();
    Console.WriteLine("-------------------------");
    Console.WriteLine();
}