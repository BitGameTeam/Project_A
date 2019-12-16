using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{
    public static SkillManagement instance;
    List<GameObject> skill_List = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for(int i = 0; ;i++)
        {
            try
            {
                GameObject c = transform.GetChild(i).gameObject;
                skill_List.Add(c);
            }
            catch
            {
                break;
            }
        }
    }

    //스킬 실행
    public void Return_Skill(int skill_Number, Transform player)
    {
        switch (skill_Number)
        {
            case 0: StartCoroutine(Smash()); break;
            case 1: StartCoroutine(Xslach()); break;
            case 2: StartCoroutine(SwordAura()); break;
        }
    }


    #region 스킬 함수들
    //강타
    IEnumerator Smash()
    {
        yield return new WaitForSeconds(0.5f);
        skill_List[0].SetActive(true);
        StopCoroutine(Smash());
    }
    //X슬래쉬
    IEnumerator Xslach()
    {
        yield return new WaitForSeconds(0.7f);
        skill_List[1].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        skill_List[2].SetActive(true);
        StopCoroutine(Xslach());
    }
    //검기
    IEnumerator SwordAura()
    {
        yield return new WaitForSeconds(0.6f);
        skill_List[3].SetActive(true);
        if(this.transform.rotation.y == 0)
        {
            skill_List[3].GetComponent<Transform>().position += new Vector3(-20 * Time.deltaTime, 0, 0);
        }
        else if(this.transform.rotation.y > 0)
        {
            skill_List[3].GetComponent<Transform>().position += new Vector3(20*Time.deltaTime, 0, 0);
        }
        //skill_List[3].GetComponent<Rigidbody>().velocity = Vector3.zero;
        //skill_List[3].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        StopCoroutine(SwordAura());
    }
    //해비 슬래쉬
    IEnumerator HeavySlash()
    {
        yield return new WaitForSeconds(0.6f);
        skill_List[3].SetActive(true);
        if (this.transform.rotation.y == 0)
        {
            skill_List[3].GetComponent<Transform>().position += new Vector3(Time.deltaTime, 0, 0);
        }
        else if (this.transform.rotation.y > 0)
        {
            skill_List[3].GetComponent<Transform>().position += new Vector3(Time.deltaTime, 0, 0);
        }
        //skill_List[3].GetComponent<Rigidbody>().velocity = Vector3.zero;
        //skill_List[3].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        StopCoroutine(SwordAura());
    }
    #endregion

}
