public class BaseForm : StateBase
{
    public override FormType StateForm => FormType.Base;

    public BaseForm(Character context) : base(context)
    {
    }

    public override void QInteract()
    {
        _context.interactController.Interact<IInteractableObstacle>();
    }

    public override void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
