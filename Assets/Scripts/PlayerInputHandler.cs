using UnityEngine;
using Fusion;

public class PlayerInputHandler : MonoBehaviour
{
    InputData m_Data;

    private void Update() // Check events like KeyDown or KeyUp in Unity's update. They might be missed otherwise
    {
        m_Data.Actions ^= Input.GetKeyDown( KeyCode.R ) ? InputAction.RESPAWN : 0; // xor the actions to flip the bit
        m_Data.Actions ^= Input.GetKeyDown( KeyCode.Space ) ? InputAction.BOOST : 0;
    }

    public void OnInput( NetworkRunner runner, NetworkInput inputContainer )
    {
        m_Data.States |= Input.GetKey( KeyCode.W ) ? InputState.FORWARD : 0;
        m_Data.States |= Input.GetKey( KeyCode.A ) ? InputState.LEFT : 0;
        m_Data.States |= Input.GetKey( KeyCode.S ) ? InputState.BACKWARD : 0;
        m_Data.States |= Input.GetKey( KeyCode.D ) ? InputState.RIGHT : 0;


        inputContainer.Set( m_Data );

        m_Data.States = 0;
    }
}
