using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimPar_Behaviour", menuName = "Scriptable Objects/Animation/AnimPar_Behaviour")]
public class AnimPar_Behaviour : FE_Behaviour
{

    public Serializable_FE_ArgBehav[] boolArgs;
    public Serializable_FE_ArgBehav[] floatArgs;
    public Serializable_FE_ArgBehav[] triggerArgs;


    public override void OnActivate()
    {
        AnimatorControllerParameter param;
        Animator anim;
        if(Owner.TryGetComponent<Animator>(out anim))
        {
            float f = 0.0f;
            foreach (Serializable_FE_ArgBehav arg in boolArgs)
            {
                bool b = arg.argument_value > 0;

                anim.SetBool(arg.argument_name, b);

            }

            foreach (Serializable_FE_ArgBehav arg in floatArgs)
            {
                anim.SetFloat(arg.argument_name, arg.argument_value);
            }

            foreach (Serializable_FE_ArgBehav arg in triggerArgs)
            {
                anim.SetTrigger(arg.argument_name);
            }
        }
     

    }

    
}
