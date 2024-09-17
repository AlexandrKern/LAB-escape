using Cysharp.Threading.Tasks;

public interface IInteractable
{
    //TODO: add enum for different types of interaction (q button, e button...)

    public UniTask Interact();
}
