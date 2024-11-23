using Junyoung;
using System;
using UnityEngine;

namespace Jongmin
{
    public class PlayerSelector : MonoBehaviour
    {
        public GameObject[] m_char_prefabs;
        public GameObject player;

        public void InstantiatePlayer()
        {
            player = Instantiate(m_char_prefabs[Convert.ToInt32(GameManager.Instance.CharacterType)]);
            Debug.Log($"캐릭터 코드가 {Convert.ToInt32(GameManager.Instance.CharacterType)}이므로 {GameManager.Instance.CharacterType}을 생성합니다.");
        }
    }
}