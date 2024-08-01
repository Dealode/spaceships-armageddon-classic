using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class BattleTextInformer : IBattleTextInformer, IInitializable
    {
        private const string BattleTextPrefabPath = "UI/BattleText";
        private const int RandomOffsetX = 20;
        private const int RandomOffsetY = 20;

        private Transform TextRoot { get; set; }
        
        private BattleText _battleTextPrefab;
        
        public void Initialize()
        {
            _battleTextPrefab = Resources.Load<BattleText>(BattleTextPrefabPath);
        }
        
        public void SetRoot(Transform root) => 
            TextRoot = root;

        public void PlayText(string text, Color color, Transform where)
        {
            var position = Camera.main.WorldToScreenPoint(where.position);
            
            var randX = Random.Range(-RandomOffsetX, RandomOffsetX);
            var randY = Random.Range(-RandomOffsetY, RandomOffsetY);
            position += new Vector3(randX, randY, 0);
            
            var battleText = Object.Instantiate(_battleTextPrefab, position, Quaternion.identity, TextRoot);
            battleText.Initialize(text, color);
            battleText.LinkTo(where, randX, randY);
        }
    }
}