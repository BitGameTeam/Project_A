using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo : MonoBehaviour
{
    #region 스킬 정보(프로퍼티)
    public int skill_Number;
    public string skill_Name;
    public float skill_Range;
    public float skill_Motion;
    public float skill_Cooltime;
    private Vector3 skill_Position;
    /*public int Skill_Number { get { return skill_Number; } set { skill_Number = value; } }
    //public string Skill_Name { get { return skill_Name; } set { skill_Name = value; } }
    //public float Skill_Range { get { return skill_Range; } set { skill_Range = value; } }
    //public float Skill_Motion { get { return skill_Motion; } set { skill_Motion = value; } }
    public float Skill_Cooltime { get { return skill_Cooltime; } set { skill_Cooltime = value; } }*/
    public SkillType sType;
    #endregion
   
    public enum SkillType
    {
        Passive = 1,
        Attack = 2,
        Buff = 3
    }

    private GameObject skill_Object;
    private void Start()
    {
        skill_Position = this.gameObject.transform.position;
        skill_Object = this.gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(HideSkill());
    }
    IEnumerator HideSkill()
    {
        yield return new WaitForSeconds(skill_Motion);
        skill_Object.SetActive(false);
        skill_Object.transform.position = skill_Position;
        StopCoroutine(HideSkill());
    }
}
