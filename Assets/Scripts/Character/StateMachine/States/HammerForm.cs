public class HammerForm : StateBase
{
    protected override FormType StateForm => FormType.Hammer;

    public HammerForm(Character context) : base(context)
    {
    }
}
