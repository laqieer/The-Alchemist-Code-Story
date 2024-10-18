// Decompiled with JetBrains decompiler
// Type: MyMsgInput
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Globalization;
using System.Text.RegularExpressions;

#nullable disable
public static class MyMsgInput
{
  public static bool isLegal(string name)
  {
    return string.IsNullOrEmpty(name) || new StringInfo(name).LengthInTextElements >= name.Length && !new Regex("^[0-9０-９\\-]+$").IsMatch(name) && !new Regex("^\\s+$").IsMatch(name);
  }
}
