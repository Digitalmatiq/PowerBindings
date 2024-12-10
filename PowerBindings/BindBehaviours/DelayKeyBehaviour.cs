using PowerBindings.Models;

namespace PowerBindings.BindBehaviours;

/// <summary>
/// Delay an action
/// </summary>
/// <param name="delay">Timeout</param>
/// <param name="behaviour">Behaviour</param>
public sealed class DelayKeyBehaviour(TimeSpan delay, IKeyBindBehaviour behaviour)
   : IKeyBindBehaviour
{
   private readonly TimeSpan _delay = delay;
   private readonly IKeyBindBehaviour _behaviour = behaviour;

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute) =>
      _behaviour.CreateBinding(x => Delay(x, _delay, onExecute));

   private static async ValueTask Delay(HookKey key, TimeSpan delay, Func<HookKey, ValueTask> onExecute)
   {
      await Task.Delay(delay, key.Token).ConfigureAwait(false);
      await onExecute(key).ConfigureAwait(false);
   }
}
