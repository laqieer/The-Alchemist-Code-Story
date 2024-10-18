// Decompiled with JetBrains decompiler
// Type: SRPG.LoginBonusRecover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class LoginBonusRecover : MonoBehaviour
  {
    [SerializeField]
    private Transform IconParent;
    [SerializeField]
    private ListItemEvents IconTemplate;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.IconTemplate, (Object) null))
        return;
      ((Component) this.IconTemplate).gameObject.SetActive(false);
    }

    private void Start()
    {
      int index1 = GlobalVars.MonthlyLoginBonus_SelectRecoverDay - 1;
      Json_PremiumLoginBonus[] premiumLoginBonuses = MonoSingleton<GameManager>.Instance.Player.FindPremiumLoginBonuses(GlobalVars.MonthlyLoginBonus_SelectTableIname);
      if (premiumLoginBonuses == null)
      {
        DebugUtility.LogError("指定したログインボーナステーブルは存在しません.");
      }
      else
      {
        Json_PremiumLoginBonus premiumLoginBonus = premiumLoginBonuses[index1];
        if (premiumLoginBonus == null)
          return;
        if (premiumLoginBonus.item != null)
        {
          for (int index2 = 0; index2 < premiumLoginBonus.item.Length; ++index2)
          {
            GiftRecieveItemData data = new GiftRecieveItemData();
            string iname = premiumLoginBonus.item[index2].iname;
            int num = premiumLoginBonus.item[index2].num;
            if (string.IsNullOrEmpty(iname))
            {
              DebugUtility.LogError("報酬が設定されていません.");
            }
            else
            {
              ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(iname, false);
              if (itemParam != null)
              {
                data.Set(iname, GiftTypes.Item, itemParam.rare, num);
                data.name = itemParam.name;
              }
              ConceptCardParam conceptCardParam = MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(iname);
              if (conceptCardParam != null)
              {
                data.Set(iname, GiftTypes.ConceptCard, conceptCardParam.rare, num);
                data.name = conceptCardParam.name;
              }
              if (itemParam == null && conceptCardParam == null)
              {
                DebugUtility.LogError("不明な識別子が報酬として設定されています.");
              }
              else
              {
                ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.IconTemplate);
                DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data);
                ((Component) listItemEvents).transform.SetParent(this.IconParent, false);
                ((Component) listItemEvents).gameObject.SetActive(true);
                ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
              }
            }
          }
        }
        if (premiumLoginBonus.coin > 0)
        {
          GiftRecieveItemData data = new GiftRecieveItemData();
          string str = "$COIN";
          int coin = premiumLoginBonus.coin;
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(str, false);
          if (itemParam != null)
          {
            data.Set(str, GiftTypes.Item, itemParam.rare, coin);
            data.name = itemParam.name;
          }
          ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.IconTemplate);
          DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents).gameObject, data);
          ((Component) listItemEvents).transform.SetParent(this.IconParent, false);
          ((Component) listItemEvents).gameObject.SetActive(true);
          ((Component) listItemEvents).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
        }
        if (premiumLoginBonus.gold <= 0)
          return;
        GiftRecieveItemData data1 = new GiftRecieveItemData();
        data1.Set(string.Empty, GiftTypes.Gold, 0, premiumLoginBonus.gold);
        ListItemEvents listItemEvents1 = Object.Instantiate<ListItemEvents>(this.IconTemplate);
        DataSource.Bind<GiftRecieveItemData>(((Component) listItemEvents1).gameObject, data1);
        ((Component) listItemEvents1).transform.SetParent(this.IconParent, false);
        ((Component) listItemEvents1).gameObject.SetActive(true);
        ((Component) listItemEvents1).GetComponentInChildren<GiftRecieveItem>().UpdateValue();
      }
    }
  }
}
