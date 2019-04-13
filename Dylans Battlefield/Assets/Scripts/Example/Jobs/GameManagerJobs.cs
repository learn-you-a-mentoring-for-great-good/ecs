using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs
{
    public class GameManagerJobs : MonoBehaviour
    {
        #region GAME_MANAGER_STUFF
        public static GameManagerJobs GM;

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

        TransformAccessArray _transforms;
        MovementJobs _moveJob;
        JobHandle _moveHandle;

        private void OnDisable()
        {
            _moveHandle.Complete();
            _transforms.Dispose();
        }

        void Start()
        {
            _ui = GetComponent<UI>();
            _transforms = new TransformAccessArray(0, -1);

            AddShips(enemyShipCount);
        }

        void Update()
        {
            _moveHandle.Complete();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddShips(enemyShipIncremement);
            }

            _moveJob = new MovementJobs()
            {
                moveSpeed = enemySpeed,
                topBound = topBound,
                bottomBound = bottomBound,
                deltaTime = Time.deltaTime
            };

            _moveHandle = _moveJob.Schedule(_transforms);

            JobHandle.ScheduleBatchedJobs();
        }

        void AddShips(int amount)
        {
            _moveHandle.Complete();

            _transforms.capacity = _transforms.length + amount;

            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 10f);

                Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
                Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

                var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;

                _transforms.Add(obj.transform);
            }

            _count += amount;
            _ui.SetElementCount(_count);
        }
    }
}