using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class WinLose : MonoBehaviour
{
    [SerializeField] Image _fade;

    private void OnEnable()
    {
        Events.OnWin += ReloadLevel;
        Events.OnLose += ReloadLevel;
    }

    private void OnDisable()
    {
        Events.OnWin -= ReloadLevel;
        Events.OnLose -= ReloadLevel;
    }

    private void ReloadLevel()
    {
        StartCoroutine(ReloadLevelCo());
    }

    IEnumerator ReloadLevelCo()
    {
        yield return new WaitForSeconds(2f);
        if (_fade)
            _fade.DOFade(1, 1).OnComplete(() =>
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                DOTween.KillAll();
            });
    }
}
    