using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] AnimeController animeController;
    [SerializeField] GameDirector gameDirector;
    [SerializeField] UserInterface userInterface;
    [SerializeField] string damageTag;
    [SerializeField] string fireBallTag;
    [SerializeField] string animePara;//�A�j���̃p�����[�^�[
    [SerializeField] int plusCost;

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
                            gameDirector.cost += plusCost;
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            animeController.TransitionAnime(animePara);
                            gameDirector.loadState = GameDirector.GameState.Clear;  
                        }
                    }
                    else
                    {
                        animeController.TransitionAnime(animePara);
                        gameDirector.loadState = GameDirector.GameState.Over;
                    }
                }
            }
        }

    }
    void Start()
    {

    }


    /// <summary>
    /// �󂯂�_���[�W
    /// </summary>
    /// <param name="collision">�R���C�_�[���g���K�[�ł͂Ȃ��ꍇ</param>
    /// <param name="collider">�R���C�_�[���g���K�[�̏ꍇ</param>
    private void HitDamage(Collision collision = null, GameObject ob = null)
    {
        GameObject root = null;//�I�u�W�F�N�g
        if (collision != null)
        {
            root = collision.gameObject.transform.root.gameObject;//�I�u�W�F�N�g��n��
        }
        else if (ob != null)
        {
            root = ob.transform.gameObject.transform.root.gameObject;//�I�u�W�F�N�g��n��
        }
        Debug.Log(56666666);
        UserInterface user = root.GetComponent<UserInterface>();//UserInterface�X�N���v�g���󂯎��
        dieCheck = userInterface.DamegeValue(user.GetState(1) , (int)user.GetState(6));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == damageTag)
        {
            HitDamage(collision);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == fireBallTag)
        {
            HitDamage(null, other);
        }
    }

    //���[�h�ύX���ɍs���A�j���[�V����
    public void ChengeMode()
    {
        animeController.TransitionAnime("chenge");
    }
}
