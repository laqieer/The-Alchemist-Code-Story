// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestMapSymbol
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class GenesisQuestMapSymbol : MonoBehaviour
  {
    [SerializeField]
    private GenesisQuestMapIcon mRefIconObject;
    private QuestParam mQuestParam;

    public GenesisQuestMapIcon Icon => this.mRefIconObject;

    public bool Init(
      QuestParam param,
      bool is_top,
      bool is_end,
      GenesisQuestMapIcon.OnClickIcon onclick_icon,
      GenesisQuestMapIcon.OnClickLockedIcon onclick_locked_icon)
    {
      if (Object.op_Equality((Object) this.mRefIconObject, (Object) null) || param == null)
        return false;
      this.mQuestParam = param;
      Transform transform1 = ((Component) this).gameObject.transform;
      Transform transform2 = transform1.Find("top");
      Transform transform3 = transform1.Find("bottom");
      if (Object.op_Equality((Object) transform2, (Object) null) || Object.op_Equality((Object) transform3, (Object) null))
        return false;
      ((Component) transform2).gameObject.SetActive(is_top);
      ((Component) transform3).gameObject.SetActive(!is_top);
      Transform transform4 = !is_top ? transform3 : transform2;
      if (is_end)
      {
        Transform transform5 = transform4.Find("root");
        if (Object.op_Inequality((Object) transform5, (Object) null))
          ((Component) transform5).gameObject.SetActive(false);
      }
      ImageArray[] componentsInChildren = ((Component) transform4).GetComponentsInChildren<ImageArray>();
      if (componentsInChildren != null)
      {
        foreach (ImageArray imageArray in componentsInChildren)
        {
          int difficulty = (int) param.difficulty;
          if (imageArray.Images != null && imageArray.Images.Length - 1 >= difficulty)
            imageArray.ImageIndex = difficulty;
        }
      }
      if (Object.op_Equality((Object) this.mRefIconObject, (Object) null) || Object.op_Equality((Object) this.mRefIconObject, (Object) null) || !this.mRefIconObject.SetIcon(param, onclick_icon, onclick_locked_icon))
        return false;
      Transform transform6 = ((Component) this.mRefIconObject).transform;
      transform6.SetParent(transform4, false);
      transform6.SetAsLastSibling();
      this.UpdateState();
      ((Component) this).gameObject.SetActive(true);
      return true;
    }

    public void UpdateState()
    {
      if (Object.op_Equality((Object) this.mRefIconObject, (Object) null) || this.mQuestParam == null)
        return;
      GenesisQuestMapSymbol.eQuestState state = GenesisQuestMapSymbol.eQuestState.LOCK;
      if (MonoSingleton<GameManager>.Instance.Player.IsQuestCleared(this.mQuestParam.iname))
        state = GenesisQuestMapSymbol.eQuestState.CLEARED;
      else if (MonoSingleton<GameManager>.Instance.Player.IsQuestAvailable(this.mQuestParam.iname))
        state = GenesisQuestMapSymbol.eQuestState.PLAYABLE;
      this.mRefIconObject.SetState(state);
      this.mRefIconObject.SetMissionStar(this.mQuestParam);
    }

    public enum eQuestState
    {
      INVALID,
      LOCK,
      PLAYABLE,
      CLEARED,
    }
  }
}
