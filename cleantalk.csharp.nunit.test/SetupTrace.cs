using System.Diagnostics;
using NUnit.Framework;

namespace cleantalk.csharp.test
{
    public class SetupTrace
    {
        [SetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [TearDown]
        public void EndTest()
        {
            Trace.Flush();
        }
    }
}