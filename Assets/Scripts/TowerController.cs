using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] UserInterface userInterface;
    [SerializeField] string damageTag;


    private bool _dieCheck;
    public bool dieCheck
    {
        get { return _dieCheck; }
        set
        {
            if (_dieCheck != value)
            {
                _dieCheck = value;

                if (_dieCheck)
                {

                }
            }
        }

    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == damageTag)
        {
            GameObject root = collision.gameObject.transform.root.gameObject;//一番親のオブジェクトを渡す
            UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
            dieCheck = userInterface.DamegeValue(user.GetDamege());
        }
    }
}
