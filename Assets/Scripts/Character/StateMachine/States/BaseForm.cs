public class BaseForm : StateBase
{
    public override FormType StateForm => FormType.Base;

    public BaseForm(Character context) : base(context)
    {
    }
}
