using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace ClientSidePrediction.CC
{
    public class NetworkedCharacterController : NetworkedClient<CharacterControllerInput, CharacterControllerState>
    {
        [Header("CharacterController/References")]
        [SerializeField] CharacterController _characterController = null;
        [Header("CharacterController/Settings")]
        [SerializeField] float _speed = 10f;
        float _verticalVelocity = 0f;
        public GameObject bulletPrefab;
        public bool shootable = true;
        public Text scoreBoard;
        public int myScore;


        public void Start()
        {
            scoreBoard = GameObject.Find("Scoreboard").GetComponent<Text>();
        }
        public override void SetState(CharacterControllerState state)
        {
            _characterController.enabled = false;
            _characterController.transform.position = state.position;
            _verticalVelocity = state.verticalVelocity;
            _characterController.enabled = true;
            scoreBoard.text = state.score.ToString();
        }

        public override void ProcessInput(CharacterControllerInput input)
        {
            if (input.fired == true && shootable == true )
            {
                GetComponent<Shooter>().ShootCMD();

                shootable = false;
                StartCoroutine(ShootableAfter2sc());
            }


            myScore = Int32.Parse(scoreBoard.text);


            if (input.resetScore == true)
            {
                myScore = 0;
                scoreBoard.text = "0";
            }

            var __movement = new Vector3(input.input.x, 0f, input.input.y);
            __movement = Vector3.ClampMagnitude(__movement, 1f) * _speed;
            if (!_characterController.isGrounded)
                _verticalVelocity += Physics.gravity.y * input.deltaTime;
            else
                _verticalVelocity = Physics.gravity.y;
            __movement.y = _verticalVelocity;
            _characterController.Move(__movement * input.deltaTime);
        }


        public IEnumerator ShootableAfter2sc()
        {
            yield return new WaitForSeconds(1f);
            shootable = true;
        }

        public IEnumerator DestroyBulletAfter3sc(GameObject bullet)
        {
            yield return new WaitForSeconds(3f);
            Destroy(bullet);
        }

        public override int GetPredictionEnabled(CharacterControllerInput input)
        {
              return input.predictionEnabled;
        }

        public override bool GetResetScoreClicked(CharacterControllerInput input)
        {
            return input.resetScore;
        }

        protected override CharacterControllerState RecordState(uint lastProcessedInputTick)
        {
            return new CharacterControllerState(_characterController.transform.position, _verticalVelocity, lastProcessedInputTick, myScore );   
        }
    }
}