using System;
using Fusion;

namespace HutongGames.PlayMaker.Addons.Fusion.Sample
{
    /// <summary>
    /// TODO: this will have to be generated, possibly from a dedicated fsm where we take the inputs
    /// </summary>
    [Serializable]
    public struct PlayMakerInputData : INetworkInput
    {
        public NetworkBool Respawn;
        public NetworkBool Boost;
        
        
        public NetworkBool Forward;
        public NetworkBool Backward;
        public NetworkBool Left;
        public NetworkBool Right;

        /// <summary>
        /// TODO: find a way to make this generic or easy to define
        /// </summary>
        public void resetStates()
        {
            Respawn = false;
            Boost = false;

            Forward = false;
            Backward = false;
            Left = false;
            Right = false;
        }
    }
}