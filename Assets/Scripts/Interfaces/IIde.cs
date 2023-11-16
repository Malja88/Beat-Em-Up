using System.Threading.Tasks;

public interface IIde
{
    void Idle();

    Task PerformActionAsync(int action, float delay);
}
