using System;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Missile : MonoBehaviour
    {
        /// <summary>
        /// The target we are trying to reach.
        /// </summary>
        [SerializeField] private Transform _target;

        private GameObject _clone;

        /// <summary>
        /// How much the missile can turn, in degrees, per second.
        /// </summary>
        [SerializeField] private float _rotationPerSecond;
        /// <summary>
        /// How fast the missile can move per second.
        /// </summary>
        [SerializeField] private float _velocityPerSecond;

        /// <summary>
        /// Amplitude of the random X position calculated in <see cref="SetRandomPosition"/>.
        /// </summary>
        [SerializeField, Range(0, 50)] private float _xAmplitude = 50f;

        [SerializeField] private float angle;

        private void OnEnable()
        {
            SetRandomPosition();

            SetRandomDirection();
        }

        private void Update()
        {
            UpdateDirection();

            UpdatePosition();


        }

        /// <summary>
        /// Sets a random X position for this object. Ranging from -<see cref="_xAmplitude"/> and <see cref="_xAmplitude"/>.
        /// </summary>
        private void SetRandomPosition()
        {
            float x = Random.Range(-_xAmplitude, _xAmplitude);

            Vector3 pos = transform.position;
            pos.x = x;
            transform.position = pos;
        }

        /// <summary>
        /// Sets a random direction between -45° and 45°.
        /// </summary>
        private void SetRandomDirection()
        {
            float angle = Random.Range(-45f, 45f);
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        /// <summary>
        /// Turns the missile in the direction of the target based on <see cref="_rotationPerSecond"/>.
        /// </summary>
        private void UpdateDirection()
        {
            var dx = _target.position.x - transform.position.x;
            var dy = _target.position.y - transform.position.y;

            _rotationPerSecond = -(float)Math.Atan(dy / dx) * (180 / (float)Math.PI);
            _rotationPerSecond = (transform.position.x > _target.position.x && _velocityPerSecond < 0) ? _rotationPerSecond -= 180 : (transform.position.x < _target.position.x && _velocityPerSecond > 0) ? _rotationPerSecond += 180 : _rotationPerSecond;
            transform.rotation = Quaternion.Euler(0, 0, 90 - _rotationPerSecond);

            if (transform.position == _target.position)
            {
                DestroyTargetAndShowAnother();
            }


        }

        /// <summary>
        /// Moves the missile forward based on <see cref="_velocityPerSecond"/>.
        /// </summary>
        private void UpdatePosition()
        {

            float step = _velocityPerSecond * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, step);

        }

        /// <summary>
        /// Destroy object when missile collide with then.
        /// </summary>
        private void DestroyTargetAndShowAnother()
        {

            _clone = (GameObject)Instantiate(_target.gameObject, _target.position, Quaternion.identity);
            Destroy(_target.gameObject);

            _target = (Transform)Instantiate(_clone.transform, _clone.transform.position, Quaternion.identity);
            Destroy(_clone.gameObject);

        }
    }
}
