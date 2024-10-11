public class MirrorForm : StateBase
{
    public override FormType StateForm => FormType.Mirror;

    public MirrorForm(Character context) : base(context)
    {
    }

    public override void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
