using Cysharp.Threading.Tasks;

public class AnthropomorphicForm : StateBase
{
    public override FormType StateForm => FormType.Anthropomorphic;

    public AnthropomorphicForm(Character context) : base(context)
    {
    }

    public override async void EInteract()
    {
        _context.interactController.Interact<IInteractableTerminal>();
        _context.inputHandler.IsEnableInput = false;
        await _context.interactController.Interact<Ladder>();
        _context.inputHandler.IsEnableInput = true;
    }

    public override void Update()
    {
        base.Update();
        if(InputVertical > 0f)
        {
            Jump();
        }
        if(InputVertical < 0f)
        {
            FallThroughPlatformEffector();
        }
    }

    private bool _isJumped = false;

    /// <summary>
    /// Wait for the player to release the button to jump again
    /// </summary>
    /// <returns></returns>
    protected async UniTask Jump()
    {
        if(!_isJumped)
        {
            _isJumped = true;
            _context.moveController.Jump();
            while (InputVertical > 0)
            { 
                await UniTask.Yield(); 
            } 
            _isJumped = false;
        }
    }

    /// <summary>
    /// Wait for the player to release the button to fall through again
    /// </summary>
    /// <returns></returns>
    private async UniTask FallThroughPlatformEffector()
    {
        if (!_isJumped)
        {
            _isJumped = true;
            _context.moveController.FallThroughPlatformEffector();
            while (InputVertical < 0)
            {
                await UniTask.Yield();
            }
            _isJumped = false;
        }
    }
}
