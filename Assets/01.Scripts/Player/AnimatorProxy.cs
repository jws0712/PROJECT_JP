using UnityEngine;

public class AnimatorProxy : MonoBehaviour
{
    [SerializeField] private Player player;

    public void EnableCombo()
    {
        player.EnableCombo();
    }

    public void UnEnableCombo()
    {
        player.UnEnableCombo();
    }
}
