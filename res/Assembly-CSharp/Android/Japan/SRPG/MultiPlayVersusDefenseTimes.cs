// Decompiled with JetBrains decompiler
// Type: SRPG.MultiPlayVersusDefenseTimes
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  public class MultiPlayVersusDefenseTimes : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_REFRESH = 0;
    [SerializeField]
    private Text DefenseCountText;
    [SerializeField]
    private Transform IconHolder;
    [SerializeField]
    private GameObject ItemIcon;
    [SerializeField]
    private GameObject CoinIcon;
    [SerializeField]
    private GameObject GoldIcon;
    [SerializeField]
    private GameObject ArenaCoinIcon;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.Refresh();
    }

    private void Awake()
    {
      if ((UnityEngine.Object) this.ItemIcon != (UnityEngine.Object) null)
        this.ItemIcon.SetActive(false);
      if ((UnityEngine.Object) this.CoinIcon != (UnityEngine.Object) null)
        this.CoinIcon.SetActive(false);
      if ((UnityEngine.Object) this.GoldIcon != (UnityEngine.Object) null)
        this.GoldIcon.SetActive(false);
      if (!((UnityEngine.Object) this.ArenaCoinIcon != (UnityEngine.Object) null))
        return;
      this.ArenaCoinIcon.SetActive(false);
    }

    private void Refresh()
    {
      if (GlobalVars.ArenaAward == null)
        return;
      Json_ArenaAward arenaAward = GlobalVars.ArenaAward;
      if ((UnityEngine.Object) this.DefenseCountText != (UnityEngine.Object) null)
        this.DefenseCountText.text = LocalizedText.Get("sys.MULTI_VERSUS_DEFENSE_COUNT", new object[1]
        {
          (object) arenaAward.defense_count
        });
      if (GlobalVars.ArenaAward.reward == null || GlobalVars.ArenaAward.reward.defense == null)
        return;
      Json_ArenaRewardInfo defense = GlobalVars.ArenaAward.reward.defense;
      if (defense.coin > 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CoinIcon);
        DataSource.Bind<int>(gameObject, defense.coin, false);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (defense.gold > 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GoldIcon);
        DataSource.Bind<int>(gameObject, defense.gold, false);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (defense.arenacoin > 0)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ArenaCoinIcon);
        DataSource.Bind<int>(gameObject, defense.arenacoin, false);
        gameObject.transform.SetParent(this.IconHolder, false);
        gameObject.SetActive(true);
      }
      if (defense.items != null && defense.items.Length > 0)
      {
        GameManager instance = MonoSingleton<GameManager>.Instance;
        foreach (Json_Item jsonItem in defense.items)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemIcon);
          ItemData data = new ItemData();
          data.Setup(0L, jsonItem.iname, jsonItem.num);
          DataSource.Bind<ItemData>(gameObject, data, false);
          gameObject.transform.SetParent(this.IconHolder, false);
          gameObject.SetActive(true);
        }
      }
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
