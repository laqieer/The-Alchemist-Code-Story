﻿// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.ToggleAlchemicPower
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG.AnimEvents
{
  public class ToggleAlchemicPower : AnimEvent
  {
    private static readonly Color SceneFadeColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    public bool Invert;
    [SerializeField]
    private bool OffEmissionLine;
    [SerializeField]
    private bool OffFade;
    private UnitController mUnitController;
    private GeneratedCharacter mGeneratedCharacter;
    public float VesselStrength = 0.3f;

    public override void OnStart(GameObject go)
    {
      this.mGeneratedCharacter = go.GetComponent<GeneratedCharacter>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mGeneratedCharacter, (UnityEngine.Object) null))
        this.mUnitController = go.GetComponentInParent<UnitController>();
      this.SetRenderMode(go, !this.Invert ? 0.0f : 1f);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGeneratedCharacter, (UnityEngine.Object) null))
        return;
      this.SceneFade(!this.Invert ? 0.0f : 1f);
    }

    public override void OnEnd(GameObject go)
    {
      this.SetRenderMode(go, !this.Invert ? 1f : 0.0f);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGeneratedCharacter, (UnityEngine.Object) null))
        return;
      this.SceneFade(!this.Invert ? 1f : 0.0f);
    }

    public override void OnTick(GameObject go, float ratio)
    {
      this.SetRenderMode(go, !this.Invert ? ratio : 1f - ratio);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGeneratedCharacter, (UnityEngine.Object) null))
        return;
      this.SceneFade(!this.Invert ? 1f : 0.0f);
    }

    private void SetRenderMode(GameObject go, float strength)
    {
      if (this.OffEmissionLine)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mGeneratedCharacter, (UnityEngine.Object) null))
      {
        this.mGeneratedCharacter.SetVesselStrength(strength, go, this.VesselStrength);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mUnitController, (UnityEngine.Object) null))
          return;
        this.mUnitController.AnimateVessel(strength, 0.0f);
      }
    }

    private void SceneFade(float strength)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mUnitController, (UnityEngine.Object) null) || this.OffFade)
        return;
      TacticsUnitController[] array = (TacticsUnitController[]) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
      {
        array = SceneBattle.Instance.GetActiveUnits();
        Array.Resize<TacticsUnitController>(ref array, array.Length + 1);
        array[array.Length - 1] = (TacticsUnitController) this.mUnitController;
      }
      FadeController.Instance.BeginSceneFade(Color.Lerp(Color.white, ToggleAlchemicPower.SceneFadeColor, strength), 0.0f, array, (TacticsUnitController[]) null);
    }
  }
}
