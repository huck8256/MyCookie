using System;
using System.Collections;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cookie
{
    public class DiceModel
    {
        public IObservable<string> Value => _value;
        public IReadOnlyReactiveProperty<bool> IsRolling => _isRolling;

        Transform _transform;
        Rigidbody _rigidbody;

        Transform[] _faces;

        Subject<string> _value;
        BoolReactiveProperty _isRolling;

        public DiceModel(Transform transform, Rigidbody rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;

            try
            {
                _faces = transform.GetComponentsInChildren<Transform>();
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("faces was not set in the Dice!");
            }

            _value = new Subject<string>();
            _isRolling = new BoolReactiveProperty();
        }

        public void Roll(float power = 500f)
        {
            _isRolling.Value = true;

            _rigidbody.AddForce(Vector3.up * power, ForceMode.Acceleration);
            _rigidbody.AddTorque(new Vector3(
                Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * power,
                ForceMode.Acceleration);

            Observable.FromCoroutine(RollingCouroutine)
                .Subscribe(_ => _value.OnNext(GetValue()));
        }
        void RollingCheck()
        {
            if (_rigidbody.velocity == Vector3.zero
                && _rigidbody.angularVelocity == Vector3.zero)
                _isRolling.Value = false;
        }
        string GetValue()
        {
            int _resultFace = 0;
            float _tempResult = _transform.position.y;

            for (int i = 0; i < _faces.Length; i++)
            {
                if (_faces[i].transform.position.y > _tempResult)
                {
                    _tempResult = _faces[i].transform.position.y;
                    _resultFace = i;
                }
            }

            return _faces[_resultFace].name;
        }
        IEnumerator RollingCouroutine()
        {
            yield return new WaitForFixedUpdate();

            while (_isRolling.Value)
            {
                RollingCheck();
                yield return null;
            }
        }
    }
}