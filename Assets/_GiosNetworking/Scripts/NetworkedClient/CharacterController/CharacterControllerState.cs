using System;
using UnityEngine;

namespace ClientSidePrediction.CC
{
    [System.Serializable]
    public struct CharacterControllerState: IEquatable<CharacterControllerState>, INetworkedClientState
    {
        public uint LastProcessedInputTick => lastProcessedInput;
        
        public Vector3 position;
        public float verticalVelocity;
        public uint lastProcessedInput;
        public int score;

        public CharacterControllerState(Vector3 position, float verticalVelocity, uint lastProcessedInput,  int score)
        {
            this.position = position;
            this.verticalVelocity = verticalVelocity;
            this.lastProcessedInput = lastProcessedInput;
            this.score = score;
        }

        public bool Equals(CharacterControllerState other)
        {
            return position.Equals(other.position) && lastProcessedInput == other.lastProcessedInput;
        }

        public bool Equals(INetworkedClientState other)
        {
            return other is CharacterControllerState __other && Equals(__other);
        }
    }
}