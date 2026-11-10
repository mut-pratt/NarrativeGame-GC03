using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public InputActionReference Move;
    public bool IsTankControls;
    public Camera Camera;
    public State CurrentState;

    /// this function does something nice
    public enum State
    {
        Idle,
        Moving,
        Talking,
    }

    void Update()
    {
        Vector2 move = Move.action.ReadValue<Vector2>();

        if (CurrentState == State.Idle)
        {
            if (Dialogue.Instance.Runner.IsDialogueRunning)
            {
                CurrentState = State.Talking;
            }

            if (move.magnitude > 0)
            {
                DoMove(move);
                CurrentState = State.Moving;
            }
        }
        else if (CurrentState == State.Moving)
        {
            DoMove(move);
            
            if (Dialogue.Instance.Runner.IsDialogueRunning)
            {
                CurrentState = State.Talking;
            }
            else if (move.magnitude == 0)
            {
                CurrentState = State.Idle;
            }
        }
        else if (CurrentState == State.Talking)
        {
            if (!Dialogue.Instance.Runner.IsDialogueRunning)
            {
                CurrentState = State.Idle;
            }
        }
    }
        

    void DoMove(Vector3 move) {
        float delta = Time.deltaTime;

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

            var position = transform.position;
            var forwards = transform.forward;
            var hits = 0;
            for (int i = 0; i < 100000; i ++)
            {
                if (Physics.Raycast(position + i * 0.001f * Vector3.up, forwards, 10f))
                {
                    hits++;
                }
            }

            Debug.Log("hits: " + hits + " at current position " + transform.position);
            Debug.Log($"hits: {hits} at current position {transform.position}", this);

            if (hits > 10000)
            {
                transform.position += Vector3.forward;
            }
            

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
