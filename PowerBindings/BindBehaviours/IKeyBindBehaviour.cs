using PowerBindings.Models;

namespace PowerBindings.BindBehaviours;

public interface IKeyBindBehaviour
{
   IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute);
}
