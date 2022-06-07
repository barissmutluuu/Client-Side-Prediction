using System;
using Mirror;

namespace ClientSidePrediction.RB
{
    public class NetworkedRigidbodyMessenger : NetworkBehaviour, INetworkedClientMessenger<RigidbodyInput, RigidbodyState>
    {
        public event Action<RigidbodyInput> OnInputReceived;
        
        public RigidbodyState LatestServerState => _latestServerState;

        RigidbodyState _latestServerState;
        
        public void SendState(RigidbodyState state)
        {
            RpcSendState(state);
        }

        public void SendInput(RigidbodyInput input)
        {
            CmdSendInput(input);
        }
        public void ResetScore()
        {
           
        }

        [ClientRpc(channel = Channels.Unreliable)]
        void RpcSendState(RigidbodyState state)
        {
            _latestServerState = state;
        }
        
        [Command(channel = Channels.Unreliable)]
        void CmdSendInput(RigidbodyInput state)
        {
            OnInputReceived?.Invoke(state);
        }

        [Command(channel = Channels.Unreliable)]
        void CmdResetScore(RigidbodyInput state)
        {
            int a = 1;
        }
    }
}