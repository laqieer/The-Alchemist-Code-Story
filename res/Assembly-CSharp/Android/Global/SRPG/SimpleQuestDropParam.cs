﻿// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleQuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class SimpleQuestDropParam
  {
    public string item_iname;
    public string[] questlist;

    public bool Deserialize(JSON_SimpleQuestDropParam json)
    {
      this.item_iname = json.iname;
      this.questlist = json.questlist;
      return true;
    }
  }
}
