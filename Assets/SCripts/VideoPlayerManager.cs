using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerManager : MonoBehaviour
{
 #region Variables
 RenderTexture rndrtxtr;
public VideoPlayer plyr;
public AudioSource src;
 public Material mat360;
 public Material DefaultMat;
 string rootAddress;
 public string videoName;
 public GameObject environmentObj;
 #endregion
 #region Properties
 #endregion
 #region Functions
    void Start()
    {
          plyr=gameObject.GetComponent<VideoPlayer>();
          plyr.playOnAwake=false;
          src=GetComponent<AudioSource>();
          src.playOnAwake=false;
          rootAddress="//storage//emulated//0//GoPlayer//";
          plyr.loopPointReached+=delegate{OnVideoEnded();};
          RenderSettings.skybox=DefaultMat;
    }
    public bool AskForAddress(string vdnm)
    {
      if(Application.platform==RuntimePlatform.WindowsEditor || Application.platform==RuntimePlatform.WindowsPlayer)
      {
         videoName="D:\\GoPlayer\\"+vdnm+".mp4";
      }
      else
      {
        videoName=rootAddress+vdnm+".mp4";
      }  
      if(plyr.isPlaying)
      return false;
      else
      {
        OnAddressPassed();
        return true;
      }
    } 


    //-----------Set the video Num
    public bool SetTheAddress(string vdnm)
    {
        if(Application.platform==RuntimePlatform.WindowsEditor || Application.platform==RuntimePlatform.WindowsPlayer)
      {
         videoName="D:\\GoPlayer\\"+vdnm+".mp4";
      }
      else
      {
        videoName=rootAddress+vdnm+".mp4";
      }  
      if(plyr.isPlaying)
      return false;
      else
      {
        OnAddressPassed();
        return true;
      }
    }
    public void OnAddressPassed()
    {   
       plyr.source=VideoSource.Url;
       plyr.url=videoName;
       plyr.audioOutputMode = VideoAudioOutputMode.AudioSource;
       plyr.controlledAudioTrackCount = 1;
       plyr.EnableAudioTrack(0, true);
       plyr.SetTargetAudioSource(0, src);
       plyr.Prepare();
       plyr.prepareCompleted+=OnPrepareComplete;
       
    }
    void OnPrepareComplete(VideoPlayer myPlyr)
    {
     rndrtxtr=new RenderTexture((int)(plyr.texture.width),(int)(plyr.texture.height),24);
     plyr.targetTexture=rndrtxtr;
     mat360.mainTexture=rndrtxtr;
    }
 //OLD VERSION
    public   void OnSpacePressed()
    {
        if(plyr.isPlaying)
        {
        return;
        }
        if(!plyr.isPrepared)
        {
        return;
        }
       rndrtxtr.Release();
       environmentObj.SetActive(false);
       RenderSettings.skybox=mat360;
        plyr.Play();
        src.Play();    
    }
    // TCP Version:tcp command for play the video
    public void OnStartCommanded()
    {
      
        if(plyr.isPlaying)
        {
        return;
        }
        if(!plyr.isPrepared)
        {
        return;
        }
       rndrtxtr.Release();
       environmentObj.SetActive(false);
       RenderSettings.skybox=mat360;
        plyr.Play();
        src.Play();
    }
 public void StopPlaying()
 {
   plyr.Stop();
   src.Stop();
   RenderSettings.skybox=DefaultMat;
   OnAddressPassed();
 }
 /*   public IEnumerator setAddress(string adrs)
   {
     while(!AskForAddress(adrs))
     {
       yield return new WaitForSeconds(1);
     }
   }
   public void startSetAddressCoroutine(string add)
   {
     StartCoroutine(setAddress(add));
   }
   */
  public void  MuteChange(bool a)
  {
    if(a)
    {
      src.volume=0;
      plyr.SetDirectAudioMute(0,a);
    }
    else
    {
      src.volume=1.0f;
      plyr.SetDirectAudioMute(0,a);
    }
    
  }
  public void OnVideoEnded()
  {
     RenderSettings.skybox=DefaultMat;
     OnAddressPassed();
  }
  
 #endregion
}
