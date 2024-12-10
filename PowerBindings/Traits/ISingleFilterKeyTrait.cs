using PowerBindings.BindBehaviours;
using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface ISingleFilterKeyTrait : IKeyHandler
{
   HookKey Key { get; }

   IDisposable CreateBinding() => new FilterKeyBehaviour(Key, SingleKeyBehaviour.Instance).CreateBinding(OnKeyPressed);
}