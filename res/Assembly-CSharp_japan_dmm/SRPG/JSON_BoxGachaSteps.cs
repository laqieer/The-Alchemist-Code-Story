// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_BoxGachaSteps
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
