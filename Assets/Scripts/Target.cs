using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class Target : MonoBehaviour
    {
        /// <summary>
        /// Amplitude of the random X position calculated in <see cref="SetRandomPosition"/>.
        /// </summary>
        [SerializeField, Range(0, 50)] private float _xAmplitude = 0;

        [SerializeField, Range(0, 25)] private float _yAmplitude = 0;

        private void OnEnable()
        {
            SetRandomPosition();
        }

        private void Update()
        {

        }
        
        /// <summary>
        /// Sets a random X position for this object. Ranging from -<see cref="_xAmplitude"/> and <see cref="_xAmplitude"/>.
        /// </summary>
        private void SetRandomPosition()
        {
            float x = Random.Range(-_xAmplitude, _xAmplitude);
            float y = Random.Range(-_yAmplitude, _yAmplitude);

            Vector3 pos = transform.position;
            pos.x = x;
            pos.y = y;
            transform.position = pos;
        }
    }
}