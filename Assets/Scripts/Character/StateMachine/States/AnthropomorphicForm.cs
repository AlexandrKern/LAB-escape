public class AnthropomorphicForm : StateBase
{
    public override FormType StateForm => FormType.Anthropomorphic;

    public AnthropomorphicForm(Character context) : base(context)
    {
    }
}
