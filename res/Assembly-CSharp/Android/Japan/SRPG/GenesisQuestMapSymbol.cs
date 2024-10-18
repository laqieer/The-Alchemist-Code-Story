// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestMapSymbol
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class GenesisQuestMapSymbol : MonoBehaviour
  {
    [SerializeField]
    private GenesisQuestMapIcon mRefIconObject;
    private QuestParam mQuestParam;

    public GenesisQuestMapIcon Icon
    {
      get
      {
        return this.mRefIconObject;
      }
    }

    public bool Init(QuestParam param, bool is_top, bool is_end, GenesisQuestMapIcon.OnClickIcon onclick_icon, GenesisQuestMapIcon.OnClickLockedIcon onclick_locked_icon)
    {
      if ((UnityEngine.Object) this.mRefIconObject == (UnityEngine.Object) null || param == null)
        return false;
      this.mQuestParam = param;
      Transform transform1 = this.gameObject.transform;
      Transform transform2 = transform1.Find("top");
      Transform transform3 = transform1.Find("bottom");
      if ((UnityEngine.Object) transform2 == (UnityEngine.Object) null || (UnityEngine.Object) transform3 == (UnityEngine.Object) null)
        return false;
      transform2.gameObject.SetActive(is_top);
      transform3.gameObject.SetActive(!is_top);
      Transform parent = !is_top ? transform3 : transform2;
      if (is_end)
      {
        Transform transform4 = parent.Find("root");
        if ((UnityEngine.Object) transform4 != (UnityEngine.Object) null)
          transform4.gameObject.SetActive(false);
      }
      ImageArray[] componentsInChildren = parent.GetComponentsInChildren<ImageArray>();
      if (componentsInChildren != null)
      {
        foreach (ImageArray imageArray in componentsInChildren)
        {
          int difficulty = (int) param.difficulty;
          if (imageArray.Images != null && imageArray.Images.Length - 1 >= difficulty)
            imageArray.ImageIndex = difficulty;
        }
      }
      if ((UnityEngine.Object) this.mRefIconObject == (UnityEngine.Object) null || (UnityEngine.Object) this.mRefIconObject == (UnityEngine.Object) null || !this.mRefIconObject.SetIcon(param, onclick_icon, onclick_locked_icon))
        return false;
      Transform transform5 = this.mRefIconObject.transform;
      transform5.SetParent(parent, false);
      transform5.SetAsLastSibling();
      this.UpdateState();
      this.gameObject.SetActive(true);
      return true;
    }

    public void UpdateState()
    {
      if ((UnityEngine.Object) this.mRefIconObject == (UnityEngine.Object) null || this.mQuestParam == null)
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
