using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Jongmin
{
    public class SoundManager : Singleton<SoundManager>
    {
        private AudioSource m_bgm_source;
        private List<AudioSource> m_effect_sources = new List<AudioSource>();
        private AudioSource m_repeat_effect_source;

        [Header("Background Sounds")]
        [SerializeField]
        private AudioClip[] m_bgm_clips;

        [Header("Effect Sounds")]
        [SerializeField]
        private AudioClip[] m_effect_clips;

        [Header("Repeatly Effect Sounds")]
        [SerializeField]
        private AudioClip[] m_repeat_effect_clips;

        public float BgmVolume
        {
            get => m_bgm_source.volume;
            set => m_bgm_source.volume = Mathf.Clamp01(value);
        }

        public float EffectVolume { get; set; } = 0.5f;

        private void Start()
        {
            m_bgm_source = gameObject.AddComponent<AudioSource>();
            for(int i = 0; i < 10; i++)
            {
                m_effect_sources.Add(gameObject.AddComponent<AudioSource>());
            }
            m_repeat_effect_source = gameObject.AddComponent<AudioSource>();

            LoadVolume();
            Initialize();
        }

        private void LoadVolume()
        {
            string file_path = Application.persistentDataPath + "/PlayerData.json";

            if(File.Exists(file_path))
            {
                string data = File.ReadAllText(file_path);

                PlayerData player_data = JsonUtility.FromJson<PlayerData>(data);

                m_bgm_source.volume = player_data.m_bgm_volume;
                SetEffectVolume(player_data.m_effect_volume);
            }
            else
            {
                Debug.Log("PlayerData.json이 없습니다. 기본 설정으로 오디오 크기를 설정합니다.");
                m_bgm_source.volume = 0.5f;
                SetEffectVolume(0.5f);
            }
        }

        public void SetEffectVolume(float volume)
        {
            EffectVolume = volume;

            foreach (var source in m_effect_sources)
            {
                source.volume = volume;
            }
        }

        // 오디오 소스 초기화, 오디오 클립 초기화 메소드
        private void Initialize()
        {
            for(int i = 0; i < m_bgm_clips.Length; i++)
            {
                m_bgm_clips[i] = Resources.Load<AudioClip>($"Sounds/bgm/{m_bgm_clips[i].name}");
            }

            for(int i = 0; i < m_effect_clips.Length; i++)
            {
                m_effect_clips[i] = Resources.Load<AudioClip>($"Sounds/effect/{m_effect_clips[i].name}");
            }

            for(int i = 0; i < m_repeat_effect_clips.Length; i++)
            {
                m_repeat_effect_clips[i] = Resources.Load<AudioClip>($"Sounds/effect/{m_repeat_effect_clips[i].name}");
            }
            
            m_bgm_source.loop = true;
        }

        private AudioSource GetPooledAudioSource()
        {
            foreach (var source in m_effect_sources)
            {
                if(!source.isPlaying)
                {
                    return source;
                }
            }

            AudioSource new_source = gameObject.AddComponent<AudioSource>();
            m_effect_sources.Add(new_source);

            return new_source;
        }

        // 사운드 이펙트 재생 메소드
        public void PlayEffect(string effect_name)
        {
            for(int i = 0; i < m_effect_clips.Length; i++)
            {
                if(m_effect_clips[i].name == effect_name)
                {
                    AudioSource source = GetPooledAudioSource();
                    source.clip = m_effect_clips[i];
                    source.Play();
                    break;
                }
            }
        }

        public void PlayEffect(string effect_name, bool loop)
        {
            m_repeat_effect_source.Stop();
            for(int i = 0; i < m_repeat_effect_clips.Length; i++)
            {
                if(m_repeat_effect_clips[i].name == effect_name)
                {
                    m_repeat_effect_source.clip = m_repeat_effect_clips[i];
                    m_repeat_effect_source.Play();
                }
            }            
        }

        // 백그라운드 사운드 재생 메소드
        public void PlayBGM(string bgm_name)
        {
            for(int i = 0; i < m_bgm_clips.Length; i++)
            {
                if(m_bgm_clips[i].name == bgm_name)
                {
                    m_bgm_source.clip = m_bgm_clips[i];
                    m_bgm_source.Play();
                }
            }
        }
        
        public void StopBGM()
        {
            m_bgm_source.Stop();
        }

        public void StopEffect()
        {
            foreach (var source in m_effect_sources)
            {
                source.Stop();
            }
        }

        public void StopEffect(bool loop)
        {
            if(m_repeat_effect_source.isPlaying)
            {
                m_repeat_effect_source.Stop();
            }
        }
    }
}

