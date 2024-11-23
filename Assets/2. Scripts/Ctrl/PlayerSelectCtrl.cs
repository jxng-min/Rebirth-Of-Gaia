using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jongmin
{
    public class PlayerSelectCtrl : MonoBehaviour
    {
        public Button[] m_player_select_buttons;
        private Button m_current_button;
        public Button m_play_button;

        private void Start()
        {
            foreach (var button in m_player_select_buttons)
            {
                button.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                button.GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f, 0.5f);
            }
        }

        private void Update()
        {
            if(m_current_button == null)
            {
                m_play_button.interactable = false;
                m_play_button.GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                m_play_button.interactable = true;
                m_play_button.GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f, 1.0f);
            }
        }

        // 선택된 버튼을 강조하는 메소드
        private void HighlightButton(Button selected_button)
        {
            if(m_current_button != null)
            {
                m_current_button.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                m_current_button.GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f, 0.5f);
            }

            m_current_button = selected_button;
            m_current_button.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            m_current_button.GetComponentInChildren<TMP_Text>().color = new Color(0f, 0f, 0f, 1.0f);
        }

        // 선택된 버튼에 따라 캐릭터 타입을 선택하는 메소드
        private void CharacterSelect(Button selected_button)
        {
            switch(selected_button.name)
            {
            case "Socia Select Button":
                GameManager.Instance.CharacterType = Character.SOCIA;
                Debug.Log("캐릭터를 Socia로 선택합니다.");
            break;

            case "Gov Select Button":
                GameManager.Instance.CharacterType = Character.GOV;
                Debug.Log("캐릭터를 Gov로 선택합니다.");
            break;

            case "Enva Select Button":
                GameManager.Instance.CharacterType = Character.ENVA;
                Debug.Log("캐릭터를 Enva로 선택합니다.");
            break;    
            }
        }

        // 인터페이스 역할을 하는 메소드
        public void OnButtonClick(Button selected_button)
        {
            HighlightButton(selected_button);
            CharacterSelect(selected_button);
        }
    }
}
