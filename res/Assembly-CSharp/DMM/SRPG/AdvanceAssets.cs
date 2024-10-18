// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class AdvanceAssets : ScriptableObject
  {
    [StringIsResourcePath(typeof (GameObject))]
    public string[] EventBG;
    [StringIsResourcePath(typeof (GameObject))]
    public string[] StageBG;
    [StringIsResourcePath(typeof (GameObject))]
    public string[] StagePreview;
    [StringIsResourcePath(typeof (GameObject))]
    public string[] BossBG;
    [StringIsResourcePath(typeof (GameObject))]
    public string[] GachaTopImage;
  }
}
