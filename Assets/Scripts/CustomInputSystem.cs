using UnityEngine;

public class CustomInputSystem : MonoBehaviour
{
    public static Vector3 GetInput()
    {
        Vector3 movement = new Vector3();
#if UNITY_EDITOR
        if (Input.GetAxis("Horizontal") != 0){
            movement.x = (Input.GetAxis("Horizontal") >0? 1 : -1) * PlayerController.RunSpeed ;
        }
        if (Input.GetAxis("Vertical") != 0){
            movement.z = (Input.GetAxis("Vertical") >0? 1 : -1) * PlayerController.RunSpeed;
        }

        movement = Vector3.ClampMagnitude(movement, PlayerController.RunSpeed);
        return movement;
#endif

#if UNITY_ANDROID
        return movement;
#endif
    }
}
