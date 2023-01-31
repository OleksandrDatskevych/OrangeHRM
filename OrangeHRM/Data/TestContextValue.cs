using NUnit.Framework;

namespace OrangeHRM.Data;

public class TestContextValues
{
    public static string ExecutableClassName => TestContext.CurrentContext.Test.ClassName;
}
