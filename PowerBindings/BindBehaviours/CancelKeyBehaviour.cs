using PowerBindings.Models;
using System.Reactive.Disposables;

namespace PowerBindings.BindBehaviours;

/// <summary>
/// Cancel action while performing it
/// </summary>
/// <param name="cancelKey">Cancel Key</param>
/// <param name="behaviour">Behaviour</param>
public sealed class CancelKeyBehaviour(HookKey cancelKey, IKeyBindBehaviour behaviour)
   : IKeyBindBehaviour
{
   public CancelKeyBehaviour(HookKey cancelKey)
      : this(cancelKey, SingleKeyBehaviour.Instance)
   {
   }

   public CancelKeyBehaviour(HookKey key, HookKey cancelKey)
      : this(cancelKey, new FilterKeyBehaviour(key, SingleKeyBehaviour.Instance))
   {
   }

   private readonly IKeyBindBehaviour _behaviour = behaviour;
   private readonly FilterKeyBehaviour _filterCancelKeyBehaviour = new(cancelKey, SingleKeyBehaviour.Instance);
   private CancellationTokenSource _cts = new();

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute)
   {
      var executeDisposable = _behaviour.CreateBinding(x => onExecute(x with { Token = _cts.Token }));
      var cancelDisposable = _filterCancelKeyBehaviour.CreateBinding(x => Cancel());

      return Disposable.Create(() =>
      {
         executeDisposable.Dispose();
         cancelDisposable.Dispose();
      });
   }

   private ValueTask Cancel()
   {
      _cts.Cancel();
      _cts.Dispose();
      _cts = new();
      return ValueTask.CompletedTask;
   }
}
