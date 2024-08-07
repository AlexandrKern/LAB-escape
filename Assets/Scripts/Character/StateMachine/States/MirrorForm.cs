public class MirrorForm : StateBase
{
    protected override FormType StateForm => FormType.Mirror;

    public MirrorForm(Character context) : base(context)
    {
    }
}
