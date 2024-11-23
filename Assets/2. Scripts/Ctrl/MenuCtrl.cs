using UnityEngine;

public class MenuCtrl : MonoBehaviour
{
    public GameObject m_function_button;
 
    public void ActiveFunctionMenu()
    {
        bool is_active = m_function_button.activeSelf;
 
        m_function_button.SetActive(!is_active);
    }
}