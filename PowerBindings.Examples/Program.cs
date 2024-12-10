using PowerBindings.Models;
using PowerBindings.Traits;

namespace PowerBindings.Examples;

public class Program
{
   public static void Main(string[] args)
   {
      ISingleFilterKeyTrait obj = new BindAKeyExampleClass();

      using var binding = obj.CreateBinding();

      Console.Read();
   }

   public sealed class BindAKeyExampleClass : ISingleFilterKeyTrait
   {
      public HookKey Key => new('A', HookKey.KeyTriggerType.KeyDown);

      public async ValueTask OnKeyPressed(HookKey key) =>
         await Console.Out.WriteLineAsync("This will be written when A is Pressed");
   }
}
