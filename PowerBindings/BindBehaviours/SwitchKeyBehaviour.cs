using PowerBindings.Models;
using System.Reactive.Disposables;

namespace PowerBindings.BindBehaviours;

internal sealed class SwitchKeyBehaviour(HookKey Key, IKeyBindBehaviour behaviour1, IKeyBindBehaviour behaviour2)
   : IKeyBindBehaviour
{
   public SwitchKeyBehaviour(HookKey key, HookKey switchKey, IKeyBindBehaviour behaviour1, IKeyBindBehaviour behaviour2)
      : this(switchKey, new FilterKeyBehaviour(key, behaviour1), new FilterKeyBehaviour(key, behaviour2))
   {
   }

   private readonly IKeyBindBehaviour _behaviour1 = behaviour1;
   private readonly IKeyBindBehaviour _behaviour2 = behaviour2;
   private readonly FilterKeyBehaviour _switchBehaviour = new(Key, SingleKeyBehaviour.Instance);

   private volatile IKeyBindBehaviour _activeBehaviour = behaviour1;
   private volatile IDisposable? _activeDisposable;

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute)
   {
      _activeDisposable = _activeBehaviour.CreateBinding(onExecute);
      var switchDisposable = _switchBehaviour.CreateBinding(x => Switch(
         ReferenceEquals(_activeBehaviour, _behaviour1) ?
            _behaviour2 :
            _behaviour1,
         onExecute));

      return Disposable.Create(() =>
      {
         _activeDisposable?.Dispose();
         switchDisposable.Dispose();
      });
   }

   private ValueTask Switch(IKeyBindBehaviour behaviour, Func<HookKey, ValueTask> onExecute)
   {
      _activeDisposable?.Dispose();
      _activeBehaviour = behaviour;
      _activeDisposable = behaviour.CreateBinding(onExecute);

      return ValueTask.CompletedTask;
   }
}
