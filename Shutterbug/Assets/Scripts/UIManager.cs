using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager s_instance;

    public static UIManager Instance { get => s_instance; set => s_instance = value; }
    #endregion

    public Image stealthBar;
    public TextMeshProUGUI pictureCount;
    public TextMeshProUGUI proofCount;
    public GameObject promptText;
    public GameObject entryPrompt;
    public GameObject exitPrompt;

    public TextMeshProUGUI timesFled;

    public GameObject handBook;
    public GameObject handBookDiscoveryText;
    public GameObject discoveriesObject;
    private bool m_handBookOpen = false;

    void Awake()
    {
        //Singleton Implementation
        if (s_instance == null)
        {
            s_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!m_handBookOpen)
            {
                handBook.GetComponent<Animator>().Play("handbook_in_from_bottom");
                m_handBookOpen = true;
            }
            else
            {
                handBook.GetComponent<Animator>().Play("handbook_out_to_bottom");
                m_handBookOpen = false;
            }
            
        }
    }

    private void Start()
    {
        promptText.SetActive(false);

        //CheckNewDiscoveries();
    }

    public void UpdatePictureCount(int _newPictureCount)
    {
        pictureCount.SetText(_newPictureCount.ToString());
    }

    public void UpdateProofCount(int _newProofCount)
    {
        proofCount.SetText(_newProofCount.ToString());
    }

    public void UpdateStealthFillAmount(float _fillValue)
    {
        stealthBar.fillAmount = _fillValue;
    }

    public void EnablePromptText()
    {
        promptText.SetActive(true);
    }

    public void EntryPrompt()
    {
        entryPrompt.GetComponent<Animator>().Play("entrymessage_fade_in");
    }

    public void ExitPrompt()
    {
        exitPrompt.GetComponent<Animator>().Play("entrymessage_fade_in");
    }

    public void AddTimeFled()
    {
        //timesFled.SetText(timesFled.text + "I");
    }

    public void CheckNewDiscoveries()
    {
        GameObject _current = GameManager.Instance.currentCryptid;
        Cryptid _stats = _current.GetComponent<CryptidProperties>().stats;

        if(_stats.KnownFleeCount)
        {
            GameObject _text = Instantiate(handBookDiscoveryText, discoveriesObject.transform);
            _text.GetComponent<TextMeshProUGUI>().text = "This cryptid tends to leave permanently after " + _stats.MaxFleeCount + " sightings";
        }

        int _counter = 0;

        foreach (bool _status in _stats.KnownPreferredRegions)
        {
            

            if(_status)
            {
                GameObject _text = Instantiate(handBookDiscoveryText, discoveriesObject.transform);
                _text.GetComponent<TextMeshProUGUI>().text = "This cryptid seems to like the " + _stats.PreferredRegions[_counter].ToString() + " area";
            }

            _counter++;
        }
    }


}
