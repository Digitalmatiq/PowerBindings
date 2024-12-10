# C# Example: Key Press Binding

This is a simple example demonstrating how to bind a key press (`A` key) in C# using the `ISingleFilterKeyTrait` trait-interface.

## How It Works

- The `BindAKeyExampleClass` implements `ISingleFilterKeyTrait`.
- It binds the `A` key to trigger an action when pressed.
- When the `A` key is pressed, a message will be printed to the console.
- Only one message at a time and also awaited until the next action triggers again

## Code Example

Below is the full code snippet:

```csharp
using System;
using System.Threading.Tasks;

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
```

## Other behaviours

- The library offers also some other behaviours to choose from like: ICancelFilterKeyTrait, IParallelFilterKeyTrait, ISerialFilterKeyTrait, ISingleFilterKeyTrait, ISwitchKeyTrait