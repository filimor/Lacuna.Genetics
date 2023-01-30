using Lacuna.Genetics.Core;
using Lacuna.Genetics.Core.Models;

var user = new User("filimor", "zkdvz3dA3!nBJcn94y**");
var jobsHandler = new JobsHandler(new LabService(), new HttpService(user));
var doJob = true;

while (doJob)
{
    try
    {
        var (response, result) = jobsHandler.DoJob();

        Console.WriteLine(response);
        Console.WriteLine(result);
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