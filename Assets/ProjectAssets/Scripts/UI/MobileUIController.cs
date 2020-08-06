using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    [SerializeField] LocalPlayerHandler handler;
    private void Start()
    {
        if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            gameObject.SetActive(false);
            return;
        }
        else
            handler.Player.ControlledByAxis = false;
    }
    public void MoveRight() { handler.Player.SetAxis(Vector2.right); }
    public void MoveLeft() { handler.Player.SetAxis(Vector2.left); }
    public void MoveUp() { handler.Player.SetAxis(Vector2.up); }
    public void MoveDown() { handler.Player.SetAxis(Vector2.down); }
    public void StopMoving() { handler.Player.SetAxis(Vector2.zero); }
    public void Jump() { handler.Player.Jump(); }
    public void ChangeColor() { handler.Player.SendColorChange(); }
}
