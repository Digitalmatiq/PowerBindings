using H.Hooks;
using PowerBindings.Models;
using System.Reactive.Linq;

namespace PowerBindings.Services;

internal sealed class KeyboardHooker : IDisposable
{
   public static KeyboardHooker Instance { get; } = new KeyboardHooker();

   private readonly Lazy<LowLevelKeyboardHook> _hook;
   private IObservable<HookKey>? _downEvents;
   private IObservable<HookKey>? _upEvents;

   private KeyboardHooker() => _hook = new(CreateHook);

   public IObservable<HookKey> UpKeyPressed
   {
      get
      {
         _upEvents ??= Observable
            .FromEventPattern<KeyboardEventArgs>(x => _hook.Value.Up += x, x => _hook.Value.Up -= x)
            .Select(x => new HookKey((char)x.EventArgs.CurrentKey, HookKey.KeyTriggerType.KeyUp));

         return _upEvents;
      }
   }

   public IObservable<HookKey> DownKeyPressed
   {
      get
      {
         _downEvents ??= Observable
            .FromEventPattern<KeyboardEventArgs>(x => _hook.Value.Down += x, x => _hook.Value.Down -= x)
            .Select(x => new HookKey((char)x.EventArgs.CurrentKey, HookKey.KeyTriggerType.KeyDown));

         return _downEvents;
      }
   }

   public void Dispose() => _hook.Value.Dispose();

   private static LowLevelKeyboardHook CreateHook()
   {
      var hook = new LowLevelKeyboardHook
      {
         OneUpEvent = true,
         IsExtendedMode = true
      };

      hook.Start();

      return hook;
   }
}
