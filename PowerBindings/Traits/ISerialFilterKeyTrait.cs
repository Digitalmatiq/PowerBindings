using PowerBindings.BindBehaviours;
using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface ISerialFilterKeyTrait : IKeyHandler
{
   HookKey Key { get; }

   IDisposable BindKey() => new FilterKeyBehaviour(Key, SerialKeyBehaviour.Instance).CreateBinding(OnKeyPressed);
}