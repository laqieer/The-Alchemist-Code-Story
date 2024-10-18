// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_BoxGachaSteps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_BoxGachaSteps
  {
    public int step;
    public int total_num;
    public int total_num_feature;
    public int total_num_normal;
    public int remain_num_feature;
    public int remain_num_normal;
    public JSON_BoxGachaItems[] box_items;
  }
}
