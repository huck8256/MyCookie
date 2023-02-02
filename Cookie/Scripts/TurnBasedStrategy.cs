using UniRx;
using UnityEngine.Events;

namespace Cookie
{
    //TBS(턴제 게임)
    public class TurnBasedStrategy
    {
        public readonly UnityEvent StartEvent;
        public readonly UnityEvent SkipEvent;

        public readonly int HeadCount;
        public IReadOnlyReactiveProperty<int> CurrentPlayer => _currentPlayer;
        public IReadOnlyReactiveProperty<int> TurnCount => _turnCount;

        IntReactiveProperty _currentPlayer = new IntReactiveProperty();
        IntReactiveProperty _turnCount = new IntReactiveProperty();

        public TurnBasedStrategy(int headCount)
        {
            HeadCount = headCount;
            StartEvent = new UnityEvent();
            SkipEvent = new UnityEvent();
        }
        public void Start(int startPlayer = 0)
        {
            _currentPlayer.Value = startPlayer;
            _turnCount.Value++;
            StartEvent.Invoke();
        }
        public void Skip()
        {
            if (_currentPlayer.Value >= HeadCount - 1)
            {
                _currentPlayer.Value = 0;
                _turnCount.Value++;
            }
            else
            {
                _currentPlayer.Value++;
            }
            SkipEvent.Invoke();
        }
    }
}
