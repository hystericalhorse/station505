using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarUIScript : MonoBehaviour
{
    [SerializeField] Button hit;
    [SerializeField] Button deal;
    // Start is called before the first frame update
    void Start()
    {
        hit.gameObject.SetActive(false);
    }

    public void Deal()
    {
        hit.gameObject.SetActive(true);
        deal.gameObject.SetActive(false);
    }

    public void ResetUI()
    {
        deal.gameObject.SetActive(true);
        hit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
