// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestMapIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class GenesisQuestMapIcon : MonoBehaviour
  {
    [SerializeField]
    private ImageArray mRefQuestIconImages;
    [SerializeField]
    private GameObject mRefClearObject;
    [SerializeField]
    private GameObject mRefNextObject;
    [SerializeField]
    private List<GameObject> mRefMissionStarLists;
    [SerializeField]
    private SRPG_Button mRefButton;
    [SerializeField]
    private SRPG_Button mRefLockButton;
    private GenesisQuestMapSymbol.eQuestState mCurrentState;
    private QuestParam mQuestParam;

    public QuestParam QuestParam
    {
      get
      {
        return this.mQuestParam;
      }
    }

    public bool SetIcon(QuestParam param, GenesisQuestMapIcon.OnClickIcon onclick, GenesisQuestMapIcon.OnClickLockedIcon onclick_locked)
    {
      if ((UnityEngine.Object) this.mRefQuestIconImages == (UnityEngine.Object) null || param == null || (param.GenesisUIIndex < 0 || this.mRefQuestIconImages.Images.Length <= param.GenesisUIIndex) || (UnityEngine.Object) this.mRefButton == (UnityEngine.Object) null)
        return false;
      this.mQuestParam = param;
      this.mRefQuestIconImages.ImageIndex = param.GenesisUIIndex;
      this.mRefButton.AddListener((SRPG_Button.ButtonClickEvent) (_param1 =>
      {
        if (onclick == null)
          return;
        onclick(this, param);
      }));
      this.mRefLockButton.AddListener((SRPG_Button.ButtonClickEvent) (_param1 =>
      {
        if (onclick_locked == null)
          return;
        onclick_locked(this);
      }));
      return true;
    }

    public void SetState(GenesisQuestMapSymbol.eQuestState state)
    {
      this.mCurrentState = state;
      this.mRefNextObject.SetActive(state == GenesisQuestMapSymbol.eQuestState.PLAYABLE);
      this.mRefButton.interactable = state != GenesisQuestMapSymbol.eQuestState.LOCK;
      this.mRefClearObject.SetActive(state == GenesisQuestMapSymbol.eQuestState.CLEARED);
      this.mRefLockButton.gameObject.SetActive(state == GenesisQuestMapSymbol.eQuestState.LOCK);
    }

    public void SetMissionStar(QuestParam param)
    {
      if (this.mRefMissionStarLists == null || param.bonusObjective == null || (param.bonusObjective.Length <= 0 || this.mRefMissionStarLists == null))
        return;
      int num = param.bonusObjective.Length - 1;
      ImageArray[] imageArrayArray = (ImageArray[]) null;
      for (int index = 0; index < this.mRefMissionStarLists.Count; ++index)
      {
        GameObject refMissionStarList = this.mRefMissionStarLists[index];
        if ((UnityEngine.Object) refMissionStarList != (UnityEngine.Object) null)
        {
          refMissionStarList.SetActive(index == num);
          if (index == num)
            imageArrayArray = refMissionStarList.GetComponentsInChildren<ImageArray>();
        }
      }
      if (imageArrayArray == null || imageArrayArray.Length <= 0 || param.bonusObjective.Length > imageArrayArray.Length)
        return;
      for (int index = 0; index < imageArrayArray.Length; ++index)
        imageArrayArray[index].ImageIndex = !param.IsMissionClear(index) ? 0 : 1;
    }

    public void SetSelected(bool flag)
    {
      this.mRefNextObject.SetActive(this.mCurrentState == GenesisQuestMapSymbol.eQuestState.PLAYABLE && !flag);
    }

    public delegate void OnClickIcon(GenesisQuestMapIcon clicked, QuestParam param);

    public delegate void OnClickLockedIcon(GenesisQuestMapIcon clicked);
  }
}
