// Decompiled with JetBrains decompiler
// Type: SRPG.ProgressWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ProgressWindow : MonoBehaviour
  {
    private static ProgressWindow mInstance;
    public Animator WindowAnimator;
    public Slider ProgressBar;
    public ProgressWindow.ProgressRatio Ratios = new ProgressWindow.ProgressRatio(1f, 0.0f);
    public string CloseTrigger = "done";
    public float DestroyDelay = 1f;
    public Text Title;
    public Text Lore;
    public Text Percentage;
    public string PercentageFormat = "{0:0}%";
    public Text Complete;
    public string CompleteFormat = "{0:0}/{1:0}";
    public ImageArray Phase;
    public GameObject[] PhaseObjects;
    public GameObject notice0;
    public GameObject notice1;
    public string ImageTable;
    public RawImage[] Images;
    public float DisplayImageThreshold = 2f;
    public GameObject ImageGroup;
    public GameObject ProgressBarRoot;
    public float MinVisibleTime;
    private float mLoadTime;
    private float mLoadProgress;
    private long mKeepTotalDownloadSize = -1;
    private long mKeepCurrentDownloadSize = -1;
    private int mCurrentImageIndex = -1;
    private List<KeyValuePair<string, string>> mImagePairs = new List<KeyValuePair<string, string>>();
    private bool mAutoUpdateProgressText = true;

    public static void SetActiveWindow(bool is_active)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.ProgressBarRoot, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.ProgressBarRoot.SetActive(is_active);
    }

    public static void SetActiveCompleteCount(bool is_active)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Complete, (UnityEngine.Object) null))
        return;
      ((Component) ProgressWindow.mInstance.Complete).gameObject.SetActive(is_active);
    }

    public static void SetAutoUpdateProgressText(bool value)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.mAutoUpdateProgressText = value;
    }

    public static void OpenGenericDownloadWindow(bool autoUpdateProgressText = true)
    {
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/AssetsDownloading"));
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
        return;
      ProgressWindow componentInChildren = gameObject.GetComponentInChildren<ProgressWindow>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
        return;
      componentInChildren.mAutoUpdateProgressText = autoUpdateProgressText;
    }

    [DebuggerHidden]
    public static IEnumerator OpenVersusLoadScreenAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProgressWindow.\u003COpenVersusLoadScreenAsync\u003Ec__Iterator0 screenAsyncCIterator0 = new ProgressWindow.\u003COpenVersusLoadScreenAsync\u003Ec__Iterator0();
      return (IEnumerator) screenAsyncCIterator0;
    }

    [DebuggerHidden]
    public static IEnumerator OpenVersusDraftLoadScreenAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProgressWindow.\u003COpenVersusDraftLoadScreenAsync\u003Ec__Iterator1 screenAsyncCIterator1 = new ProgressWindow.\u003COpenVersusDraftLoadScreenAsync\u003Ec__Iterator1();
      return (IEnumerator) screenAsyncCIterator1;
    }

    [DebuggerHidden]
    public static IEnumerator OpenRankMatchLoadScreenAsync()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProgressWindow.\u003COpenRankMatchLoadScreenAsync\u003Ec__Iterator2 screenAsyncCIterator2 = new ProgressWindow.\u003COpenRankMatchLoadScreenAsync\u003Ec__Iterator2();
      return (IEnumerator) screenAsyncCIterator2;
    }

    public static void OpenQuestLoadScreen(string title, string lore)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
      {
        ProgressWindow progressWindow = !MonoSingleton<GameManager>.Instance.IsVersusMode() ? (!(bool) GlobalVars.GvGBattleMode || (bool) GlobalVars.GvGBattleReplay ? AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen") : AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_GvG")) : (GlobalVars.IsVersusDraftMode ? AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_Draft") : AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_VS"));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) progressWindow, (UnityEngine.Object) null))
        {
          UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) ((Component) UnityEngine.Object.Instantiate<ProgressWindow>(progressWindow)).gameObject);
          GameUtility.FadeIn(0.1f);
        }
      }
      if (string.IsNullOrEmpty(title))
        title = string.Empty;
      if (string.IsNullOrEmpty(lore))
        lore = string.Empty;
      ProgressWindow.SetTexts(title, lore);
    }

    [DebuggerHidden]
    public static IEnumerator LoadAsyncPrefab()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ProgressWindow.\u003CLoadAsyncPrefab\u003Ec__Iterator3 prefabCIterator3 = new ProgressWindow.\u003CLoadAsyncPrefab\u003Ec__Iterator3();
      return (IEnumerator) prefabCIterator3;
    }

    public static void OpenQuestLoadScreen(QuestParam quest)
    {
      string title = (string) null;
      string lore = (string) null;
      if (quest != null)
      {
        title = quest.name;
        if (quest.type == QuestTypes.Tower)
          title = quest.title + " " + quest.name;
        if (!string.IsNullOrEmpty(quest.storyTextID))
          lore = LocalizedText.Get(quest.storyTextID);
        else if (quest.IsGuildRaid && GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Mock)
          title = quest.name + "(" + LocalizedText.Get("sys.GUILDRAID_SWITCH_BATTALETEST") + ")";
        else if (quest.IsGvG)
          lore = LocalizedText.Get("sys.GVG_TEXT_LOADING");
      }
      ProgressWindow.OpenQuestLoadScreen(title, lore);
    }

    public static void SetTexts(string title, string lore)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Title, (UnityEngine.Object) null))
        ProgressWindow.mInstance.Title.text = title;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Lore, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.Lore.text = lore;
    }

    public static void SetQuestText(QuestParam quest, string title = null, string lore = null)
    {
      if (quest == null)
        return;
      if (string.IsNullOrEmpty(title))
        title = quest.name;
      if (string.IsNullOrEmpty(lore))
        lore = LocalizedText.Get(quest.storyTextID);
      if (quest.IsGuildRaid && GlobalVars.CurrentBattleType.Get() == GuildRaidBattleType.Mock)
        title = quest.name + "(" + LocalizedText.Get("sys.GUILDRAID_SWITCH_BATTALETEST") + ")";
      else if (quest.IsGvG)
        lore = LocalizedText.Get("sys.GVG_TEXT_LOADING");
      ProgressWindow.SetTexts(title, lore);
    }

    public static void SetLoadProgress(float t)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.mLoadProgress = t;
    }

    public static void SetLoadProgress(float progress, long current, long target)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null) || ProgressWindow.mInstance.mAutoUpdateProgressText)
        return;
      ProgressWindow.mInstance.mLoadProgress = progress;
      float num1 = 0.0f;
      if ((double) ProgressWindow.mInstance.Ratios.Download > 0.0)
        num1 += progress / ProgressWindow.mInstance.Ratios.Download;
      if ((double) ProgressWindow.mInstance.Ratios.Deserilize > 0.0)
        num1 += ProgressWindow.mInstance.mLoadProgress / ProgressWindow.mInstance.Ratios.Deserilize;
      float num2 = num1 / (ProgressWindow.mInstance.Ratios.Download + ProgressWindow.mInstance.Ratios.Deserilize);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.ProgressBar, (UnityEngine.Object) null))
        ProgressWindow.mInstance.ProgressBar.value = num2;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Percentage, (UnityEngine.Object) null))
      {
        string str = string.Format(ProgressWindow.mInstance.PercentageFormat, (object) (int) ((double) num2 * 100.0));
        if (ProgressWindow.mInstance.Percentage.text != str)
          ProgressWindow.mInstance.Percentage.text = str;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.Complete, (UnityEngine.Object) null))
        return;
      if (ProgressWindow.mInstance.mKeepTotalDownloadSize != target)
      {
        ProgressWindow.mInstance.mKeepTotalDownloadSize = target;
        ProgressWindow.mInstance.mKeepCurrentDownloadSize = -1L;
      }
      if (ProgressWindow.mInstance.mKeepCurrentDownloadSize >= current)
        return;
      ProgressWindow.mInstance.mKeepCurrentDownloadSize = current;
      string str1 = string.Format(ProgressWindow.mInstance.CompleteFormat, (object) current, (object) target);
      if (!(ProgressWindow.mInstance.Complete.text != str1))
        return;
      ProgressWindow.mInstance.Complete.text = str1;
    }

    public static void SetDestroyDelay(float delay)
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.DestroyDelay = delay;
    }

    public static void Close()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      Animator animator = !UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance.WindowAnimator, (UnityEngine.Object) null) ? ((Component) ProgressWindow.mInstance).GetComponent<Animator>() : ProgressWindow.mInstance.WindowAnimator;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) animator, (UnityEngine.Object) null))
        animator.SetTrigger(ProgressWindow.mInstance.CloseTrigger);
      if ((double) ProgressWindow.mInstance.DestroyDelay >= 0.0)
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) ProgressWindow.mInstance).gameObject, ProgressWindow.mInstance.DestroyDelay);
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    public static bool IsInstance()
    {
      return UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null);
    }

    private void Start()
    {
      if (this.Images != null)
      {
        for (int index = 0; index < this.Images.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Images[index], (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Graphic) this.Images[index]).material, (UnityEngine.Object) null))
            ((Graphic) this.Images[index]).material = new Material(((Graphic) this.Images[index]).material);
        }
      }
      if (!string.IsNullOrEmpty(this.ImageTable))
      {
        this.LoadImageTable();
        this.StartCoroutine(this.AnimationThread());
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ImageGroup, (UnityEngine.Object) null))
        return;
      this.ImageGroup.SetActive(false);
    }

    private void OnEnable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance = this;
    }

    private void OnDisable()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) this))
        return;
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    public static bool ShouldKeepVisible
    {
      get
      {
        return UnityEngine.Object.op_Inequality((UnityEngine.Object) ProgressWindow.mInstance, (UnityEngine.Object) null) && (double) ProgressWindow.mInstance.mLoadTime < (double) ProgressWindow.mInstance.MinVisibleTime;
      }
    }

    private void Update()
    {
      this.mLoadTime += Time.unscaledDeltaTime;
      int phase = (int) AssetDownloader.Phase;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Phase, (UnityEngine.Object) null))
        this.Phase.ImageIndex = phase;
      else if (this.PhaseObjects != null && this.PhaseObjects.Length > 0)
      {
        for (int index = 0; index < this.PhaseObjects.Length; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PhaseObjects[index], (UnityEngine.Object) null))
            this.PhaseObjects[index].SetActive(false);
        }
        if (this.PhaseObjects.Length > phase && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PhaseObjects[phase], (UnityEngine.Object) null))
          this.PhaseObjects[phase].SetActive(true);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.notice0, (UnityEngine.Object) null))
      {
        if (phase == 1)
          this.notice0.SetActive(true);
        else
          this.notice0.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.notice1, (UnityEngine.Object) null))
      {
        if (phase == 1)
          this.notice1.SetActive(true);
        else
          this.notice1.SetActive(false);
      }
      if (!this.mAutoUpdateProgressText)
        return;
      float num1 = 0.0f;
      if ((double) this.Ratios.Download > 0.0)
        num1 += AssetDownloader.Progress / this.Ratios.Download;
      if ((double) this.Ratios.Deserilize > 0.0)
        num1 += this.mLoadProgress / this.Ratios.Deserilize;
      float num2 = num1 / (this.Ratios.Download + this.Ratios.Deserilize);
      this.ProgressBar.value = num2;
      long totalDownloadSize = AssetDownloader.TotalDownloadSize;
      long currentDownloadSize = AssetDownloader.CurrentDownloadSize;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Percentage, (UnityEngine.Object) null))
      {
        string str = string.Format(this.PercentageFormat, (object) (int) ((double) num2 * 100.0));
        if (this.Percentage.text != str)
          this.Percentage.text = str;
      }
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.Complete, (UnityEngine.Object) null))
        return;
      if (this.mKeepTotalDownloadSize != totalDownloadSize)
      {
        this.mKeepTotalDownloadSize = totalDownloadSize;
        this.mKeepCurrentDownloadSize = -1L;
      }
      if (this.mKeepCurrentDownloadSize >= currentDownloadSize)
        return;
      this.mKeepCurrentDownloadSize = currentDownloadSize;
      string str1 = string.Format(this.CompleteFormat, (object) currentDownloadSize, (object) totalDownloadSize);
      if (!(this.Complete.text != str1))
        return;
      this.Complete.text = str1;
    }

    [DebuggerHidden]
    private IEnumerator AnimationThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ProgressWindow.\u003CAnimationThread\u003Ec__Iterator4()
      {
        \u0024this = this
      };
    }

    private void LoadImageTable()
    {
      TextAsset textAsset = Resources.Load<TextAsset>(this.ImageTable);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) textAsset, (UnityEngine.Object) null))
        return;
      StringReader stringReader = new StringReader(textAsset.text);
      string str;
      while ((str = stringReader.ReadLine()) != null)
      {
        if (!string.IsNullOrEmpty(str))
        {
          string[] strArray = str.Split('\t');
          this.mImagePairs.Add(new KeyValuePair<string, string>(strArray[0], strArray[1]));
        }
      }
    }

    [Serializable]
    public struct ProgressRatio
    {
      [Range(0.0f, 1f)]
      public float Download;
      [Range(0.0f, 1f)]
      public float Deserilize;

      public ProgressRatio(float a, float b)
      {
        this.Download = a;
        this.Deserilize = b;
      }
    }
  }
}
