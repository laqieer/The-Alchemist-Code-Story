// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.StandardResolverHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Resolvers;
using MessagePack.Unity;

#nullable disable
namespace MessagePack.Internal
{
  internal static class StandardResolverHelper
  {
    public static readonly IFormatterResolver[] DefaultResolvers = new IFormatterResolver[6]
    {
      BuiltinResolver.Instance,
      AttributeFormatterResolver.Instance,
      UnityResolver.Instance,
      (IFormatterResolver) DynamicEnumResolver.Instance,
      DynamicGenericResolver.Instance,
      (IFormatterResolver) DynamicUnionResolver.Instance
    };
  }
}
