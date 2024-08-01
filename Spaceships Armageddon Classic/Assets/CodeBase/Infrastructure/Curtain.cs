using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _points;
    
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }
    
        public async void Hide()
        {
            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= 0.1f;
                UniTask.WaitForSeconds(0.1F);
            }
        
            gameObject.SetActive(false);
        }
    }
}
