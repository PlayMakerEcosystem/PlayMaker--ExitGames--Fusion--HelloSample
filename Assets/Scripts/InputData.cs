using Fusion;
using UnityEngine;

[System.Flags]
public enum InputAction
{
    RESPAWN = 1 << 0,
    BOOST = 1 << 1,
}

[System.Flags]
public enum InputState
{
    FORWARD = 1 << 0,
    BACKWARD = 1 << 1,
    LEFT = 1 << 2,
    RIGHT = 1 << 3,
}

public struct InputData : INetworkInput
{
    NetworkBool IsPreprocessed;

    public InputAction Actions;
    public InputState States;

    public bool GetAction( InputAction action )
    {
        Debug.Assert( IsPreprocessed, "Actions are not preprocessed yet. Actions will not be read correctly." );
        return ( Actions & action ) == action;
    }

    public bool GetState( InputState state )
    {
        return ( States & state ) == state;
    }

    public InputAction Preprocess( InputAction previousActions )
    {
        Debug.Assert( IsPreprocessed == false, "Trying to preprocess actions twice. Will result in incorrect actions." );

        var originalActions = Actions;
        Actions = previousActions ^ Actions; // xor the previous and current actions to get the state change.
        IsPreprocessed = true;

        return originalActions;
    }
}
