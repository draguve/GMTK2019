using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine.UI;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// a class to save sound settings (music on or off, sfx on or off)
    /// </summary>
    [Serializable]
    public class SoundSettings
    {
        public bool MusicOn = true;
        public bool SfxOn = true;
    }

    [Serializable]
    public struct PauseEvent
    {
        public bool gamePaused;
        
        
        static PauseEvent e;
        public static void Trigger(bool toPause)
        {
            e.gamePaused = toPause;
            MMEventManager.TriggerEvent(e);
        }
    }
    

    /// <summary>
    /// This persistent singleton handles sound playing
    /// </summary>
    [AddComponentMenu("TopDown Engine/Managers/Sound Manager")]
    public class SoundManager : PersistentSingleton<SoundManager>, MMEventListener<MMSfxEvent>,MMEventListener<PauseEvent> {
        [Header("Settings")]
        /// the current sound settings 
        public SoundSettings Settings;

        [Header("Music")]
        /// the music volume
        [Range(0, 1)]
        public float MusicVolume = 0.3f;

        [Header("Sound Effects")]
        /// the sound fx volume
        [Range(0, 1)]
        public float SfxVolume = 1f;

        [Header("Pause")]
        /// whether or not Sfx should be muted when the game is paused
        public bool MuteSfxOnPause = true;

        protected const string _saveFolderName = "TopDownEngine/";
        protected const string _saveFileName = "sound.settings";
        protected AudioSource _backgroundMusic;
        protected List<AudioSource> _loopingSounds;

        /// <summary>
        /// Plays a background music.
        /// Only one background music can be active at a time.
        /// </summary>
        /// <param name="Clip">Your audio clip.</param>
        public virtual void PlayBackgroundMusic(AudioSource Music)
        {
            // if the music's been turned off, we do nothing and exit
            if (!Settings.MusicOn)
                return;
            // if we already had a background music playing, we stop it
            if (_backgroundMusic != null)
                _backgroundMusic.Stop();
            // we set the background music clip
            _backgroundMusic = Music;
            // we set the music's volume
            _backgroundMusic.volume = MusicVolume;
            // we set the loop setting to true, the music will loop forever
            _backgroundMusic.loop = true;
            // we start playing the background music
            _backgroundMusic.Play();
        }

        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <returns>An audiosource</returns>
        /// <param name="sfx">The sound clip you want to play.</param>
        /// <param name="location">The location of the sound.</param>
        /// <param name="loop">If set to true, the sound will loop.</param>
        public virtual AudioSource PlaySound(AudioClip sfx, Vector3 location, bool loop = false)
        {
            if (!Settings.SfxOn)
                return null;
            // we create a temporary game object to host our audio source
            GameObject temporaryAudioHost = new GameObject("TempAudio");
            // we set the temp audio's position
            temporaryAudioHost.transform.position = location;
            // we add an audio source to that host
            AudioSource audioSource = temporaryAudioHost.AddComponent<AudioSource>() as AudioSource;
            // we set that audio source clip to the one in paramaters
            audioSource.clip = sfx;
            // we set the audio source volume to the one in parameters
            audioSource.volume = SfxVolume;
            // we set our loop setting
            audioSource.loop = loop;
            // we start playing the sound
            audioSource.Play();

            if (!loop)
            {
                // we destroy the host after the clip has played
                Destroy(temporaryAudioHost, sfx.length);
            }
            else
            {
                _loopingSounds.Add(audioSource);
            }

            // we return the audiosource reference
            return audioSource;
        }

        /// <summary>
        /// Stops the looping sounds if there are any
        /// </summary>
        /// <param name="source">Source.</param>
        public virtual void StopLoopingSound(AudioSource source)
        {
            if (source != null)
            {
                _loopingSounds.Remove(source);
                Destroy(source.gameObject);
            }
        }

        public void ChangeMusicVolume(Slider slider)
        {
            MusicVolume = slider.value;
        }
        public void ChangeSfxVolume(Slider slider)
        {
            SfxVolume = slider.value;
        }

        /// <summary>
        /// Sets the music on/off setting based on the value in parameters
        /// This value will be saved, and any music played after that setting change will comply
        /// </summary>
        /// <param name="status"></param>
		protected virtual void SetMusic(bool status)
        {
            Settings.MusicOn = status;
            SaveSoundSettings();
            if (status)
            {
                UnmuteBackgroundMusic();
            }
            else
            {
                MuteBackgroundMusic();
            }
        }

        /// <summary>
        /// Sets the SFX on/off setting based on the value in parameters
        /// This value will be saved, and any SFX played after that setting change will comply
        /// </summary>
        /// <param name="status"></param>
		protected virtual void SetSfx(bool status)
        {
            Settings.SfxOn = status;
            SaveSoundSettings();
        }

        /// <summary>
        /// Sets the music setting to On
        /// </summary>
		public virtual void MusicOn() { SetMusic(true); }

        /// <summary>
        /// Sets the Music setting to Off
        /// </summary>
		public virtual void MusicOff() { SetMusic(false); }

        /// <summary>
        /// Sets the SFX setting to On
        /// </summary>
		public virtual void SfxOn() { SetSfx(true); }

        /// <summary>
        /// Sets the SFX setting to Off
        /// </summary>
		public virtual void SfxOff() { SetSfx(false); }

        /// <summary>
        /// Saves the sound settings to file
        /// </summary>
		protected virtual void SaveSoundSettings()
        {
            SaveLoadManager.Save(Settings, _saveFileName, _saveFolderName);
        }

        /// <summary>
        /// Loads the sound settings from file (if found)
        /// </summary>
		protected virtual void LoadSoundSettings()
        {
            SoundSettings settings = (SoundSettings)SaveLoadManager.Load(_saveFileName, _saveFolderName);
            if (settings != null)
            {
                Settings = settings;
            }
        }

        /// <summary>
        /// Resets the sound settings by destroying the save file
        /// </summary>
		protected virtual void ResetSoundSettings()
        {
            SaveLoadManager.DeleteSave(_saveFileName, _saveFolderName);
        }

        /// <summary>
        /// When we grab a sfx event, we play the corresponding sound
        /// </summary>
        /// <param name="sfxEvent"></param>
		public virtual void OnMMEvent(MMSfxEvent sfxEvent)
        {
            PlaySound(sfxEvent.ClipToPlay, this.transform.position);
        }
        
        /// <summary>
        /// Mutes all sfx currently playing
        /// </summary>
        protected virtual void MuteAllSfx()
        {
            foreach (AudioSource source in _loopingSounds)
            {
                if (source != null)
                {
                    source.mute = true;
                }
            }
        }

        /// <summary>
        /// Unmutes all sfx currently playing
        /// </summary>
		protected virtual void UnmuteAllSfx()
        {
            foreach (AudioSource source in _loopingSounds)
            {
                if (source != null)
                {
                    source.mute = false;
                }
            }
        }

        /// <summary>
        /// Unmutes the background music
        /// </summary>
        public virtual void UnmuteBackgroundMusic()
        {
            if (_backgroundMusic != null)
            {
                _backgroundMusic.mute = false;
            }
        }

        /// <summary>
        /// Mutes the background music
        /// </summary>
        public virtual void MuteBackgroundMusic()
        {
            if (_backgroundMusic != null)
            {
                _backgroundMusic.mute = true;
            }
        }

        /// <summary>
        /// On enable we start listening for events
        /// </summary>
        protected virtual void OnEnable()
        {
            this.MMEventStartListening<MMSfxEvent>();
            this.MMEventStartListening<PauseEvent>();
            LoadSoundSettings();
            _loopingSounds = new List<AudioSource>();
        }

        /// <summary>
        /// On disable we stop listening for events
        /// </summary>
		protected virtual void OnDisable()
        {
            if (_enabled)
            {
                this.MMEventStopListening<MMSfxEvent>();
                this.MMEventStopListening<PauseEvent>();
            }
        }

        public void OnMMEvent(PauseEvent eventType)
        {
            if (eventType.gamePaused)
            {
                if (MuteSfxOnPause)
                {
                    MuteAllSfx();
                }
            }
            if (MuteSfxOnPause)
            {
                UnmuteAllSfx();
            }
            
        }
    }
}