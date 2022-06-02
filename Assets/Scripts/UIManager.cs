using UnityEngine;
using System.Collections;
using VIDE_Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
 
public class UIManager : MonoBehaviour
{
    public GameObject container_NPC;
    public GameObject container_PLAYER;
    public GameObject inicio;
    public GameObject persona;
    public GameObject tutorial;
    public GameObject box;
    public GameObject image;
    public GameObject image2;
    public Text text_NPC;
    public Text[] text_Choices;
    public string[] mid_speak;
    public string[] final_speak;
    string sceneName;
    public AudioSource audioSource;
    public int pontos = 0;
    public bool first = true;
 
    // Use this for initialization
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        persona = GameObject.FindWithTag("persona");
        VD.LoadDialogues(sceneName);
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        inicio.SetActive(false);
        box.SetActive(false);
        persona.SetActive(false);
        tutorial.SetActive(true);
        image.SetActive(false);
        image2.SetActive(false);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && first)
        {
            first = false;
            inicio.SetActive(true);
            tutorial.SetActive(false);
            box.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return) && !first)
        {
            if (!VD.isActive)
            {
                Begin();
            }
            else
            {
                VD.Next();
            }
        }
    }
 
    void Begin()
    {
        inicio.SetActive(false);
        persona.SetActive(true);
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }
 
    void UpdateUI(VD.NodeData data)
    {
        // botaoOff();
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        if (data.isPlayer)
        {
            container_PLAYER.SetActive(true);
            for (int i = 0; i < text_Choices.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(true);
                    text_Choices[i].text = data.comments[i];
                }
                else
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            container_NPC.SetActive(true);
            text_NPC.text = data.comments[data.commentIndex];
            //Play Audio if any
            if (data.audios[data.commentIndex] != null){
                audioSource.clip = data.audios[data.commentIndex];
                audioSource.Play();
            }
        }
    }
 
    // void botaoOff()
    // {
    //     container_PLAYER.transform.GetChild(1).GetComponent<Animator>().SetBool("Normal",true);
    //     container_PLAYER.transform.GetChild(2).GetComponent<Animator>().SetBool("Normal",true);
    //     container_PLAYER.transform.GetChild(3).GetComponent<Animator>().SetBool("Normal",true);
    // }
    void End(VD.NodeData data)
    {
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }
 
    void OnDisable()
    {
        if (container_NPC != null)
            End(null);
    }
 
    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if (Input.GetMouseButtonUp(0))
            VD.Next();
    }
    public void Bom()
    {
        pontos++;
    }
    public void Ruim()
    {
        pontos--;
    }
    public void PontoMeio()
    {
        container_PLAYER.SetActive(false);
        container_NPC.SetActive(true);
        if(pontos>0)
            text_NPC.text = mid_speak[0];
        if(pontos==0)
            text_NPC.text = mid_speak[1];    
        if(pontos<0)
            text_NPC.text = mid_speak[2];
    }
    public void PontoFim()
    {
        container_PLAYER.SetActive(false);
        container_NPC.SetActive(true);
        if(pontos>1)
            text_NPC.text = final_speak[0];
        else if(pontos<-1)
            text_NPC.text = final_speak[2];
        else
            text_NPC.text = final_speak[1];    
    }
    public void Saida()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Vitoria()
    {
        if(pontos >= 2)
        {
            image.SetActive(true);
            image.GetComponent<Animator>().SetInteger("New Int", 1);
        }
        else
        {
            image2.SetActive(true);
            image2.GetComponent<Animator>().SetInteger("New Int", 1);
        }
        
    }
        
}