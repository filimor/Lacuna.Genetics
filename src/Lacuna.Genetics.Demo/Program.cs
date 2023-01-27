using Lacuna.Genetics.Core;
using Lacuna.Genetics.Core.Models;

var user = new User("filimor", "zkdvz3dA3!nBJcn94y**");

var doJob = true;
var jobsHandler = new JobsHandler(user, new Laboratory(), new HttpService());

while (doJob)
{
    try
    {
        var response = jobsHandler.DoJobAsync().Result;
        Console.WriteLine($"{response.Code}\t{response.Job?.Type}\t{response.Job?.Id}");
        Console.WriteLine(response.Message);
        Console.WriteLine();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        Console.WriteLine(e.InnerException?.Message);
        Console.WriteLine(e.StackTrace);
        Console.WriteLine(e.Source);
        Console.WriteLine();
    }

    Console.WriteLine("Do another? (y/n)");
    doJob = Console.ReadLine() == "y";
}