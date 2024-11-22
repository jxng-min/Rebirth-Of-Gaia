using Jongmin;
using Junyoung;
using System;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject[] m_char_prefabs;
    public GameObject player;

    void Start()
    {
        player = Instantiate(m_char_prefabs[Convert.ToInt32(GameManager.Instance.CharacterType)]);
    }
}
