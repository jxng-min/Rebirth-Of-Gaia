using Junyoung;
using UnityEngine;

public class StageSelectCtrl : MonoBehaviour
{
    private GameObject m_stage_manager;

    private void Start()
    {
        m_stage_manager = GameObject.Find("StageManager");
    }
    public void StageButtonDown(int stage_index)
    {
        Debug.Log("Selected Stage: " + stage_index);
        m_stage_manager.GetComponent<StageManager>().LoadStage(stage_index);
    }

}
