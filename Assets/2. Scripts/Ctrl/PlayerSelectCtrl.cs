using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jongmin
{
    public class PlayerSelectCtrl : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] m_character_select_panels = new GameObject[3];

        private int m_index = 0;

        private void Start()
        {
            m_character_select_panels[m_index].SetActive(true);
        }

        public void LeftButtonSelect()
        {
            if(m_index == 0)
            {
                m_index = 2;
            }
            else
            {
                m_index--;
            }

            for(int i = 0; i < 3; i++)
            {
                if(i == m_index)
                {
                    m_character_select_panels[i].SetActive(true);
                }
                else
                {
                    m_character_select_panels[i].SetActive(false);
                }
            }
        }

        public void RightButtonSelect()
        {
            if(m_index == 2)
            {
                m_index = 0;
            }
            else
            {
                m_index++;
            }

            for(int i = 0; i < 3; i++)
            {
                if(i == m_index)
                {
                    m_character_select_panels[i].SetActive(true);
                }
                else
                {
                    m_character_select_panels[i].SetActive(false);
                }
            }            
        }

        public void SociaStartButtonPressed()
        {
            GameManager.Instance.CharacterType = Character.SOCIA;
            SceneCtrl.ReplaceScene("Game");
        }

        public void GovStartButtonPressed()
        {
            GameManager.Instance.CharacterType = Character.GOV;
            SceneCtrl.ReplaceScene("Game");
        }

        public void EnvaStartButtonPressed()
        {
            GameManager.Instance.CharacterType = Character.ENVA;
            SceneCtrl.ReplaceScene("Game");
        }
    }
}
