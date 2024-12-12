using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUtility : MonoBehaviour
{
    private PlayerAttack meleeAttack;
    private SpellAttack magicAttack;

    private void Start()
    {
        meleeAttack = gameObject.GetComponent<PlayerAttack>();
        magicAttack = gameObject.GetComponent<SpellAttack>();
    }
    
    public void EquipMelee() {
        meleeAttack.enabled = true;

        magicAttack.enabled = false;

    }

    public void EquipMagic() {
        meleeAttack.enabled = false;

        magicAttack.enabled = true;

    }
}
