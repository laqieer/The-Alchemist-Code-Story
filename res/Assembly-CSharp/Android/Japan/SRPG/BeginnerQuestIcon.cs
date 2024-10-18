﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BeginnerQuestIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "更新", FlowNode.PinTypes.Input, 0)]
  public class BeginnerQuestIcon : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private bool ShowOnlyBeginnerPeriod;
    [SerializeField]
    private GameObject Badge;
    [SerializeField]
    private GameObject Emmision;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      if (this.ShowOnlyBeginnerPeriod)
        this.gameObject.SetActive(MonoSingleton<GameManager>.Instance.Player.IsBeginner());
      bool flag1 = ((IEnumerable<TipsParam>) MonoSingleton<GameManager>.Instance.MasterParam.Tips).Where<TipsParam>((Func<TipsParam, bool>) (t => !t.hide)).Select<TipsParam, string>((Func<TipsParam, string>) (t => t.iname)).Except<string>((IEnumerable<string>) MonoSingleton<GameManager>.Instance.Tips).Any<string>();
      if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
        this.Badge.SetActive(flag1);
      if (!((UnityEngine.Object) this.Emmision != (UnityEngine.Object) null))
        return;
      bool flag2 = ((IEnumerable<QuestParam>) MonoSingleton<GameManager>.Instance.Player.AvailableQuests).Where<QuestParam>((Func<QuestParam, bool>) (q =>
      {
        if (q.type == QuestTypes.Beginner)
          return q.IsDateUnlock(-1L);
        return false;
      })).Any<QuestParam>((Func<QuestParam, bool>) (q => q.state != QuestStates.Cleared));
      this.Emmision.SetActive(flag1 || flag2);
    }
  }
}
