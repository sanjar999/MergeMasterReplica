using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class WinLose : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] UnitSpawner _unitSpawner;
    [SerializeField] Image _fade;
    private bool _isCalled;

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

    private void Start()
    {
        if (_fade)
            _fade.DOFade(0, 1);
    }

    private void ReloadLevel()
    {
        if (!_isCalled)
        {
            StartCoroutine(ReloadLevelCo());
            _isCalled = true;
        }
    }

    IEnumerator ReloadLevelCo()
    {
        yield return new WaitForSeconds(2f);
        if (_fade)
            _fade.DOFade(1, 1).OnComplete(() => {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
                DOTween.KillAll();
            });
    }
}
