using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public InputActionReference Move;
    public bool IsTankControls;
    public Camera Camera;

    // Update is called once per frame
    void Update()
    {
        if (Dialogue.Instance.Runner.IsDialogueRunning)
        {
            return;
        }

        float delta = Time.deltaTime;
        Vector2 move = Move.action.ReadValue<Vector2>();

        if (IsTankControls)
        {
            transform.position += transform.forward * Speed * move.y * delta;

            // rotate around the given axis, however much is wanted
            transform.Rotate(Vector3.up, RotationSpeed * move.x * delta);
        } else { 
            Vector3 forward = Vector3.ProjectOnPlane(Camera.transform.forward, Vector3.up);
            Vector3 right = Vector3.ProjectOnPlane(Camera.transform.right, Vector3.up);

            // how we got there https://miro.com/app/board/uXjVHvkubSk=/?moveToWidget=3458764687775622928&cot=14
            // transform.position += move.y * Speed * delta * forward;
            // transform.position += move.x * Speed * delta * right;
            // transform.position += (move.y * forward + move.x * right) * Speed * delta;

            Vector3 moveDir = move.y * forward + move.x * right;
            
            transform.position += moveDir * Speed * delta;
            transform.forward = Vector3.RotateTowards(transform.forward, moveDir, Mathf.Deg2Rad * RotationSpeed * delta, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable)
        {
            interactable.StartInteraction();
        }

        Collectable collectable = other.GetComponent<Collectable>();
        if (collectable)
        {
            collectable.Collect();
        }
    }
}
