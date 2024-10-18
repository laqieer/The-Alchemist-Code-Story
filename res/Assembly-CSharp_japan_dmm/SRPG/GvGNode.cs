// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGNode : MonoBehaviour
  {
    [SerializeField]
    private GameObject mOffenseGuild;
    [SerializeField]
    private GameObject mDefenseGuild;
    [SerializeField]
    private Image mOffenseGuildImage;
    [SerializeField]
    private Image mDefenseGuildImage;
    [SerializeField]
    private Text mNodeName;
    [SerializeField]
    private GameObject mCoolTime;
    [SerializeField]
    private Text mCoolTimeText;
    [SerializeField]
    private ImageArray mDefensePartyIcon;
    [SerializeField]
    private GameObject mPrepare;
    [SerializeField]
    private GameObject mPrepareAnimON;
    [SerializeField]
    private GameObject mPrepareAnimOFF;
    [SerializeField]
    private GameObject mBattle;
    [SerializeField]
    private GameObject mBattleAnimON;
    [SerializeField]
    private GameObject mBattleAnimOFF;
    [SerializeField]
    private GameObject mDefense;
    [SerializeField]
    private GameObject mDefenseAnimON;
    [SerializeField]
    private GameObject mDefenseAnimOFF;
    [SerializeField]
    private ChangeMaterialList mChangeColorList;
    private GvGNodeData mGvGNodeData;
    private float mCoolTimeCount;

    private void Update()
    {
      if (this.mGvGNodeData != null && this.mGvGNodeData.IsAttackWait && (double) this.mCoolTimeCount > 0.0)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCoolTime, (UnityEngine.Object) null) && !this.mCoolTime.activeSelf)
          GameUtility.SetGameObjectActive(this.mCoolTime, true);
        TimeSpan timeSpan = this.mGvGNodeData.AttackEnableTime - TimeManager.ServerTime;
        this.mCoolTimeCount = (float) timeSpan.TotalSeconds;
        this.mCoolTimeText.text = string.Format(LocalizedText.Get("sys.GVG_DECLARE_COOL_TIME"), (object) timeSpan.Minutes, (object) timeSpan.Seconds);
      }
      else
      {
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCoolTime, (UnityEngine.Object) null) || !this.mCoolTime.activeSelf)
          return;
        GameUtility.SetGameObjectActive(this.mCoolTime, false);
      }
    }

    public void Refresh(GvGNodeData data)
    {
      if (data == null)
      {
        GameUtility.SetGameObjectActive(this.mOffenseGuild, false);
        GameUtility.SetGameObjectActive(this.mDefenseGuild, false);
        GameUtility.SetGameObjectActive(this.mPrepare, false);
        GameUtility.SetGameObjectActive(this.mBattle, false);
        GameUtility.SetGameObjectActive(this.mDefense, false);
      }
      else
      {
        this.mGvGNodeData = data;
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNodeName, (UnityEngine.Object) null))
          this.mNodeName.text = data.NodeParam.Name;
        bool active = data.IsAttackWait && GvGManager.Instance.IsGvGStatusOffencePhase;
        GameUtility.SetGameObjectActive(this.mCoolTime, active);
        this.mCoolTimeCount = !active ? 0.0f : (float) (data.AttackEnableTime - TimeManager.ServerTime).TotalSeconds;
        GvGManager instance = GvGManager.Instance;
        ViewGuildData viewGuild1 = instance.FindViewGuild(data.DeclaredGuildId);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mOffenseGuild, (UnityEngine.Object) null))
        {
          DataSource.Bind<ViewGuildData>(this.mOffenseGuild, viewGuild1);
          GameUtility.SetGameObjectActive(this.mOffenseGuild, viewGuild1 != null);
          if (viewGuild1 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mOffenseGuildImage, (UnityEngine.Object) null))
            instance.SetNodeColorIndex(instance.GetMatchingOrderIndex(viewGuild1.id), this.mOffenseGuildImage);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefenseGuild, (UnityEngine.Object) null))
        {
          ViewGuildData viewGuild2 = instance.FindViewGuild(data.GuildId);
          DataSource.Bind<ViewGuildData>(this.mDefenseGuild, viewGuild2);
          GameUtility.SetGameObjectActive(this.mDefenseGuild, viewGuild2 != null);
          if (viewGuild2 != null && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefenseGuildImage, (UnityEngine.Object) null))
            instance.SetNodeColorIndex(instance.GetMatchingOrderIndex(viewGuild2.id), this.mDefenseGuildImage);
        }
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefensePartyIcon, (UnityEngine.Object) null))
        {
          if (data.DefensePartyNum == 0)
            GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, false);
          else if (this.mGvGNodeData.IsAttackWait)
          {
            GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, false);
          }
          else
          {
            GameUtility.SetGameObjectActive((Component) this.mDefensePartyIcon, true);
            this.mDefensePartyIcon.ImageIndex = GvGManager.Instance.GetDefensePartyIconIndex(data);
          }
        }
        this.SetNodeStatus(data.State);
        GameParameter.UpdateAll(((Component) this).gameObject);
      }
    }

    private void SetImageMaterial(Image image, Material mat)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) image, (UnityEngine.Object) null) || !((Component) image).gameObject.GetActive())
        return;
      ((Graphic) image).material = mat;
    }

    private void SetNodeStatus(GvGNodeState state)
    {
      GameUtility.SetGameObjectActive(this.mPrepare, false);
      GameUtility.SetGameObjectActive(this.mBattle, false);
      GameUtility.SetGameObjectActive(this.mDefense, false);
      GameUtility.SetGameObjectActive(this.mPrepareAnimON, false);
      GameUtility.SetGameObjectActive(this.mPrepareAnimOFF, false);
      GameUtility.SetGameObjectActive(this.mBattleAnimON, false);
      GameUtility.SetGameObjectActive(this.mBattleAnimOFF, false);
      GameUtility.SetGameObjectActive(this.mDefenseAnimON, false);
      GameUtility.SetGameObjectActive(this.mDefenseAnimOFF, false);
      GvGManager instance = GvGManager.Instance;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      GvGManager.GvGStatus gvGstatus = instance.GvGStatusPhase();
      if (this.mGvGNodeData.IsAttackWait || !instance.IsGvGStatusOffencePhase && gvGstatus != GvGManager.GvGStatus.Declaration && gvGstatus != GvGManager.GvGStatus.DeclarationCoolTime)
        return;
      bool active = instance.GetNodeBattleGuildImageIndex(this.mGvGNodeData, true) != 0 && instance.GetNodeBattleGuildImageIndex(this.mGvGNodeData, false) != 0;
      if (state == GvGNodeState.DeclareSelf || state == GvGNodeState.DeclareOther || state == GvGNodeState.DeclaredEnemy)
      {
        bool gstatusOffencePhase = instance.IsGvGStatusOffencePhase;
        GameUtility.SetGameObjectActive(this.mPrepare, !gstatusOffencePhase);
        if (state == GvGNodeState.DeclaredEnemy && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mDefense, (UnityEngine.Object) null))
          GameUtility.SetGameObjectActive(this.mDefense, gstatusOffencePhase);
        else
          GameUtility.SetGameObjectActive(this.mBattle, gstatusOffencePhase);
        GameUtility.SetGameObjectActive(this.mPrepareAnimON, !active);
        GameUtility.SetGameObjectActive(this.mPrepareAnimOFF, active);
        GameUtility.SetGameObjectActive(this.mBattleAnimON, !active);
        GameUtility.SetGameObjectActive(this.mBattleAnimOFF, active);
        GameUtility.SetGameObjectActive(this.mDefenseAnimON, !active);
        GameUtility.SetGameObjectActive(this.mDefenseAnimOFF, active);
      }
      int index = instance.GetMatchingOrderIndex(this.mGvGNodeData.GuildId);
      if (this.mGvGNodeData.DeclaredGuildId != 0)
        index = instance.GetMatchingOrderIndex(this.mGvGNodeData.DeclaredGuildId);
      else if (!active)
        index = instance.GvGSelfColor;
      else if (this.mGvGNodeData.GuildId == 0)
        index = instance.GvGNPCColor;
      instance.SetNodeColorIndex(index, this.mChangeColorList);
    }

    public void OnSelectNode()
    {
      if (GvGManager.Instance.GvGStatusPhase() == GvGManager.GvGStatus.Finished)
        return;
      int nodeId = -1;
      GvGNodeData dataOfClass = DataSource.FindDataOfClass<GvGNodeData>(((Component) this).gameObject, (GvGNodeData) null);
      if (dataOfClass != null)
        nodeId = dataOfClass.NodeId;
      if (nodeId < 0)
        return;
      GvGManager.Instance.OpenNodeInfo(nodeId);
    }
  }
}
