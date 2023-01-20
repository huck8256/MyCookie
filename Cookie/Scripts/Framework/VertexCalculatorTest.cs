using UnityEngine;

namespace Cookie.Framework
{
    public enum VertexPosition
    {
        LeftTop,
        RightTop,
        LeftBottom,
        RightBottom
    };
    [RequireComponent(typeof(VertexCalculator))]
    public class VertexCalculatorTest : MonoBehaviour
    {
        public VertexPosition VertexPosition;
        [SerializeField] Transform _targetTransform;
        VertexCalculator _vertexCalculator;


        private void Start()
        {
            _vertexCalculator = GetComponent<VertexCalculator>();
        }
        void Update()
        {
            switch (VertexPosition)
            {
                case VertexPosition.LeftTop:
                    if (_vertexCalculator.ShapeType == ShapeType.Square)
                        _targetTransform.position = _vertexCalculator.Vertexs[0];
                    break;
                case VertexPosition.RightTop:
                    if (_vertexCalculator.ShapeType == ShapeType.Square)
                        _targetTransform.position = _vertexCalculator.Vertexs[1];
                    break;
                case VertexPosition.LeftBottom:
                    if (_vertexCalculator.ShapeType == ShapeType.Square)
                        _targetTransform.position = _vertexCalculator.Vertexs[2];
                    break;
                case VertexPosition.RightBottom:
                    if (_vertexCalculator.ShapeType == ShapeType.Square)
                        _targetTransform.position = _vertexCalculator.Vertexs[3];
                    break;
            }
        }
    }
}