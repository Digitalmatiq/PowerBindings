using System.Reactive.Linq;

namespace PowerBindings.Extensions;

internal static class RXExtensions
{
   internal static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(x => Observable.FromAsync(new Func<Task>(() => onNextAsync.Invoke(x))))
            .Concat()
            .Subscribe();

   internal static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
       source
           .Select(x => Observable.FromAsync(new Func<Task>(() => onNextAsync.Invoke(x))))
           .Merge()
           .Subscribe();

   internal static IDisposable SubscribeAsyncExclusive<T>(this IObservable<T> source, Func<T, Task> onNextAsync)
   {
      var isProcessing = false;

      return source
          .Where(_ => !Volatile.Read(ref isProcessing))
          .Select(async x =>
          {
             Volatile.Write(ref isProcessing, true);

             try
             {
                await onNextAsync(x).ConfigureAwait(false);
             }
             finally
             {
                Volatile.Write(ref isProcessing, false);
             }
          })
          .Subscribe(x => x.Wait());
   }
}
