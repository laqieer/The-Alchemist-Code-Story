﻿// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisQuestMapIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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

    public QuestParam QuestParam => this.mQuestParam;

    public bool SetIcon(
      QuestParam param,
      GenesisQuestMapIcon.OnClickIcon onclick,
      GenesisQuestMapIcon.OnClickLockedIcon onclick_locked)
    {
      if (Object.op_Equality((Object) this.mRefQuestIconImages, (Object) null) || param == null || param.GenesisUIIndex < 0 || this.mRefQuestIconImages.Images.Length <= param.GenesisUIIndex || Object.op_Equality((Object) this.mRefButton, (Object) null))
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
      ((Selectable) this.mRefButton).interactable = state != GenesisQuestMapSymbol.eQuestState.LOCK;
      this.mRefClearObject.SetActive(state == GenesisQuestMapSymbol.eQuestState.CLEARED);
      ((Component) this.mRefLockButton).gameObject.SetActive(state == GenesisQuestMapSymbol.eQuestState.LOCK);
    }

    public void SetMissionStar(QuestParam param)
    {
      if (this.mRefMissionStarLists == null || param.bonusObjective == null || param.bonusObjective.Length <= 0 || this.mRefMissionStarLists == null)
        return;
      int num = param.bonusObjective.Length - 1;
      ImageArray[] imageArrayArray = (ImageArray[]) null;
      for (int index = 0; index < this.mRefMissionStarLists.Count; ++index)
      {
        GameObject refMissionStarList = this.mRefMissionStarLists[index];
        if (Object.op_Inequality((Object) refMissionStarList, (Object) null))
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
