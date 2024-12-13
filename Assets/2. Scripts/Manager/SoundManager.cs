using System.IO;
using UnityEngine;

namespace Jongmin
{
    public class SoundManager : Singleton<SoundManager>
    {
        private AudioSource m_bgm_source;
        private AudioSource m_effect_source;

        [Header("Background Sounds")]
        [SerializeField]
        private AudioClip[] m_bgm_clips;

        [SerializeField]
        private string[] m_bgm_clip_names;

        [Header("Effect Sounds")]
        [SerializeField]
        private AudioClip[] m_effect_clips;

        [SerializeField]
        private string[] m_effect_clip_names;

        public float BgmVolume
        {
            get => m_bgm_source.volume;
            set => m_bgm_source.volume = Mathf.Clamp01(value);
        }
        public float EffectVolume
        {
            get => m_effect_source.volume;
            set => m_effect_source.volume = Mathf.Clamp01(value);
        }

        private void Start()
        {
            m_bgm_source = gameObject.AddComponent<AudioSource>();
            m_effect_source = gameObject.AddComponent<AudioSource>();

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
                m_effect_source.volume = player_data.m_effect_volume;
            }
            else
            {
                Debug.Log("PlayerData.json이 없습니다. 기본 설정으로 오디오 크기를 설정합니다.");
                m_bgm_source.volume = 0.5f;
                m_effect_source.volume = 0.5f;
            }
        }

        // 오디오 소스 초기화, 오디오 클립 초기화 메소드
        private void Initialize()
        {
            for(int i = 0; i < m_bgm_clips.Length; i++)
            {
                m_bgm_clips[i] = Resources.Load<AudioClip>($"Sounds/bgm/{m_bgm_clip_names[i]}");
            }

            for(int i = 0; i < m_effect_clips.Length; i++)
            {
                m_effect_clips[i] = Resources.Load<AudioClip>($"Sounds/effect/{m_effect_clip_names[i]}");
            }

            m_bgm_source.loop = true;
            m_effect_source.loop = false;
        }

        // 사운드 이펙트 재생 메소드
        public void PlayEffect(string effect_name)
        {
            switch(effect_name)
            {
            case "":
                m_effect_source.PlayOneShot(m_effect_clips[0]);
                break;
            
            default:
                Debug.Log("매개변수로 전달한 사운드 이펙트 파일이 없습니다.");
                break;
            }
        }

        // 백그라운드 사운드 재생 메소드
        public void PlayeBGM(string bgm_name)
        {
            switch(bgm_name)
            {
            case "Title":
                m_bgm_source.PlayOneShot(m_bgm_clips[0]);
                break;
            
            default:
                Debug.Log("매개변수로 전달한 백그라운드 사운드 파일이 없습니다.");
                break;
            }
        }

        public void StopBGM()
        {
            m_bgm_source.Stop();
        }
    }
}

