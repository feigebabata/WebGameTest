#define FIXED_UPDATE

using System;
using System.Collections;
using System.Collections.Generic;
using FGUFW;
using UnityEngine;

namespace FGUFW.MonoGameplay
{
    public abstract class Play<T>:Part where T:Play<T>
    {
        public IOrderedMessenger<Enum> Messenger;

        [SerializeField]
        private PlayFrameData _frameData;
        public PlayFrameData FrameData=>_frameData;
        private float _playCreatedTime;

        public override IEnumerator OnCreating(Part play,Part parent)
        {
            Messenger = new OrderedMessenger<Enum>();

            yield return base.OnCreating(this,this);
            Debug.Log($"{this.GetType().Name} Create End.");

            yield return OnPreload();
            Debug.Log($"{this.GetType().Name} Preload End.");
#if FIXED_UPDATE
                _playCreatedTime = Time.fixedTime;
#else
                _playCreatedTime = Time.time;
#endif
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            Messenger = null;

            Debug.Log($"{this.GetType().Name} Destroy End.");
        }

#if FIXED_UPDATE
        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        void FixedUpdate()
        {
            _frameData.DeltaTime = Time.fixedDeltaTime;
            _frameData.WorldTime = Time.fixedTime - _playCreatedTime;
            OnUpdate();
        }
#else
        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            _frameData.DeltaTime = Time.deltaTime;
            _frameData.WorldTime = Time.time - _playCreatedTime;
            OnUpdate();
        }
#endif

        private void OnUpdate()
        {
            _frameData.Index++;
            OnUpdate(in _frameData);
        }

    }

}
