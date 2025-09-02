using UnityEngine;
using UnityEngine.UI;

public class BitManager : MonoBehaviour
{
    [SerializeField] GameObject[] _bitText;
    [SerializeField] GameObject _multiplierText;
    [SerializeField] int _range = 7;

    Text[,] _bitTexts;
    Text[] _multiplierTexts;

    int[,] _bitSquare;
    int[] _bit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        _bitSquare = new int[_range, _range];
        _bit = new int[_range];
        _bitTexts = new Text[_range, _range];
        _multiplierTexts = new Text[_range];

        int rand;
        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range; j++)
            {
                rand = Random.Range(0, 2);
                _bitSquare[i, j] = rand;

                _bitTexts[i, j] = _bitText[i].transform.GetChild(j).GetComponent<Text>();
            }
        }

        for (int i = 0; i < _range; i++)
        {
            _multiplierTexts[i] = _multiplierText.transform.GetChild(i).GetComponent<Text>();
        }

        BitUpdate();
    }

    /// <summary>
    /// 演算後の更新を行う関数
    /// </summary>
    void BitUpdate()
    {
        for (int i = 0; i < _range; i++)
        {
            _bit[i] = _bitSquare[_range / 2, i];
            _multiplierTexts[i].text = _bitSquare[_range / 2, i].ToString();
        }

        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range; j++)
            {
                _bitTexts[i, j].text = _bitSquare[i, j].ToString();
            }
        }
    }

    /// <summary>
    /// AND演算を行う関数
    /// </summary>
    /// <param name="index">指定した列のインデックス</param>
    public void ANDOperation(int index)
    {
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[index, i] * _bit[i] == 1)
            {
                _bitSquare[index, i] = 1;
            }
            else
            {
                _bitSquare[index, i] = 0;
            }
        }

        BitUpdate();
    }

    /// <summary>
    /// OR演算を行う関数
    /// </summary>
    /// <param name="index">指定した列のインデックス</param>
    public void OROperation(int index)
    {
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[index, i] == 1 || _bit[i] == 1)
            {
                _bitSquare[index, i] = 1;
            }
            else
            {
                _bitSquare[index, i] = 0;
            }
        }

        BitUpdate();
    }

    /// <summary>
    /// XOR演算を行う関数
    /// </summary>
    /// <param name="index">指定した列のインデックス</param>
    public void XOROperation(int index)
    {
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[index, i] == _bit[i])
            {
                _bitSquare[index, i] = 0;
            }
            else
            {
                _bitSquare[index, i] = 1;
            }
        }

        BitUpdate();
    }

    /// <summary>
    /// NOT演算を行う関数
    /// </summary>
    /// <param name="index">指定した列のインデックス</param>
    public void NOTOperation(int index)
    {
        for (int i = 0; i < _range; i++)
        {
            if (_bitSquare[index, i] == 1)
            {
                _bitSquare[index, i] = 0;
            }
            else
            {
                _bitSquare[index, i] = 1;
            }
        }

        BitUpdate();
    }

    /// <summary>
    /// 左ビットシフトを行う関数
    /// </summary>
    /// <param name="index">指定した行のインデックス</param>
    public void LeftShift(int index)
    {
        for (int i = 0; i < _range - 1; i++)
        {
            _bitSquare[index, i] = _bitSquare[index, i + 1];
        }
        _bitSquare[index, _bitSquare.GetLength(0) - 1] = 0;

        BitUpdate();
    }

    /// <summary>
    /// 右ビットシフトを行う関数
    /// </summary>
    /// <param name="index">指定した行のインデックス</param>
    public void RightShift(int index)
    {
        for (int i = 1; i < _range; i++)
        {
            _bitSquare[index, _range - i] = _bitSquare[index, _range - i - 1];
        }
        _bitSquare[index, 0] = 0;

        BitUpdate();
    }
}
