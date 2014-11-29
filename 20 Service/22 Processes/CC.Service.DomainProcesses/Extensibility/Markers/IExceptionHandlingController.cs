using CC.Common.Base.Logging;
using Emit.LocalizationProvider.Definition;

namespace CC.Service.DomainProcesses.Extensibility.Markers
{
    /// <summary>
    /// Controller marker for exceptionh handling controller classes
    /// </summary>
    public interface IExceptionHandlingController : IController
    {
        ILogger Logger { get; set; }

        ILocalizationProvider LocalizationProvider { get; set; }
    }
}
