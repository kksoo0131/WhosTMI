using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator filpAnime;
    //Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool Run(GameObject _object, Vector3 _position)
    {
        //�ִϸ��̼��� ����� �� ���ӿ�����Ʈ ����
        filpAnime = _object.transform.GetComponent<Animator>();
        filpAnime.Play("filp");
        return true;
    }

    public void Cancel()
    {
        if (gameObject == null)
        {
            throw new System.Exception("GameObject Dosen't Exist!!!");
        }
        else
        {
            filpAnime.Play("unfilp");
        }
    }
}


