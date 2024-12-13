using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
using Utilities.Audio;
using Random = UnityEngine.Random;

// This script is for Player Movement and Trajectory line
namespace Player
{
    public class Movement : MonoBehaviour
    {
        //Public Variables
        public float frontforce = 4.0f;
        public float jumpForce = 6.0f;
        public bool isOnGround = true;
        public static bool leftFirstBox;
        public int lineSegments;
        //Private Variables
        private Rigidbody _playerRb;
        private LineRenderer _lineRenderer;
        [SerializeField]private float _touchTimer;
        private Vector3 _upforce;
        private Vector3 _forwardforce;
        private Vector3 _result;
        private bool _isStarted = false;

        //Objects and Components
        public GameObject CF;
        public  Animator animator;
        public Animator crossfade; 
        private void Start()
        {
            _playerRb = GetComponent<Rigidbody>();
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = lineSegments;
           animator = GetComponent<Animator>();
        }

        private void OnAnimatorMove()
        {
            transform.SetPositionAndRotation(animator.targetPosition, animator.targetRotation);
        }

        private void LateUpdate()
        { 
            
            if (Input.touchCount > 0 && isOnGround)  //getting Touch Input
            {
                Touch touch = Input.GetTouch(0);
                _upforce = transform.up * jumpForce;
                _forwardforce = transform.forward * frontforce;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _touchTimer = 0;
                        frontforce = Random.Range(3.5f, 4.5f);
                       
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        _lineRenderer.enabled = true;
                        _touchTimer += Time.deltaTime;
                        if (_touchTimer > 2)
                        {
                            _touchTimer = 2;
                        }
                        _result = _upforce + (_forwardforce * _touchTimer);
                        Visualize(_result);
                        break;
                    case TouchPhase.Ended:
                       _playerRb.AddForce(_upforce, ForceMode.Impulse);
                        _playerRb.AddForce((_forwardforce * _touchTimer), ForceMode.Impulse);
                       isOnGround = false;
                       _lineRenderer.enabled = false;
                       animator.SetBool("IsJump", true);
                      SFXManager.instance.PlaySound("Jump");
                        break;
                    case TouchPhase.Canceled:
                 
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            //Game Over scene
            if (transform.position.y < 0 && _isStarted == false)
            {
                StartCoroutine(Dying());
                _isStarted = true;
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                leftFirstBox = true;
                animator.SetBool("IsJump", false);
            }

            if (collision.gameObject.CompareTag("firstbox"))
            {
                isOnGround = true;
                leftFirstBox = false;
            }
        }

        //Calculating Trajectory
        Vector3 CalculateposInTime(Vector3 vo, float time)
        {
            Vector3 Vxz = vo;
            Vxz.y = 0f;

            Vector3 result = transform.position + vo * time;
            float  sY =(float) (-0.5 * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + transform.position.y/2;
            result.y = sY;

            return result;
        }

        //Drawing trajectory line
        void Visualize(Vector3 vo)
        {
            for (int i = 0; i < lineSegments; i++)
            {
                Vector3 pos = CalculateposInTime(vo, i / (float)lineSegments);
                _lineRenderer.SetPosition(i, pos);
            }
        }
        
        //Game over Scene loader
        IEnumerator Dying()
        {
            SFXManager.instance.PlaySound("Dying");
            crossfade.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("GameOver");
        }
    }
}