﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMissionTypeAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class QuestMissionTypeAttribute : Attribute
  {
    private QuestMissionValueType m_ValueType;

    public QuestMissionTypeAttribute(QuestMissionValueType valueType)
    {
      this.m_ValueType = valueType;
    }

    public QuestMissionValueType ValueType
    {
      get
      {
        return this.m_ValueType;
      }
    }
  }
}
