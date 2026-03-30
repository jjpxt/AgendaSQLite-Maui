using MeuChat.Classes;
namespace MeuChat;

public partial class App : Application
{
    public static Servicos BancoDeDados { get; private set; }

    public App()
    {
        InitializeComponent();
        BancoDeDados = new Servicos();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}