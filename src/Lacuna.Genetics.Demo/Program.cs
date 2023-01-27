using Lacuna.Genetics.Core;
using Lacuna.Genetics.Core.Models;

var user = new User("filimor", "zkdvz3dA3!nBJcn94y**");
var jobsHandler = new JobsHandler(user, new Laboratory(), new HttpService());
var doJob = true;

while (doJob)
{
    try
    {
        var (response, result) = jobsHandler.DoJobAsync().Result;

        Console.WriteLine($"JOB ID: {response.Job!.Id}");
        Console.WriteLine($"JOB TYPE: {response.Job!.Type}");
        Console.WriteLine(
            $"JOB RESPONSE: {response.Code} {(!string.IsNullOrEmpty(response.Message) ? '-' : ' ')} {response.Message}\n");

        Console.WriteLine(response.Job!.Type == JobsHandler.JobTypeEncodeStrand
            ? $"STRAND:\n{response.Job?.Strand}"
            : $"STRAND ENCODED:\n{response.Job?.StrandEncoded}");

        if (response.Job!.Type == JobsHandler.JobTypeCheckGene)
        {
            Console.WriteLine($"\nGENE ENCODED:\n{response.Job?.GeneEncoded}");
        }

        switch (response.Job!.Type)
        {
            case JobsHandler.JobTypeDecodeStrand:
                Console.WriteLine($"\nSTRAND:\n{result.Strand}\n");
                break;
            case JobsHandler.JobTypeEncodeStrand:
                Console.WriteLine($"\nSTRAND ENCODED:\n{result.StrandEncoded}\n");
                break;
            case JobsHandler.JobTypeCheckGene:
                Console.WriteLine($"\nIS ACTIVATED:\n{result.IsActivated}\n");
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine();
    }

    Console.Write("Do another job? (y/n) ");
    doJob = Console.ReadLine() == "y";
    Console.WriteLine();
    Console.WriteLine("-------------------------");
    Console.WriteLine();
}