// Decompiled with JetBrains decompiler
// Type: SRPG.EventAction_BGM
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  [EventActionInfo("BGM再生", "BGMを再生します", 7368789, 8947780)]
  public class EventAction_BGM : EventAction
  {
    public static readonly int DEMO_BGM_ST = 34;
    public static readonly int DEMO_BGM_ED = 99;
    public string BGM;

    public override void OnActivate()
    {
      if (string.IsNullOrEmpty(this.BGM))
      {
        MonoSingleton<MySound>.Instance.StopBGM();
        if ((bool) ((UnityEngine.Object) SceneBattle.Instance))
          SceneBattle.Instance.EventPlayBgmID = (string) null;
      }
      else
      {
        MonoSingleton<MySound>.Instance.PlayBGM(this.BGM, (string) null, EventAction.IsUnManagedAssets(this.BGM, true));
        if ((bool) ((UnityEngine.Object) SceneBattle.Instance))
        {
          EventScript.ScriptSequence scriptSequence = !(bool) ((UnityEngine.Object) this.Sequence) ? (EventScript.ScriptSequence) null : this.Sequence.ParentSequence;
          if (scriptSequence != null && scriptSequence.IsSavePlayBgmID)
            SceneBattle.Instance.EventPlayBgmID = this.BGM;
        }
      }
      this.ActivateNext();
    }

    public override string[] GetUnManagedAssetListData()
    {
      if (string.IsNullOrEmpty(this.BGM) || string.IsNullOrEmpty(this.BGM))
        return (string[]) null;
      return EventAction.GetUnManagedStreamAssets(new string[1]{ this.BGM }, true);
    }
  }
}
