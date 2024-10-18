// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisAssets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
