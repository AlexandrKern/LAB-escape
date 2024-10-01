public class MirrorForm : StateBase
{
    public override FormType StateForm => FormType.Mirror;

    public MirrorForm(Character context) : base(context)
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
