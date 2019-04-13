using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

using UnityEngine;

using float3 = Unity.Mathematics.float3;

namespace ECS
{
    public class GameManagerECS : MonoBehaviour
    {
        #region GAME_MANAGER_STUFF
        public static GameManagerECS GM;

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

        EntityManager _manager;

        void Start()
        {
            _ui = GetComponent<UI>();
            _manager = World.Active.EntityManager;
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
            NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
            _manager.Instantiate(enemyShipPrefab, entities);

            for (int i = 0; i < amount; i++)
            {
                float xVal = Random.Range(leftBound, rightBound);
                float zVal = Random.Range(0f, 10f);

                _manager.SetComponentData(entities[i], new Translation { Value = new float3(xVal, 0f, zVal) });
                _manager.SetComponentData(entities[i], new Rotation { Value = new Quaternion(0, 1, 0, 0) });
                _manager.SetComponentData(entities[i], new MoveSpeed { Value = enemySpeed });
            }
            entities.Dispose();

            _count += amount;
            _ui.SetElementCount(_count);
        }
    }
}