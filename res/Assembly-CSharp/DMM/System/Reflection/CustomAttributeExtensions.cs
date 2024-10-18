// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeExtensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace System.Reflection
{
  public static class CustomAttributeExtensions
  {
    public static T GetCustomAttribute<T>(MemberInfo memberInfo, bool inherit)
    {
      return (T) ((IEnumerable<object>) memberInfo.GetCustomAttributes(typeof (T), inherit)).FirstOrDefault<object>();
    }
  }
}
