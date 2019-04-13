using UnityEngine;

namespace Old
{
    public class GameManagerOld : MonoBehaviour
    {
        #region GAME_MANAGER_STUFF
        public static GameManagerOld GM;

        [Header("Simulation Settings")]
        public float topBound = 16.5f;
        public float bottomBound = -13.5f;
        public float leftBound = -23.5f;
        public float rightBound = 23.5f;

        [Header("Enemy Settings")]
        public GameObject enemyShipPrefab;
        public float enemySpeed = 1f;

        [Header("Spawn Settings")]
        public int enemyShipCount = 1;
        public int enemyShipIncremement = 1;

        UI _ui;
        int _count;

        void Awake()
        {
            if (GM == null)
            {
                GM = this;
            }
            else if (GM != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        void Start()
        {
            _ui = GetComponent<UI>();

            AddShips(enemyShipCount);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddShips(enemyShipIncremement);
            }
        }

        void AddShips(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 10f);

                Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

                var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;
            }

            _count += amount;
            _ui.SetElementCount(_count);
        }
    }
}