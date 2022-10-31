using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupBase : MonoBehaviour
{
    public GameState popupState;

    private bool isPopupClosed;

    [SerializeField]
    protected bool _showAdOnOpen = false;

    protected AdCalls _adsManager;

    protected virtual void Awake()
    {
        this._adsManager = AdCalls.instance;

        if (this._adsManager == null)
            this._adsManager = FindObjectOfType<AdCalls>();
    }

    protected virtual void Start()
    {
        if(this._showAdOnOpen)
        {
            this._adsManager.Admob_Unity();
        }

        UpdateUI();
    }

    private void Update()
    {
        // Back pressed
        if (GameManager.Instance.GetCurrentState() == popupState && Input.GetKeyUp(KeyCode.Escape))
        {
            OnBackButtonPressed();
        }
    }

    private void OnDisable()
    {
        if(!isPopupClosed)
            MenusManager.Instance.OnPopupClosed();
    }

    public virtual void OnBackButtonPressed()
    {
        SoundsManager.Instance.PlaySound(SoundClip.BUTTONCLICK);
        GameManager.Instance.BackState();
        Destroy(gameObject);
    }

    public void ClosePopup()
    {
        isPopupClosed = true;
        Destroy(gameObject);
    }

    public abstract void UpdateUI();
}