// Decompiled with JetBrains decompiler
// Type: SRPG.VersusFriendMatchDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class VersusFriendMatchDraft : MonoBehaviour
  {
    [SerializeField]
    private GameObject mPulldownSelectIconGO;
    [SerializeField]
    private Pulldown mPulldown;
    private List<ToggledPulldownItem> mItems;

    private void Start()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<VersusDraftDeckParam> versusDraftDecksNow = instance.GetVersusDraftDecksNow(instance.mVersusEnableId);
      if (versusDraftDecksNow == null || versusDraftDecksNow.Count == 0)
        return;
      this.mItems = new List<ToggledPulldownItem>();
      this.mPulldown.ClearItems();
      this.mPulldown.OnSelectionChangeDelegate = new Pulldown.SelectItemEvent(this.SelectDeck);
      for (int index = 0; index < versusDraftDecksNow.Count; ++index)
      {
        PulldownItem pulldownItem = this.mPulldown.AddItem(versusDraftDecksNow[index].Name, versusDraftDecksNow[index].Id);
        if (pulldownItem is ToggledPulldownItem)
        {
          pulldownItem.OnStatusChanged(index == 0);
          this.mItems.Add((ToggledPulldownItem) pulldownItem);
        }
      }
      this.mPulldown.Selection = 0;
      this.mPulldown.interactable = true;
      if (!Object.op_Inequality((Object) this.mPulldownSelectIconGO, (Object) null) || this.mItems.Count > 1)
        return;
      this.mPulldownSelectIconGO.SetActive(false);
    }

    private void SelectDeck(int deck_id)
    {
      MonoSingleton<GameManager>.Instance.VSDraftId = (long) deck_id;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (Object.op_Inequality((Object) instance, (Object) null))
      {
        MyPhoton.MyRoom currentRoom = instance.GetCurrentRoom();
        if (currentRoom != null)
        {
          JSON_MyPhotonRoomParam myPhotonRoomParam = JSON_MyPhotonRoomParam.Parse(currentRoom.json);
          myPhotonRoomParam.draft_deck_id = deck_id;
          instance.SetRoomParam(myPhotonRoomParam.Serialize());
        }
      }
      for (int index = 0; index < this.mItems.Count; ++index)
        this.mItems[index].OnStatusChanged(this.mItems[index].Value == deck_id);
    }
  }
}
