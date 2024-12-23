﻿using PowerBindings.Models;

namespace PowerBindings.BindBehaviours;

/// <summary>
/// Trigger actions only if filtering condition is met
/// </summary>
/// <param name="key">Filtering key</param>
/// <param name="behaviour">Behaviour</param>
public sealed class FilterKeyBehaviour(HookKey key, IKeyBindBehaviour behaviour)
   : IKeyBindBehaviour
{
   public FilterKeyBehaviour(HookKey key)
      : this(key, SingleKeyBehaviour.Instance)
   {
   }

   private readonly HookKey _key = key;
   private readonly IKeyBindBehaviour _behaviour = behaviour;

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute) =>
      _behaviour.CreateBinding(x => Filter(x, _key, onExecute));

   private static async ValueTask Filter(HookKey key, HookKey filter, Func<HookKey, ValueTask> onExecute)
   {
      if (key == filter)
         await onExecute(key).ConfigureAwait(false);
   }
}