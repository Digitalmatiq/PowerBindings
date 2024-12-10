using PowerBindings.BindBehaviours;
using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface IParallelFilterKeyTrait : IKeyHandler
{
   HookKey Key { get; }

   IDisposable CreateBinding() => new FilterKeyBehaviour(Key, ParallelKeyBehaviour.Instance).CreateBinding(OnKeyPressed);
}