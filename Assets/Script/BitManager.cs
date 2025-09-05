using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitManager : MonoBehaviour
{
    [SerializeField] Text _braveText;
    [SerializeField] Text _influenceText;
    [SerializeField] int _bitRange = 5;
    [SerializeField] Text[] _stateTexts;

    int _braveBit;
    int _influenceBit;
    bool[] _notFlag;
    bool[] _orFlag;

    int Poison = 1 << 0;
    int Paralysis = 1 << 1;
    int Sleep = 1 << 2;
    int Burning = 1 << 3;
    int Confusion = 1 << 4;


    private void Start()
    {
        SetUp();
    }

    void SetUp()
    {
        _braveBit = 0;
        _influenceBit = 0;

        TextUpdate();
    }

    void TextUpdate()
    {
        for (int i = 0; i < _bitRange; i++)
        {
            _braveText.text = Convert.ToString(_braveBit, 2);
            _influenceText.text = Convert.ToString(_influenceBit, 2);
        }
    }


    void ANDOperation(ref int ans)
    {


        TextUpdate();
    }


    void OROperation(ref int bit, ref int multiplier)
    {


        TextUpdate();
    }


    void XOROperation(ref int bit, ref int multiplier)
    {


        TextUpdate();
    }

    /// <summary>
    /// 状態異常のフラグを反転させる関数
    /// </summary>
    /// <param name="state">反転させたい状態異常のフラグがあるインデックス</param>
    public void NOTOperation(int state)
    {
        _notFlag[state] = !_notFlag[state];
    }


    void LeftShift(ref int bit)
    {


        TextUpdate();
    }


    void RightShift(ref int bit)
    {


        TextUpdate();
    }

    public void BraveOperation(int index)
    {
        switch (index)
        {
            case 0:
                //ANDOperation(ref _braveBit, ref _influenceBit);
                break;
            case 1:
                OROperation(ref _braveBit, ref _influenceBit);
                break;
            case 2:
                XOROperation(ref _braveBit, ref _influenceBit);
                break;
            case 3:
                //NOTOperation(ref _braveBit);
                break;
            case 4:
                LeftShift(ref _braveBit);
                break;
            case 5:
                RightShift(ref _braveBit);
                break;
            default:
                break;
        }
    }

    public void MultiplierOperation(int index)
    {
        switch (index)
        {
            case 0:
                ANDOperation(ref _influenceBit);
                break;
            case 1:
                //OROperation(ref _influenceBit);
                break;
            case 2:
                //XOROperation(ref _influenceBit);
                break;
            case 3:
                //NOTOperation(ref _influenceBit);
                break;
            case 4:
                LeftShift(ref _influenceBit);
                break;
            case 5:
                RightShift(ref _influenceBit);
                break;
            default:
                break;
        }
    }

    /*
    [SerializeField] GameObject[] _bitTextGroup;
    [SerializeField] GameObject _multiplierTextGroup;
    [SerializeField] GameObject _operationButtonGroup;
    [SerializeField] int _range = 7;

    Text[,] _bitTexts;
    Text[] _multiplierTexts;
    Button[] _operationButtons;

    int[,] _bitSquare;
    int[] _bitTarget;
    public int[] BitTarget { get { return _bitTarget; } }
    int _rowIndex;
    int _columnIndex;
    bool _rightCross;
    bool _leftCross;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
    }

    /// <summary>
    /// 初期設定を行う関数
    /// </summary>
    void SetUp()
    {
        _bitSquare = new int[_range, _range];
        _bitTarget = new int[_range];
        _bitTexts = new Text[_range, _range];
        _multiplierTexts = new Text[_range];
        _operationButtons = new Button[_range];
        _rowIndex = -1;
        _columnIndex = -1;

        int rand;
        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range; j++)
            {
                rand = UnityEngine.Random.Range(0, 2);
                _bitSquare[i, j] = rand;

                _bitTexts[i, j] = _bitTextGroup[i].transform.GetChild(j).GetComponent<Text>();
            }
        }

        for (int i = 0; i < _range; i++)
        {
            _multiplierTexts[i] = _multiplierTextGroup.transform.GetChild(i).GetComponent<Text>();
            _operationButtons[i] = _operationButtonGroup.transform.GetChild(i).GetComponent<Button>();
        }

        BitUpdate();
    }

    /// <summary>
    /// 演算結果の反映を行う関数
    /// </summary>
    void BitUpdate()
    {
        for (int i = 0; i < _range; i++)
        {
            _bitTarget[i] = _bitSquare[_range / 2, i];
            _multiplierTexts[i].text = _bitSquare[_range / 2, i].ToString();
        }

        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range; j++)
            {
                _bitTexts[i, j].text = _bitSquare[i, j].ToString();
            }
        }

        _rowIndex = -1;

        ButtonStateUpdate();
    }

    //ビット演算は右または下を基準に計算する

    /// <summary>
    /// AND演算を行う関数
    /// </summary>
    public void ANDOperation()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        //行が選択されていたら
        if (_rowIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[_rowIndex, i] * _bitTarget[i] == 1)
                {
                    _bitSquare[_rowIndex, i] = 1;
                }
                else
                {
                    _bitSquare[_rowIndex, i] = 0;
                }
            }
        }

        //列が選択されていたら
        if (_columnIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[i, _columnIndex] * _bitTarget[i] == 1)
                {
                    _bitSquare[i, _columnIndex] = 1;
                }
                else
                {
                    _bitSquare[i, _columnIndex] = 0;
                }
            }
        }

        BitUpdate();
        OneCheck();
    }

    /// <summary>
    /// OR演算を行う関数
    /// </summary>
    public void OROperation()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        if (_rowIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[_rowIndex, i] == 1 || _bitTarget[i] == 1)
                {
                    _bitSquare[_rowIndex, i] = 1;
                }
                else
                {
                    _bitSquare[_rowIndex, i] = 0;
                }
            }
        }

        if (_columnIndex != -1)
        {

            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[i, _columnIndex] == 1 || _bitTarget[i] == 1)
                {
                    _bitSquare[i, _columnIndex] = 1;
                }
                else
                {
                    _bitSquare[i, _columnIndex] = 0;
                }
            }
        }

        BitUpdate();
        OneCheck();
    }

    /// <summary>
    /// XOR演算を行う関数
    /// </summary>
    public void XOROperation()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        if (_rowIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[_rowIndex, i] == _bitTarget[i])
                {
                    _bitSquare[_rowIndex, i] = 0;
                }
                else
                {
                    _bitSquare[_rowIndex, i] = 1;
                }
            }
        }

        if (_columnIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[i, _columnIndex] == _bitTarget[i])
                {
                    _bitSquare[i, _columnIndex] = 0;
                }
                else
                {
                    _bitSquare[i, _columnIndex] = 1;
                }
            }
        }

        BitUpdate();
        OneCheck();
    }

    /// <summary>
    /// NOT演算を行う関数
    /// </summary>
    public void NOTOperation()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        if (_rowIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[_rowIndex, i] == 1)
                {
                    _bitSquare[_rowIndex, i] = 0;
                }
                else
                {
                    _bitSquare[_rowIndex, i] = 1;
                }
            }
        }

        if (_columnIndex != -1)
        {
            for (int i = 0; i < _range; i++)
            {
                if (_bitSquare[i, _columnIndex] == 1)
                {
                    _bitSquare[i, _columnIndex] = 0;
                }
                else
                {
                    _bitSquare[i, _columnIndex] = 1;
                }
            }
        }

        BitUpdate();
        OneCheck();
    }

    /// <summary>
    /// 左ビットシフトを行う関数
    /// </summary>
    public void LeftorUpShift()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        if (_rowIndex != -1)
        {
            for (int i = 0; i < _range - 1; i++)
            {
                _bitSquare[_rowIndex, i] = _bitSquare[_rowIndex, i + 1];
            }
            _bitSquare[_rowIndex, _range - 1] = 0;
        }

        if (_columnIndex != -1)
        {
            for (int i = 0; i < _range - 1; i++)
            {
                _bitSquare[i, _columnIndex] = _bitSquare[i + 1, _columnIndex];
            }
            _bitSquare[_range - 1, _columnIndex] = 0;
        }

        BitUpdate();
        OneCheck();
    }

    /// <summary>
    /// 右ビットシフトを行う関数
    /// </summary>
    public void RightorDownShift()
    {
        if (_rowIndex == -1 && _columnIndex == -1) return;

        if (_rowIndex != -1)
        {
            for (int i = 1; i < _range; i++)
            {
                _bitSquare[_rowIndex, _range - i] = _bitSquare[_rowIndex, _range - 1 - i];
            }
            _bitSquare[_rowIndex, 0] = 0;
        }

        if (_columnIndex != -1)
        {
            for (int i = 1; i < _range; i++)
            {
                _bitSquare[_range - i, _columnIndex] = _bitSquare[_range - 1 - i, _columnIndex];
            }
            _bitSquare[0, _columnIndex] = 0;
        }

        BitUpdate();
        OneCheck();
    }

    public void SelectedRowIndex(int index)
    {
        _rowIndex = index;
        _columnIndex = -1;
        Debug.Log("Row : " + _rowIndex);
    }

    public void SelectedColumnIndex(int index)
    {
        _columnIndex = index;
        _rowIndex = -1;
        Debug.Log("Column : " + _columnIndex);
    }

    /// <summary>
    /// 斜めに１がそろっているかを調べる関数
    /// </summary>
    void OneCheck()
    {
        _rightCross = true;
        _leftCross = true;

        //右肩下がりを調べる
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[i, i] != 1)
            {
                _rightCross = false;
                break;
            }
        }

        //右肩上がりを調べる
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[i, _range - 1 - i] != 1)
            {
                _leftCross = false;
                break;
            }
        }

        for (int i = 0; i < _range; i++)
        {
            if (_rightCross)
            {
                _bitSquare[i, i] = 0;
                _bitTexts[i, i].text = _bitSquare[i, i].ToString();

            }

            if (_leftCross)
            {
                _bitSquare[i, _range - 1 - i] = 0;
                _bitTexts[i, _range - 1 - i].text = _bitSquare[i, _range - 1 - i].ToString();
            }
        }
    }

    /// <summary>
    /// ビットの値によってボタンの使用可能状態を切り替える関数
    /// </summary>
    void ButtonStateUpdate()
    {
        for (int i = 0; i < _range; i++)
        {
            if (_bitTarget[i] == 1)
            {
                _operationButtons[i].interactable = true;
            }
            else
            {
                _operationButtons[i].interactable = false;
            }
        }
    }*/
}
