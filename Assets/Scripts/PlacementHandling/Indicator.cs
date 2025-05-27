using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer[] meshes;
        
        public void SetMaterial(Material material)
        {
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.sharedMaterial = material;
            }
        }

        public void SetColor(Color color)
        {
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.sharedMaterial.color = color;
            }
        }

        public void FindMeshes()
        {
            meshes = GetComponentsInChildren<MeshRenderer>();
        }


    }
}
