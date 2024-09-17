public class BaseForm : StateBase
{
    public override FormType StateForm => FormType.Base;

    public BaseForm(Character context) : base(context)
    {
    }

    public override async void QInteract()//TODO: maybe change the return type to UniTask
    {
        _context.inputHandler.IsEnableInput = false;
        await _context.interactController.Interact<IInteractableObstacle>();
        _context.inputHandler.IsEnableInput = true;

    }

    public override void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
    }
}
