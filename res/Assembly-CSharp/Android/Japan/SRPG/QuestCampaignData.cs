// Decompiled with JetBrains decompiler
// Type: SRPG.QuestCampaignData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
