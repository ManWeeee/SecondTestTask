using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellAnimation : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;

    private float moveTime = .08f;
    private float appendTime = .08f;
    private void Start()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private Sequence _sequence;
    public void Move(Cell from, Cell to, bool isMerging)
    {
        from.CancelAniamtion();
        to.SetAnimation(this);

        background.color = from.Background.color;
        text.text = from.Text.text;
        text.color = from.Text.color;

        transform.position = from.transform.position;

        _sequence = DOTween.Sequence();

        _sequence.Append(transform.DOMove(to.transform.position, moveTime).SetEase(Ease.InOutQuad));
        if (isMerging)
        {
            _sequence.AppendCallback(() =>
            {
                background.color = to.Background.color;
                text.color = to.Text.color;
                text.text = to.Text.text;
            });
            _sequence.Append(transform.DOScale(1.1f, appendTime));
            _sequence.Append(transform.DOScale(1f, appendTime));
        }

        _sequence.AppendCallback(() =>
        {
            to.UpdateTile();
            Destroy();
        });
    }

    public void Appear(Cell cell)
    {
        cell.CancelAniamtion();
        cell.SetAnimation(this);
        
        background.color = cell.Background.color;
        text.text = cell.Text.text;
        text.color = cell.Text.color;

        transform.position = cell.transform.position;
        transform.localScale = Vector3.zero;

        _sequence = DOTween.Sequence();

        _sequence.Append(cell.transform.DOScale(1.1f, appendTime * 2));
        _sequence.Append(cell.transform.DOScale(1f, appendTime * 2));

        _sequence.AppendCallback(() =>
        {
            cell.UpdateTile();
            Destroy();
        });
    }

    public void Destroy()
    {
        _sequence.Kill();
        Destroy(gameObject);
    }
}
