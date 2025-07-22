using Game.ObjectPoolHandling;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace Game.GameplayHandling
{
    public class CropManager : MonoBehaviour
    {
        [SerializeField] private CropPoolData[] poolsData;
        [SerializeField] private Grid grid;

        private Dictionary<Vector3Int ,CropPlacementData> placements;
        private Dictionary<CropType, ObjectPool<Crop>> cropPools;

        public void SpawnCrop(CropType cropType, Vector3 position)
        {
            Vector3Int cellPosition = grid.WorldToCell(position);
            
            Vector3 precisePosition = grid.GetCellCenterWorld(cellPosition);
            if(Contains(cellPosition))
            {
                return;
            }

            var pool = cropPools[cropType];
            var crop = pool.Get();
            crop.CropPool = pool;
            crop.transform.position = precisePosition;
            AddCropData(crop.Type, cellPosition);
        }

        public void DespawnCrop(Crop crop)
        {
            Vector3Int cellPosition = grid.WorldToCell(crop.transform.position);
            if (!Contains(cellPosition))
            {
                return;
            }
            var pool = crop.CropPool;
            pool.ReturnToPool(crop);
            RemoveCropData(cellPosition);
        }

        private void AddCropData(CropType type, Vector3Int position)
        {
            placements.Add(position, new CropPlacementData(type, position));
        }

        private void RemoveCropData(Vector3Int position)
        {
            placements.Remove(position);
        }
        
        private bool Contains(Vector3Int position)
        {
            return placements.ContainsKey(position);
        }

        public Crop CreateCropInstance(Crop prefab)
        {
            var instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.transform.parent = transform;
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void OnGetCrop(Crop crop)
        {
            crop.gameObject.SetActive(true);
        }

        private void OnCropReturnToPool(Crop crop)
        {
            crop.gameObject.SetActive(false);
        }

        private void OnCropDestroy(Crop crop)
        {
            Destroy(crop.gameObject);
        }

        private void Awake()
        {
            placements = new Dictionary<Vector3Int, CropPlacementData>();
            cropPools = new Dictionary<CropType, ObjectPool<Crop>>();
        }

        private void Start()
        {
            foreach (var pool in poolsData)
            {
                CropType type = pool.prefab.Type;
                ObjectPool<Crop> instance = new ObjectPool<Crop>(() => CreateCropInstance(pool.prefab), 
                                                                       OnGetCrop, 
                                                                       OnCropReturnToPool, 
                                                                       OnCropDestroy, 
                                                                       pool.count);

                cropPools.Add(type, instance);
            }
        }

    }
}
