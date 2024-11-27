using UnityEngine;

[CreateAssetMenu(fileName = "Joystick Value", menuName = "Scriptable Object/Joystick Value", order = int.MaxValue)]
public class JoyStickValue : ScriptableObject
{
    public Vector2 m_joy_touch;
}