using UnityEngine;

namespace Old
{
    public class MovementOld : MonoBehaviour
    {
        void Update()
        {
            Vector3 pos = transform.position;
            pos += transform.forward * GameManagerOld.GM.enemySpeed * Time.deltaTime;

            if (pos.z < GameManagerOld.GM.bottomBound)
            {
                pos.z = GameManagerOld.GM.topBound;
            }

            transform.position = pos;
        }
    }
}