using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Random = System.Random;

public class AbilityItem : MonoBehaviour
{
    [SerializeField] private bool _isCustomIndex = false;
    [SerializeField] private int _index = 0;

    [Inject] protected AudioManager _audioManager;
    private void Awake()
    {
        if (!_isCustomIndex)
        {
            Random rnd = new Random();
            _index = rnd.Next(AbilityDictionary.abilityDictionary.Count);
        }

        if (_index > AbilityDictionary.abilityDictionary.Count)
        {
            _index = AbilityDictionary.abilityDictionary.Count;
        }

    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out AbilityHolder abilityHolder))
        {

            AbilityDictionary.abilityDictionary.TryGetValue(_index, out var ability);
            abilityHolder.setAbility(ability);
            _audioManager.Play("AbilityOnPick");
            Destroy(gameObject);
        }
    }
}
