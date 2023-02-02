using UnityEngine.AI;
using UnityEngine;
using UniRx;

namespace Cookie
{
    public class NavMeshMovement
    {
        public IReadOnlyReactiveProperty<int> CurrentArea => _currentArea;

        Transform _transform;
        NavMeshAgent _navMeshAgent;
        VertexCalculator[] _area;
        IntReactiveProperty _currentArea;

        Vector3 destination;
        BoolReactiveProperty _isMove;

        public NavMeshMovement(Transform transform, NavMeshAgent navMeshAgent, VertexCalculator[] vertexCalculator, bool autoMove = true)
        {
            _transform = transform;
            _navMeshAgent = navMeshAgent;
            _area = vertexCalculator;

            _currentArea = new IntReactiveProperty();
            _isMove = new BoolReactiveProperty();

            destination = _navMeshAgent.destination;

            SetCurrentArea();

            _isMove
               .Where(_ => autoMove)
               .Where(x => !x)
               .Subscribe(_ => RandomMove())
               .AddTo(transform);

            Observable.EveryUpdate()
                .Where(_ => autoMove)
                .Subscribe(_ => IsMoveCheck())
                .AddTo(transform);
        }
        public void AreaMove()
        {
            if (_currentArea.Value >= _area.Length - 1)
                _currentArea.Value = 0;
            else
                _currentArea.Value++;

            Vector3 targetPos = _area[_currentArea.Value].Transform.position;

            destination = targetPos;
            _navMeshAgent.destination = destination;
            _isMove.Value = true;
        }
        void SetCurrentArea()
        {
            Vector3 _myPos = _transform.position;

            for (int i = 0; i < _area.Length; i++)
            {
                if (_myPos.x >= _area[i].VertexRange.X.x
                    && _myPos.x <= _area[i].VertexRange.X.y
                    && _myPos.z >= _area[i].VertexRange.Z.x
                    && _myPos.z <= _area[i].VertexRange.Z.y)
                    _currentArea.Value = i;
            }
        }
        void RandomMove()
        {
            Vector3 targetPos = GetRandomPosition(_area[_currentArea.Value]);

            destination = targetPos;
            _navMeshAgent.destination = destination;
            _isMove.Value = true;
        }
      
        private void IsMoveCheck()
        {
            if (Vector3.Distance(destination, _transform.position) <= _navMeshAgent.radius * 2)
                _isMove.Value = false;
            else
                _isMove.Value = true;
        }
        private Vector3 GetRandomPosition(VertexCalculator vertexCalculator)
        {
            Vector3 _randomPosition;

            float _x = Random.Range(vertexCalculator.VertexRange.X.x, vertexCalculator.VertexRange.X.y);
            float _y = Random.Range(vertexCalculator.VertexRange.Y.x, vertexCalculator.VertexRange.Y.y);
            float _z = Random.Range(vertexCalculator.VertexRange.Z.x, vertexCalculator.VertexRange.Z.y);

            _randomPosition = new Vector3(_x, _y, _z);
            return _randomPosition;
        }


    }
}
