﻿// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAttackRangeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UnitAttackRangeWindow : MonoBehaviour
  {
    private static readonly int RANGE_BLOCK_MAX = 7;
    public int BlockSize = 25;
    public Transform Parent;
    public GameObject RangeTemplate;
    public GameObject SpaceTemplate;
    public Text RangeMinMax;

    private void Start()
    {
      if ((UnityEngine.Object) this.RangeTemplate != (UnityEngine.Object) null)
        this.RangeTemplate.SetActive(false);
      if ((UnityEngine.Object) this.SpaceTemplate != (UnityEngine.Object) null)
        this.SpaceTemplate.SetActive(false);
      UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedUnitUniqueID);
      if (unitDataByUniqueId == null)
      {
        DebugUtility.Log("Not Selected Unit!!");
      }
      else
      {
        JobData jobData = unitDataByUniqueId.CurrentJob;
        long selectedJobUniqueId = (long) GlobalVars.SelectedJobUniqueID;
        for (int index = 0; index < unitDataByUniqueId.Jobs.Length; ++index)
        {
          if (unitDataByUniqueId.Jobs[index].UniqueID == selectedJobUniqueId)
          {
            jobData = unitDataByUniqueId.Jobs[index];
            break;
          }
        }
        SkillData attackSkill = jobData.GetAttackSkill();
        int attackRangeMax = unitDataByUniqueId.GetAttackRangeMax(attackSkill);
        int attackRangeMin = unitDataByUniqueId.GetAttackRangeMin(attackSkill);
        ESelectType selectRange = attackSkill.SkillParam.select_range;
        GridLayoutGroup component1 = this.Parent.GetComponent<GridLayoutGroup>();
        if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
        {
          DebugUtility.Log("Parent is not attachment GridLayoutGroup");
        }
        else
        {
          component1.constraintCount = Mathf.Max(attackRangeMax * 2 + 1, UnitAttackRangeWindow.RANGE_BLOCK_MAX);
          if (component1.constraintCount > UnitAttackRangeWindow.RANGE_BLOCK_MAX + 1)
          {
            component1.cellSize = new Vector2((float) this.BlockSize, (float) this.BlockSize);
            component1.spacing = new Vector2(5f, 5f);
          }
          int num1 = component1.constraintCount / 2;
          List<string> stringList1 = new List<string>();
          List<string> stringList2;
          switch (selectRange)
          {
            case ESelectType.Diamond:
              stringList2 = this.GetDiamondRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            case ESelectType.Square:
              stringList2 = this.GetSquareRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            case ESelectType.Laser:
              stringList2 = this.GetLaserRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            case ESelectType.All:
              stringList2 = this.GetAllRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            case ESelectType.Bishop:
              stringList2 = this.GetBishopRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            case ESelectType.Horse:
              stringList2 = this.GetHorseRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
            default:
              stringList2 = this.GetCrossRange(new Vector2((float) num1, (float) num1), attackRangeMin, attackRangeMax);
              break;
          }
          for (int index1 = 0; index1 < component1.constraintCount; ++index1)
          {
            for (int index2 = 0; index2 < component1.constraintCount; ++index2)
            {
              string str = index2.ToString() + "," + index1.ToString();
              GameObject original = this.SpaceTemplate;
              if (stringList2.IndexOf(str) != -1 || index2 == num1 && index1 == num1)
                original = this.RangeTemplate;
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
              gameObject.transform.SetParent(this.Parent, false);
              gameObject.name = "Grid" + index2.ToString() + "-" + index1.ToString();
              gameObject.SetActive(true);
              if (!((UnityEngine.Object) original == (UnityEngine.Object) this.SpaceTemplate))
              {
                Image component2 = gameObject.GetComponent<Image>();
                if (index2 == num1 && index1 == num1)
                  component2.color = (Color) new Color32((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
                else
                  component2.color = (Color) new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
              }
            }
          }
          int num2 = attackRangeMin + 1;
          string str1 = LocalizedText.Get("sys.TEXT_RANGE_DEFAULT", (object) num2.ToString(), (object) attackRangeMax.ToString());
          if (num2 == attackRangeMax)
            str1 = LocalizedText.Get("sys.TEXT_RANGE_MINMAX_EQUAL", new object[1]
            {
              (object) attackRangeMax
            });
          if (!((UnityEngine.Object) this.RangeMinMax != (UnityEngine.Object) null))
            return;
          this.RangeMinMax.text = str1;
        }
      }
    }

    private List<string> GetCrossRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      for (int index1 = min + 1; index1 <= max; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index2, 0] * index1;
          int num2 = Unit.DIRECTION_OFFSETS[index2, 1] * index1;
          string str = ((int) target.x + num1).ToString() + "," + ((int) target.y + num2).ToString();
          stringList.Add(str);
        }
      }
      return stringList;
    }

    private List<string> GetDiamondRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= max && (min <= 0 || max <= 0 || Math.Abs(-index2) + Math.Abs(-index1) > min))
          {
            string str = (target.x + (float) index2).ToString() + "," + (target.y + (float) index1).ToString();
            stringList.Add(str);
          }
        }
      }
      return stringList;
    }

    private List<string> GetSquareRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          if (min <= 0 || max <= 0 || min < Math.Abs(index2))
          {
            string str = ((int) target.x + index2).ToString() + "," + ((int) target.y + index1).ToString();
            stringList.Add(str);
          }
        }
      }
      return stringList;
    }

    private List<string> GetLaserRange(Vector2 target, int min = 0, int max = 0)
    {
      return new List<string>();
    }

    private List<string> GetAllRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          string str = index2.ToString() + "," + index1.ToString();
          stringList.Add(str);
        }
      }
      return stringList;
    }

    private List<string> GetBishopRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        if (min <= 0 || max <= 0 || min < Math.Abs(index1))
        {
          for (int index2 = -max; index2 <= max; ++index2)
          {
            if ((min <= 0 || max <= 0 || min < Math.Abs(index2)) && Math.Abs(index2) == Math.Abs(index1))
            {
              string str = (target.x + (float) index2).ToString() + "," + (target.y + (float) index1).ToString();
              stringList.Add(str);
            }
          }
        }
      }
      return stringList;
    }

    private List<string> GetHorseRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> stringList = new List<string>();
      ++max;
      for (int index1 = -max; index1 <= max; ++index1)
      {
        if (min <= 0 || max <= 0 || min < Math.Abs(min))
        {
          for (int index2 = -max; index2 <= max; ++index2)
          {
            if ((min <= 0 || max <= 0 || min < Math.Abs(min)) && (Math.Abs(index2) == Math.Abs(index1) || Math.Abs(index2) <= 1 && Math.Abs(index1) <= 1))
            {
              string str = (target.x + (float) index2).ToString() + "," + (target.y + (float) index1).ToString();
              stringList.Add(str);
            }
          }
        }
      }
      return stringList;
    }
  }
}
