// Decompiled with JetBrains decompiler
// Type: SGS3.SGS3ImageLoader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using UnityEngine;

namespace SGS3
{
  public class SGS3ImageLoader : MonoSingleton<SGS3ImageLoader>
  {
    protected Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();
    private static string lang;

    public static void SetLang()
    {
      SGS3ImageLoader.lang = GameUtility.Config_Language;
    }

    [DebuggerHidden]
    public IEnumerator FetchAll(List<string> paths)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGS3ImageLoader.\u003CFetchAll\u003Ec__Iterator4F() { paths = paths, \u003C\u0024\u003Epaths = paths };
    }

    [DebuggerHidden]
    public IEnumerator DownloadAll(List<string> hashes, string cryptPath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGS3ImageLoader.\u003CDownloadAll\u003Ec__Iterator50() { cryptPath = cryptPath, hashes = hashes, \u003C\u0024\u003EcryptPath = cryptPath, \u003C\u0024\u003Ehashes = hashes, \u003C\u003Ef__this = this };
    }

    [DebuggerHidden]
    protected IEnumerator WWWDownload(string hash, string cryptPath)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGS3ImageLoader.\u003CWWWDownload\u003Ec__Iterator51() { hash = hash, cryptPath = cryptPath, \u003C\u0024\u003Ehash = hash, \u003C\u0024\u003EcryptPath = cryptPath };
    }

    public static string Hash(string key)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(key);
      string empty = string.Empty;
      using (SHA512 shA512 = (SHA512) new SHA512Managed())
        return BitConverter.ToString(shA512.ComputeHash(bytes)).Replace("-", string.Empty).ToLower();
    }

    public static int URLExists(string url)
    {
      int num = 0;
      WebRequest webRequest = WebRequest.Create(url);
      webRequest.Timeout = 1200;
      webRequest.Method = "HEAD";
      HttpWebResponse httpWebResponse = (HttpWebResponse) null;
      try
      {
        httpWebResponse = (HttpWebResponse) webRequest.GetResponse();
        num = 1;
      }
      catch (WebException ex)
      {
        if (ex.Status == WebExceptionStatus.Timeout)
          num = 2;
        UnityEngine.Debug.Log((object) ex.Status);
        UnityEngine.Debug.Log((object) (url + " doesn't exist: " + ex.Message));
      }
      finally
      {
        httpWebResponse?.Close();
      }
      return num;
    }

    public static string ResolveHash(string bashHash, int page)
    {
      if (SGS3ImageLoader.lang == "None")
        SGS3ImageLoader.lang = "english";
      string str = bashHash;
      if (page > 0)
        str = SGS3ImageLoader.Hash(page.ToString() + bashHash);
      if (SGS3ImageLoader.lang != "english")
        str = SGS3ImageLoader.Hash(page.ToString() + str + SGS3ImageLoader.lang);
      return str;
    }

    private static object GrabPath(object item)
    {
      SGS3ImageLoader.Items items = (SGS3ImageLoader.Items) item;
      items.output = new List<string>();
      for (int page = 0; page < items.depth; ++page)
      {
        string str = SGS3ImageLoader.ResolveHash(items.hash, page);
        string url = AssetDownloader.GachaBannerURL + str + "/image.png";
        int num1 = SGS3ImageLoader.URLExists(url);
        int num2 = 0;
        while (num1 == 2 && ++num2 <= 10)
          num1 = SGS3ImageLoader.URLExists(url);
        if (num1 == 1)
          items.output.Add(str);
      }
      return (object) items;
    }

    public static string GeneratePath(string cryptoFolder, string hash, bool withFilename = false)
    {
      string path = Path.Combine(Application.persistentDataPath, cryptoFolder + "/" + hash + "/image.png");
      if (withFilename)
        return path;
      return Path.GetDirectoryName(path);
    }

    [DebuggerHidden]
    public IEnumerator LoadAndCacheTexture(string cryptfolder, string hash, int page)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SGS3ImageLoader.\u003CLoadAndCacheTexture\u003Ec__Iterator52() { cryptfolder = cryptfolder, hash = hash, page = page, \u003C\u0024\u003Ecryptfolder = cryptfolder, \u003C\u0024\u003Ehash = hash, \u003C\u0024\u003Epage = page, \u003C\u003Ef__this = this };
    }

    private class Threader
    {
      private static volatile int gCurThreadCnt = 0;
      private static Thread queueThread = (Thread) null;
      private static EventWaitHandle waiter = new EventWaitHandle(false, EventResetMode.AutoReset);
      private static System.Collections.Generic.Queue<ThreadStart> actions = new System.Collections.Generic.Queue<ThreadStart>();
      private static readonly object _locker = new object();
      private const int MAX_THREAD = 32;
      protected volatile bool isDone;
      protected object param;

      private Threader(object param)
      {
        this.isDone = false;
        this.param = param;
        if (SGS3ImageLoader.Threader.queueThread != null)
          return;
        SGS3ImageLoader.Threader.queueThread = new Thread((ThreadStart) (() =>
        {
          while (true)
          {
            int num = 0;
            lock (SGS3ImageLoader.Threader._locker)
              num = SGS3ImageLoader.Threader.actions.Count;
            if (num <= 0)
            {
              SGS3ImageLoader.Threader.waiter.WaitOne();
            }
            else
            {
              while (SGS3ImageLoader.Threader.gCurThreadCnt >= 32)
                Thread.Sleep(100);
              ThreadStart start;
              lock (SGS3ImageLoader.Threader._locker)
                start = SGS3ImageLoader.Threader.actions.Dequeue();
              ++SGS3ImageLoader.Threader.gCurThreadCnt;
              new Thread(start).Start();
            }
          }
        }));
        SGS3ImageLoader.Threader.queueThread.Start();
      }

      public static SGS3ImageLoader.Threader StartThread(SGS3ImageLoader.Threader.Process process, object param)
      {
        SGS3ImageLoader.Threader t = new SGS3ImageLoader.Threader(param);
        lock (SGS3ImageLoader.Threader._locker)
          SGS3ImageLoader.Threader.actions.Enqueue((ThreadStart) (() =>
          {
            t.param = process(t.param);
            t.isDone = true;
            --SGS3ImageLoader.Threader.gCurThreadCnt;
          }));
        SGS3ImageLoader.Threader.waiter.Set();
        return t;
      }

      public bool IsDone()
      {
        return this.isDone;
      }

      public object Param()
      {
        return this.param;
      }

      public delegate object Process(object param);
    }

    private struct Items
    {
      public string hash;
      public int depth;
      public List<string> output;
    }
  }
}
