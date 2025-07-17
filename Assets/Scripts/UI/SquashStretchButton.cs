using Game.Core;
using Game.SoundManagement;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(SquashStretchEffect), typeof(Button))]
    public class SquashStretchButton : MonoBehaviour
    {
        [SerializeField]private SoundData pressSound;
        private Button button;
        private SquashStretchEffect squashEffect;

        public void AddListener(UnityAction call)
        {
            if(squashEffect == null)
            {
                squashEffect = GetComponent<SquashStretchEffect>();
            }
            squashEffect.OnEndEffect.AddListener(call);
        }

        public void RemoveListener(UnityAction call)
        {
            try
            {
                squashEffect.OnEndEffect.RemoveListener(call);
            }
            catch(Exception exception)
            {
                Debug.LogError("Error while removing listener from OnEndEffect: " + exception.Message);
            }
        }
        private void PlayClickSound()
        {
            if(ServiceLocator.Global.TryGetService<SoundManager>(out SoundManager manager) 
             && pressSound != null && pressSound.clip != null)
            {
                manager.Play(pressSound, Vector3.zero, true);
            }
            else
            {
                Debug.LogError("Error while playing click sound!");
            }
            
        }
        private void Awake() {
            button = GetComponent<Button>();
            squashEffect = GetComponent<SquashStretchEffect>();
        }
        // Start is called before the first frame update
        void Start()
        {
            button.onClick.AddListener(squashEffect.PlayEffect);
            button.onClick.AddListener(PlayClickSound);
        }
    }
}

