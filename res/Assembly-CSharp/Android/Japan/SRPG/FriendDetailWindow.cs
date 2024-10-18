// Decompiled with JetBrains decompiler
// Type: SRPG.FriendDetailWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [AddComponentMenu("")]
  public class FriendDetailWindow : MonoBehaviour
  {
    public string ToolTipPrefab = string.Empty;
    private FriendDetailWindow.Mode m_Mode = FriendDetailWindow.Mode.DEFAULT;
    private SerializeValueList m_ValueList;
    private FriendData m_FriendData;
    private ChatPlayerData m_ChatPlayerData;
    private SupportElementListRootWindow m_ElementWindow;
    private GameObject m_ToolTip;

    private void Awake()
    {
    }

    private void Setup(SerializeValueList valueList)
    {
      this.m_ValueList = valueList == null ? new SerializeValueList() : valueList;
      this.m_ValueList.SetActive(1, false);
      this.m_ValueList.SetActive(2, false);
      this.m_ValueList.SetActive(3, false);
      this.m_ValueList.SetActive(4, false);
      this.m_ValueList.SetActive(5, false);
      this.m_ValueList.SetActive(6, false);
      if (this.m_ValueList.GetBool("notification"))
        this.m_Mode = FriendDetailWindow.Mode.NOTIFICATION;
      else if (this.m_ValueList.GetBool("search"))
        this.m_Mode = FriendDetailWindow.Mode.SEARCH;
      else if (this.m_ValueList.GetBool("block"))
      {
        this.m_Mode = FriendDetailWindow.Mode.BLOCK;
        GlobalVars.SelectedFriend = (FriendData) null;
      }
      else if (this.m_ValueList.GetBool("chat"))
      {
        this.m_Mode = FriendDetailWindow.Mode.CHAT;
        GlobalVars.SelectedFriend = (FriendData) null;
      }
      else if (this.m_ValueList.GetBool("guild_member"))
      {
        this.m_Mode = FriendDetailWindow.Mode.GUILD_MEMBER;
        GlobalVars.SelectedFriend = (FriendData) null;
      }
      else
        this.m_Mode = FriendDetailWindow.Mode.DEFAULT;
      this.m_ValueList.SetField("mode", this.m_Mode.ToString());
      this.m_ElementWindow = this.m_ValueList.GetComponent<SupportElementListRootWindow>("element_window");
      this.Bind();
    }

    public void Refresh()
    {
      bool sw = false;
      if ((UnityEngine.Object) this.m_ElementWindow != (UnityEngine.Object) null)
        this.m_ElementWindow.SetSupportUnitData(this.m_ValueList.GetObject<UnitData[]>("data_units", (UnitData[]) null));
      this.Bind();
      this.m_ValueList.SetActive((int) this.m_Mode, true);
      switch (this.m_Mode)
      {
        case FriendDetailWindow.Mode.DEFAULT:
          sw = true;
          break;
        case FriendDetailWindow.Mode.BLOCK:
        case FriendDetailWindow.Mode.CHAT:
          bool flag = this.m_ChatPlayerData != null && this.m_ChatPlayerData.fuid == MonoSingleton<GameManager>.Instance.Player.FUID;
          if (this.m_Mode == FriendDetailWindow.Mode.CHAT)
          {
            this.m_ValueList.SetActive(4, true);
            this.m_ValueList.SetActive("btn_block", true);
            this.m_ValueList.SetActive("btn_block_on", true);
            this.m_ValueList.SetActive("btn_block_off", false);
          }
          else
          {
            this.m_ValueList.SetActive("btn_block", true);
            this.m_ValueList.SetActive("btn_block_on", false);
            this.m_ValueList.SetActive("btn_block_off", true);
          }
          if (!flag)
          {
            if (this.m_ChatPlayerData != null)
            {
              if (this.m_ChatPlayerData.IsFriend)
              {
                sw = true;
                this.m_ValueList.SetActive("btn_block_friend_add", false);
                this.m_ValueList.SetActive("btn_block_friend_remove", true);
                break;
              }
              this.m_ValueList.SetActive("btn_block_friend_add", true);
              this.m_ValueList.SetActive("btn_block_friend_remove", false);
              break;
            }
            break;
          }
          this.m_ValueList.SetActive("btn_block", false);
          this.m_ValueList.SetActive("btn_block_friend", false);
          break;
        case FriendDetailWindow.Mode.GUILD_MEMBER:
          if (this.m_ChatPlayerData == null || !(this.m_ChatPlayerData.fuid == MonoSingleton<GameManager>.Instance.Player.FUID))
          {
            if (this.m_ChatPlayerData != null)
            {
              if (this.m_ChatPlayerData.IsFriend)
              {
                sw = true;
                this.m_ValueList.SetActive("btn_guild_friend_add", false);
                this.m_ValueList.SetActive("btn_guild_friend_remove", true);
                break;
              }
              this.m_ValueList.SetActive("btn_guild_friend_add", true);
              this.m_ValueList.SetActive("btn_guild_friend_remove", false);
              break;
            }
            break;
          }
          this.m_ValueList.SetActive("btn_guild_friend_add", false);
          this.m_ValueList.SetActive("btn_guild_friend_remove", false);
          break;
      }
      if (sw)
      {
        if (this.m_FriendData != null && this.m_FriendData.IsFavorite)
        {
          this.m_ValueList.SetActive("btn_favorite_on", true);
          this.m_ValueList.SetActive("btn_favorite_off", false);
        }
        else
        {
          this.m_ValueList.SetActive("btn_favorite_on", false);
          this.m_ValueList.SetActive("btn_favorite_off", true);
        }
      }
      this.m_ValueList.SetActive("btn_favorite", sw);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void SetFriendData(FriendData data)
    {
      this.m_FriendData = data;
      ViewGuildData data1 = (ViewGuildData) null;
      int num = 0;
      if (data.ViewGuild != null)
      {
        data1 = data.ViewGuild;
        num = data.ViewGuild.id;
      }
      else if (this.m_ValueList != null)
      {
        data1 = this.m_ValueList.GetObject<ViewGuildData>(GuildSVB_Key.VIEW_GUILD, (ViewGuildData) null);
        if (data1 != null)
          num = data1.id;
      }
      this.m_ValueList.SetField(GuildSVB_Key.GUILD_ID, num);
      DataSource.Bind<ViewGuildData>(this.gameObject, data1, false);
    }

    public void SetChatPlayerData(ChatPlayerData data)
    {
      this.m_ChatPlayerData = data;
      this.m_ValueList.SetField("fuid", data.fuid);
    }

    public void Bind()
    {
      switch (this.m_Mode)
      {
        case FriendDetailWindow.Mode.DEFAULT:
        case FriendDetailWindow.Mode.NOTIFICATION:
          if (GlobalVars.SelectedFriend != null)
          {
            this.SetFriendData(GlobalVars.SelectedFriend);
            break;
          }
          break;
        case FriendDetailWindow.Mode.SEARCH:
          if (GlobalVars.FoundFriend != null)
          {
            this.SetFriendData(GlobalVars.FoundFriend);
            break;
          }
          break;
        case FriendDetailWindow.Mode.BLOCK:
        case FriendDetailWindow.Mode.CHAT:
          if (GlobalVars.SelectedFriend != null)
          {
            this.SetFriendData(GlobalVars.SelectedFriend);
            break;
          }
          if (this.m_ChatPlayerData != null)
          {
            this.SetFriendData(this.m_ChatPlayerData.ToFriendData());
            break;
          }
          break;
        case FriendDetailWindow.Mode.GUILD_MEMBER:
          if (GlobalVars.SelectedFriend != null)
          {
            this.SetFriendData(GlobalVars.SelectedFriend);
            break;
          }
          if (this.m_ChatPlayerData != null)
          {
            this.SetFriendData(this.m_ChatPlayerData.ToFriendData());
            break;
          }
          break;
      }
      if (this.m_FriendData == null)
        return;
      this.m_ValueList.SetField("fuid", this.m_FriendData.FUID);
      GlobalVars.SelectedFriend = this.m_FriendData;
      GlobalVars.SelectedFriendID = this.m_FriendData.FUID;
      DataSource.Bind<FriendData>(this.gameObject, this.m_FriendData, false);
      if (this.m_FriendData.ViewGuild == null)
        return;
      DataSource.Bind<ViewGuildData>(this.gameObject, this.m_FriendData.ViewGuild, false);
      this.m_ValueList.SetField(GuildSVB_Key.GUILD_ID, this.m_FriendData.ViewGuild.id);
    }

    public void OnEvent(string key, string value)
    {
      if (key == null)
        return;
      if (!(key == "START"))
      {
        if (!(key == "OPEN"))
        {
          if (!(key == "REFRESH"))
          {
            if (!(key == "SELECT"))
            {
              if (!(key == "HOLD"))
                return;
              this.OnEvent_ToolTip();
            }
            else
              this.OnEvent_ToolTip();
          }
          else
            this.Refresh();
        }
        else
          this.OnEvent_Open();
      }
      else
        this.Setup(EventCall.currentValue as SerializeValueList);
    }

    private void OnEvent_Open()
    {
      this.Refresh();
    }

    private void OnEvent_ToolTip()
    {
      if ((UnityEngine.Object) this.m_ToolTip != (UnityEngine.Object) null)
        return;
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue == null)
        return;
      UnitData dataSource = currentValue.GetDataSource<UnitData>("_self");
      if (dataSource == null || string.IsNullOrEmpty(this.ToolTipPrefab))
        return;
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>(this.ToolTipPrefab));
      DataSource.Bind<UnitData>(root, dataSource, false);
      UnitJobDropdown componentInChildren1 = root.GetComponentInChildren<UnitJobDropdown>();
      if ((UnityEngine.Object) componentInChildren1 != (UnityEngine.Object) null)
      {
        componentInChildren1.gameObject.SetActive(true);
        Selectable component1 = componentInChildren1.gameObject.GetComponent<Selectable>();
        if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          component1.interactable = false;
        Image component2 = componentInChildren1.gameObject.GetComponent<Image>();
        if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
          component2.color = new Color(0.5f, 0.5f, 0.5f);
      }
      ArtifactSlots componentInChildren2 = root.GetComponentInChildren<ArtifactSlots>();
      AbilitySlots componentInChildren3 = root.GetComponentInChildren<AbilitySlots>();
      if ((UnityEngine.Object) componentInChildren2 != (UnityEngine.Object) null && (UnityEngine.Object) componentInChildren3 != (UnityEngine.Object) null)
      {
        componentInChildren2.Refresh(false);
        componentInChildren3.Refresh(false);
      }
      ConceptCardSlots componentInChildren4 = root.GetComponentInChildren<ConceptCardSlots>();
      if ((UnityEngine.Object) componentInChildren4 != (UnityEngine.Object) null)
        componentInChildren4.Refresh(false);
      GameParameter.UpdateAll(root);
      this.m_ToolTip = root;
    }

    public enum Mode
    {
      NONE,
      DEFAULT,
      NOTIFICATION,
      SEARCH,
      BLOCK,
      CHAT,
      GUILD_MEMBER,
    }
  }
}
