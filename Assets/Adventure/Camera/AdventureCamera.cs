using UnityEngine;

public class AdventureCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    const string k_CollectableVariableName = "$collectables";

    public static AdventureCamera Instance;

    public Camera Camera;

    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}