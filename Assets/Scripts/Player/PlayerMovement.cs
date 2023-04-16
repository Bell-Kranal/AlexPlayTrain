using Data;
using Infrastructure.Services;
using Input;
using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private float _speed;

        private IInputService _inputService;
        private CharacterController _characterController;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            _characterController = GetComponent<CharacterController>();
            
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.GetAxis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.GetAxis);

                movementVector.y = 0f;
                movementVector.Normalize();
                
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * _speed * Time.deltaTime);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {
                    Warp(savedPosition);
                }
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}