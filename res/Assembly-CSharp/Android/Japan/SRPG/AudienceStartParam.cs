﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AudienceStartParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class AudienceStartParam
  {
    public JSON_MyPhotonPlayerParam[] players;
    public BattleCore.Json_BtlInfo btlinfo;
  }
}
