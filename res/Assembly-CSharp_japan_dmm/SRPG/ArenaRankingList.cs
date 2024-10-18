// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArenaRankingList : SRPG_ListBase
  {
    public ReqBtlColoRanking.RankingTypes RankingType;
    public ListItemEvents ListItem_Normal;
    public ListItemEvents ListItem_Self;
    public GameObject OwnRankingInfo;
    public GameObject DetailWindow;
    private ArenaPlayer arenaPlayerOwner;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.ListItem_Normal, (Object) null))
        ((Component) this.ListItem_Normal).gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if (Object.op_Equality((Object) this.ListItem_Normal, (Object) null))
        return;
      Transform transform = ((Component) this).transform;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      ArenaPlayer[] arenaRanking = instance.GetArenaRanking(this.RankingType);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      this.arenaPlayerOwner = new ArenaPlayer();
      this.arenaPlayerOwner.PlayerName = player.Name;
      this.arenaPlayerOwner.PlayerLevel = player.Lv;
      this.arenaPlayerOwner.ArenaRank = player.ArenaRank;
      this.arenaPlayerOwner.battle_at = player.ArenaLastAt;
      this.arenaPlayerOwner.SelectAward = player.SelectedAward;
      PartyData partyOfType = player.FindPartyOfType(PlayerPartyTypes.Arena);
      for (int index = 0; index < 3; ++index)
      {
        long unitUniqueId = partyOfType.GetUnitUniqueID(index);
        this.arenaPlayerOwner.Unit[index] = player.FindUnitDataByUniqueID(unitUniqueId);
      }
      DataSource.Bind<ArenaPlayer>(this.OwnRankingInfo.gameObject, this.arenaPlayerOwner);
      this.OwnRankingInfo.gameObject.SetActive(false);
      this.OwnRankingInfo.gameObject.SetActive(true);
      for (int index = 0; index < arenaRanking.Length; ++index)
      {
        ListItemEvents listItemEvents1 = (ListItemEvents) null;
        if (arenaRanking[index].FUID == instance.Player.FUID)
          listItemEvents1 = this.ListItem_Self;
        if (Object.op_Equality((Object) listItemEvents1, (Object) null))
          listItemEvents1 = this.ListItem_Normal;
        ListItemEvents listItemEvents2 = Object.Instantiate<ListItemEvents>(listItemEvents1);
        DataSource.Bind<ArenaPlayer>(((Component) listItemEvents2).gameObject, arenaRanking[index]);
        DataSource.Bind<ViewGuildData>(((Component) listItemEvents2).gameObject, arenaRanking[index].ViewGuild);
        SerializeValueBehaviour component = ((Component) listItemEvents2).GetComponent<SerializeValueBehaviour>();
        if (Object.op_Inequality((Object) component, (Object) null) && arenaRanking[index].ViewGuild != null)
          component.list.SetField(GuildSVB_Key.GUILD_ID, arenaRanking[index].ViewGuild.id);
        listItemEvents2.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents2.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        this.AddItem(listItemEvents2);
        ((Component) listItemEvents2).transform.SetParent(transform, false);
        ((Component) listItemEvents2).gameObject.SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
      if (Object.op_Equality((Object) this.DetailWindow, (Object) null))
        return;
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.DetailWindow);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass);
      gameObject.GetComponent<ArenaPlayerInfo>().UpdateValue();
    }
  }
}
