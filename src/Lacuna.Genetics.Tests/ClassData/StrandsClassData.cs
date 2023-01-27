using System.Collections;

namespace Lacuna.Genetics.Tests.ClassData;

internal class StrandsClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "CATCGTCAGGAC", "TbSh" };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}