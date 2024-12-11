using UnityEngine;

namespace Junyoung
{
    public interface IEnemyState
    {
        public void Handle(EnemyCtrl enemy_ctrl);
    }
}

