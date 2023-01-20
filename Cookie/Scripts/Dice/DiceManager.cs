using UnityEngine;

namespace MyCookie.Dice
{
    public class DiceManager : MonoBehaviour
    {
        [SerializeField] GameObject[] _gameObjects;
        int _diceCount;
        Dice[] dice;
        // Start is called before the first frame update
        void Start()
        {
            _diceCount = _gameObjects.Length;
            dice = new Dice[_diceCount];
            for (int i = 0; i < _diceCount; i++)
            { 
                dice[i] = new Dice(_gameObjects[i], _gameObjects[i].GetComponent<Rigidbody>());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                for (int i = 0; i < _diceCount; i++)
                {
                    dice[i].Roll();
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                for (int i = 0; i < _diceCount; i++)
                {
                    Debug.Log(dice[i].GetValue());
                }
            }
        }
    }
}
