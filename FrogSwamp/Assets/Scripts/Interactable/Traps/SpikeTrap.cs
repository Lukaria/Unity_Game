using UnityEngine;
using Zenject;

public class SpikeTrap : Interactable {

    private Animator spikeTrapAnim;

    [Inject] protected AudioManager _audioManager;

    void Awake()
    {
        spikeTrapAnim = GetComponent<Animator>();
        SetActivatedByPlayer(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        spikeTrapAnim.SetTrigger("open");
        _audioManager.Play("SpikeTrap");
        
    }

    private void OnTriggerExit(Collider other)
    {
        spikeTrapAnim.SetTrigger("close");
    }



}