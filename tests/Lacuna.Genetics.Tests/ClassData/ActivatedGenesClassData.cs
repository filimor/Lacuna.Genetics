using System.Collections;

namespace Lacuna.Genetics.Tests.ClassData;

internal class ActivatedGenesClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "WVpYV8LkTqZeef1CkFPDuxjSkzePAeixX0C31pDTKpP+JHOlCTa9BwuxGi1m8BSqZpLWvXDGX1hxpviCLXgWi09qRGvgHbHiFIsr",
            "slkAS0Ml0RVp2RRNOkYwCoWo49XygRrivSP9NGW3PwVC9jcD+gLa6hvOhCdgLHguBKvnYZIhFn0/HY0fKJywspkPC0QPLRCCzfi5JPzygpGrzi+5z95zId+6w910usJ/HdKNUSE0H9ebZJSZZ/GATJr8dFlt9uK7m+lkVWozDRI2OgcqZDFvdq9YHmpJ2bntOXdCd0ZoESbv0rU3z8psZ1eBYy3qVudpjf2qFdh4ccEwdxvz8JbMzSjgOpJ7N6fl67YTyHaLAuKP2epIvvxOd2DzB1iueXbBtJUFChtU7VVWxCkhZKlLpHQ4YxerhA4skcMyhzBPN3E/UhPghvjHOuv0bsVmhhgK9b6w8ROctbMhw/hdOoL9IKW8s1WwB24xa9slC+PRO5dKZD+tVmW0pQo85oKeOWQd90ofpdLCVu5Qf4k4d6+ecn8TpqeGWJzMWwLVshFw+wIR"
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}