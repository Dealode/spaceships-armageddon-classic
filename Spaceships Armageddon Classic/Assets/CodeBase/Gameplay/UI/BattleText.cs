using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class BattleText : MonoBehaviour
    {
        [SerializeField] TMP_Text _text;
        private Camera _camera;
        private Transform _linkedTransform;
        private int _offsetX;
        private int _offsetY;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void Initialize(string text, Color color)
        {
            _text.text = text;
            _text.color = color;
            
            Destroy(gameObject, 1.5f);
        }

        private void Update()
        {
            if (_linkedTransform == null)
                return;

            var target = _camera.WorldToScreenPoint(_linkedTransform.position);
            target += new Vector3(_offsetX, _offsetY, 0);
            transform.position = target;
        }

        public void LinkTo(Transform root, int offsetX, int offsetY)
        {
            _offsetY = offsetY;
            _offsetX = offsetX;
            _linkedTransform = root;
        }
    }
}