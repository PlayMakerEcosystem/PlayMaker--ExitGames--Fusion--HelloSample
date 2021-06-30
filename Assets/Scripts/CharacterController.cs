using UnityEngine;
using Fusion;

[RequireComponent( typeof( Rigidbody ), typeof( NetworkRigidbody ) )]
public class CharacterController : NetworkBehaviour
{
    Rigidbody m_Body;
    BoostAnimation m_BoostAnim;

    TickTimer m_BoostCooldown = TickTimer.None;

    public float MovementSpeed = 5000f;
    public float BoostFactor = 3000f;
    public float BoostCooldownInSeconds = 2f;
    public float AreaOfInterestRadius = 5f;

    [Networked]
    private InputAction m_PreviousInputActions { get; set; }

    private void Awake()
    {
        m_BoostAnim = GetComponent<BoostAnimation>();
        m_Body = GetComponent<Rigidbody>();
    }

    public override void Spawned()
    {
        Runner.SetPlayerAlwaysInterested( Object.InputAuthority, Object, true );
    }

    public override void FixedUpdateNetwork()
    {
        Runner.AddPlayerAreaOfInterest( Object.InputAuthority, transform.position, AreaOfInterestRadius );
        if( GetInput<InputData>( out var input ) )
        {
            PreprocessInput( ref input );
            UpdateRespawn( input );
            UpdateMovement( input );
        }
    }

    void PreprocessInput( ref InputData input )
    {
        m_PreviousInputActions = input.Preprocess( m_PreviousInputActions );
    }

    void UpdateMovement( InputData input )
    {
        float speed = MovementSpeed * Runner.DeltaTime;
        if( input.GetAction( InputAction.BOOST ) && ( m_BoostCooldown.IsRunning == false || m_BoostCooldown.Expired( Runner ) ) )
        {
            RpcStartBoost();
            speed += BoostFactor;
            m_BoostCooldown = TickTimer.CreateFromSeconds( Runner, BoostCooldownInSeconds );
        }

        if( input.GetState( InputState.FORWARD ) )
        {
            m_Body.AddForce( Vector3.forward * speed );
        }
        else if( input.GetState( InputState.BACKWARD ) )
        {
            m_Body.AddForce( Vector3.back * speed );
        }

        if( input.GetState( InputState.LEFT ) )
        {
            m_Body.AddForce( Vector3.left * speed );
        }
        else if( input.GetState( InputState.RIGHT ) )
        {
            m_Body.AddForce( Vector3.right * speed );
        }
    }

    void UpdateRespawn( InputData input )
    {
        if( transform.position.y < -10f || input.GetAction( InputAction.RESPAWN ) )
        {
            transform.position = Vector3.up * 3f;
            m_Body.velocity = Vector3.zero;
            m_Body.angularVelocity = Vector3.zero;
        }
    }

    [Rpc( RpcSources.All, RpcTargets.All, InvokeLocal = true, InvokeResim = true )]
    void RpcStartBoost()
    {
        m_BoostAnim.StartBoostAnimation();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere( transform.position, AreaOfInterestRadius );
    }
}
