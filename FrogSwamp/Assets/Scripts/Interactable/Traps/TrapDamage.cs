using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _trapDamage = 5f;
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerIHealthScript = other.GetComponent<PlayerHealth>();

        if (playerIHealthScript == null) return;
        
        playerIHealthScript.TakeDamage(_trapDamage);
    }
}
