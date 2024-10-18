// Decompiled with JetBrains decompiler
// Type: SRPG.StringIsTextID
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class StringIsTextID : PropertyAttribute
  {
    public bool ContainsVoiceID;

    public StringIsTextID(bool containsVoiceID = false) => this.ContainsVoiceID = containsVoiceID;
  }
}
