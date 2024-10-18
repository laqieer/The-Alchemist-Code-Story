// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaRankingList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if ((UnityEngine.Object) this.ListItem_Normal != (UnityEngine.Object) null)
        this.ListItem_Normal.gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if ((UnityEngine.Object) this.ListItem_Normal == (UnityEngine.Object) null)
        return;
      Transform transform = this.transform;
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
      DataSource.Bind<ArenaPlayer>(this.OwnRankingInfo.gameObject, this.arenaPlayerOwner, false);
      this.OwnRankingInfo.gameObject.SetActive(false);
      this.OwnRankingInfo.gameObject.SetActive(true);
      for (int index = 0; index < arenaRanking.Length; ++index)
      {
        ListItemEvents original = (ListItemEvents) null;
        if (arenaRanking[index].FUID == instance.Player.FUID)
          original = this.ListItem_Self;
        if ((UnityEngine.Object) original == (UnityEngine.Object) null)
          original = this.ListItem_Normal;
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(original);
        DataSource.Bind<ArenaPlayer>(listItemEvents.gameObject, arenaRanking[index], false);
        DataSource.Bind<ViewGuildData>(listItemEvents.gameObject, arenaRanking[index].ViewGuild, false);
        SerializeValueBehaviour component = listItemEvents.GetComponent<SerializeValueBehaviour>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null && arenaRanking[index].ViewGuild != null)
          component.list.SetField(GuildSVB_Key.GUILD_ID, arenaRanking[index].ViewGuild.id);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        this.AddItem(listItemEvents);
        listItemEvents.transform.SetParent(transform, false);
        listItemEvents.gameObject.SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
      if ((UnityEngine.Object) this.DetailWindow == (UnityEngine.Object) null)
        return;
      ArenaPlayer dataOfClass = DataSource.FindDataOfClass<ArenaPlayer>(go, (ArenaPlayer) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailWindow);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass, false);
      gameObject.GetComponent<ArenaPlayerInfo>().UpdateValue();
    }
  }
}
