using PowerBindings.Models;

namespace PowerBindings.BindBehaviours;

/// <summary>
/// Generic Role base interface for executing key behaviours
/// </summary>
public interface IKeyBindBehaviour
{
   IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute);
}
