using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PreviewSystem : MonoBehaviour
    {
        [SerializeField] private ObjectsDataSO database;
        [SerializeField] private Material transparentMaterialPrefab;
        [SerializeField] private Color placeableColor;
        [SerializeField] private Color unplaceableColor;

        private Material transparentMaterialInstance;
        private Dictionary<int,Indicator> indicators;


        public void SetPreview(Vector3 worldPosition,
                                int id,
                                bool placeableStatus)
        {
            foreach(var data in indicators)
            {
                int currentID = data.Key;
                if(currentID == id)
                {
                    indicators[currentID].gameObject.SetActive(true);
                    indicators[currentID].transform.position = worldPosition;
                    indicators[currentID].SetColor(placeableStatus ? 
                                                   placeableColor : 
                                                   unplaceableColor);
                }
                else
                {
                    indicators[currentID].gameObject.SetActive(false);
                }
            }
        }


        private void Awake()
        {
            indicators = new();
        }

        // Start is called before the first frame update
        void Start()
        {
            if(indicators.Count > 0)
            {
                return;
            }

            transparentMaterialInstance = new Material(transparentMaterialPrefab);

            foreach(var data in database.objectsData)
            {
                GameObject placeVisual = Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
                Indicator indicator = placeVisual.AddComponent<Indicator>();
                indicator.FindMeshes();
                indicator.SetMaterial(transparentMaterialInstance);
                indicators[data.ID] = indicator;
                placeVisual.transform.parent = transform;
                placeVisual.SetActive(false);
            }
        }

        private void OnEnable()
        {
            foreach (var data in indicators)
            {
                int currentID = data.Key;
                indicators[currentID].gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            foreach (var data in indicators)
            {
                int currentID = data.Key;
                indicators[currentID].gameObject.SetActive(false);
            }
        }
    }
}
