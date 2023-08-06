using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//çÏê¨ìríÜ
public class KnifeThrowController : MonoBehaviour
{
    [SerializeField] SortrLayerController sortLayerController;
    // Start is called before the first frame update
    void Start()
    {
        sortLayerController = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<SortrLayerController>();
        sortLayerController.addUnit(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    this.gameObject.SetActive(false);
    //}
}
