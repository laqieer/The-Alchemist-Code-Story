// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GenesisAssets : ScriptableObject
  {
    [StringIsResourcePath(typeof (GameObject))]
    public string[] ChapterBG;
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
