﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitQueue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitQueue : MonoBehaviour
  {
    public GameObject[] Items = new GameObject[0];
    public UnitQueue.Layer[] Units = new UnitQueue.Layer[0];
    public Button[] UnitButtons = new Button[0];
    public static UnitQueue Instance;
    public GameObject LastUnit;
    private Unit[] mCurrentUnits;

    public void Refresh(Unit unit)
    {
      for (int index1 = 0; index1 < this.mCurrentUnits.Length; ++index1)
      {
        if (this.mCurrentUnits[index1] == unit)
        {
          if (index1 < this.Items.Length && (UnityEngine.Object) this.Items[index1] != (UnityEngine.Object) null)
            GameParameter.UpdateAll(this.Items[index1]);
          if (index1 < this.Units.Length && this.Units[index1].Layers != null)
          {
            for (int index2 = 0; index2 < this.Units[index1].Layers.Length; ++index2)
            {
              if ((UnityEngine.Object) this.Units[index1].Layers[index2] != (UnityEngine.Object) null)
                GameParameter.UpdateAll(this.Units[index1].Layers[index2]);
            }
          }
        }
      }
    }

    private void Start()
    {
      this.mCurrentUnits = new Unit[Mathf.Max(this.Items.Length, this.Units.Length)];
    }

    public void Refresh(int offset = 0)
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      BattleCore battle = instance.Battle;
      if (battle == null || battle.Order.Count == 0)
        return;
      int num = Mathf.Max(this.Items.Length, this.Units.Length);
      for (int index1 = 0; index1 < num; ++index1)
      {
        BattleCore.OrderData data = battle.Order[index1 % battle.Order.Count];
        Unit unit = data.Unit;
        if (index1 < this.mCurrentUnits.Length)
          this.mCurrentUnits[index1] = unit;
        if (index1 < this.Items.Length && (UnityEngine.Object) this.Items[index1] != (UnityEngine.Object) null)
        {
          DataSource.Bind<Unit>(this.Items[index1], unit);
          DataSource.Bind<BattleCore.OrderData>(this.Items[index1], data);
          GameParameter.UpdateAll(this.Items[index1]);
        }
        if (index1 < this.Units.Length)
        {
          for (int index2 = 0; index2 < this.Units[index1].Layers.Length; ++index2)
          {
            if (!((UnityEngine.Object) this.Units[index1].Layers[index2] == (UnityEngine.Object) null))
            {
              DataSource.Bind<Unit>(this.Units[index1].Layers[index2], unit);
              GameParameter.UpdateAll(this.Units[index1].Layers[index2]);
            }
          }
        }
      }
      if (!((UnityEngine.Object) this.LastUnit != (UnityEngine.Object) null))
        return;
      DataSource.Bind<Unit>(this.LastUnit, battle.Order[0].Unit);
      DataSource.Bind<BattleCore.OrderData>(this.LastUnit, battle.Order[0]);
      GameParameter.UpdateAll(this.LastUnit);
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) UnitQueue.Instance == (UnityEngine.Object) null))
        return;
      UnitQueue.Instance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) UnitQueue.Instance == (UnityEngine.Object) this))
        return;
      UnitQueue.Instance = (UnitQueue) null;
    }

    [Serializable]
    public struct Layer
    {
      public GameObject[] Layers;
    }
  }
}
