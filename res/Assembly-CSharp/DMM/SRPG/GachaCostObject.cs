// Decompiled with JetBrains decompiler
// Type: SRPG.GachaCostObject
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
      get => this.m_root;
      set => this.m_root = value;
    }

    public GameObject TicketObject
    {
      get => this.m_ticket;
      set => this.m_ticket = value;
    }

    public GameObject DefaultObject
    {
      get => this.m_default;
      set => this.m_default = value;
    }

    public GameObject DefaultBGObject
    {
      get => this.m_default_bg;
      set => this.m_default_bg = value;
    }

    public void Refresh() => this.UpdateCostData();

    private void UpdateCostData()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_root.GetComponent<Button>(), (UnityEngine.Object) null))
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
        DataSource.Bind<ItemData>(this.m_ticket, itemDataByItemId);
        this.m_ticket.SetActive(true);
        GameParameter.UpdateAll(this.m_ticket);
      }
      else
      {
        this.m_default_bg.GetComponent<ImageArray>().ImageIndex = !dataOfClass.IsGold ? 1 : 0;
        this.RefreshCostNum(this.m_default_bg, dataOfClass.IsGold, dataOfClass.FixCost);
        this.m_default.SetActive(true);
      }
    }

    private bool RefreshCostNum(GameObject _root, bool needComma, int _cost = 0)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CostNum, (UnityEngine.Object) null))
        this.m_CostNum = _root.transform.Find("Text_value");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CostFree, (UnityEngine.Object) null))
        this.m_CostFree = _root.transform.Find("num_free");
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CostNum, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_CostFree, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("消費コストを表示するオブジェクトが存在しません");
        return false;
      }
      ((Component) this.m_CostNum).gameObject.SetActive(false);
      ((Component) this.m_CostFree).gameObject.SetActive(false);
      if (_cost <= 0)
      {
        ((Component) this.m_CostFree).gameObject.SetActive(true);
        return false;
      }
      ((Component) this.m_CostNum).GetComponent<Text>().text = !needComma ? _cost.ToString() : CurrencyBitmapText.CreateFormatedText(_cost.ToString());
      int num1 = (int) Math.Log10(_cost <= 0 ? 1.0 : (double) _cost) + 1;
      int num2 = _cost;
      for (int index = 7; index > 0; --index)
      {
        string str = "num/value_" + Mathf.Pow(10f, (float) (index - 1)).ToString();
        Transform transform = _root.transform.Find(str);
        if (num1 < index)
        {
          ((Component) transform).gameObject.SetActive(false);
        }
        else
        {
          int num3 = (int) Mathf.Pow(10f, (float) (index - 1));
          int num4 = num2 / num3;
          ((Component) transform).gameObject.SetActive(true);
          ((Component) transform).GetComponent<ImageArray>().ImageIndex = num4;
          num2 %= num3;
        }
      }
      ((Component) this.m_CostNum).gameObject.SetActive(true);
      return true;
    }
  }
}
