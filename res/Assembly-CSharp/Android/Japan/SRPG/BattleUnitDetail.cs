﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  public class BattleUnitDetail : MonoBehaviour
  {
    private static BattleUnitDetail mInstance = (BattleUnitDetail) null;
    private static int[][] mFluctValues = new int[3][]{ new int[3]{ 1, 20, 50 }, new int[3]{ 5, 20, 45 }, new int[3]{ 1, 20, 50 } };
    [Space(10f)]
    public GameObject GoLeaderSkill;
    public GameObject GoLeaderSkillBadge;
    [Space(5f)]
    public GameObject GoLeader2Skill;
    [Space(5f)]
    public GameObject GoFriendSkill;
    [Space(10f)]
    public GameObject GoStatusParent;
    public BattleUnitDetailStatus StatusBaseItem;
    [Space(10f)]
    public GameObject GoElementParent;
    public BattleUnitDetailElement ElementBaseItem;
    [Space(10f)]
    public GameObject GoTagParent;
    public BattleUnitDetailTag TagBaseItem;
    public BattleUnitDetailTag TagBaseWideItem;
    private const int TAG_BOUNDARY_LEN = 2;
    private const int TAG_ENTRY_GRID_BASE = 1;
    private const int TAG_ENTRY_GRID_WIDE = 2;
    private const int TAG_ENTRY_GRID_MAX = 8;
    [Space(10f)]
    public GameObject GoAtkDetailParent;
    public BattleUnitDetailAtkDetail AtkDetailBaseItem;
    [Space(10f)]
    public GameObject GoCondParent;
    public BattleUnitDetailCond CondBaseItem;
    private SceneBattle mSb;
    private BattleCore mBc;
    private TargetPlate mTargetSub;
    private TowerFloorParam mTF_Param;

    private TowerFloorParam TF_Param
    {
      get
      {
        if (this.mTF_Param == null)
        {
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          if ((bool) ((UnityEngine.Object) instanceDirect) && (bool) ((UnityEngine.Object) this.mSb) && (this.mBc != null && this.mBc.IsTower))
            this.mTF_Param = instanceDirect.FindTowerFloor(this.mSb.CurrentQuest.iname);
        }
        return this.mTF_Param;
      }
    }

    public static BattleUnitDetail Instance
    {
      get
      {
        return BattleUnitDetail.mInstance;
      }
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) BattleUnitDetail.mInstance == (UnityEngine.Object) null))
        return;
      BattleUnitDetail.mInstance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) BattleUnitDetail.mInstance == (UnityEngine.Object) this))
        return;
      BattleUnitDetail.mInstance = (BattleUnitDetail) null;
    }

    private void Start()
    {
      this.mSb = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) this.mSb) || !(bool) ((UnityEngine.Object) this.mSb.BattleUI))
        return;
      this.mBc = this.mSb.Battle;
      if (this.mBc == null)
        return;
      this.mTargetSub = this.mSb.BattleUI.TargetSub;
      if (!(bool) ((UnityEngine.Object) this.mTargetSub))
        return;
      if (this.mBc.IsMultiTower)
      {
        if ((bool) ((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(true);
        if ((bool) ((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(true);
        if ((bool) ((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(false);
      }
      else if (this.mBc.IsMultiPlay && !this.mBc.IsMultiVersus)
      {
        if ((bool) ((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(this.mBc.IsMultiLeaderSkill);
        if ((bool) ((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(false);
        if ((bool) ((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(false);
      }
      else
      {
        if ((bool) ((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(true);
        if ((bool) ((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(false);
        if (this.mBc.IsTower)
        {
          TowerFloorParam tfParam = this.TF_Param;
          if ((bool) ((UnityEngine.Object) this.GoFriendSkill))
            this.GoFriendSkill.SetActive(tfParam.can_help);
        }
        else if ((bool) ((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(!this.mBc.IsMultiVersus);
      }
      if ((bool) ((UnityEngine.Object) this.GoLeaderSkillBadge))
        this.GoLeaderSkillBadge.SetActive(this.mBc.IsMultiTower);
      this.Refresh(this.mTargetSub.SelectedUnit);
    }

    public static void DestroyChildGameObjects(GameObject go_parent, List<GameObject> go_ignore_lists = null)
    {
      if (!(bool) ((UnityEngine.Object) go_parent))
        return;
      for (int index = go_parent.transform.childCount - 1; index >= 0; --index)
      {
        Transform child = go_parent.transform.GetChild(index);
        if ((bool) ((UnityEngine.Object) child) && (go_ignore_lists == null || !go_ignore_lists.Contains(child.gameObject)))
          GameUtility.DestroyGameObject(child.gameObject);
      }
    }

    public static void DestroyChildGameObjects<T>(GameObject go_parent, List<GameObject> go_ignore_lists = null)
    {
      if (!(bool) ((UnityEngine.Object) go_parent))
        return;
      for (int index = go_parent.transform.childCount - 1; index >= 0; --index)
      {
        Transform child = go_parent.transform.GetChild(index);
        if ((bool) ((UnityEngine.Object) child) && (object) child.GetComponent<T>() != null && (go_ignore_lists == null || !go_ignore_lists.Contains(child.gameObject)))
          GameUtility.DestroyGameObject(child.gameObject);
      }
    }

    public void Refresh(Unit unit)
    {
      if (this.mBc == null || unit == null)
        return;
      DataSource component1 = this.gameObject.GetComponent<DataSource>();
      if ((bool) ((UnityEngine.Object) component1))
        component1.Clear();
      if ((bool) ((UnityEngine.Object) this.GoLeaderSkill))
      {
        DataSource component2 = this.GoLeaderSkill.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component2))
          component2.Clear();
      }
      if ((bool) ((UnityEngine.Object) this.GoLeader2Skill))
      {
        DataSource component2 = this.GoLeader2Skill.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component2))
          component2.Clear();
      }
      if ((bool) ((UnityEngine.Object) this.GoFriendSkill))
      {
        DataSource component2 = this.GoFriendSkill.GetComponent<DataSource>();
        if ((bool) ((UnityEngine.Object) component2))
          component2.Clear();
      }
      DataSource.Bind<Unit>(this.gameObject, unit, false);
      BaseStatus status = unit.UnitData.Status;
      BaseStatus currentStatus = unit.CurrentStatus;
      BaseStatus maximumStatus = unit.MaximumStatus;
      SkillData data1 = (SkillData) null;
      SkillData data2 = (SkillData) null;
      SkillData data3 = (SkillData) null;
      if (!this.mBc.IsMultiTower)
      {
        if (unit.Side == EUnitSide.Player)
        {
          if (this.mBc.Leader != null)
            data1 = this.mBc.Leader.LeaderSkill;
          if (this.mBc.Friend != null && this.mBc.IsFriendStatus)
            data3 = this.mBc.Friend.LeaderSkill;
        }
        if (this.mBc.IsMultiVersus && unit.Side == EUnitSide.Enemy && this.mBc.EnemyLeader != null)
          data1 = this.mBc.EnemyLeader.LeaderSkill;
      }
      else if (unit.Side == EUnitSide.Player)
      {
        if (this.mBc.MtLeaderIndexList.Count >= 1)
        {
          int mtLeaderIndex = this.mBc.MtLeaderIndexList[0];
          if (mtLeaderIndex >= 0)
            data1 = this.mBc.Player[mtLeaderIndex].LeaderSkill;
        }
        if (this.mBc.MtLeaderIndexList.Count >= 2)
        {
          int mtLeaderIndex = this.mBc.MtLeaderIndexList[1];
          if (mtLeaderIndex >= 0)
            data2 = this.mBc.Player[mtLeaderIndex].LeaderSkill;
        }
      }
      if ((bool) ((UnityEngine.Object) this.GoLeaderSkill) && data1 != null)
        DataSource.Bind<SkillData>(this.GoLeaderSkill, data1, false);
      if ((bool) ((UnityEngine.Object) this.GoLeader2Skill) && data2 != null)
        DataSource.Bind<SkillData>(this.GoLeader2Skill, data2, false);
      if ((bool) ((UnityEngine.Object) this.GoFriendSkill) && data3 != null)
        DataSource.Bind<SkillData>(this.GoFriendSkill, data3, false);
      if ((bool) ((UnityEngine.Object) this.GoStatusParent) && (bool) ((UnityEngine.Object) this.StatusBaseItem))
      {
        this.StatusBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoStatusParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.StatusBaseItem.gameObject
        }));
        for (int index = 0; index < 13; ++index)
        {
          BattleUnitDetailStatus unitDetailStatus = UnityEngine.Object.Instantiate<BattleUnitDetailStatus>(this.StatusBaseItem);
          if ((bool) ((UnityEngine.Object) unitDetailStatus))
          {
            unitDetailStatus.transform.SetParent(this.GoStatusParent.transform);
            unitDetailStatus.transform.localScale = Vector3.one;
            int val = 0;
            int add = 0;
            switch (index)
            {
              case 0:
                val = (int) maximumStatus.param.hp;
                add = (int) maximumStatus.param.hp - (int) status.param.hp;
                break;
              case 1:
                val = (int) maximumStatus.param.mp;
                add = (int) maximumStatus.param.mp - (int) status.param.mp;
                break;
              case 2:
                val = (int) currentStatus.param.atk;
                add = (int) currentStatus.param.atk - (int) status.param.atk;
                break;
              case 3:
                val = (int) currentStatus.param.def;
                add = (int) currentStatus.param.def - (int) status.param.def;
                break;
              case 4:
                val = (int) currentStatus.param.mag;
                add = (int) currentStatus.param.mag - (int) status.param.mag;
                break;
              case 5:
                val = (int) currentStatus.param.mnd;
                add = (int) currentStatus.param.mnd - (int) status.param.mnd;
                break;
              case 6:
                val = (int) currentStatus.param.dex;
                add = (int) currentStatus.param.dex - (int) status.param.dex;
                break;
              case 7:
                val = (int) currentStatus.param.spd;
                add = (int) currentStatus.param.spd - (int) status.param.spd;
                break;
              case 8:
                val = (int) currentStatus.param.cri;
                add = (int) currentStatus.param.cri - (int) status.param.cri;
                break;
              case 9:
                val = (int) currentStatus.param.luk;
                add = (int) currentStatus.param.luk - (int) status.param.luk;
                break;
              case 10:
                val = unit.GetCombination();
                add = 0;
                break;
              case 11:
                val = (int) currentStatus.param.mov;
                add = (int) currentStatus.param.mov - (int) status.param.mov;
                break;
              case 12:
                val = (int) currentStatus.param.jmp;
                add = (int) currentStatus.param.jmp - (int) status.param.jmp;
                break;
            }
            unitDetailStatus.SetStatus((BattleUnitDetailStatus.eBudStat) index, val, add);
            unitDetailStatus.gameObject.SetActive(true);
          }
        }
      }
      if ((bool) ((UnityEngine.Object) this.GoElementParent) && (bool) ((UnityEngine.Object) this.ElementBaseItem))
      {
        this.ElementBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoElementParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.ElementBaseItem.gameObject
        }));
        int length = Enum.GetNames(typeof (EElement)).Length;
        for (int index = 1; index < length; ++index)
        {
          BattleUnitDetailElement unitDetailElement = UnityEngine.Object.Instantiate<BattleUnitDetailElement>(this.ElementBaseItem);
          if ((bool) ((UnityEngine.Object) unitDetailElement))
          {
            unitDetailElement.transform.SetParent(this.GoElementParent.transform);
            unitDetailElement.transform.localScale = Vector3.one;
            int per = 0;
            switch (index)
            {
              case 1:
                per = (int) currentStatus.element_resist.fire;
                break;
              case 2:
                per = (int) currentStatus.element_resist.water;
                break;
              case 3:
                per = (int) currentStatus.element_resist.wind;
                break;
              case 4:
                per = (int) currentStatus.element_resist.thunder;
                break;
              case 5:
                per = (int) currentStatus.element_resist.shine;
                break;
              case 6:
                per = (int) currentStatus.element_resist.dark;
                break;
            }
            BattleUnitDetail.eBudFluct fluct = BattleUnitDetail.ExchgBudFluct(per, BattleUnitDetail.eFluctChk.ELEMENT);
            unitDetailElement.SetElement((EElement) index, fluct);
            unitDetailElement.gameObject.SetActive(true);
          }
        }
      }
      if ((bool) ((UnityEngine.Object) this.GoTagParent) && (bool) ((UnityEngine.Object) this.TagBaseItem) && (bool) ((UnityEngine.Object) this.TagBaseWideItem))
      {
        this.TagBaseItem.gameObject.SetActive(false);
        this.TagBaseWideItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoTagParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[2]
        {
          this.TagBaseItem.gameObject,
          this.TagBaseWideItem.gameObject
        }));
        int num = 0;
        string[] tags = unit.GetTags();
        if (tags != null)
        {
          foreach (string tag in tags)
          {
            BattleUnitDetailTag battleUnitDetailTag;
            if (tag.Length <= 2)
            {
              if (num + 1 <= 8)
              {
                battleUnitDetailTag = UnityEngine.Object.Instantiate<BattleUnitDetailTag>(this.TagBaseItem);
                ++num;
              }
              else
                break;
            }
            else if (num + 2 <= 8)
            {
              battleUnitDetailTag = UnityEngine.Object.Instantiate<BattleUnitDetailTag>(this.TagBaseWideItem);
              num += 2;
            }
            else
              break;
            if ((bool) ((UnityEngine.Object) battleUnitDetailTag))
            {
              battleUnitDetailTag.transform.SetParent(this.GoTagParent.transform);
              battleUnitDetailTag.transform.localScale = Vector3.one;
              battleUnitDetailTag.SetTag(tag);
              battleUnitDetailTag.gameObject.SetActive(true);
            }
          }
        }
      }
      if ((bool) ((UnityEngine.Object) this.GoAtkDetailParent) && (bool) ((UnityEngine.Object) this.AtkDetailBaseItem))
      {
        this.AtkDetailBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<BattleUnitDetailAtkDetail>(this.GoAtkDetailParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.AtkDetailBaseItem.gameObject
        }));
        int[] numArray = new int[3]{ (int) currentStatus[BattleBonus.HitRate], (int) currentStatus[BattleBonus.AvoidRate], (int) currentStatus[ParamTypes.Rec] - 100 };
        for (int index = 0; index < numArray.Length; ++index)
        {
          BattleUnitDetail.eBudFluct fluct = BattleUnitDetail.ExchgBudFluct(numArray[index], BattleUnitDetail.eFluctChk.DEFAULT);
          if (fluct != BattleUnitDetail.eBudFluct.NONE)
          {
            BattleUnitDetailAtkDetail unitDetailAtkDetail = UnityEngine.Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem);
            if ((bool) ((UnityEngine.Object) unitDetailAtkDetail))
            {
              unitDetailAtkDetail.transform.SetParent(this.GoAtkDetailParent.transform);
              unitDetailAtkDetail.transform.localScale = Vector3.one;
              unitDetailAtkDetail.SetAll((BattleUnitDetailAtkDetail.eAllType) (7 + index), fluct);
              unitDetailAtkDetail.gameObject.SetActive(true);
            }
          }
        }
        int length = Enum.GetNames(typeof (AttackDetailTypes)).Length;
        for (int index1 = 0; index1 < 3; ++index1)
        {
          BattleUnitDetailAtkDetail.eType type = (BattleUnitDetailAtkDetail.eType) index1;
          for (int index2 = 1; index2 < length; ++index2)
          {
            int per = 0;
            switch (index2)
            {
              case 1:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.SlashAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Slash];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Slash];
                    break;
                }
              case 2:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.PierceAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Pierce];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Pierce];
                    break;
                }
              case 3:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.BlowAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Blow];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Blow];
                    break;
                }
              case 4:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.ShotAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Shot];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Shot];
                    break;
                }
              case 5:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.MagicAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Magic];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Magic];
                    break;
                }
              case 6:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.JumpAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_Jump];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_Jump];
                    break;
                }
            }
            BattleUnitDetail.eBudFluct fluct = BattleUnitDetail.ExchgBudFluct(per, BattleUnitDetail.eFluctChk.ATK_DETAIL);
            if (fluct != BattleUnitDetail.eBudFluct.NONE)
            {
              BattleUnitDetailAtkDetail unitDetailAtkDetail = UnityEngine.Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem);
              if ((bool) ((UnityEngine.Object) unitDetailAtkDetail))
              {
                unitDetailAtkDetail.transform.SetParent(this.GoAtkDetailParent.transform);
                unitDetailAtkDetail.transform.localScale = Vector3.one;
                unitDetailAtkDetail.SetAtkDetail((AttackDetailTypes) index2, type, fluct);
                unitDetailAtkDetail.gameObject.SetActive(true);
              }
            }
          }
        }
      }
      if ((bool) ((UnityEngine.Object) this.GoCondParent) && (bool) ((UnityEngine.Object) this.CondBaseItem))
      {
        this.CondBaseItem.gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<BattleUnitDetailCond>(this.GoCondParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          this.CondBaseItem.gameObject
        }));
        foreach (Unit.UnitShield shield in unit.Shields)
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem);
          if ((bool) ((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.transform.SetParent(this.GoCondParent.transform);
            battleUnitDetailCond.transform.localScale = Vector3.one;
            battleUnitDetailCond.SetCondShield(shield.shieldType, (int) shield.hp);
            battleUnitDetailCond.gameObject.SetActive(true);
          }
        }
        EUnitCondition[] values = (EUnitCondition[]) Enum.GetValues(typeof (EUnitCondition));
        for (int index = 0; index < values.Length; ++index)
        {
          if (unit.IsUnitCondition(values[index]))
          {
            BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem);
            if ((bool) ((UnityEngine.Object) battleUnitDetailCond))
            {
              battleUnitDetailCond.transform.SetParent(this.GoCondParent.transform);
              battleUnitDetailCond.transform.localScale = Vector3.one;
              battleUnitDetailCond.SetCond(values[index]);
              battleUnitDetailCond.gameObject.SetActive(true);
            }
          }
        }
      }
      GameParameter.UpdateAll(this.gameObject);
      GlobalEvent.Invoke("BATTLE_UNIT_DETAIL_REFRESH", (object) this);
    }

    public static BattleUnitDetail.eBudFluct ExchgBudFluct(int per, BattleUnitDetail.eFluctChk fluct_chk = BattleUnitDetail.eFluctChk.DEFAULT)
    {
      int[] mFluctValue = BattleUnitDetail.mFluctValues[(int) fluct_chk];
      if (per > 0)
      {
        if (per > mFluctValue[2])
          return BattleUnitDetail.eBudFluct.UP_L;
        if (per > mFluctValue[1])
          return BattleUnitDetail.eBudFluct.UP_M;
        if (per > mFluctValue[0])
          return BattleUnitDetail.eBudFluct.UP_S;
      }
      else if (per < 0)
      {
        if (per < -mFluctValue[2])
          return BattleUnitDetail.eBudFluct.DW_L;
        if (per < -mFluctValue[1])
          return BattleUnitDetail.eBudFluct.DW_M;
        if (per < -mFluctValue[0])
          return BattleUnitDetail.eBudFluct.DW_S;
      }
      return BattleUnitDetail.eBudFluct.NONE;
    }

    public static BattleUnitDetail.eBudFluct ExchgBudFluct(int val, int max, BattleUnitDetail.eFluctChk fluct_chk = BattleUnitDetail.eFluctChk.DEFAULT)
    {
      if (max != 0)
        return BattleUnitDetail.ExchgBudFluct(val * 100 / max, fluct_chk);
      return BattleUnitDetail.eBudFluct.NONE;
    }

    public enum eBudFluct
    {
      NONE,
      DW_L,
      DW_M,
      DW_S,
      UP_S,
      UP_M,
      UP_L,
    }

    public enum eFluctChk
    {
      DEFAULT,
      ELEMENT,
      ATK_DETAIL,
    }

    private enum eFluctSize
    {
      VAL_S,
      VAL_M,
      VAL_L,
    }
  }
}
