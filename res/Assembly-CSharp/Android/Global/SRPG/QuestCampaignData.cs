// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class QuestCampaignData
  {
    public QuestCampaignValueTypes type;
    public string unit;
    public int value;

    public float GetRate()
    {
      return (float) this.value / 100f;
    }
  }
}
