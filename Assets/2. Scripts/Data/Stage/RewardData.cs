using System;
using UnityEngine;
namespace Taekyung
{
    [System.Serializable]
    public class RewardData
    {
        public int m_exp;
    }
    [System.Serializable]
    public class RewardDataWrapper
    {
        public RewardData[] RewardData;
    }
}
