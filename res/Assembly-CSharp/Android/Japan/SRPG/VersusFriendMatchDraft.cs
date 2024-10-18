// Decompiled with JetBrains decompiler
// Type: SRPG.VersusFriendMatchDraft
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

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
      if (!((UnityEngine.Object) this.mPulldownSelectIconGO != (UnityEngine.Object) null) || this.mItems.Count > 1)
        return;
      this.mPulldownSelectIconGO.SetActive(false);
    }

    private void SelectDeck(int deck_id)
    {
      MonoSingleton<GameManager>.Instance.VSDraftId = (long) deck_id;
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
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
