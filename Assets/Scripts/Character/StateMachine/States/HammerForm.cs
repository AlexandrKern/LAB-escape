public class HammerForm : StateBase
{
    public override FormType StateForm => FormType.Hammer;

    public HammerForm(Character context) : base(context)
    {
    }

    public override void QInteract()
    {
        _context._interactController.Interact<IInteractableDestroyable>();
    }
}
