public class BaseForm : StateBase
{
    public override FormType StateForm => FormType.Base;

    public BaseForm(Character context) : base(context)
    {
    }

    public override void QInteract()
    {
        _context._interactController.Interact<IInteractableObstacle>();
    }

    public override void EInteract()
    {
        _context._interactController.Interact<IInteractableTerminal>();
    }
}
