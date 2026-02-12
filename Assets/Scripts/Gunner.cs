using UnityEngine;
using UnityEngine.UI;

public enum ColorState { Green, Blue, Red }

public class Gunner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnposition;
    [SerializeField] private Renderer _playerRenderer;

    [SerializeField] private GameObject _bulletGreenPrefab;
    [SerializeField] private GameObject _bulletBluePrefab;
    [SerializeField] private GameObject _bulletRedPrefab;

    [SerializeField] private Image _cooldownBar;

    private GameObject _currentBulletPrefab;
    private float _fireCooldown = 1f;
    private float _lastFireTime = 0f;

    private ColorState _currentColor = ColorState.Green;
    public ColorState CurrentColor => _currentColor;

    private Animator _animator;
    private bool _readyToShootAnimationPlayed = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        CambiarColor(ColorState.Green);
    }

    private void Update()
    {
        float timeSinceLastFire = Time.time - _lastFireTime;
        float fillAmount = Mathf.Clamp01(timeSinceLastFire / _fireCooldown);

        if (_cooldownBar != null)
            _cooldownBar.fillAmount = fillAmount;

      //  if (Input.GetKeyDown(KeyCode.C))
       // {
       //     switch (_currentColor)
       //     {
      //          case ColorState.Green: CambiarColor(ColorState.Blue); break;
      //          case ColorState.Blue: CambiarColor(ColorState.Red); break;
      //          case ColorState.Red: CambiarColor(ColorState.Green); break;
     //       }
     //   }

        if (timeSinceLastFire >= _fireCooldown && !_readyToShootAnimationPlayed)
        {
            if (_animator != null)
            {
                _animator.SetTrigger("ReadyToShoot");
            }

            _readyToShootAnimationPlayed = true;
        }

        if (Input.GetMouseButton(0) && timeSinceLastFire >= _fireCooldown)
        {
            Disparar();
            _lastFireTime = Time.time;
        }
    }

    public void CambiarColor(ColorState colorState)
    {
        _currentColor = colorState;

        switch (colorState)
        {
            case ColorState.Green:
                _playerRenderer.material.color = Color.green;
                _currentBulletPrefab = _bulletGreenPrefab;
                _fireCooldown = 0.4f;
                Time.timeScale = 1f;
                break;
            case ColorState.Blue:
                _playerRenderer.material.color = Color.blue;
                _currentBulletPrefab = _bulletBluePrefab;
                _fireCooldown = 1.6f;
                Time.timeScale = 0.9f;
                break;
            case ColorState.Red:
                _playerRenderer.material.color = Color.red;
                _currentBulletPrefab = _bulletRedPrefab;
                _fireCooldown = 0.05f;
                Time.timeScale = 1.5f;
                break;
        }
    }

    private void Disparar()
    {
        if (_currentBulletPrefab == null)
        {
            Debug.LogWarning("No hay prefab de bala asignado.");
            return;
        }

        GameObject bullet = Instantiate(_currentBulletPrefab, _spawnposition.transform.position, Quaternion.identity);
        bullet.transform.up = _spawnposition.transform.right;

        if (_animator != null)
        {
            _animator.ResetTrigger("Shoot");
            _animator.SetTrigger("Shoot");
        }

        _readyToShootAnimationPlayed = false;
    }

    public float GetFireCooldown() => _fireCooldown;
    public float GetLastFireTime() => _lastFireTime;
}
