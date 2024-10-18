// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetail : MonoBehaviour
  {
    [Space(10f)]
    public GameObject GoLeaderSkill;
    public GameObject GoLeaderSkillBadge;
    [Space(5f)]
    public GameObject GoLeader2Skill;
    [Space(5f)]
    public GameObject GoFriendSkill;
    public GameObject ConceptCardLSObj;
    public BitmapText ConceptCardLSCoef;
    [Space(10f)]
    public GameObject GoStatusParent;
    public BattleUnitDetailStatus StatusBaseItem;
    public BattleUnitDetailStatus StatusBaseHpItem;
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
    private static BattleUnitDetail mInstance = (BattleUnitDetail) null;
    private static int[][] mFluctValues = new int[3][]
    {
      new int[3]{ 1, 20, 50 },
      new int[3]{ 5, 20, 45 },
      new int[3]{ 1, 20, 50 }
    };

    private TowerFloorParam TF_Param
    {
      get
      {
        if (this.mTF_Param == null)
        {
          GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instanceDirect) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSb) && this.mBc != null && this.mBc.IsTower)
            this.mTF_Param = instanceDirect.FindTowerFloor(this.mSb.CurrentQuest.iname);
        }
        return this.mTF_Param;
      }
    }

    public static BattleUnitDetail Instance => BattleUnitDetail.mInstance;

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) BattleUnitDetail.mInstance, (UnityEngine.Object) null))
        return;
      BattleUnitDetail.mInstance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) BattleUnitDetail.mInstance, (UnityEngine.Object) this))
        return;
      BattleUnitDetail.mInstance = (BattleUnitDetail) null;
    }

    private void Start()
    {
      this.mSb = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSb) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mSb.BattleUI))
        return;
      this.mBc = this.mSb.Battle;
      if (this.mBc == null)
        return;
      this.mTargetSub = this.mSb.BattleUI.TargetSub;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.mTargetSub))
        return;
      if (this.mBc.IsMultiTower)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(true);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(true);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(false);
      }
      else if (this.mBc.IsMultiPlay && !this.mBc.IsMultiVersus)
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(this.mBc.IsMultiLeaderSkill);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(false);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(false);
      }
      else
      {
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkill))
          this.GoLeaderSkill.SetActive(true);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeader2Skill))
          this.GoLeader2Skill.SetActive(false);
        if (this.mBc.IsTower)
        {
          TowerFloorParam tfParam = this.TF_Param;
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill))
            this.GoFriendSkill.SetActive(tfParam.can_help);
        }
        else if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill))
          this.GoFriendSkill.SetActive(!this.mBc.IsMultiVersus);
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkillBadge))
        this.GoLeaderSkillBadge.SetActive(this.mBc.IsMultiTower);
      this.Refresh(this.mTargetSub.SelectedUnit);
    }

    public static void DestroyChildGameObjects(
      GameObject go_parent,
      List<GameObject> go_ignore_lists = null)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go_parent))
        return;
      for (int index = go_parent.transform.childCount - 1; index >= 0; --index)
      {
        Transform child = go_parent.transform.GetChild(index);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) child) && (go_ignore_lists == null || !go_ignore_lists.Contains(((Component) child).gameObject)))
          GameUtility.DestroyGameObject(((Component) child).gameObject);
      }
    }

    public static void DestroyChildGameObjects<T>(
      GameObject go_parent,
      List<GameObject> go_ignore_lists = null)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) go_parent))
        return;
      for (int index = go_parent.transform.childCount - 1; index >= 0; --index)
      {
        Transform child = go_parent.transform.GetChild(index);
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) child) && (object) ((Component) child).GetComponent<T>() != null && (go_ignore_lists == null || !go_ignore_lists.Contains(((Component) child).gameObject)))
          GameUtility.DestroyGameObject(((Component) child).gameObject);
      }
    }

    public void Refresh(Unit unit)
    {
      if (this.mBc == null || unit == null)
        return;
      DataSource component1 = ((Component) this).gameObject.GetComponent<DataSource>();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component1))
        component1.Clear();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkill))
      {
        DataSource component2 = this.GoLeaderSkill.GetComponent<DataSource>();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component2))
          component2.Clear();
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeader2Skill))
      {
        DataSource component3 = this.GoLeader2Skill.GetComponent<DataSource>();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component3))
          component3.Clear();
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill))
      {
        DataSource component4 = this.GoFriendSkill.GetComponent<DataSource>();
        if (UnityEngine.Object.op_Implicit((UnityEngine.Object) component4))
          component4.Clear();
      }
      DataSource.Bind<Unit>(((Component) this).gameObject, unit);
      BaseStatus status = unit.UnitData.Status;
      BaseStatus currentStatus = unit.CurrentStatus;
      BaseStatus maximumStatus = unit.MaximumStatus;
      SkillData data1 = (SkillData) null;
      SkillData data2 = (SkillData) null;
      SkillData data3 = (SkillData) null;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardLSObj, (UnityEngine.Object) null))
        this.ConceptCardLSObj.SetActive(false);
      if (!this.mBc.IsMultiTower)
      {
        if (unit.Side == EUnitSide.Player)
        {
          if (this.mBc.Leader != null)
            data1 = this.mBc.Leader.LeaderSkill;
          if (this.mBc.Friend != null && this.mBc.IsFriendStatus)
            data3 = this.CheckFriendLeaderSkill(this.mBc.Leader, this.mBc.Friend);
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
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeaderSkill) && data1 != null)
        DataSource.Bind<SkillData>(this.GoLeaderSkill, data1);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoLeader2Skill) && data2 != null)
        DataSource.Bind<SkillData>(this.GoLeader2Skill, data2);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoFriendSkill) && data3 != null)
        DataSource.Bind<SkillData>(this.GoFriendSkill, data3);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoStatusParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.StatusBaseItem) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.StatusBaseHpItem))
      {
        ((Component) this.StatusBaseItem).gameObject.SetActive(false);
        ((Component) this.StatusBaseHpItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoStatusParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[2]
        {
          ((Component) this.StatusBaseItem).gameObject,
          ((Component) this.StatusBaseHpItem).gameObject
        }));
        for (int stat = 0; stat < 13; ++stat)
        {
          BattleUnitDetailStatus unitDetailStatus1 = this.StatusBaseItem;
          if (stat == 0)
            unitDetailStatus1 = this.StatusBaseHpItem;
          BattleUnitDetailStatus unitDetailStatus2 = UnityEngine.Object.Instantiate<BattleUnitDetailStatus>(unitDetailStatus1, this.GoStatusParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitDetailStatus2))
          {
            long val = 0;
            long add = 0;
            switch (stat)
            {
              case 0:
                val = unit.GetMaximumHP();
                add = unit.GetMaximumHP() - unit.GetBaseHP();
                break;
              case 1:
                val = (long) (int) maximumStatus.param.mp;
                add = (long) ((int) maximumStatus.param.mp - (int) status.param.mp);
                break;
              case 2:
                val = (long) (int) currentStatus.param.atk;
                add = (long) ((int) currentStatus.param.atk - (int) status.param.atk);
                break;
              case 3:
                val = (long) (int) currentStatus.param.def;
                add = (long) ((int) currentStatus.param.def - (int) status.param.def);
                break;
              case 4:
                val = (long) (int) currentStatus.param.mag;
                add = (long) ((int) currentStatus.param.mag - (int) status.param.mag);
                break;
              case 5:
                val = (long) (int) currentStatus.param.mnd;
                add = (long) ((int) currentStatus.param.mnd - (int) status.param.mnd);
                break;
              case 6:
                val = (long) (int) currentStatus.param.dex;
                add = (long) ((int) currentStatus.param.dex - (int) status.param.dex);
                break;
              case 7:
                val = (long) (int) currentStatus.param.spd;
                add = (long) ((int) currentStatus.param.spd - (int) status.param.spd);
                break;
              case 8:
                val = (long) (int) currentStatus.param.cri;
                add = (long) ((int) currentStatus.param.cri - (int) status.param.cri);
                break;
              case 9:
                val = (long) (int) currentStatus.param.luk;
                add = (long) ((int) currentStatus.param.luk - (int) status.param.luk);
                break;
              case 10:
                val = (long) unit.GetCombination();
                add = 0L;
                break;
              case 11:
                val = (long) (int) currentStatus.param.mov;
                add = (long) ((int) currentStatus.param.mov - (int) status.param.mov);
                break;
              case 12:
                val = (long) (int) currentStatus.param.jmp;
                add = (long) ((int) currentStatus.param.jmp - (int) status.param.jmp);
                break;
            }
            unitDetailStatus2.SetStatus((BattleUnitDetailStatus.eBudStat) stat, val, add);
            ((Component) unitDetailStatus2).gameObject.SetActive(true);
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoElementParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ElementBaseItem))
      {
        ((Component) this.ElementBaseItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoElementParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.ElementBaseItem).gameObject
        }));
        int length = Enum.GetNames(typeof (EElement)).Length;
        for (int elem = 1; elem < length; ++elem)
        {
          BattleUnitDetailElement unitDetailElement = UnityEngine.Object.Instantiate<BattleUnitDetailElement>(this.ElementBaseItem, this.GoElementParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitDetailElement))
          {
            int per = 0;
            switch ((byte) elem)
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
            unitDetailElement.SetElement((EElement) elem, fluct);
            ((Component) unitDetailElement).gameObject.SetActive(true);
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoTagParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TagBaseItem) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TagBaseWideItem))
      {
        ((Component) this.TagBaseItem).gameObject.SetActive(false);
        ((Component) this.TagBaseWideItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects(this.GoTagParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[2]
        {
          ((Component) this.TagBaseItem).gameObject,
          ((Component) this.TagBaseWideItem).gameObject
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
                battleUnitDetailTag = UnityEngine.Object.Instantiate<BattleUnitDetailTag>(this.TagBaseItem, this.GoTagParent.transform, false);
                ++num;
              }
              else
                break;
            }
            else if (num + 2 <= 8)
            {
              battleUnitDetailTag = UnityEngine.Object.Instantiate<BattleUnitDetailTag>(this.TagBaseWideItem, this.GoTagParent.transform, false);
              num += 2;
            }
            else
              break;
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailTag))
            {
              battleUnitDetailTag.SetTag(tag);
              ((Component) battleUnitDetailTag).gameObject.SetActive(true);
            }
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoAtkDetailParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.AtkDetailBaseItem))
      {
        ((Component) this.AtkDetailBaseItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<BattleUnitDetailAtkDetail>(this.GoAtkDetailParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.AtkDetailBaseItem).gameObject
        }));
        int[] numArray = new int[4]
        {
          (int) currentStatus[BattleBonus.HitRate],
          (int) currentStatus[BattleBonus.AvoidRate],
          currentStatus[ParamTypes.Rec] - 100,
          (int) currentStatus[BattleBonus.CriticalDamageRate]
        };
        for (int index = 0; index < numArray.Length; ++index)
        {
          BattleUnitDetail.eBudFluct fluct = BattleUnitDetail.ExchgBudFluct(numArray[index]);
          if (fluct != BattleUnitDetail.eBudFluct.NONE)
          {
            BattleUnitDetailAtkDetail unitDetailAtkDetail = UnityEngine.Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem, this.GoAtkDetailParent.transform, false);
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitDetailAtkDetail))
            {
              unitDetailAtkDetail.SetAll((BattleUnitDetailAtkDetail.eAllType) (7 + index), fluct);
              ((Component) unitDetailAtkDetail).gameObject.SetActive(true);
            }
          }
        }
        int length = Enum.GetNames(typeof (AttackDetailTypes)).Length;
        for (int index = 0; index < 3; ++index)
        {
          BattleUnitDetailAtkDetail.eType type = (BattleUnitDetailAtkDetail.eType) index;
          for (int atk_detail = 0; atk_detail < length; ++atk_detail)
          {
            int per = 0;
            switch ((byte) atk_detail)
            {
              case 0:
                switch (type)
                {
                  case BattleUnitDetailAtkDetail.eType.ASSIST:
                    per = (int) currentStatus[BattleBonus.NoDivAttack];
                    break;
                  case BattleUnitDetailAtkDetail.eType.RESIST:
                    per = (int) currentStatus[BattleBonus.Resist_NoDiv];
                    break;
                  case BattleUnitDetailAtkDetail.eType.AVOID:
                    per = (int) currentStatus[BattleBonus.Avoid_NoDiv];
                    break;
                }
                break;
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
                break;
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
                break;
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
                break;
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
                break;
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
                break;
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
                break;
            }
            BattleUnitDetail.eBudFluct fluct = BattleUnitDetail.ExchgBudFluct(per, BattleUnitDetail.eFluctChk.ATK_DETAIL);
            if (fluct != BattleUnitDetail.eBudFluct.NONE)
            {
              BattleUnitDetailAtkDetail unitDetailAtkDetail = UnityEngine.Object.Instantiate<BattleUnitDetailAtkDetail>(this.AtkDetailBaseItem, this.GoAtkDetailParent.transform, false);
              if (UnityEngine.Object.op_Implicit((UnityEngine.Object) unitDetailAtkDetail))
              {
                unitDetailAtkDetail.SetAtkDetail((AttackDetailTypes) atk_detail, type, fluct);
                ((Component) unitDetailAtkDetail).gameObject.SetActive(true);
              }
            }
          }
        }
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoCondParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.CondBaseItem))
      {
        ((Component) this.CondBaseItem).gameObject.SetActive(false);
        BattleUnitDetail.DestroyChildGameObjects<BattleUnitDetailCond>(this.GoCondParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.CondBaseItem).gameObject
        }));
        foreach (Unit.UnitShield shield in unit.Shields)
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.SetCondShield(shield.shieldType, (int) shield.hp);
            ((Component) battleUnitDetailCond).gameObject.SetActive(true);
          }
        }
        if (unit.IsFtgtTargetValid())
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.SetCondForcedTargeting();
            ((Component) battleUnitDetailCond).gameObject.SetActive(true);
          }
        }
        if (unit.IsFtgtFromValid())
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.SetCondBeForcedTargeted();
            ((Component) battleUnitDetailCond).gameObject.SetActive(true);
          }
        }
        if (unit.Protects.Count != 0)
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.SetCondProtect();
            ((Component) battleUnitDetailCond).gameObject.SetActive(true);
          }
        }
        if (unit.Guards.Count != 0)
        {
          BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
          {
            battleUnitDetailCond.SetCondGuard();
            ((Component) battleUnitDetailCond).gameObject.SetActive(true);
          }
        }
        EUnitCondition[] values = (EUnitCondition[]) Enum.GetValues(typeof (EUnitCondition));
        for (int index = 0; index < values.Length; ++index)
        {
          if (unit.IsUnitCondition(values[index]))
          {
            BattleUnitDetailCond battleUnitDetailCond = UnityEngine.Object.Instantiate<BattleUnitDetailCond>(this.CondBaseItem, this.GoCondParent.transform, false);
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) battleUnitDetailCond))
            {
              battleUnitDetailCond.SetCond(values[index]);
              ((Component) battleUnitDetailCond).gameObject.SetActive(true);
            }
          }
        }
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
      GlobalEvent.Invoke("BATTLE_UNIT_DETAIL_REFRESH", (object) this);
    }

    public static BattleUnitDetail.eBudFluct ExchgBudFluct(
      int per,
      BattleUnitDetail.eFluctChk fluct_chk = BattleUnitDetail.eFluctChk.DEFAULT)
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

    public static BattleUnitDetail.eBudFluct ExchgBudFluct(
      int val,
      int max,
      BattleUnitDetail.eFluctChk fluct_chk = BattleUnitDetail.eFluctChk.DEFAULT)
    {
      return max != 0 ? BattleUnitDetail.ExchgBudFluct(val * 100 / max, fluct_chk) : BattleUnitDetail.eBudFluct.NONE;
    }

    private SkillData CheckFriendLeaderSkill(Unit _leader_unit, Unit _friend_unit)
    {
      if (_leader_unit == null || _leader_unit.LeaderSkill == null || _leader_unit.LeaderSkill.Condition != ESkillCondition.CardLsSkill || _leader_unit.UnitData == null || _leader_unit.UnitData.MainConceptCard == null)
        return _friend_unit.LeaderSkill;
      if (_friend_unit == null || _friend_unit.UnitData == null || _friend_unit.UnitData.MainConceptCard == null)
        return (SkillData) null;
      bool flag = false;
      for (int index = 0; index < _leader_unit.UnitData.MainConceptCard.Param.concept_card_groups.Length; ++index)
        flag |= MonoSingleton<GameManager>.Instance.MasterParam.CheckConceptCardgroup(_leader_unit.UnitData.MainConceptCard.Param.concept_card_groups[index], _friend_unit.UnitData.MainConceptCard.Param);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardLSObj, (UnityEngine.Object) null))
      {
        this.ConceptCardLSObj.SetActive(flag);
        if (flag && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ConceptCardLSCoef, (UnityEngine.Object) null))
          ((Text) this.ConceptCardLSCoef).text = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardLsBuffFriendCoef((int) _friend_unit.UnitData.MainConceptCard.Rarity, (int) _friend_unit.UnitData.MainConceptCard.AwakeCount).ToString();
      }
      return (SkillData) null;
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
