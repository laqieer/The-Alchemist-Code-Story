// Decompiled with JetBrains decompiler
// Type: SRPG.RuneSetEff
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
  [Serializable]
  public class RuneSetEff
  {
    public const int DEFAULT_SET_EFFECT_TYPE = 0;
    public int seteff_type;
    public string name;
    public byte icon_index;
    public byte cost;
    public RuneSetEffState[] state;

    public bool Deserialize(JSON_RuneSetEff json)
    {
      this.seteff_type = json.seteff_type;
      this.name = json.name;
      this.icon_index = (byte) json.icon_index;
      this.cost = (byte) json.cost;
      this.state = new RuneSetEffState[json.state.Length];
      for (int index = 0; index < json.state.Length; ++index)
      {
        this.state[index] = new RuneSetEffState();
        this.state[index].Deserialize(json.state[index]);
      }
      return true;
    }

    public static bool IsDefaultEffectType(int effect_type) => effect_type == 0;

    public void AddRuneSetEffectBaseStatus(
      EElement buffTargetElement,
      ref BaseStatus addStatus,
      ref BaseStatus scaleStatus,
      bool isDrawBaseStatus)
    {
      for (int index = 0; index < this.state.Length; ++index)
      {
        BaseStatus addStatus1 = (BaseStatus) null;
        BaseStatus scaleStatus1 = (BaseStatus) null;
        this.state[index].CreateBaseStatus(buffTargetElement, ref addStatus1, ref scaleStatus1, isDrawBaseStatus);
        if (isDrawBaseStatus)
        {
          DrawBaseStatus src = addStatus1 as DrawBaseStatus;
          DrawBaseStatus drawBaseStatus = addStatus as DrawBaseStatus;
          if (src != null && drawBaseStatus != null)
            drawBaseStatus.AddDrawStatus(src);
        }
        else if (addStatus1 != null && addStatus != null)
          addStatus.Add(addStatus1);
        if (isDrawBaseStatus)
        {
          DrawBaseStatus src = scaleStatus1 as DrawBaseStatus;
          DrawBaseStatus drawBaseStatus = scaleStatus as DrawBaseStatus;
          if (src != null && drawBaseStatus != null)
            drawBaseStatus.AddDrawStatus(src);
        }
        else if (scaleStatus1 != null && scaleStatus != null)
          scaleStatus.Add(scaleStatus1);
      }
    }
  }
}
