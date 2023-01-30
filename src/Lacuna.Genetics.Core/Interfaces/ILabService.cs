using Lacuna.Genetics.Core.Models;

namespace Lacuna.Genetics.Core.Interfaces;

public interface ILabService
{
    Result Analyze(Job job);
}