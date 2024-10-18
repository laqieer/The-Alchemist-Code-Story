// Decompiled with JetBrains decompiler
// Type: SRPG.StatusEffect
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class StatusEffect : MonoBehaviour
  {
    public GameObject[] StatusSlot;
    private bool[] mNowConditions;
    private float mElapsed;
    private int mActiveParamCount;

    private void Start()
    {
      this.mNowConditions = new bool[(int) Unit.MAX_UNIT_CONDITION + 5];
      this.Reset();
    }

    private void Reset()
    {
      if (this.StatusSlot != null)
      {
        for (int index = 0; index < this.StatusSlot.Length; ++index)
          this.StatusSlot[index].SetActive(false);
      }
      if (this.mNowConditions != null)
        this.mNowConditions.Initialize();
      this.mElapsed = 0.0f;
      this.mActiveParamCount = 0;
    }

    public void SetStatus(Unit unit)
    {
      this.Reset();
      EUnitCondition[] values = (EUnitCondition[]) Enum.GetValues(typeof (EUnitCondition));
      int count = 0;
      for (int idx = 0; idx < values.Length; ++idx)
      {
        if (unit.IsUnitCondition(values[idx]))
        {
          this.mNowConditions[idx] = true;
          ++this.mActiveParamCount;
          if (this.SetStatusSlot(count, idx))
            ++count;
        }
        else
          this.mNowConditions[idx] = false;
      }
      bool[] flagArray = new bool[5]
      {
        unit.Shields.Count != 0,
        unit.IsFtgtTargetValid(),
        unit.IsFtgtFromValid(),
        unit.Protects.Count != 0,
        unit.Guards.Count != 0
      };
      for (int index = 0; index < 5; ++index)
      {
        int idx = values.Length + index;
        if (idx >= this.mNowConditions.Length)
          break;
        this.mNowConditions[idx] = false;
        if (flagArray[index])
        {
          this.mNowConditions[idx] = true;
          ++this.mActiveParamCount;
          if (this.SetStatusSlot(count, idx))
            ++count;
        }
      }
    }

    private bool SetStatusSlot(int count, int idx)
    {
      if (this.StatusSlot == null || count < 0 || count >= this.StatusSlot.Length)
        return false;
      this.StatusSlot[count].SetActive(true);
      ImageArray component = this.StatusSlot[count].GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) && component.Images != null && component.Images.Length != 0)
        component.ImageIndex = 0 > idx || idx >= component.Images.Length ? component.Images.Length - 1 : idx;
      return true;
    }

    private void Update()
    {
      int count = 0;
      if (this.mActiveParamCount > 0)
      {
        int num1 = (int) ((double) this.mElapsed / 2.0);
        if ((this.mActiveParamCount - 1) / this.StatusSlot.Length < num1)
        {
          this.mElapsed = 0.0f;
          num1 = 0;
        }
        int num2 = 0;
        for (int idx = 0; idx < this.mNowConditions.Length; ++idx)
        {
          if (this.mNowConditions[idx])
          {
            if (num2 - num1 * this.StatusSlot.Length < 0)
            {
              ++num2;
            }
            else
            {
              if (this.SetStatusSlot(count, idx))
                ++count;
              if (count >= this.StatusSlot.Length)
                break;
            }
          }
        }
      }
      if (count < this.StatusSlot.Length - 1)
      {
        do
        {
          this.StatusSlot[count].SetActive(false);
        }
        while (++count < this.StatusSlot.Length);
      }
      this.mElapsed += Time.deltaTime;
    }

    public enum eOtherStatus
    {
      SHIELD,
      FORCED_TARGETING,
      BE_FORCED_TARGETED,
      PROTECT,
      GUARD,
      MAX,
    }
  }
}
