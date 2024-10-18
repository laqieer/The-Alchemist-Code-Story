// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeListWindowContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGNodeListWindowContent : MonoBehaviour
  {
    [SerializeField]
    private Text mTitle;
    [SerializeField]
    private GameObject MapParent;
    [SerializeField]
    private GameObject PrepareStatus;
    [SerializeField]
    private GameObject OffenseStatus;
    [SerializeField]
    private GameObject DefenseStatus;
    [SerializeField]
    private GameObject DeclaredStatus;
    [SerializeField]
    private Text mTitleType;
    [SerializeField]
    private ImageArray mDefensePartyIcon;
    [SerializeField]
    private GameObject CoolTimeStatus;
    [SerializeField]
    private Text mCoolTimeText;
    [SerializeField]
    private Button mDetailButton;
    [SerializeField]
    private Button mDefenseButton;
    [SerializeField]
    private Button mConfirmButton;
    private GvGNodeData mGvGNodeData;
    private float mCoolTimeCount;

    private void Update()
    {
      if (this.mGvGNodeData != null && this.mGvGNodeData.IsAttackWait && (double) this.mCoolTimeCount > 0.0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoolTimeStatus, (UnityEngine.Object) null) && !this.CoolTimeStatus.activeSelf)
          GameUtility.SetGameObjectActive(this.CoolTimeStatus, true);
        TimeSpan timeSpan = this.mGvGNodeData.AttackEnableTime - TimeManager.ServerTime;
        this.mCoolTimeCount = (float) timeSpan.TotalSeconds;
        this.mCoolTimeText.text = string.Format(LocalizedText.Get("sys.GVG_DECLARE_COOL_TIME"), (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.CoolTimeStatus, (UnityEngine.Object) null) || !this.CoolTimeStatus.activeSelf)
          return;
        GameUtility.SetGameObjectActive(this.CoolTimeStatus, false);
      }
    }

    public void Initialize()
    {
      GvGNodeData node = DataSource.FindDataOfClass<GvGNodeData>(((Component) this).gameObject, (GvGNodeData) null);
      if (node == null)
        return;
      this.mGvGNodeData = node;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTitle))
        this.mTitle.text = node.NodeParam.Name;
      QuestParam data = Array.Find<QuestParam>(MonoSingleton<GameManager>.Instance.Quests, (Predicate<QuestParam>) (q => q.iname == node.NodeParam.QuestId));
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MapParent, (UnityEngine.Object) null) && data != null)
        DataSource.Bind<QuestParam>(this.MapParent, data);
      bool flag = true;
      GameUtility.SetGameObjectActive(this.PrepareStatus, false);
      GameUtility.SetGameObjectActive(this.OffenseStatus, false);
      GameUtility.SetGameObjectActive(this.DefenseStatus, false);
      GameUtility.SetGameObjectActive(this.DeclaredStatus, false);
      GameUtility.SetGameObjectActive(((Component) this.mDetailButton).gameObject, false);
      GameUtility.SetGameObjectActive(((Component) this.mDefenseButton).gameObject, false);
      GameUtility.SetGameObjectActive(((Component) this.mConfirmButton).gameObject, false);
      if (GvGManager.Instance.GvGStatusPhase() == GvGManager.GvGStatus.Finished)
        return;
      if (node.IsAttackWait)
      {
        this.mCoolTimeCount = (float) (node.AttackEnableTime - TimeManager.ServerTime).TotalSeconds;
      }
      else
      {
        switch (node.State)
        {
          case GvGNodeState.OccupySelf:
            GameUtility.SetGameObjectActive(this.DeclaredStatus, true);
            break;
          case GvGNodeState.DeclareSelf:
            bool gstatusOffencePhase = GvGManager.Instance.IsGvGStatusOffencePhase;
            GameUtility.SetGameObjectActive(this.PrepareStatus, !gstatusOffencePhase);
            GameUtility.SetGameObjectActive(this.OffenseStatus, gstatusOffencePhase);
            flag = false;
            break;
          case GvGNodeState.DeclaredEnemy:
            GameUtility.SetGameObjectActive(this.DefenseStatus, true);
            break;
        }
      }
      if (flag)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTitleType, (UnityEngine.Object) null))
          this.mTitleType.text = LocalizedText.Get("sys.GVG_DEFENSE_TEAM");
        GameUtility.SetGameObjectActive(((Component) this.mDetailButton).gameObject, false);
        GameUtility.SetGameObjectActive(((Component) this.mDefenseButton).gameObject, true);
        GameUtility.SetGameObjectActive(((Component) this.mConfirmButton).gameObject, false);
      }
      else
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mTitleType, (UnityEngine.Object) null))
          this.mTitleType.text = LocalizedText.Get("sys.GVG_OFFENSE_TEAM");
        GameUtility.SetGameObjectActive(((Component) this.mDetailButton).gameObject, false);
        GameUtility.SetGameObjectActive(((Component) this.mDefenseButton).gameObject, false);
        GameUtility.SetGameObjectActive(((Component) this.mConfirmButton).gameObject, true);
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefensePartyIcon, (UnityEngine.Object) null))
        return;
      if (node.DefensePartyNum == 0)
      {
        GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, false);
      }
      else
      {
        GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, true);
        this.mDefensePartyIcon.ImageIndex = GvGManager.Instance.GetDefensePartyIconIndex(node);
      }
    }
  }
}
