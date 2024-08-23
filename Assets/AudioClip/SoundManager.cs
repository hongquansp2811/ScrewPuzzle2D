//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public partial class SoundManager : MonoBehaviour
//{
//    public static SoundManager ins;

//    [HideInInspector] public AudioSource backgroundSound;
//    [HideInInspector] public AudioSource efxSound;


//    private void Awake()
//    {
//        ins = this;
//        DontDestroyOnLoad(this.gameObject);
//        backgroundSound = gameObject.AddComponent<AudioSource>();
//        efxSound = gameObject.AddComponent<AudioSource>();

//        PlayMusicBg(ins.bgMusic);
//    }

//    #region Bg sound
//    public static void PlayMusicBg(SoundInfor sound)
//    {
//        if (PlayerPrefs.GetInt("CanPlayMusic", 1) == 1)
//        {
//            ins.backgroundSound.clip = sound.Clip;
//            ins.backgroundSound.volume = 0.3f;
//            ins.backgroundSound.loop = true;
//            ins.backgroundSound.Play();
//        }
//    }
//    public static void StopMusicBg()
//    {
//        ins.backgroundSound.Stop();
//    }
//    public static void SetupMusicBg(bool _b)
//    {
//        ins.backgroundSound.mute = !_b;
//    }

//    //volume: 0 ~ 1
//    public static void UpdateVolumeBg(float volume)
//    {
//        ins.backgroundSound.volume = volume;
//    }
//    #endregion

//    #region Efx sound
//    public static void PlayEfxSound(SoundInfor sound)
//    {
//        if (PlayerPrefs.GetInt("CanPlaySounds", 1) == 1)
//        {
//            ins.efxSound.clip = sound.Clip;
//            ins.efxSound.loop = false;
//            ins.efxSound.volume = 1;
//            ins.efxSound.PlayOneShot(ins.efxSound.clip);
//        }
//    }

//    public static void StopAllEfxSound()
//    {
//        ins.efxSound.Stop();
//    }
//    #endregion

//    public void ChangeSound(SoundInfor sound, float time)
//    {
//        if (PlayerPrefs.GetInt("CanPlayMusic", 1) == 1)
//        {
//            float spacetime = time / 2;

//            ChangeVol(.1f, spacetime,
//                () =>
//                {
//                    PlayMusicBg(sound);
//                    ChangeVol(1, spacetime, null);
//                });
//        }
//    }

//    public void ChangeVol(float vol, float time, UnityAction callBack)
//    {
//        StartCoroutine(ChangeVolume(vol, time, callBack));
//    }

//    private IEnumerator ChangeVolume(float vol, float time, UnityAction callBack)
//    {
//        var volume = backgroundSound.volume;
//        float stepVol = (vol - backgroundSound.volume) / 10;
//        float stepTime = time / 10;

//        for (int i = 0; i < 10; i++)
//        {
//            volume += stepVol;
//            backgroundSound.volume = volume;
//            //yield return Cache.GetWFS(stepTime);
//            yield return null;
//        }

//        callBack?.Invoke();
//    }

//    public void BtnClick()
//    {
//        PlayEfxSound(ins.UIClick);
//    }
//}

//[Serializable]
//public class SoundInfor
//{
//    public AudioClip Clip;
//    [Range(0, 1)]
//    public float Volume = 1;
//}
