using PowerBindings.BindBehaviours;
using PowerBindings.Models;

namespace PowerBindings.Traits;

public interface ISwitchKeyTrait : IKeyHandler
{
   HookKey Key { get; }

   HookKey SwitchKey { get; }

   IKeyBindBehaviour OriginalBehaviour => SingleKeyBehaviour.Instance;

   IKeyBindBehaviour OtherBehaviour { get; }

   IDisposable BindKey() => new SwitchKeyBehaviour(Key, SwitchKey, OriginalBehaviour, OtherBehaviour).CreateBinding(OnKeyPressed);
}