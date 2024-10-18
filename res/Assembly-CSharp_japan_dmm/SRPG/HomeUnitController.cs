// Decompiled with JetBrains decompiler
// Type: SRPG.HomeUnitController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class HomeUnitController : UnitController
  {
    private const string ID_IDLE = "idle";
    private const string ID_ACTION = "action";
    public Color DirectLitColor = Color.white;
    public Color IndirectLitColor = Color.clear;

    public static SectionParam GetHomeWorld()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      string iname = (string) GlobalVars.HomeBgSection;
      if (string.IsNullOrEmpty(iname))
      {
        QuestParam lastStoryQuest = instance.Player.FindLastStoryQuest();
        if (lastStoryQuest != null && lastStoryQuest.Chapter != null)
        {
          iname = lastStoryQuest.Chapter.section;
          GlobalVars.HomeBgSection.Set(iname);
        }
      }
      return instance.FindWorld(iname);
    }

    protected override void Start()
    {
      CriticalSection.Enter(CriticalSections.SceneChange);
      this.LoadEquipments = true;
      base.Start();
    }

    protected override void OnDestroy() => base.OnDestroy();

    protected override void OnEnable() => base.OnEnable();

    protected override void OnDisable() => base.OnDisable();

    protected override void PostSetup()
    {
      base.PostSetup();
      this.LoadUnitAnimationAsync("idle", "unit_info_idle0", true);
      this.LoadUnitAnimationAsync("action", "unit_info_act0", true);
      this.StartCoroutine(this.LoadAsync());
    }

    [DebuggerHidden]
    private IEnumerator LoadAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new HomeUnitController.\u003CLoadAsync\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
