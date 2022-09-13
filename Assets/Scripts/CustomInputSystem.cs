using UnityEngine;

[RequireComponent(typeof (FixedJoystick))]
public class CustomInputSystem : MonoBehaviour
{
    [SerializeField] private static FixedJoystick _joystick;
    private void Awake() {
        _joystick = GetComponent<FixedJoystick>();
    }
    public static Vector3 GetInput()
    {
        return new Vector3 (_joystick.Horizontal * PlayerController.RunSpeed, 0, _joystick.Vertical * PlayerController.RunSpeed);
    }
}
