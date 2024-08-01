using UnityEngine;

namespace CodeBase.UI
{
    public interface IBattleTextInformer
    {
        void PlayText(string text, Color color, Transform where);
        void SetRoot(Transform root);
    }
}