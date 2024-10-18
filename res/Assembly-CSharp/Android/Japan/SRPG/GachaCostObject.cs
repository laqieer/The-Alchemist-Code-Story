// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCostObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class GachaCostObject : MonoBehaviour
  {
    private GameObject m_root;
    private GameObject m_ticket;
    private GameObject m_default;
    private GameObject m_default_bg;
    private Transform m_CostNum;
    private Transform m_CostFree;

    public GameObject RootObject
    {
      get
      {
        return this.m_root;
      }
      set
      {
        this.m_root = value;
      }
    }

    public GameObject TicketObject
    {
      get
      {
        return this.m_ticket;
      }
      set
      {
        this.m_ticket = value;
      }
    }

    public GameObject DefaultObject
    {
      get
      {
        return this.m_default;
      }
      set
      {
        this.m_default = value;
      }
    }

    public GameObject DefaultBGObject
    {
      get
      {
        return this.m_default_bg;
      }
      set
      {
        this.m_default_bg = value;
      }
    }

    public void Refresh()
    {
      this.UpdateCostData();
    }

    private void UpdateCostData()
    {
      if ((UnityEngine.Object) this.m_root.GetComponent<Button>() == (UnityEngine.Object) null)
        return;
      this.m_default.SetActive(false);
      this.m_ticket.SetActive(false);
      GachaRequestParam dataOfClass = DataSource.FindDataOfClass<GachaRequestParam>(this.m_root, (GachaRequestParam) null);
      if (dataOfClass == null)
        return;
      if (dataOfClass.IsTicketGacha)
      {
        ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(dataOfClass.Ticket);
        if (itemDataByItemId == null)
          return;
        DataSource.Bind<ItemData>(this.m_ticket, itemDataByItemId, false);
        this.m_ticket.SetActive(true);
        GameParameter.UpdateAll(this.m_ticket);
      }
      else
      {
        this.m_default_bg.GetComponent<ImageArray>().ImageIndex = !dataOfClass.IsGold ? 1 : 0;
        this.RefreshCostNum(this.m_default_bg, dataOfClass.Cost);
        this.m_default.SetActive(true);
      }
    }

    private bool RefreshCostNum(GameObject _root, int _cost = 0)
    {
      if ((UnityEngine.Object) this.m_CostNum == (UnityEngine.Object) null)
        this.m_CostNum = _root.transform.Find("num");
      if ((UnityEngine.Object) this.m_CostFree == (UnityEngine.Object) null)
        this.m_CostFree = _root.transform.Find("num_free");
      if ((UnityEngine.Object) this.m_CostNum == (UnityEngine.Object) null || (UnityEngine.Object) this.m_CostFree == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("消費コストを表示するオブジェクトが存在しません");
        return false;
      }
      this.m_CostNum.gameObject.SetActive(false);
      this.m_CostFree.gameObject.SetActive(false);
      if (_cost <= 0)
      {
        this.m_CostFree.gameObject.SetActive(true);
        return false;
      }
      int num1 = (int) Math.Log10(_cost <= 0 ? 1.0 : (double) _cost) + 1;
      int num2 = _cost;
      for (int index = 7; index > 0; --index)
      {
        string name = "num/value_" + Mathf.Pow(10f, (float) (index - 1)).ToString();
        Transform transform = _root.transform.Find(name);
        if (num1 < index)
        {
          transform.gameObject.SetActive(false);
        }
        else
        {
          int num3 = (int) Mathf.Pow(10f, (float) (index - 1));
          int num4 = num2 / num3;
          transform.gameObject.SetActive(true);
          transform.GetComponent<ImageArray>().ImageIndex = num4;
          num2 %= num3;
        }
      }
      this.m_CostNum.gameObject.SetActive(true);
      return true;
    }
  }
}
