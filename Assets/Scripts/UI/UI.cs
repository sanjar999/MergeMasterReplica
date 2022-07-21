using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button _fightBtn;
    [SerializeField] private TMP_Text _stageNumber;
    [SerializeField] private TMP_Text _coinsAmount;
    [SerializeField] private TMP_Text _unitPrice;
    [SerializeField] private StageManager _stageManager;
    [SerializeField] private GameProgress _gameProgress;

    private void OnEnable()
    {
        Events.OnDealDamage += IncreaseCoinsAmount;
        Events.OnBuyUnit += DecreaseCoinsAmount;
        Events.OnSpawn += () => _unitPrice.text = _gameProgress.GetUnitPrice().ToString();
    }

    private void OnDisable()
    {
        Events.OnDealDamage -= IncreaseCoinsAmount;
        Events.OnBuyUnit -= DecreaseCoinsAmount;
        Events.OnSpawn -= () => _unitPrice.text = _gameProgress.GetUnitPrice().ToString();
    }

    private void Start()
    {
        _stageNumber.text = _stageManager.GetCurrentStage().ToString();
        _fightBtn.onClick.AddListener(() => Events.OnFight?.Invoke());
        _unitPrice.text = _gameProgress.GetUnitPrice().ToString();
        _coinsAmount.text = _gameProgress.GetCoins().ToString();
    }

    public void IncreaseCoinsAmount(int amount)
    {
        var coins = _gameProgress.GetCoins() + (amount * _stageManager.GetCurrentStage());
        _coinsAmount.text = coins.ToString();
        _gameProgress.SetCoins(coins);
    }
    public void DecreaseCoinsAmount(int amount)
    {
        var coins = _gameProgress.GetCoins() + amount;
        _coinsAmount.text = coins.ToString();
        _gameProgress.SetCoins(coins);
    }

    #region FPS
    [SerializeField] private TMP_Text _fps;
    [SerializeField, Range(0.1f, 2f)] private float sampleDuration = 1f;
    private int frames;
    private float duration;

    private void Update()
    {
        CalculateFps();
    }

    private void CalculateFps()
    {
        float frameDuration = Time.unscaledDeltaTime;
        frames += 1;
        duration += frameDuration;
        if (duration >= sampleDuration)
        {
            _fps.SetText("FPS:{0:0}", frames / duration);
            frames = 0;
            duration = 0f;
        }
    }
    #endregion
}