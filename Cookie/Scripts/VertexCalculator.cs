using System;
using UnityEngine;

namespace Cookie
{
    public enum ShapeType
    {
        Square
    };
    [Serializable]
    public struct VertexRange
    {
        public Vector2 X;
        public Vector2 Y;
        public Vector2 Z;
    }
    public class VertexCalculator : MonoBehaviour
    {
        public ShapeType ShapeType;
        public VertexRange VertexRange { get => _vertexRange; }
        public Transform Transform { get => _transform; }
        [SerializeField] bool _debug;
        [HideInInspector] public Vector3[] Vertexs { get => _vertexs; }
        

        Transform _transform;
        Vector3[] _vertexs;
        VertexRange _vertexRange;
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            UpdateVertexs();
        }

        public void UpdateVertexs()
        {
            switch (ShapeType)
            {
                case ShapeType.Square:
                    _vertexs = new Vector3[4];
                    _vertexs[0] = new Vector3(-_transform.localScale.x * 5 + _transform.position.x, _transform.position.y, _transform.localScale.z * 5 + _transform.position.z);
                    _vertexs[1] = new Vector3(_transform.localScale.x * 5 + _transform.position.x, _transform.position.y, _transform.localScale.z * 5 + _transform.position.z);
                    _vertexs[2] = new Vector3(-_transform.localScale.x * 5 + _transform.position.x, _transform.position.y, -_transform.localScale.z * 5 + _transform.position.z);
                    _vertexs[3] = new Vector3(_transform.localScale.x * 5 + _transform.position.x, _transform.position.y, -_transform.localScale.z * 5 + _transform.position.z);
                    break;
            }
            UpdateVertexsRange();
            if (_debug)
                VertexsLog(_vertexs);
        }
        private void VertexsLog(Vector3[] vertexs)
        {
            for (int i = 0; i < vertexs.Length; i++)
            {
                Debug.Log(_vertexs[i]);
            }
        }
        private void UpdateVertexsRange()
        {
            _vertexRange.X = new Vector2(_vertexs[0].x, _vertexs[1].x);
            _vertexRange.Y = new Vector2(_vertexs[0].y, _vertexs[0].y);
            _vertexRange.Z = new Vector2(_vertexs[3].z, _vertexs[0].z);
        }
    }
}