using PowerBindings.BindBehaviours;
using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface ICancelFilterKeyTrait : IKeyHandler
{
   HookKey Key { get; }

   HookKey CancelKey { get; }

   IDisposable BindKey() => new CancelKeyBehaviour(Key, CancelKey).CreateBinding(OnKeyPressed);
}