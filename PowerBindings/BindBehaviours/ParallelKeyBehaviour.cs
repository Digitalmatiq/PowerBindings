using PowerBindings.Extensions;
using PowerBindings.Models;
using PowerBindings.Services;
using System.Reactive.Disposables;

namespace PowerBindings.BindBehaviours;

internal sealed class ParallelKeyBehaviour : IKeyBindBehaviour
{
   public static ParallelKeyBehaviour Instance { get; } = new();

   public IDisposable CreateBinding(Func<HookKey, ValueTask> onExecute)
   {
      var upStreamSub = KeyboardHooker.Instance.UpKeyPressed.SubscribeAsyncConcurrent(async x => await onExecute(x));
      var downStreamSub = KeyboardHooker.Instance.DownKeyPressed.SubscribeAsyncConcurrent(async x => await onExecute(x));

      return Disposable.Create(() =>
      {
         upStreamSub.Dispose();
         downStreamSub.Dispose();
      });
   }
}
