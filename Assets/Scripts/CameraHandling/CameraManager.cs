using System.Collections.Generic;
using UnityEngine;
using Game.Core;
using Unity.Cinemachine;

namespace Game.CameraHandling
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera initialCamera;
        private List<CinemachineCamera> cameras;
        
        public void MakeCameraImportant(CinemachineCamera camera)
        {
            for(int i = 0; i < cameras.Count; i++)
            {
                var current = cameras[i];
                if(current.GetHashCode() == camera.GetHashCode())
                {
                    Debug.Log("Making camera (" + camera.transform.name + ") important");
                    current.Priority.Value = 20;
                }
                else
                {
                    current.Priority.Value = 0;
                }
            }
        }

        public void MakeInitialCameraImportant()
        {
            MakeCameraImportant(initialCamera);
        }

        public void RegisterCamera(CinemachineCamera camera)
        {
            if(camera != null && !cameras.Contains(camera))
            {
                cameras.Add(camera);
            }
        }
        private void Awake()
        {
            cameras = new List<CinemachineCamera>();
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            var cameraInstances = GameObject.FindObjectsByType(typeof(CinemachineCamera), FindObjectsSortMode.InstanceID);

            for(int i = 0; i < cameraInstances.Length; i++)
            {
                var camera = cameraInstances[i] as CinemachineCamera;
                if(cameras.IndexOf(camera) == -1)
                {
                    cameras.Add(camera);
                }
            }

            
            if(initialCamera != null)
            {
                MakeInitialCameraImportant();
            }

            ServiceLocator.ForSceneOf(this).Register<CameraManager>(this);
        }
    }
}
