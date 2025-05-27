using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Objects Data SO", menuName = "SO/Objects Data SO")]
    public class ObjectsDataSO : ScriptableObject
    {
        public List<ObjectData> objectsData;        
    }
}
