using UnityEngine;

[RequireComponent(typeof(Animation))]
public class StoneChest : Interactable
{
    // Start is called before the first frame update
    
    [SerializeField] private AnimationClip _animationClip;
    private Animation _animation;
    private PlayerInteract playerInteractScript = null;
   

    private bool isOpened = false;
    void Awake()
    {
        _animation = gameObject.GetComponent<Animation>();
        SetActivatedByPlayer(true);
    }
    public override void Interact()
    {
        if (!isOpened)
        {
            _animation.Play(_animationClip.name);
            if(playerInteractScript != null)
                playerInteractScript.SetHintCanvas(false);
        }
        isOpened = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        playerInteractScript = other.GetComponent<PlayerInteract>();
        
        if(playerInteractScript == null) return;
        playerInteractScript.OnInteract += Interact;
        if(!isOpened) playerInteractScript.SetHintCanvas(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(playerInteractScript == null) return;

        playerInteractScript.OnInteract -= Interact;
        playerInteractScript.SetHintCanvas(false);
        
        playerInteractScript = null;
    }
}
