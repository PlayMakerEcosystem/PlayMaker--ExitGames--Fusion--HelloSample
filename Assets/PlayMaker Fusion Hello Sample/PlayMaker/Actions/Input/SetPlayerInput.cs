
using HutongGames.PlayMaker.Addons.Fusion.Actions;
using UnityEngine;

namespace HutongGames.PlayMaker.Addons.Fusion.Sample
{
    [ActionCategory("Fusion Sample")]
    [Tooltip("Sets the player Network Inputs")]
    public class SetPlayerInput : FusionComponentActionBase<PlayMakerFusionInputDataProxy>
    {
        [CheckForComponent(typeof(PlayMakerFusionInputDataProxy))]
        [Tooltip("The Game Object with the NetworkObject attached.")]
        public FsmOwnerDefault gameObject;

        public FsmBool searchInParent;
        
        [Tooltip("Respawn Input")]
        public FsmBool respawn;
        
        [Tooltip("Boost Input")]
        public FsmBool boost;
        
        [Tooltip("Left Input")]
        public FsmBool left;
        
        [Tooltip("Right Input")]
        public FsmBool right;
        
        [Tooltip("Bakward Input")]
        public FsmBool backward;
        
        [Tooltip("Forward Input")]
        public FsmBool forward;
        
        [Tooltip("Send this event if there was no PlayMakerFusionInputDataProxy Object found")]
        public FsmEvent failure;
        
        public bool everyFrame;
        
        public override void Reset()
        {
            gameObject = null;
            searchInParent = true;
            
            respawn = null;
            boost = null;
            left = null;
            right = null;
            backward = null;
            forward = null;
            
            failure = null;
            everyFrame = false;
        }
		
        public override void OnEnter()
        {
            ExecuteAction();
            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            ExecuteAction();
        }

        void ExecuteAction()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject),searchParent: searchInParent.Value))
            {
                if (failure != null) Fsm.Event(failure);
                return;	
            }

            
            this.cachedComponent.CurrentInputData.Respawn = respawn.Value;
            this.cachedComponent.CurrentInputData.Boost = boost.Value;
            
            this.cachedComponent.CurrentInputData.Left = left.Value;
            this.cachedComponent.CurrentInputData.Right = right.Value;
            this.cachedComponent.CurrentInputData.Backward = backward.Value;
            this.cachedComponent.CurrentInputData.Forward = forward.Value;
            
        }
    }
}