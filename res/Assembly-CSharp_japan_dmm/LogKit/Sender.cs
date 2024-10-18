﻿// Decompiled with JetBrains decompiler
// Type: LogKit.Sender
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Threading;

#nullable disable
namespace LogKit
{
  public class Sender
  {
    private readonly LinkedList<Guid> queue = new LinkedList<Guid>();
    private readonly string key;

    public Sender(string key)
    {
      this.key = key;
      this.Init();
    }

    private void Init()
    {
      string[] files = Directory.GetFiles(Logger.GetLogFilePath(this.key));
      if (files.Length > 100)
      {
        // ISSUE: reference to a compiler-generated field
        if (Sender.\u003C\u003Ef__mg\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          Sender.\u003C\u003Ef__mg\u0024cache0 = new ParameterizedThreadStart(Sender.FileDeleteThread);
        }
        // ISSUE: reference to a compiler-generated field
        new Thread(Sender.\u003C\u003Ef__mg\u0024cache0).Start((object) files);
      }
      else
      {
        for (int index = 0; index < files.Length; ++index)
          this.queue.AddLast(new Guid(Path.GetFileName(files[index])));
      }
    }

    private static void FileDeleteThread(object data)
    {
      foreach (string path in (string[]) data)
        System.IO.File.Delete(path);
    }

    public void Push(Guid logId)
    {
      lock ((object) this.queue)
        this.queue.AddLast(logId);
    }

    private void Pop(Guid logId)
    {
      lock ((object) this.queue)
      {
        this.queue.Remove(logId);
        try
        {
          System.IO.File.Delete(Logger.GetLogFilePath(this.key, logId));
        }
        catch (Exception ex)
        {
        }
      }
    }

    public void Emit()
    {
      if (!Logger.IsSetServerUrl)
        return;
      if (this.queue.Count == 0)
        return;
      try
      {
        if (string.IsNullOrEmpty(Logger.LogCollectionUrl))
        {
          List<Guid> guidList = new List<Guid>();
          foreach (Guid guid in this.queue)
            guidList.Add(guid);
          foreach (Guid logId in guidList)
            this.Pop(logId);
        }
        else
        {
          ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((sender, certificate, chain, sslPolicyErrors) => true);
          Guid logId = this.queue.First.Value;
          byte[] buffer = System.IO.File.ReadAllBytes(Logger.GetLogFilePath(this.key, logId));
          HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(Logger.LogCollectionUrl);
          httpWebRequest.Method = "POST";
          httpWebRequest.ContentType = "application/json";
          httpWebRequest.ContentLength = (long) buffer.Length;
          using (Stream requestStream = httpWebRequest.GetRequestStream())
          {
            requestStream.Write(buffer, 0, buffer.Length);
            requestStream.Close();
          }
          HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse();
          int statusCode = (int) response.StatusCode;
          response.Close();
          if (200 > statusCode || statusCode > 299)
            return;
          this.Pop(logId);
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
