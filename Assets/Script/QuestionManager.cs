using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] Text _questionText;
    BitManager _bitManager;

    int[] _questionBit;
    int _answerCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bitManager = GetComponent<BitManager>();
        _questionBit = new int[_bitManager.BitRange];
        MakeQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeQuestion()
    {
        for(int i = 0; i < _bitManager.BitRange; i++)
        {
            _questionBit[i] = UnityEngine.Random.Range(0, 2);
        }

        _questionText.text = String.Join(' ', _questionBit);

        _answerCount = 0;
    }

    public void CheckBraveBit()
    {
        bool match = true;
        for (int i = 0; i < _bitManager.BitRange; i++)
        {
            if (_questionBit[i] != _bitManager.BraveBit[i])
            {
                match = false;
                break;
            }
        }
        _answerCount++;

        if (match)
        {
            Debug.Log($"Correct : {_answerCount}");
            MakeQuestion();
        }
    }
}
