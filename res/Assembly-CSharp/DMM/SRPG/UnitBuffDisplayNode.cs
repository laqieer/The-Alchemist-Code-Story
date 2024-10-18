// Decompiled with JetBrains decompiler
// Type: SRPG.UnitBuffDisplayNode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class UnitBuffDisplayNode : MonoBehaviour
  {
    public GameObject Icon;
    public GameObject Arrows;
    private RectTransform m_RectTransform;
    private UnitBuffDisplayNode.DispType m_DispType;
    private UnitBuffDisplayNode.EffectType m_EffectType;

    private static bool IsNoBuffType(UnitBuffDisplayNode.BuffType buff_type)
    {
      switch (buff_type)
      {
        case UnitBuffDisplayNode.BuffType.FTT:
        case UnitBuffDisplayNode.BuffType.FTF:
        case UnitBuffDisplayNode.BuffType.PRT:
        case UnitBuffDisplayNode.BuffType.GRD:
          return true;
        default:
          return false;
      }
    }

    public RectTransform rectTransform
    {
      get
      {
        if (Object.op_Equality((Object) this.m_RectTransform, (Object) null))
          this.m_RectTransform = ((Component) this).GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    private void Awake()
    {
    }

    private void OnDestroy() => this.Release();

    public void Setup(UnitBuffDisplayNode.Param param)
    {
      this.m_DispType = param.dispType;
      this.m_EffectType = param.effectType;
      if (Object.op_Inequality((Object) this.Icon, (Object) null) && this.m_DispType != UnitBuffDisplayNode.DispType.NONE)
      {
        if (param.data != null)
        {
          Image component = this.Icon.GetComponent<Image>();
          if (Object.op_Inequality((Object) component, (Object) null))
          {
            component.sprite = param.data.sprite;
            this.Icon.SetActive(true);
          }
        }
        else
          this.Icon.SetActive(false);
      }
      if (!Object.op_Inequality((Object) this.Arrows, (Object) null))
        return;
      if (this.m_EffectType != UnitBuffDisplayNode.EffectType.NONE)
      {
        string str = "arrow_" + this.m_EffectType.ToString().ToLower();
        Transform transform = this.Arrows.transform;
        int num = 0;
        for (int childCount = transform.childCount; num < childCount; ++num)
        {
          Transform child = transform.GetChild(num);
          if (((Object) child).name == str)
            ((Component) child).gameObject.SetActive(true);
          else
            ((Component) child).gameObject.SetActive(false);
        }
      }
      else
      {
        if (!param.isNoBuffType)
          return;
        Transform transform = this.Arrows.transform;
        for (int index = 0; index < transform.childCount; ++index)
          ((Component) transform.GetChild(index)).gameObject.SetActive(false);
      }
    }

    private void Release()
    {
    }

    private void Update()
    {
    }

    public UnitBuffDisplayNode.DispType GetDispType() => this.m_DispType;

    public UnitBuffDisplayNode.EffectType GetEffectType() => this.m_EffectType;

    public void SetPos(float x, float y) => this.rectTransform.anchoredPosition = new Vector2(x, y);

    public static UnitBuffDisplayNode.Param[] CreateParams(
      UnitBuffDisplay parent,
      Unit owner,
      BuffAttachment buff)
    {
      List<UnitBuffDisplayNode.Param> objList = new List<UnitBuffDisplayNode.Param>();
      for (int index = 1; index < 9; ++index)
      {
        UnitBuffDisplayNode.BuffType buffType = (UnitBuffDisplayNode.BuffType) index;
        if (!UnitBuffDisplayNode.IsNoBuffType(buffType) && (double) UnitBuffDisplayNode.GetValue(buffType, buff) != 0.0)
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(buffType);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, buff, nodeData));
        }
      }
      if (objList.Count == 0)
        objList.Add(new UnitBuffDisplayNode.Param(owner, buff, (UnitBuffDisplay.NodeData) null));
      return objList.ToArray();
    }

    public static UnitBuffDisplayNode.Param[] CreateOtherParams(UnitBuffDisplay parent, Unit owner)
    {
      List<UnitBuffDisplayNode.Param> objList = new List<UnitBuffDisplayNode.Param>();
      if (owner != null)
      {
        if (owner.IsFtgtTargetValid())
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(UnitBuffDisplayNode.BuffType.FTT);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, (BuffAttachment) null, nodeData));
        }
        if (owner.IsFtgtFromValid())
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(UnitBuffDisplayNode.BuffType.FTF);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, (BuffAttachment) null, nodeData));
        }
        if (owner.Protects.Count != 0)
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(UnitBuffDisplayNode.BuffType.PRT);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, (BuffAttachment) null, nodeData));
        }
        if (owner.Guards.Count != 0)
        {
          UnitBuffDisplay.NodeData nodeData = parent.GetNodeData(UnitBuffDisplayNode.BuffType.GRD);
          if (nodeData != null)
            objList.Add(new UnitBuffDisplayNode.Param(owner, (BuffAttachment) null, nodeData));
        }
      }
      return objList.ToArray();
    }

    public static int GetValue(UnitBuffDisplayNode.BuffType buffType, BuffAttachment buff)
    {
      switch (buffType)
      {
        case UnitBuffDisplayNode.BuffType.ATK:
          return (int) buff.status.param.atk;
        case UnitBuffDisplayNode.BuffType.DEF:
          return (int) buff.status.param.def;
        case UnitBuffDisplayNode.BuffType.MAG:
          return (int) buff.status.param.mag;
        case UnitBuffDisplayNode.BuffType.MND:
          return (int) buff.status.param.mnd;
        default:
          return 0;
      }
    }

    public static bool NeedDispOn(UnitBuffDisplayNode.Param param)
    {
      return param.dispType != UnitBuffDisplayNode.DispType.NONE;
    }

    public enum BuffType
    {
      NONE,
      ATK,
      DEF,
      MAG,
      MND,
      FTT,
      FTF,
      PRT,
      GRD,
      MAX,
    }

    public enum DispType
    {
      NONE,
      ATK,
      DEF,
      MAG,
      MND,
      FTT,
      FTF,
      PRT,
      GRD,
    }

    public enum EffectType
    {
      NONE,
      UP,
      DOWN,
    }

    public struct Param
    {
      public Unit owner;
      public BuffAttachment buff;
      public UnitBuffDisplay.NodeData data;

      public Param(Unit _owner, BuffAttachment _buff, UnitBuffDisplay.NodeData _data)
      {
        this.owner = _owner;
        this.buff = _buff;
        this.data = _data;
      }

      public bool isAlive
      {
        get
        {
          if (this.owner != null)
          {
            if (this.isNoBuffType)
            {
              switch (this.data.buff)
              {
                case UnitBuffDisplayNode.BuffType.FTT:
                  if (this.owner.IsFtgtTargetValid())
                    return true;
                  break;
                case UnitBuffDisplayNode.BuffType.FTF:
                  if (this.owner.IsFtgtFromValid())
                    return true;
                  break;
                case UnitBuffDisplayNode.BuffType.PRT:
                  if (this.owner.Protects.Count != 0)
                    return true;
                  break;
                case UnitBuffDisplayNode.BuffType.GRD:
                  if (this.owner.Guards.Count != 0)
                    return true;
                  break;
              }
            }
            else
            {
              int index = 0;
              for (int count = this.owner.BuffAttachments.Count; index < count; ++index)
              {
                if (this.buff == this.owner.BuffAttachments[index])
                  return true;
              }
            }
          }
          return false;
        }
      }

      public bool isNeedDispOn => UnitBuffDisplayNode.NeedDispOn(this);

      public UnitBuffDisplayNode.BuffType buffType
      {
        get => this.data != null ? this.data.buff : UnitBuffDisplayNode.BuffType.NONE;
      }

      public UnitBuffDisplayNode.DispType dispType
      {
        get => this.data != null ? this.data.disp : UnitBuffDisplayNode.DispType.NONE;
      }

      public UnitBuffDisplayNode.EffectType effectType
      {
        get
        {
          if (this.value > 0)
            return UnitBuffDisplayNode.EffectType.UP;
          return this.value < 0 ? UnitBuffDisplayNode.EffectType.DOWN : UnitBuffDisplayNode.EffectType.NONE;
        }
      }

      public int value
      {
        get
        {
          return this.data != null && !this.isNoBuffType ? UnitBuffDisplayNode.GetValue(this.data.buff, this.buff) : 0;
        }
      }

      public bool isNoBuffType
      {
        get => this.data != null && UnitBuffDisplayNode.IsNoBuffType(this.data.buff);
      }
    }
  }
}
