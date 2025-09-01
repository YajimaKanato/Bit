using System;
using UnityEngine;

public class BitManager : MonoBehaviour
{
    [SerializeField] int _range = 7;

    int[,] _bitSquare;
    int _bit;

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
        int rand;
        for (int i = 0; i < _range; i++)
        {
            for (int j = 0; j < _range; j++)
            {
                rand = UnityEngine.Random.Range(0, 2);
                _bitSquare[i, j] = rand;
            }
        }

        for (int i = 0; i < _range; i++)
        {
            _bit |= _bitSquare[_range / 2, i] << _range - i - 1;
        }
    }

    public void ANDOperation()
    {

    }

    public void OROperation()
    {

    }

    public void XOROperation()
    {

    }

    public void NOTOperation()
    {

    }

    public void LeftShift()
    {

    }

    public void RightShift()
    {

    }
}
