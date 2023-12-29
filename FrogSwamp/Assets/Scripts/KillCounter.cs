
using TMPro;
using UnityEngine;

using Zenject;
using System;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private int _counter = 10;

    private TMP_Text _textMeshPro;
    
    public event Action OnAllKilled;
    void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
        
        _textMeshPro.SetText(_counter.ToString());
    }

    public int GetCounter()
    {
        return _counter;
    }

    public void SetCounter(int value)
    {
        _counter = value;
        UpdateText();
    }

    public void EnemyKilledSignal()
    {
        --_counter;
        if (_counter == 0)
        {
            OnAllKilled?.Invoke();
        }
        if (_counter > -1)
        {
            UpdateText();    
        }
        
    }

    void UpdateText()
    {
        _textMeshPro.SetText(_counter.ToString());
    }

    public void SetText(string txt)
    {
        _textMeshPro.SetText(txt);
    }
}
