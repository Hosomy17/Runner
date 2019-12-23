using UnityEngine;

public class RecyclerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Coin") || collider.CompareTag("Bomb"))
            ObjectPool.Instance.ReturnObject(collider.gameObject);
    }
}
