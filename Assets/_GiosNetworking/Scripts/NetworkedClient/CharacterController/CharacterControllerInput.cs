using UnityEngine;

namespace ClientSidePrediction.CC
{
    [System.Serializable]
    public struct CharacterControllerInput : INetworkedClientInput
    {
        public uint Tick => tick;
        public float DeltaTime => deltaTime;
        
        public uint tick;
        public Vector2 input;
        public float deltaTime;
        public int predictionEnabled;
        public bool fired;
        public bool resetScore;

        public CharacterControllerInput(Vector2 input, uint tick, float deltaTime, int predictionEnabled, bool fired, bool resetScore)
        {
            this.input = input;
            this.tick = tick;
            this.deltaTime = deltaTime;
            this.predictionEnabled = predictionEnabled;
            this.fired = fired;
            this.resetScore = resetScore;
        }
    }
}