using DG.Tweening;
using UnityEngine;

public class CellAnimator : MonoBehaviour
{
    [SerializeField] private CellAnimation _animationPref;
    private void Awake()
    {
        DOTween.Init();
    }

    public void SmoothMerging(Cell from, Cell to, bool isMerging)
    {
        Instantiate(_animationPref, from.transform, false).Move(from, to, isMerging);
    }

    public void SmoothApearance(Cell cell)
    {
        Instantiate(_animationPref, cell.transform, false).Appear(cell);
    }
}
