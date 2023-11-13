using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_1_Texture : MonoBehaviour {

    public Character_1 character;
    public Character_1_HitBox characterHitbox;
    public GameObject material;
    public GameObject Sword;
    public GameObject GreatSword;
    public GameObject Shield;
    private void Start()
    {
        var cubeRenderer = material.GetComponent<Renderer>();
        if (character.Is_Player_or_Enemy)
        {
            cubeRenderer.material.SetColor("_Color", Color.white);
        }
        else
        {
            cubeRenderer.material.SetColor("_Color", Color.gray);
        }
    }
    // Update is called once per frame
    void Update () {
        //Character Color
        var cubeRenderer = material.GetComponent<Renderer>();
        if (character.IS_Invinciable_Real())
        {
            cubeRenderer.material.SetColor("_Color", Color.white);
        }
        else {
            /*
            if (character.Is_Player_or_Enemy)
            {
            */
                Color c;

                switch (character.Get_CharacterMode_B1H2Q3())
                {
                    case 1:  c = new Color(1f, 1f, 0.5f, 1f); // Yellow
                        break;
                    case 2:  c = new Color(1f, 0.35f, 0.35f, 1f); // Red
                        break;
                    case 3:  c = new Color(0.5f, 0.5f, 1f, 1f); // Blue
                        break;
                    default: c  = Color.white;
                        break;

                }
                cubeRenderer.material.SetColor("_Color", c);
                //cubeRenderer.material.SetColor("_Color", Color.white);
                /*
            }
            else
            {
                cubeRenderer.material.SetColor("_Color", Color.gray);
            }
            */
        }

        // Shield Color
        var shieldRenderer = Shield.GetComponent<Renderer>();
        if (characterHitbox.BlockHitBox_Actived_Type() == 2)
        {
            Shield.transform.localScale = new Vector3(1.2f,0.5f,1.2f);
            shieldRenderer.material.SetColor("_Color", Color.red);
        }
        else if (characterHitbox.BlockHitBox_Actived_Type() == 1)
        {
            Shield.transform.localScale = new Vector3(0.8f,0.5f,0.8f);
            shieldRenderer.material.SetColor("_Color", Color.magenta);
        }
        else
        {
            Shield.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            shieldRenderer.material.SetColor("_Color", Color.yellow);
        }

        // Sword Color
        var swordRenderer = Sword.GetComponent<Renderer>();
        var greatswordRenderer = GreatSword.GetComponent<Renderer>();
        if (characterHitbox.Is_HitBox_Actived() && (characterHitbox.BlockHitBox_Actived_Type()==0))
        {
            swordRenderer.material.SetColor("_Color", Color.red);
            greatswordRenderer.material.SetColor("_Color", Color.red);
        }
        else
        {
            swordRenderer.material.SetColor("_Color", Color.white);
            greatswordRenderer.material.SetColor("_Color", Color.white);

        }


    }
}
