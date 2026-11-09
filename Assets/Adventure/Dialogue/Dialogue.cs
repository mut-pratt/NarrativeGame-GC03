using UnityEngine;
using Yarn.Unity;

public class Dialogue : MonoBehaviour
{
    const string k_CollectableVariableName = "$collectables";

    public static Dialogue Instance;

    public DialogueRunner Runner;
    
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

    public void OnCollect()
    {
        // increment the $collectables variable by 1
        VariableStorageBehaviour variableStorage = Runner.VariableStorage;

        // usually Try___ returns a bool, and sets and "out" parameter
        // you would expect
        // float currentCount = variableStorage.GetValue("$collectables")
        bool hasValue = variableStorage.TryGetValue(k_CollectableVariableName, out float collectables);

        collectables += 1;

        variableStorage.SetValue(k_CollectableVariableName, collectables);
    }

    public void StartNode(string node)
    {
        Runner.StartDialogue(node);
    }
}
