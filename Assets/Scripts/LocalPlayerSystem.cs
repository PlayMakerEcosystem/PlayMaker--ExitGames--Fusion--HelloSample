using UnityEngine;
using Fusion;

public class LocalPlayerSystem : MonoBehaviour
{
    public Player Player { get; private set; }
    public PlayerCamera PlayerCamera;
    public PlayerInputHandler InputHandler;
    public void AssignOwner( Player player, NetworkRunner runner )
    {
        Player = player;
        PlayerCamera.CameraTarget = player.GetComponentInChildren<CameraTarget>();

        var events = runner.GetComponent<NetworkEvents>();
        events.OnInput.AddListener( InputHandler.OnInput );
    }
}
