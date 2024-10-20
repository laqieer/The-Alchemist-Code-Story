﻿// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_FadeScreen
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [EventActionInfo("カメラ/フェード", "画面をフェードイン・フェードアウトさせます", 5592405, 4473992)]
  public class EventAction_FadeScreen : EventAction
  {
    public EventAction_FadeScreen.FadeModes Mode;
    [HideInInspector]
    public bool FadeOut;
    public bool Async;
    public float FadeTime = 3f;
    [HideInInspector]
    public Color FadeColor = Color.black;
    [StringIsActorID]
    [SerializeField]
    private string[] ExcludeUnits = new string[0];
    [StringIsActorID]
    [SerializeField]
    private string[] IncludeUnits = new string[0];

    public override void OnActivate() => this.StartFade();

    private void StartFade()
    {
      if (this.Mode == EventAction_FadeScreen.FadeModes.Screen)
      {
        if (this.FadeOut)
          FadeController.Instance.FadeTo(this.FadeColor, this.FadeTime);
        else
          GameUtility.FadeIn(this.FadeTime);
      }
      else
      {
        FadeController instance = FadeController.Instance;
        TacticsUnitController[] tacticsUnitControllerArray1 = (TacticsUnitController[]) null;
        TacticsUnitController[] tacticsUnitControllerArray2 = (TacticsUnitController[]) null;
        if (this.ExcludeUnits != null && this.ExcludeUnits.Length > 0)
        {
          tacticsUnitControllerArray2 = new TacticsUnitController[this.ExcludeUnits.Length];
          for (int index = 0; index < tacticsUnitControllerArray2.Length; ++index)
            tacticsUnitControllerArray2[index] = TacticsUnitController.FindByUniqueName(this.ExcludeUnits[index]);
        }
        if (this.IncludeUnits != null && this.IncludeUnits.Length > 0)
        {
          tacticsUnitControllerArray1 = new TacticsUnitController[this.IncludeUnits.Length];
          for (int index = 0; index < tacticsUnitControllerArray1.Length; ++index)
            tacticsUnitControllerArray1[index] = TacticsUnitController.FindByUniqueName(this.IncludeUnits[index]);
        }
        instance.BeginSceneFade(this.FadeColor, this.FadeTime, tacticsUnitControllerArray2 == null ? (TacticsUnitController[]) null : tacticsUnitControllerArray2, tacticsUnitControllerArray1 == null ? (TacticsUnitController[]) null : tacticsUnitControllerArray1);
      }
      if (!this.Async)
        return;
      this.ActivateNext();
    }

    public override void Update()
    {
      if (this.Mode == EventAction_FadeScreen.FadeModes.Screen)
      {
        if (GameUtility.IsScreenFading)
          return;
      }
      else if (FadeController.Instance.IsSceneFading)
        return;
      this.ActivateNext();
    }

    public override void GoToEndState()
    {
      if (this.Mode == EventAction_FadeScreen.FadeModes.Screen)
      {
        if (this.FadeOut)
          FadeController.Instance.FadeTo(this.FadeColor, 0.0f);
        else
          GameUtility.FadeIn(0.0f);
      }
      else
      {
        FadeController instance = FadeController.Instance;
        TacticsUnitController[] tacticsUnitControllerArray1 = (TacticsUnitController[]) null;
        TacticsUnitController[] tacticsUnitControllerArray2 = (TacticsUnitController[]) null;
        if (this.ExcludeUnits != null && this.ExcludeUnits.Length > 0)
        {
          tacticsUnitControllerArray2 = new TacticsUnitController[this.ExcludeUnits.Length];
          for (int index = 0; index < tacticsUnitControllerArray2.Length; ++index)
            tacticsUnitControllerArray2[index] = TacticsUnitController.FindByUniqueName(this.ExcludeUnits[index]);
        }
        if (this.IncludeUnits != null && this.IncludeUnits.Length > 0)
        {
          tacticsUnitControllerArray1 = new TacticsUnitController[this.IncludeUnits.Length];
          for (int index = 0; index < tacticsUnitControllerArray1.Length; ++index)
            tacticsUnitControllerArray1[index] = TacticsUnitController.FindByUniqueName(this.IncludeUnits[index]);
        }
        instance.BeginSceneFade(this.FadeColor, 0.0f, tacticsUnitControllerArray2 == null ? (TacticsUnitController[]) null : tacticsUnitControllerArray2, tacticsUnitControllerArray1 == null ? (TacticsUnitController[]) null : tacticsUnitControllerArray1);
      }
    }

    public enum FadeModes
    {
      Screen,
      Scene,
    }
  }
}