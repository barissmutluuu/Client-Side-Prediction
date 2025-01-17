﻿using System;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace ClientSidePrediction.CC
{
    public class CharacterControllerMessenger : NetworkBehaviour, INetworkedClientMessenger<CharacterControllerInput, CharacterControllerState>
    {
        public event Action<CharacterControllerInput> OnInputReceived;

        public CharacterControllerState LatestServerState => _latestServerState;

        CharacterControllerState _latestServerState;
        
        public void SendState(CharacterControllerState state)
        {
            RpcSendState(state);
        }

        public void SendInput(CharacterControllerInput input)
        {
            CmdSendInput(input);
        }

        public  void ResetScore()
        {
            CmdResetScore();
        }
        
        [ClientRpc(channel = Channels.Unreliable)]
        void RpcSendState(CharacterControllerState state)
        {
            _latestServerState = state;
        }

        
        [Command(channel = Channels.Unreliable)]
        void CmdSendInput(CharacterControllerInput input)
        {
            OnInputReceived?.Invoke(input);
        }
        [Command(channel = Channels.Unreliable)]
        void CmdResetScore()
        {
            GameObject.Find("Scoreboard").GetComponent<Text>().text="0";
            GameObject.Find("Target").GetComponent<TargetMove>().score = 0;

        }
    }
}