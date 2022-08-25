using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static float runSpeed = 0.1f;
    public static float RunSpeed { get { return runSpeed; } private set { runSpeed = RunSpeed; } }

    private void FixedUpdate()
    {
       transform.Translate(CustomInputSystem.GetInput());
    }
}
