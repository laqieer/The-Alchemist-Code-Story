﻿// Decompiled with JetBrains decompiler
// Type: SRPG.QuestMissionTypeAttribute
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace SRPG
{
  public class QuestMissionTypeAttribute : Attribute
  {
    private QuestMissionValueType m_ValueType;

    public QuestMissionTypeAttribute(QuestMissionValueType valueType)
    {
      this.m_ValueType = valueType;
    }

    public QuestMissionValueType ValueType => this.m_ValueType;
  }
}
