using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface IKeyHandler
{
   ValueTask OnKeyPressed(HookKey key);
}
