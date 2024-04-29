using UnityEngine;

public class ColliderTransition : MonoBehaviour
{
    // Reference to the target BoxCollider
    private BoxCollider targetCollider;

    // Start is called before the first frame update
    void Start()
    {
        BoxCollider[] allColliders = GetComponents<BoxCollider>();

        if (allColliders.Length > 1) // 确保有超过一个BoxCollider
        {
            // 选择第二个BoxCollider，索引为1
            targetCollider = allColliders[1];
        }
    }

    // Method to enable the previously disabled BoxCollider
    public void EnableTargetCollider()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = true; // 启用BoxCollider
        }
    }

    public void UnenableTargetCollider()
    {
        if (targetCollider != null)
        {
            targetCollider.enabled = false; // 启用BoxCollider
        }
    }
}
