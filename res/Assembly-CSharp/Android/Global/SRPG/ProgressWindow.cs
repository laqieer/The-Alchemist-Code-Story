// Decompiled with JetBrains decompiler
// Type: SRPG.ProgressWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class ProgressWindow : MonoBehaviour
  {
    public ProgressWindow.ProgressRatio Ratios = new ProgressWindow.ProgressRatio(1f, 0.0f);
    public string CloseTrigger = "done";
    public float DestroyDelay = 1f;
    public string PercentageFormat = "{0:0}%";
    public string CompleteFormat = "{0:0}/{1:0}";
    public float DisplayImageThreshold = 2f;
    private long mKeepTotalDownloadSize = -1;
    private long mKeepCurrentDownloadSize = -1;
    private int mCurrentImageIndex = -1;
    private List<KeyValuePair<string, string>> mImagePairs = new List<KeyValuePair<string, string>>();
    private static ProgressWindow mInstance;
    public Animator WindowAnimator;
    public Slider ProgressBar;
    public Text Title;
    public Text Lore;
    public Text Percentage;
    public Text Complete;
    public ImageArray Phase;
    public GameObject notice0;
    public GameObject notice1;
    [SerializeField]
    private GameObject noticeTxt;
    [SerializeField]
    private Text downloadTxt;
    public string ImageTable;
    public RawImage[] Images;
    public GameObject ImageGroup;
    public GameObject buttonL;
    public GameObject buttonR;
    public Text introduction;
    public float MinVisibleTime;
    private float mLoadTime;
    private float mLoadProgress;
    private string previousLanguage;

    public static void OpenGenericDownloadWindow()
    {
      UnityEngine.Object.Instantiate<GameObject>(AssetManager.Load<GameObject>("UI/AssetsDownloading"));
    }

    public static void OpenVersusLoadScreen()
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance == (UnityEngine.Object) null))
        return;
      ProgressWindow original = AssetManager.Load<ProgressWindow>("UI/HomeMultiPlay_VersusMatching");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) UnityEngine.Object.Instantiate<ProgressWindow>(original).gameObject);
      GameUtility.FadeIn(0.1f);
    }

    public static void OpenBackgroundDownloaderBar()
    {
      ProgressWindow original = AssetManager.Load<ProgressWindow>("SGDevelopment/Tutorial/BackgroundDownloaderBar");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) UnityEngine.Object.Instantiate<ProgressWindow>(original).gameObject);
    }

    public static void OpenRankMatchLoadScreen()
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance == (UnityEngine.Object) null))
        return;
      ProgressWindow original = AssetManager.Load<ProgressWindow>("UI/HomeRankMatch_Matching");
      if (!((UnityEngine.Object) original != (UnityEngine.Object) null))
        return;
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) UnityEngine.Object.Instantiate<ProgressWindow>(original).gameObject);
      GameUtility.FadeIn(0.1f);
    }

    public static void OpenQuestLoadScreen(string title, string lore)
    {
      if ((UnityEngine.Object) ProgressWindow.mInstance == (UnityEngine.Object) null)
      {
        ProgressWindow original = !MonoSingleton<GameManager>.Instance.IsVersusMode() ? AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen") : AssetManager.Load<ProgressWindow>("UI/QuestLoadScreen_VS");
        if ((UnityEngine.Object) original != (UnityEngine.Object) null)
        {
          UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) UnityEngine.Object.Instantiate<ProgressWindow>(original).gameObject);
          GameUtility.FadeIn(0.1f);
        }
      }
      if (string.IsNullOrEmpty(title))
        title = string.Empty;
      if (string.IsNullOrEmpty(lore))
        lore = string.Empty;
      ProgressWindow.SetTexts(title, lore);
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
      }
      ProgressWindow.OpenQuestLoadScreen(title, lore);
    }

    public static void SetTexts(string title, string lore)
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance != (UnityEngine.Object) null))
        return;
      if ((UnityEngine.Object) ProgressWindow.mInstance.Title != (UnityEngine.Object) null)
        ProgressWindow.mInstance.Title.text = title;
      if (!((UnityEngine.Object) ProgressWindow.mInstance.Lore != (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.Lore.text = lore;
    }

    public static void SetLoadProgress(float t)
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance != (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.mLoadProgress = t;
    }

    public static void SetDestroyDelay(float delay)
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance != (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance.DestroyDelay = delay;
    }

    public static void Close()
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance != (UnityEngine.Object) null))
        return;
      Animator animator = !((UnityEngine.Object) ProgressWindow.mInstance.WindowAnimator != (UnityEngine.Object) null) ? ProgressWindow.mInstance.GetComponent<Animator>() : ProgressWindow.mInstance.WindowAnimator;
      if ((UnityEngine.Object) animator != (UnityEngine.Object) null)
        animator.SetTrigger(ProgressWindow.mInstance.CloseTrigger);
      if ((double) ProgressWindow.mInstance.DestroyDelay >= 0.0)
        UnityEngine.Object.Destroy((UnityEngine.Object) ProgressWindow.mInstance.gameObject, ProgressWindow.mInstance.DestroyDelay);
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    private void Start()
    {
      this.mCurrentImageIndex = 0;
      if ((bool) ((UnityEngine.Object) this.introduction))
      {
        this.previousLanguage = GameUtility.Config_Language;
        this.introduction.text = !(GameUtility.Config_Language != "None") ? string.Empty : LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1));
      }
      if (this.Images != null)
      {
        for (int index = 0; index < this.Images.Length; ++index)
        {
          if ((UnityEngine.Object) this.Images[index] != (UnityEngine.Object) null && (UnityEngine.Object) this.Images[index].material != (UnityEngine.Object) null)
            this.Images[index].material = new Material(this.Images[index].material);
        }
      }
      if (!string.IsNullOrEmpty(this.ImageTable))
      {
        this.LoadImageTable();
        this.StartCoroutine(this.AnimationThread());
      }
      if ((UnityEngine.Object) this.ImageGroup != (UnityEngine.Object) null)
        this.ImageGroup.SetActive(false);
      if (!((UnityEngine.Object) this.downloadTxt != (UnityEngine.Object) null))
        return;
      this.downloadTxt.text = LocalizedText.Get("download.FILE_CHECK");
    }

    private void OnEnable()
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance == (UnityEngine.Object) null))
        return;
      ProgressWindow.mInstance = this;
    }

    private void OnDisable()
    {
      if (!((UnityEngine.Object) ProgressWindow.mInstance == (UnityEngine.Object) this))
        return;
      ProgressWindow.mInstance = (ProgressWindow) null;
    }

    public static bool ShouldKeepVisible
    {
      get
      {
        if ((UnityEngine.Object) ProgressWindow.mInstance != (UnityEngine.Object) null)
          return (double) ProgressWindow.mInstance.mLoadTime < (double) ProgressWindow.mInstance.MinVisibleTime;
        return false;
      }
    }

    public void MovePrevious()
    {
      --this.mCurrentImageIndex;
      if (this.mImagePairs.Count == 0)
        this.LoadImageTable();
      if (this.mCurrentImageIndex < 0)
        this.mCurrentImageIndex = this.mImagePairs.Count - 1;
      this.introduction.text = LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1));
      this.StartCoroutine(this.AnimationThread());
    }

    public void MoveNext()
    {
      ++this.mCurrentImageIndex;
      if (this.mImagePairs.Count == 0)
        this.LoadImageTable();
      if (this.mCurrentImageIndex > this.mImagePairs.Count - 1)
        this.mCurrentImageIndex = 0;
      this.introduction.text = LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1));
      this.StartCoroutine(this.AnimationThread());
    }

    private void Update()
    {
      float num1 = 0.0f;
      this.mLoadTime += Time.unscaledDeltaTime;
      if ((double) this.Ratios.Download > 0.0)
        num1 += AssetDownloader.Progress / this.Ratios.Download;
      if ((double) this.Ratios.Deserilize > 0.0)
        num1 += this.mLoadProgress / this.Ratios.Deserilize;
      float num2 = num1 / (this.Ratios.Download + this.Ratios.Deserilize);
      this.ProgressBar.value = num2;
      long totalDownloadSize = AssetDownloader.TotalDownloadSize;
      long currentDownloadSize = AssetDownloader.CurrentDownloadSize;
      int phase = (int) AssetDownloader.Phase;
      if ((UnityEngine.Object) this.Phase != (UnityEngine.Object) null)
        this.Phase.ImageIndex = phase;
      if ((UnityEngine.Object) this.downloadTxt != (UnityEngine.Object) null)
        this.downloadTxt.text = phase != 0 ? LocalizedText.Get("download.DATA_DOWNLOAD") : LocalizedText.Get("download.FILE_CHECK");
      if ((bool) ((UnityEngine.Object) this.noticeTxt))
      {
        if (phase == 1)
          this.noticeTxt.SetActive(true);
        else
          this.noticeTxt.SetActive(false);
      }
      if ((UnityEngine.Object) this.Percentage != (UnityEngine.Object) null)
      {
        string str = string.Format(this.PercentageFormat, (object) (int) ((double) num2 * 100.0));
        if (this.Percentage.text != str)
          this.Percentage.text = str;
      }
      if ((UnityEngine.Object) this.Complete != (UnityEngine.Object) null)
      {
        if (this.mKeepTotalDownloadSize != totalDownloadSize)
        {
          this.mKeepTotalDownloadSize = totalDownloadSize;
          this.mKeepCurrentDownloadSize = -1L;
        }
        if (this.mKeepCurrentDownloadSize < currentDownloadSize)
        {
          this.mKeepCurrentDownloadSize = currentDownloadSize;
          string str = string.Format(this.CompleteFormat, (object) currentDownloadSize, (object) totalDownloadSize);
          if (this.Complete.text != str)
            this.Complete.text = str;
        }
      }
      if (!(bool) ((UnityEngine.Object) this.introduction) || !(this.previousLanguage != GameUtility.Config_Language))
        return;
      this.previousLanguage = GameUtility.Config_Language;
      this.introduction.text = LocalizedText.Get("download.FLAVOUR_TEXT" + (object) (this.mCurrentImageIndex + 1));
    }

    [DebuggerHidden]
    private IEnumerator AnimationThread()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ProgressWindow.\u003CAnimationThread\u003Ec__Iterator116() { \u003C\u003Ef__this = this };
    }

    private void LoadImageTable()
    {
      TextAsset textAsset = UnityEngine.Resources.Load<TextAsset>(this.ImageTable);
      if ((UnityEngine.Object) textAsset == (UnityEngine.Object) null)
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
