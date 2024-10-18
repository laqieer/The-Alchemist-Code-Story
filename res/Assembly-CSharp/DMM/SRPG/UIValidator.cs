// Decompiled with JetBrains decompiler
// Type: SRPG.UIValidator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (Selectable))]
  [DisallowMultipleComponent]
  public class UIValidator : MonoBehaviour
  {
    public static List<UIValidator> Validators = new List<UIValidator>();
    [BitMask]
    public CriticalSections Mask;
    public InputField Input;
    [BitMask]
    public UIValidator.ToggleMasks ToggleMask = UIValidator.ToggleMasks.Interactable;

    public static void UpdateValidators(CriticalSections updateMask, CriticalSections activeMask)
    {
      for (int index = UIValidator.Validators.Count - 1; index >= 0; --index)
      {
        if ((UIValidator.Validators[index].Mask & updateMask) != (CriticalSections) 0)
          UIValidator.Validators[index].UpdateInteractable(activeMask);
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Input, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<string>) this.Input.onValueChanged).AddListener(new UnityAction<string>((object) this, __methodptr(OnInputFieldChange)));
      }
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    private void UpdateInteractable(CriticalSections csMask)
    {
      bool flag = true;
      if ((csMask & this.Mask) != (CriticalSections) 0)
        flag = false;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Input, (UnityEngine.Object) null) && string.IsNullOrEmpty(this.Input.text))
        flag = false;
      if ((this.ToggleMask & UIValidator.ToggleMasks.Enable) != (UIValidator.ToggleMasks) 0)
      {
        Selectable component = ((Component) this).GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          ((Behaviour) component).enabled = flag;
      }
      if ((this.ToggleMask & UIValidator.ToggleMasks.Interactable) != (UIValidator.ToggleMasks) 0)
      {
        Selectable component = ((Component) this).GetComponent<Selectable>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.interactable = flag;
      }
      if ((this.ToggleMask & UIValidator.ToggleMasks.BlockRaycast) == (UIValidator.ToggleMasks) 0)
        return;
      CanvasGroup component1 = ((Component) this).GetComponent<CanvasGroup>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      component1.blocksRaycasts = flag;
    }

    private void OnEnable()
    {
      UIValidator.Validators.Add(this);
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    private void OnDisable() => UIValidator.Validators.Remove(this);

    private bool IsEmoji(char a)
    {
      if (char.IsSurrogate(a))
        return true;
      string pattern = "[\uE63E-\uE6A5]|[\uE6AC-\uE6AE]|[\uE6B1-\uE6BA]|[\uE6CE-\uE757]|î[\u0098-\u009D][\u0080-¿]|(?:î[±-\u00B3µ¶\u00BD-¿]|ï[\u0081-\u0083])[\u0080-¿]|î[\u0080\u0081\u0084\u0085\u0088\u0089\u008C\u008D\u0090\u0091\u0094][\u0080-¿]|(?:(?:#|[0-9])⃣)";
      return Regex.IsMatch(a.ToString(), pattern);
    }

    private bool IsEmojiWork(char a)
    {
      if ("©®   ‼⁉™ℹ↩↪⌚⌛⏩⏪⏫⏬⏰⏳Ⓜ▪▫▶◀◻◼◽◾☀☁☎☑☔☕☝☺♈♉♊♋♌♍♎♏♐♑♒♓♠♣♥♦♨♻♿⚓⚠⚡⚪⚫⚽⚾⛄⛅⛎⛔⛪⛲⛳⛵⛺⛽✂✅✈✉✊✋✌✏✒✔✖✨✳✴❄❇❌❎❓❔❕❗❤➕➖➗➡➰⤴⤵⬅⬆⬇⬛⬜⭐⭕〰〽㊗㊙".IndexOf(a) >= 0)
        return true;
      int[] numArray1 = new int[94]
      {
        126980,
        127183,
        127344,
        127345,
        127358,
        127359,
        127374,
        127489,
        127490,
        127514,
        127535,
        127568,
        127569,
        127759,
        127761,
        127763,
        127764,
        127765,
        127769,
        127771,
        127775,
        127776,
        127792,
        127793,
        127796,
        127797,
        127942,
        127944,
        127946,
        128012,
        128013,
        128014,
        128017,
        128018,
        128020,
        128023,
        128024,
        128025,
        128064,
        128238,
        128259,
        128527,
        128530,
        128531,
        128532,
        128534,
        128536,
        128538,
        128540,
        128541,
        128542,
        128557,
        128565,
        128643,
        128644,
        128645,
        128647,
        128649,
        128652,
        128655,
        128657,
        128658,
        128659,
        128661,
        128663,
        128665,
        128666,
        128674,
        128676,
        128677,
        128690,
        128694,
        128697,
        128704,
        127464,
        127475,
        127465,
        127466,
        127466,
        127480,
        127467,
        127479,
        127468,
        127463,
        127470,
        127481,
        127471,
        127477,
        127472,
        127479,
        127479,
        127482,
        127482,
        127480
      };
      foreach (int num in numArray1)
      {
        if (num == (int) a)
          return true;
      }
      int[] numArray2 = new int[66]
      {
        8596,
        8601,
        127377,
        127386,
        127538,
        127546,
        127744,
        127756,
        127799,
        127823,
        127825,
        127867,
        127872,
        127891,
        127904,
        127940,
        127968,
        127971,
        127973,
        127984,
        128026,
        128041,
        128043,
        128062,
        128066,
        128100,
        128102,
        128107,
        128110,
        128172,
        128174,
        128181,
        128184,
        128235,
        128240,
        128247,
        128249,
        128252,
        128266,
        128276,
        128278,
        128299,
        128302,
        128317,
        128336,
        128347,
        128507,
        128511,
        128513,
        128518,
        128521,
        128525,
        128544,
        128549,
        128552,
        128555,
        128560,
        128563,
        128567,
        128576,
        128581,
        128640,
        128679,
        128685,
        128698,
        128702
      };
      for (int index = 0; index < numArray2.Length; index += 2)
      {
        if (numArray2[index] <= (int) a && (int) a <= numArray2[index + 1])
          return true;
      }
      return false;
    }

    private void OnInputFieldChange(string value)
    {
      for (int index = 0; index < value.Length; ++index)
      {
        if (this.IsEmoji(value[index]))
        {
          this.Input.text = value.Remove(index);
          break;
        }
      }
      this.UpdateInteractable(CriticalSection.GetActive());
    }

    [Flags]
    public enum ToggleMasks
    {
      Interactable = 1,
      Enable = 2,
      BlockRaycast = 4,
    }
  }
}
