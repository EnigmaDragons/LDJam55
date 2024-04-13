using UnityEngine;
using Input = UnityEngine.Input;

public static class InteractButton
{
    private const KeyCode InteractKey = KeyCode.F;

    public static bool IsDown()
    {
        return Input.GetKeyDown(InteractKey);
    }
}
