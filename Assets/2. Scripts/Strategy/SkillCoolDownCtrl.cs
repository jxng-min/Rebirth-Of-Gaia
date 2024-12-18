using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SkillCoolDownCtrl : MonoBehaviour
{

    public bool m_is_ready { get; private set; } = true;


    public IEnumerator CoolDownCoroutine(float cool_down_time)
    {
        m_is_ready = false;
        yield return new WaitForSeconds(cool_down_time);
        m_is_ready= true;
    }

}

