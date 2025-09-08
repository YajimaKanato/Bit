using UnityEngine;

public class StateButton : MonoBehaviour
{
    [SerializeField] GameObject _selectEffect;

    bool _selected;
    private void Start()
    {
        _selectEffect.SetActive(false);
    }

    public void SelectEffect()
    {
        _selected = !_selected;
        _selectEffect.SetActive(_selected);
    }
}
