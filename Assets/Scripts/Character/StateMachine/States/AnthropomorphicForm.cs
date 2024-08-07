public class AnthropomorphicForm : StateBase
{
    protected override FormType StateForm => FormType.Anthropomorphic;

    public AnthropomorphicForm(Character context) : base(context)
    {
    }
}
