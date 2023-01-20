using UnityEngine;

namespace MyCookie.Dice
{
    public class Dice
    {
        GameObject _dice;
        Transform[] _faces;
        Rigidbody _rigidbody;
        string _value;
        bool _isRolling;

        public Dice(GameObject dice, Rigidbody rigidbody)
        {
            _dice = dice;
            _rigidbody = rigidbody;
            _faces = dice.GetComponentsInChildren<Transform>();
        }

        public void Roll(float power = 500f)
        {
            _rigidbody.AddForce(Vector3.up * power, ForceMode.Acceleration);
            _rigidbody.AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * power, ForceMode.Acceleration);
        }
        public bool IsRolling()
        {
            if (_rigidbody.velocity == Vector3.zero && _rigidbody.angularVelocity == Vector3.zero)
            {
                _isRolling = false;
            }
            else
            {
                _isRolling = true;
            }
            return _isRolling;
        }
        public string GetValue()
        {
            float _standard = _dice.transform.position.y;

            for(int i = 0; i < _faces.Length; i++)
            {
                if(_faces[i].transform.position.y > _standard)
                {
                    _standard = _faces[i].transform.position.y;
                    _value = _faces[i].name;
                }
            }
            return _value;
        }
    }
}
