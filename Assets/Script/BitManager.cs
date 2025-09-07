using System;
using UnityEngine;
using UnityEngine.UI;

public class BitManager : MonoBehaviour
{
    [System.Serializable]
    class StateClass
    {
        string _stateName;
        public string StateName { get { return _stateName; } }
        int[] _bit;
        public int[] Bit { get { return _bit; } }

        /// <summary>
        /// ビット表示を初期化するコンストラクタ
        /// </summary>
        /// <param name="range"></param>
        public StateClass(int range, int index, string name)
        {
            _bit = new int[range];
            _bit[index] = 1;
            Array.Reverse(_bit);
            _stateName = name;
        }

        /// <summary>
        /// NOT演算を行う関数
        /// </summary>
        public void NOTOperation()
        {
            for (int i = 0; i < _bit.Length; i++)
            {
                _bit[i] = 1 - _bit[i];
            }
        }
    }

    public enum StateName
    {
        Poison,
        Paralysis,
        Sleep,
        Burning,
        Confusion
    }

    [SerializeField] int _bitRange = 5;
    [SerializeField] Text[] _stateNameTexts;
    [SerializeField] Text[] _stateBitTexts;
    [SerializeField] Text _braveBitText;

    StateClass[] _stateClass;

    int[] _braveBit;
    bool[] _orFlag;

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// 初期化関数
    /// </summary>
    void SetUp()
    {
        _braveBit = new int[_bitRange];
        _orFlag = new bool[_bitRange];
        _stateClass = new StateClass[_bitRange];
        foreach (var state in Enum.GetValues(typeof(StateName)))
        {
            _stateClass[(int)state] = new StateClass(_bitRange, (int)state, ((StateName)state).ToString());
        }

        TextUpdate();
    }

    /// <summary>
    /// テキストを更新する関数
    /// </summary>
    void TextUpdate()
    {
        for (int i = 0; i < _bitRange; i++)
        {
            _stateBitTexts[i].text = String.Join(' ', _stateClass[i].Bit);
            _stateNameTexts[i].text = _stateClass[i].StateName;
            _braveBitText.text = String.Join(' ', _braveBit);
        }
    }

    /// <summary>
    /// 勇者のAND演算を行う関数
    /// </summary>
    public void BraveANDOperation()
    {
        //演算を行うためのビット生成
        var stateBit = new int[_bitRange];
        for (int i = 0; i < _bitRange; i++)
        {
            if (_orFlag[i])
            {
                for (int j = 0; j < _bitRange; j++)
                {
                    stateBit[j] = _stateClass[i].Bit[j] + stateBit[j] == 0 ? 0 : 1;
                }
            }
        }

        //演算実行
        for (int i = 0; i < _bitRange; i++)
        {
            _braveBit[i] = _braveBit[i] * stateBit[i] == 1 ? 1 : 0;
        }

        TextUpdate();
        Debug.Log("BraveAND");
    }

    /// <summary>
    /// 勇者のOR演算を行う関数
    /// </summary>
    public void BraveOROperation()
    {
        //演算を行うためのビット生成
        var stateBit = new int[_bitRange];
        for (int i = 0; i < _bitRange; i++)
        {
            if (_orFlag[i])
            {
                for(int j=0; j < _bitRange; j++)
                {
                    stateBit[j] = _stateClass[i].Bit[j] + stateBit[j] == 0 ? 0 : 1;
                }
            }
        }

        //演算実行
        for (int i = 0; i < _bitRange; i++)
        {
            _braveBit[i] = _braveBit[i] + stateBit[i] == 0 ? 0 : 1;
        }

        TextUpdate();
        Debug.Log("BraveOR");
    }

    /// <summary>
    /// 状態のOR演算を行う関数
    /// </summary>
    /// <param name="state">合わせたい状態のフラグがあるインデックス</param>
    public void StateOROperation(int state)
    {
        _orFlag[state] = !_orFlag[state];
        Debug.Log("StateOR");
        TextUpdate();
    }

    /// <summary>
    /// 状態のNOT演算を行う関数
    /// </summary>
    /// <param name="state">反転させたい状態のフラグがあるインデックス</param>
    public void StateNOTOperation(int state)
    {
        _stateClass[state].NOTOperation();
        _stateBitTexts[state].text = String.Join(' ', _stateClass[state].Bit);
        Debug.Log("StateNOT");
    }
}
