namespace System.Runtime.CompilerServices
{
    // Enables C# 9 `init` and `record` on targets that don’t define this type (UWP/.NET Native)
    public sealed class IsExternalInit { }
}