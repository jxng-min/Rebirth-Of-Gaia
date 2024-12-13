using UnityEngine;

public class PersadeCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_persades = new GameObject[10];

    public void UpdatePersade(int index)
    {
        // 원래 사용해야 할 로직
        // for(int i = 0; i < m_persades.Length; i++)
        // {
        //     m_persades[i].SetActive(false);

        //     if(i == index)
        //     {
        //         m_persades[i].SetActive(true);
        //     }
        // }

        if(index % 2 == 0)
        {
            m_persades[0].SetActive(true);
            m_persades[1].SetActive(false);
        }
        else
        {
            m_persades[0].SetActive(false);
            m_persades[1].SetActive(true);
        }
    }
}
