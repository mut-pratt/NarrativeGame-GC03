using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class Interactable : MonoBehaviour
{
    public YarnProject Project;

    //[YarnNode("Project")]
    [YarnNode(nameof(Project))]
    public string Node;
    
    public void StartInteraction()
    {
        Debug.Log("started interaction");
        
        //Dialogue.Instance.Runner.StartDialogue(Node);
        Dialogue.Instance.StartNode(Node);
    }
}
