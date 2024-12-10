using PowerBindings.Extensions;
using PowerBindings.Models;
using PowerBindings.Services;
using System.Reactive.Disposables;

namespace PowerBindings.BindBehaviours;

internal sealed class SerialKeyBehaviour : IKeyBindBehaviour
{
   public static SerialKeyBehaviour Instance { get; } = new();

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute)
   {
      var upStreamSub = KeyboardHooker.Instance.UpKeyPressed.SubscribeAsync(async x => await onExecute(x));
      var downStreamSub = KeyboardHooker.Instance.DownKeyPressed.SubscribeAsync(async x => await onExecute(x));

      return Disposable.Create(() =>
      {
         upStreamSub.Dispose();
         downStreamSub.Dispose();
      });
   }
}
