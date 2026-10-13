using UnityEngine;

public class Collectable : MonoBehaviour
{
    void Update()
    {
        
    }

    public void Collect()
    {
        Dialogue.Instance.OnCollect();
        Destroy(gameObject);
    }
}
