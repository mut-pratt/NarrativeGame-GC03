using UnityEngine;
using UnityEngine.InputSystem;

public class Movement0930 : MonoBehaviour
{
    public InputActionReference Move;
    public InputActionReference Run;

    public float Speed;
    public float Speed_Run;

    Light _Light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        var delta = Time.deltaTime;

        if (Run.action.IsPressed())
        {
            Color.RGBToHSV(_Light.color, out var h, out var s, out var v);
            _Light.color = Color.HSVToRGB(h + Speed_Run * delta, s, v);
        }

        var move = Move.action.ReadValue<Vector2>();
        var rotation = transform.rotation.eulerAngles;
        rotation.y += move.x * Speed * delta;
        rotation.x += move.y * Speed * delta;
        transform.rotation = Quaternion.Euler(rotation);


    }
}
