using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] UserInterface userInterface;
    [SerializeField] string damageTag;
    [SerializeField] string fireBallTag;

    [SerializeField] bool enemy;

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
                    if(enemy)
                    {
                        if(gameDirector.towerCount != 0)
                        {
                            gameDirector.towerCount--;
                            Destroy(this.gameObject);
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
            }
        }

    }
    void Start()
    {

    }


    /// <summary>
    /// 受けるダメージ
    /// </summary>
    /// <param name="collision">コライダーがトリガーではない場合</param>
    /// <param name="collider">コライダーがトリガーの場合</param>
    private void HitDamage(Collision collision = null, Collider collider = null)
    {
        GameObject root = null;
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//一番親のオブジェクトを渡す
        }
        else if (collider != null)
        {
            root = collider.gameObject.transform.root.gameObject;//一番親のオブジェクトを渡す
        }
        UserInterface user = root.GetComponentInChildren<Canvas>().GetComponent<UserInterface>();//UserInterfaceスクリプトを受け取る
        dieCheck = userInterface.DamegeValue(user.GetDamege());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == damageTag)
        {
            HitDamage(collision);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == fireBallTag)
        {
            HitDamage(null, other);
        }
    }
}
