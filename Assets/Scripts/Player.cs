using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Player : NetworkBehaviour
{
    public GameObject LocalPlayerSystem;

    public override void Spawned()
    {
        if( Object.HasInputAuthority )
        {
            var rootObjects = Runner.SimulationUnityScene.GetRootGameObjects();
            foreach ( var go in rootObjects)
            {
                var localSystem = go.GetComponentInChildren<LocalPlayerSystem>();
                if ( localSystem != null )
                {
                    localSystem.AssignOwner( this, Runner );
                    break;
                }
            }
        }
    }

    public override void StateAuthorityChanged()
    {
        Debug.Log( "State authority changed for " + gameObject.name, gameObject );
        Runner.Despawn( Object );
    }

    [Rpc( RpcSources.All, RpcTargets.StateAuthority )]
    public void RpcPlayerQuit( RpcInfo info = default )
    {
        Debug.Log( "Disconnect RPC called." + info.Source );
        if( info.Source == Object.StateAuthority )
        {
            Debug.Log( "Disconnecting Server." );
            return;
        }
        Runner.Disconnect( info.Source );
    }
}
