// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestBoxAddWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestBoxAddWindow : MonoBehaviour
  {
    [SerializeField]
    private Text mMessageText;
    [SerializeField]
    private Button mDecideButton;

    private void Start() => this.InitText();

    private void InitText()
    {
      if (Object.op_Equality((Object) this.mMessageText, (Object) null))
        return;
      int addCount = MonoSingleton<GameManager>.Instance.Player.AutoRepeatQuestBox.AddCount;
      AutoRepeatQuestBoxParam repeatQuestBoxParam1 = MonoSingleton<GameManager>.Instance.MasterParam.GetAutoRepeatQuestBoxParam(addCount);
      AutoRepeatQuestBoxParam repeatQuestBoxParam2 = MonoSingleton<GameManager>.Instance.MasterParam.GetAutoRepeatQuestBoxParam(addCount + 1);
      AutoRepeatQuestBoxParam repeatQuestBoxParam3 = MonoSingleton<GameManager>.Instance.MasterParam.GetLastAutoRepeatQuestBoxParam();
      if (repeatQuestBoxParam1 == null)
      {
        DebugUtility.LogError("MasterParam > AutoRepeatQuestBox に指定された拡張回数のデータが見つかりませんでした [拡張回数:" + (object) addCount + "]");
        this.mMessageText.text = string.Empty;
        if (!Object.op_Inequality((Object) this.mDecideButton, (Object) null))
          return;
        ((Selectable) this.mDecideButton).interactable = false;
      }
      else if (repeatQuestBoxParam2 == null)
      {
        DebugUtility.LogError("現在拡張できる最大回数に達しているのに、Box枠の拡張をしようとしています");
        this.mMessageText.text = string.Empty;
        if (!Object.op_Inequality((Object) this.mDecideButton, (Object) null))
          return;
        ((Selectable) this.mDecideButton).interactable = false;
      }
      else
      {
        int coin = repeatQuestBoxParam2.Coin;
        int num = repeatQuestBoxParam2.Size - repeatQuestBoxParam1.Size;
        int size1 = repeatQuestBoxParam3.Size;
        int size2 = repeatQuestBoxParam1.Size;
        int size3 = repeatQuestBoxParam2.Size;
        this.mMessageText.text = string.Format(LocalizedText.Get("sys.AUTO_REPEAT_QUEST_BOX_ADD_CONFIRM_TEXT"), (object) coin, (object) num, (object) size1, (object) size2, (object) size3);
      }
    }
  }
}
