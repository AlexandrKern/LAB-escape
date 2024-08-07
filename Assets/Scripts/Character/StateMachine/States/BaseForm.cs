public class BaseForm : StateBase
{
    protected override FormType StateForm => FormType.Base;

    public BaseForm(Character context) : base(context)
    {
    }
}
