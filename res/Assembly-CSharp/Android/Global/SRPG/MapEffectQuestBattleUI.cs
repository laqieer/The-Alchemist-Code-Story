// Decompiled with JetBrains decompiler
// Type: SRPG.MapEffectQuestBattleUI
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class MapEffectQuestBattleUI : MonoBehaviour
  {
    public SRPG_Button ButtonMapEffect;
    public string PrefabMapEffectQuest;
    private LoadRequest mReqMapEffect;

    private void Start()
    {
      if (!(bool) ((UnityEngine.Object) this.ButtonMapEffect))
        return;
      this.ButtonMapEffect.AddListener((SRPG_Button.ButtonClickEvent) (button => this.OpenMapEffect()));
      if (string.IsNullOrEmpty(this.PrefabMapEffectQuest))
        return;
      this.mReqMapEffect = AssetManager.LoadAsync<GameObject>(this.PrefabMapEffectQuest);
    }

    private void OpenMapEffect()
    {
      if (this.mReqMapEffect == null || !this.mReqMapEffect.isDone && this.mReqMapEffect.asset == (UnityEngine.Object) null)
        return;
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      SceneBattle instance1 = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instanceDirect) || !(bool) ((UnityEngine.Object) instance1))
        return;
      QuestParam quest = instanceDirect.FindQuest(instance1.CurrentQuest.iname);
      if (quest == null)
        return;
      GameObject instance2 = MapEffectQuest.CreateInstance(this.mReqMapEffect.asset as GameObject, this.transform.parent.parent);
      if (!(bool) ((UnityEngine.Object) instance2))
        return;
      instance2.transform.SetAsLastSibling();
      DataSource.Bind<QuestParam>(instance2, quest);
      instance2.SetActive(true);
      MapEffectQuest component = instance2.GetComponent<MapEffectQuest>();
      if (!(bool) ((UnityEngine.Object) component))
        return;
      component.Setup();
    }
  }
}
