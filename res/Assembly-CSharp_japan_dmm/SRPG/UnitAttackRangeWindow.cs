// Decompiled with JetBrains decompiler
// Type: SRPG.UnitAttackRangeWindow
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
  public class UnitAttackRangeWindow : MonoBehaviour
  {
    public Transform Parent;
    public GameObject RangeTemplate;
    public GameObject SpaceTemplate;
    public Text RangeMinMax;
    public int BlockSize = 25;
    private static readonly int RANGE_BLOCK_MAX = 7;

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RangeTemplate, (UnityEngine.Object) null))
        this.RangeTemplate.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SpaceTemplate, (UnityEngine.Object) null))
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
        GridLayoutGroup component1 = ((Component) this.Parent).GetComponent<GridLayoutGroup>();
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) component1, (UnityEngine.Object) null))
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
              GameObject gameObject1 = this.SpaceTemplate;
              if (stringList2.IndexOf(str) != -1 || index2 == num1 && index1 == num1)
                gameObject1 = this.RangeTemplate;
              GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
              gameObject2.transform.SetParent(this.Parent, false);
              ((UnityEngine.Object) gameObject2).name = "Grid" + index2.ToString() + "-" + index1.ToString();
              gameObject2.SetActive(true);
              if (!UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) this.SpaceTemplate))
              {
                Image component2 = gameObject2.GetComponent<Image>();
                if (index2 == num1 && index1 == num1)
                  ((Graphic) component2).color = Color32.op_Implicit(new Color32((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue));
                else
                  ((Graphic) component2).color = Color32.op_Implicit(new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue));
              }
            }
          }
          int num2 = attackRangeMin + 1;
          string str1 = LocalizedText.Get("sys.TEXT_RANGE_DEFAULT", (object) num2.ToString(), (object) attackRangeMax.ToString());
          if (num2 == attackRangeMax)
            str1 = LocalizedText.Get("sys.TEXT_RANGE_MINMAX_EQUAL", (object) attackRangeMax);
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.RangeMinMax, (UnityEngine.Object) null))
            return;
          this.RangeMinMax.text = str1;
        }
      }
    }

    private List<string> GetCrossRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> crossRange = new List<string>();
      for (int index1 = min + 1; index1 <= max; ++index1)
      {
        for (int index2 = 0; index2 < 4; ++index2)
        {
          int num1 = Unit.DIRECTION_OFFSETS[index2, 0] * index1;
          int num2 = Unit.DIRECTION_OFFSETS[index2, 1] * index1;
          int num3 = (int) target.x + num1;
          int num4 = (int) target.y + num2;
          string str = num3.ToString() + "," + num4.ToString();
          crossRange.Add(str);
        }
      }
      return crossRange;
    }

    private List<string> GetDiamondRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> diamondRange = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          if (Math.Abs(index2) + Math.Abs(index1) <= max && (min <= 0 || max <= 0 || Math.Abs(-index2) + Math.Abs(-index1) > min))
          {
            string str = (target.x + (float) index2).ToString() + "," + (target.y + (float) index1).ToString();
            diamondRange.Add(str);
          }
        }
      }
      return diamondRange;
    }

    private List<string> GetSquareRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> squareRange = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          if (min <= 0 || max <= 0 || min < Math.Abs(index2))
          {
            int num1 = (int) target.x + index2;
            int num2 = (int) target.y + index1;
            string str = num1.ToString() + "," + num2.ToString();
            squareRange.Add(str);
          }
        }
      }
      return squareRange;
    }

    private List<string> GetLaserRange(Vector2 target, int min = 0, int max = 0)
    {
      return new List<string>();
    }

    private List<string> GetAllRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> allRange = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        for (int index2 = -max; index2 <= max; ++index2)
        {
          string str = index2.ToString() + "," + index1.ToString();
          allRange.Add(str);
        }
      }
      return allRange;
    }

    private List<string> GetBishopRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> bishopRange = new List<string>();
      for (int index1 = -max; index1 <= max; ++index1)
      {
        if (min <= 0 || max <= 0 || min < Math.Abs(index1))
        {
          for (int index2 = -max; index2 <= max; ++index2)
          {
            if ((min <= 0 || max <= 0 || min < Math.Abs(index2)) && Math.Abs(index2) == Math.Abs(index1))
            {
              string str = (target.x + (float) index2).ToString() + "," + (target.y + (float) index1).ToString();
              bishopRange.Add(str);
            }
          }
        }
      }
      return bishopRange;
    }

    private List<string> GetHorseRange(Vector2 target, int min = 0, int max = 0)
    {
      List<string> horseRange = new List<string>();
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
              horseRange.Add(str);
            }
          }
        }
      }
      return horseRange;
    }
  }
}
