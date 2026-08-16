using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void PressMenuButton()
    {
        GameManager.Instance.PressMenuButton();
    }

    public void RestartGame()
    {
        PressMenuButton();
    }
}
