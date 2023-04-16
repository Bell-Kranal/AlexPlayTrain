using UnityEngine;

namespace Trees
{
    [RequireComponent(typeof(TreeDestroyer), typeof(Collider))]
    public class TreeColliderEnabler : MonoBehaviour
    {
        private TreeDestroyer _destroyer;
        private Collider _collider;
        
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _destroyer = GetComponent<TreeDestroyer>();
        }

        private void OnEnable() =>
            _destroyer.EnabledOrDisabled += ColliderEnableOrDisable;

        private void OnDisable() =>
            _destroyer.EnabledOrDisabled -= ColliderEnableOrDisable;

        private void ColliderEnableOrDisable() =>
            _collider.enabled = !_collider.enabled;
    }
}