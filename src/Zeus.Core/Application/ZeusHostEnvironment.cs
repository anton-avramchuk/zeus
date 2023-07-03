using Zeus.Core.Application.Abstraction;

namespace Zeus.Core.Application
{
    public class ZeusHostEnvironment : IZeusHostEnvironment
    {
        public string? EnvironmentName { get; set; }
    }
}
