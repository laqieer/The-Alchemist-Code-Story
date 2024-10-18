// Decompiled with JetBrains decompiler
// Type: SRPG.JSON_WeatherParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class JSON_WeatherParam
  {
    public string iname;
    public string name;
    public string expr;
    public string icon;
    public string effect;
    public string[] buff_ids;
    public string[] cond_ids;
  }
}
