using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class LocalSpawnTest : NetworkBehaviour
{
    public NetworkObject Prefab;

    bool m_Spawn = false;

    private void Update()
    {
        m_Spawn |= Input.GetKeyDown( KeyCode.P );
    }

    public override void FixedUpdateNetwork()
    {
        if( m_Spawn )
        {
            m_Spawn = false;
            Object.Runner.Spawn( Prefab, transform.position, transform.rotation );
        }
    }
}
