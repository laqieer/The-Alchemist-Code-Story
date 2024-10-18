// Decompiled with JetBrains decompiler
// Type: SRPG.TowerUnitIsDead
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;

namespace SRPG
{
  public class TowerUnitIsDead : MonoBehaviour, IGameParameter
  {
    public GameObject dead;
    public GameObject target;
    public CanvasGroup canvas_group;

    private void Start()
    {
      this.UpdateValue();
    }

    public void UpdateValue()
    {
      this.dead.SetActive(false);
      UnitData data = DataSource.FindDataOfClass<UnitData>(this.target, (UnitData) null);
      if (data == null)
        return;
      TowerResuponse towerResuponse = MonoSingleton<GameManager>.Instance.TowerResuponse;
      if (towerResuponse.pdeck == null)
        return;
      TowerResuponse.PlayerUnit playerUnit = towerResuponse.pdeck.Find((Predicate<TowerResuponse.PlayerUnit>) (x => data.UnitParam.iname == x.unitname));
      this.dead.SetActive(playerUnit != null && playerUnit.isDied);
    }
  }
}
