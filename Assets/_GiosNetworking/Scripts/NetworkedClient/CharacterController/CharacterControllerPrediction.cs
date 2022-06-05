using ClientSidePrediction.RB;
using UnityEngine;

namespace ClientSidePrediction.CC
{
    public class CharacterControllerPrediction : ClientPrediction<CharacterControllerInput, CharacterControllerState> 
    {
        protected override CharacterControllerInput GetInput(float deltaTime, uint currentTick)
        {
            var __inputs = new Vector2
            {
                x = Input.GetAxis("Horizontal"),
                y = Input.GetAxis("Vertical")
            };

            int predictionEnabled = 0;

            if (Input.GetKey("o"))
            {
                predictionEnabled = 1;
            }else if (Input.GetKey("p"))
            {
                predictionEnabled = 0;
            }
            else
            {
                predictionEnabled = 3;
            }

           

            return new CharacterControllerInput(__inputs, currentTick, deltaTime, predictionEnabled);
        }
    }
}