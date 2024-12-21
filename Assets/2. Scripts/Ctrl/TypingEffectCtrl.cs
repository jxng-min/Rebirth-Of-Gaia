using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Jongmin
{
    public class TypingEffectCtrl : MonoBehaviour
    {
        private TalkUIManager m_talk_manager;

        [SerializeField]
        private int m_cps;

        private string m_target_text;
        [SerializeField]
        private Text m_current_text;

        private int m_current_idx;
        private float m_interval;
        private bool m_state;

        public UnityAction<bool> EndCursor = (isEnd) => { };
        
        private void Start()
        {
            m_state = false;
            m_talk_manager = GetComponent<TalkUIManager>();
            m_current_text = GetComponent<Text>();
            
            m_cps = 25;
        }

        // 대화 UI 창에 출력하고자 하는 텍스트를 설정하는 메소드
        public void SetText(string text)
        {
            m_target_text = text;

            TypingEffectStart();
        }
        public void SetTextHard(string text)
        {
            m_current_text.text = text;
        }

        // 텍스트 출력을 시작하는 메소드
        private void TypingEffectStart()
        {
            m_state = true;
            EndCursor(false);

            m_current_text.text = "";
            m_current_idx = 0;

            m_interval = 1.0f / m_cps;
            Invoke("TypingEffecting", m_interval);
        }

        // 텍스트 출력을 하는 동안 호출되는 메소드
        private void TypingEffecting()
        {
            if(m_current_text.text == m_target_text)
            {
                TypingEffectEnd();
                return;
            }

            m_current_text.text += m_target_text[m_current_idx++];

            Invoke("TypingEffecting", m_interval);
        }

        // 텍스트 출력이 마무리되면 호출되는 메소드
        private void TypingEffectEnd()
        {
            m_state = false;
            EndCursor(true);
        }

        public string CurrentText()
        {
            return m_current_text.text;
        }
        public bool CurrentState()
        {
            return m_state;
        }
    }
}

