using PowerBindings.Extensions;
using PowerBindings.Models;
using PowerBindings.Services;
using System.Reactive.Disposables;

namespace PowerBindings.BindBehaviours;

internal sealed class SingleKeyBehaviour : IKeyBindBehaviour
{
   public static SingleKeyBehaviour Instance { get; } = new();

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute)
   {
      var upStreamSub = KeyboardHooker.Instance.UpKeyPressed.SubscribeAsyncExclusive(async x => await onExecute(x));
      var downStreamSub = KeyboardHooker.Instance.DownKeyPressed.SubscribeAsyncExclusive(async x => await onExecute(x));

      return Disposable.Create(() =>
      {
         upStreamSub.Dispose();
         downStreamSub.Dispose();
      });
   }
}
