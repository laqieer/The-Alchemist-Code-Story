// Decompiled with JetBrains decompiler
// Type: SRPG.RuneLotteryBaseState
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
  public class RuneLotteryBaseState : RuneLotteryState
  {
    public short[] enh_param;

    public bool Deserialize(JSON_RuneLotteryBaseState json)
    {
      this.Deserialize((JSON_RuneLotteryState) json);
      this.enh_param = new short[json.enh_param.Length];
      for (int index = 0; index < json.enh_param.Length; ++index)
      {
        this.enh_param[index] = (short) 0;
        this.enh_param[index] = (short) json.enh_param[index];
      }
      return true;
    }
  }
}
