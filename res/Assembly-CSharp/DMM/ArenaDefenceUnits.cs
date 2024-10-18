// Decompiled with JetBrains decompiler
// Type: ArenaDefenceUnits
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
public class ArenaDefenceUnits : MonoBehaviour
{
  public static List<UnitData> mArenaDefUnits = new List<UnitData>();
  private static bool mIsLoadEnd;
  private static bool mIsLoading;
  private static IEnumerator mLoadIEnumerator;

  private void Start()
  {
    this.Clear();
    ArenaDefenceUnits.mLoadIEnumerator = this.LoadAsyncArenaDefUnits();
    this.StartCoroutine(ArenaDefenceUnits.mLoadIEnumerator);
  }

  public static void CompleteLoading()
  {
    if (ArenaDefenceUnits.mIsLoadEnd)
      return;
    if (ArenaDefenceUnits.mIsLoading)
    {
      if (ArenaDefenceUnits.mLoadIEnumerator == null)
        return;
      while (!ArenaDefenceUnits.mIsLoadEnd)
        ArenaDefenceUnits.mLoadIEnumerator.MoveNext();
    }
    else
      ArenaDefenceUnits.LoadArenaDefUnits();
  }

  private static void LoadArenaDefUnits()
  {
    if (ArenaDefenceUnits.mIsLoadEnd)
      return;
    PlayerData player = MonoSingleton<GameManager>.Instance.Player;
    if (ArenaDefenceUnits.mArenaDefUnits == null)
      ArenaDefenceUnits.mArenaDefUnits = new List<UnitData>(player.Units.Count);
    for (int index = 0; index < player.Units.Count; ++index)
    {
      UnitData unitData = new UnitData();
      unitData.Setup(player.Units[index]);
      unitData.TempFlags |= UnitData.TemporaryFlags.TemporaryUnitData | UnitData.TemporaryFlags.AllowJobChange;
      unitData.SetJob(PlayerPartyTypes.ArenaDef);
      ArenaDefenceUnits.mArenaDefUnits.Add(unitData);
    }
    ArenaDefenceUnits.mIsLoadEnd = true;
  }

  [DebuggerHidden]
  private IEnumerator LoadAsyncArenaDefUnits()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ArenaDefenceUnits.\u003CLoadAsyncArenaDefUnits\u003Ec__Iterator0 defUnitsCIterator0 = new ArenaDefenceUnits.\u003CLoadAsyncArenaDefUnits\u003Ec__Iterator0();
    return (IEnumerator) defUnitsCIterator0;
  }

  private void Clear()
  {
    if (ArenaDefenceUnits.mIsLoading && ArenaDefenceUnits.mLoadIEnumerator != null)
    {
      this.StopCoroutine(ArenaDefenceUnits.mLoadIEnumerator);
      ArenaDefenceUnits.mLoadIEnumerator = (IEnumerator) null;
    }
    if (ArenaDefenceUnits.mArenaDefUnits != null)
      ArenaDefenceUnits.mArenaDefUnits.Clear();
    ArenaDefenceUnits.mIsLoadEnd = false;
    ArenaDefenceUnits.mIsLoading = false;
  }

  private void OnDestroy() => this.Clear();
}
