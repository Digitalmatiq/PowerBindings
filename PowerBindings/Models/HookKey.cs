using static PowerBindings.Models.HookKey;

namespace PowerBindings.Models;

public readonly record struct HookKey(char C, KeyTriggerType Trigger)
{
   public CancellationToken Token { get; init; }

   public bool Equals(HookKey other) => other.C == C && other.Trigger == Trigger;

   public override int GetHashCode() => HashCode.Combine(C, Trigger);

   public enum KeyTriggerType
   {
      KeyDown,
      KeyUp
   }
}