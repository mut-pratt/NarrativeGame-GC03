using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class Interactable : MonoBehaviour
{
    public YarnProject Project;
    public CinemachineCamera Camera;

    //[YarnNode("Project")]
    [YarnNode(nameof(Project))]
    public string Node;

    public void Start()
    {
        DialogueRunner runner = Dialogue.Instance.Runner;
        runner.onDialogueComplete.AddListener(OnDialogueComplete);
    }

    void OnDialogueComplete()
    {
        Debug.Log($"dialogue completed for {name}!!!");
        Camera.gameObject.SetActive(false);
    }
    
    public void StartInteraction()
    {
        Debug.Log("started interaction");
        
        //Dialogue.Instance.Runner.StartDialogue(Node);
        Dialogue.Instance.StartNode(Node);

        // turn camera on (camera only cuts if its not active)
        Camera.gameObject.SetActive(false);
        Camera.gameObject.SetActive(true);
    }
}
